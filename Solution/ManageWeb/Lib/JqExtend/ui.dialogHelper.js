
jQuery.fn.dialog = function (options) {
    options = options || {};
    return this.each(function () {
        var node = this.nodeName.toLowerCase(), self = this;
        if (node == 'a') {
            jQuery(this).click(function () {
                var active = dialogHelper.linkedTo(this),
                    href = this.getAttribute('href'),
                    localOptions = jQuery.extend({ actuator: this, title: this.title }, options);
                if (active) {
                    active.show();
                } else if (href.indexOf('#') >= 0) {
                    var content = jQuery(href.substr(href.indexOf('#'))),
                        newContent = content.clone(true);
                    content.remove();
                    localOptions.unloadOnHide = false;
                    new dialogHelper(newContent, localOptions);
                } else { // fall back to AJAX; could do with a same-origin check
                    if (!localOptions.cache) localOptions.unloadOnHide = true;
                    dialogHelper.load(this.href, localOptions);
                }

                return false;
            });
        } else if (node == 'form') {
            jQuery(this).bind('submit.dialog', function () {
                dialogHelper.confirm(options.message || 'Please confirm:', function () {
                    jQuery(self).unbind('submit.dialog').submit();
                });
                return false;
            });
        }
    });
};

//
// dialogHelper Class

function dialogHelper(element, options) {

    this.dialog = jQuery(dialogHelper.WRAPPER);

    jQuery.data(this.dialog[0], 'dialog', this);
    this.frameCloseCommand = "";
    this.visible = false;
    this.options = jQuery.extend({}, dialogHelper.DEFAULTS, options || {});

    if (this.options.modal) {
        this.options = jQuery.extend(this.options, { center: true, draggable: false });
    }
    if (this.options.isframe) {
        this.options = jQuery.extend(this.options, { draggable: true });

    }


    // options.actuator == DOM element that opened this dialog
    // association will be automatically deleted when this dialog is remove()d
    if (this.options.actuator) {
        jQuery.data(this.options.actuator, 'active.dialog', this);
    }

    this.setContent(element || "<div></div>");
    this._setupTitleBar();

    this.dialog.css('display', 'none').appendTo(document.body);
    this.toTop();

    if (this.options.fixed) {
        if (jQuery.browser.msie && jQuery.browser.version < 7) {
            this.options.fixed = false; // IE6 doesn't support fixed positioning
        } else {
            this.dialog.addClass('fixed');
        }
    }

    if (this.options.center && dialogHelper._u(this.options.x, this.options.y)) {
        this.center();
    } else {
        this.moveTo(
            dialogHelper._u(this.options.x) ? this.options.x : dialogHelper.DEFAULT_X,
            dialogHelper._u(this.options.y) ? this.options.y : dialogHelper.DEFAULT_Y
        );
    }

    if (this.options.show) this.show();

};

dialogHelper.EF = function () { };

jQuery.extend(dialogHelper, {

    WRAPPER: "<table cellspacing='0' cellpadding='0' class='dialog-wrapper'>" +
                "<tr> <td class='dialog-inner'></td></tr>" +
                "</table>",
    // WRAPPER: "<div class='dialog-wrapper'><div class='dialog-inner'> </div></div>",

    DEFAULTS: {
        title: null,           // 标识
        closeable: true,           // 是否显示关闭按钮
        draggable: true,           //是否有拖动
        clone: false,          //是否复制
        actuator: null,           // 框架激活对话
        center: true,           // 是否对屏幕中间
        show: true,           //是否显示
        modal: false,          // 是否显示模式对话框
        fixed: true,           // 是否相对于BODY的绝对定位
        isframe: false,           // 是否frame:
        closeText: '[×]',      // 关闭按钮信息
        unloadOnHide: false,          //是否启动框架隐藏事件
        clickToFront: false,          //框架被点击是否移到置顶
        behaviours: dialogHelper.EF,        // 绑定自己对框架附加事件
        afterDrop: dialogHelper.EF,        // 框架拖动放开事件
        afterShow: dialogHelper.EF,        // 显示框架之后执行事件
        afterHide: dialogHelper.EF,        // 关闭框架之后执行事件
        beforeUnload: dialogHelper.EF         // callback fired after dialog is unloaded. executed in context of dialogHelper instance.
    },

    DEFAULT_X: 50,
    DEFAULT_Y: 50,
    zIndex: 1337,
    dragConfigured: false, // only set up one drag handler for all dialogs
    resizeConfigured: false,
    dragging: null,

    // load a URL and display in dialog
    // url - url to load
    // options keys (any not listed below are passed to dialog constructor)
    //   type: HTTP method, default: GET
    //   cache: cache retrieved content? default: false
    //   filter: jQuery selector used to filter remote content
    load: function (url, options) {

        options = options || {};

        var ajax = {
            url: url, type: 'GET', dataType: 'html', cache: false, success: function (html) {
                html = jQuery(html);
                if (options.filter) html = jQuery(options.filter, html);
                new dialogHelper(html, options);
            }
        };
        jQuery.each(['type', 'cache'], function () {
            if (this in options) {
                ajax[this] = options[this];
                delete options[this];
            }
        });

        jQuery.ajax(ajax);

    },

    //根据对象获取框架对象
    get: function (ele) {
        var p = jQuery(ele).parents('.dialog-wrapper');
        return p.length ? jQuery.data(p[0], 'dialog') : null;
    },

    // returns the dialog instance which has been linked to a given element via the
    // 'actuator' constructor option.
    linkedTo: function (ele) {
        return jQuery.data(ele, 'active.dialog');
    },
    dialogframe: function (pagetile, pageurl, closeBack) {
        ///	<summary>
        ///	 对话框加载iframe
        ///	</summary>
        ///	<param name="pagetile" type="String">
        ///	页标题
        ///	</param>
        ///	<param name="pageurl" type="String">
        ///	加载的URL地址
        ///	</param>
        options = jQuery.extend({ modal: true, closeable: true, isframe: true, title: pagetile, afterHide: function () { if (closeBack) { closeBack(this.frameCloseCommand) }; } }, { show: true, unloadOnHide: true });
        var iframebody = jQuery('<iframe id="dialog_frame" height="50" class="dialog-loading"  onload="dialogHelper._frameload(this)"  name="dialog_frame' + Math.round(Math.random() * 1000) + '" frameborder="0" hspace="0" src="' + pageurl + '"></iframe>');
        new dialogHelper(iframebody, options);
    },
    _frameload: function (e) {
        //                $(e).removeClass("dialog-loading");
        //                var mainheight = $(e).contents().find("body").height();
        //                var mainwidth = $(e).contents().find("body").width();

        //                dialogHelper.get(e).tween(mainwidth, mainheight);
    },
    close: function (frame, commandName) {

        var self = dialogHelper.get("iframe[name='" + frame + "']");
        if (commandName) {
            self.frameCloseCommand = commandName;
        }
        self.hide();
    },
    setIframeSize: function (frame, mainwidth, mainheight) {

        $("iframe[name='" + frame + "']").removeClass("dialog-loading");
        var dialogtemp = dialogHelper.get("iframe[name='" + frame + "']");
        if (dialogtemp) {
            dialogtemp.tween(mainwidth, mainheight);
        }



    },

    alert: function (message) {
        ///	<summary>
        ///	消息提示框
        ///	</summary>
        ///	<param name="message" type="String">
        ///	消息内容
        ///	</param>

        return dialogHelper.messageDialog(message, { "确定": null }, { title: "消息提示" });
    },
    confirm: function (message, okfunction) {
        ///	<summary>
        ///	消息确认框
        ///	</summary>
        ///	<param name="message" type="String">
        ///	消息内容
        ///	</param>
        ///	<param name="okfunction" type="String">
        ///	确定执行脚本方法
        ///	</param>
        return dialogHelper.messageDialog(message, { "确定": function () { okfunction(); }, "取消": null }, { title: "消息提示" });

    },
    messageDialog: function (message, buttons, options) {
        ///	<summary>
        ///	消息对话框
        ///	</summary>
        ///	<param name="message" type="String">
        ///	消息内容
        ///	</param>
        ///	<param name="buttons" type="Json">
        ///	按钮对象如{"确定":function(){},"取消":function (){}}
        ///	</param>
        ///	<param name="options" type="Json">
        ///	参数配置{title: "消息提示"}
        ///   title: null,           // 标识
        /// closeable: true,           // 是否显示关闭按钮
        /// draggable: true,           //是否有拖动
        /// clone: false,          //是否复制
        /// actuator: null,           //激活事件对象
        ///  center: true,           // 是否对屏幕中间
        /// show: true,           //是否显示
        /// modal: false,          // 是否显示模式对话框
        /// fixed: true,           // 是否相对于BODY的绝对定位
        /// isframe: false,           // 是否frame:
        /// closeText: '[关闭]',      // 关闭按钮信息
        /// unloadOnHide: false,          // should this dialog be removed from the DOM after being hidden?
        /// clickToFront: false,          // 对击框架是否置顶
        /// behaviours: dialogHelper.EF,        // 添加绑定事情响应
        /// afterDrop: dialogHelper.EF,        // 拖到框架放开执行事件
        /// afterShow: dialogHelper.EF,        // 显示框架之后执行事件
        /// afterHide: dialogHelper.EF,        // 关闭框架之后执行事件
        /// beforeUnload: dialogHelper.EF         // callback fired after dialog is unloaded. executed in context of dialogHelper instance.
        ///	</param>

        options = jQuery.extend({ modal: true, closeable: false },
                                options || {},
                                { show: true, unloadOnHide: true });

        var messagebody = jQuery('<div></div>').append(jQuery('<div class="dialog-message-bar"></div>').html(message));
        var self = this, hasButtons = false;

        (typeof buttons == 'object' && buttons !== null &&
			$.each(buttons, function () { return !(hasButtons = true); }));

        if (hasButtons) {
            var buttonsbox = jQuery('<div class="dialog-message-btn-bar"></div>');
            $.each(buttons, function (name, fn) {
                $('<button type="button"></button>')
					.addClass(
						'ui-state-default ' +
						'ui-corner-all'
					)
					.text(name)
					.click(function () { dialogHelper.get(this).hide(function () { if (fn != null) { fn.apply(name); } }) })
					.hover(
						function () {
						    $(this).addClass('ui-state-hover');
						},
						function () {
						    $(this).removeClass('ui-state-hover');
						}
					)
					.focus(function () {
					    $(this).addClass('ui-state-focus');
					})
					.blur(function () {
					    $(this).removeClass('ui-state-focus');
					})
					.appendTo(buttonsbox);
            });
        }
        messagebody.append(buttonsbox);
        new dialogHelper(messagebody, options);

    },

    // returns true if a modal dialog is visible, false otherwise
    isModalVisible: function () {
        return jQuery('.dialog-modal-blackout').length > 0;
    },

    _u: function () {
        for (var i = 0; i < arguments.length; i++)
            if (typeof arguments[i] != 'undefined') return false;
        return true;
    },

    _handleResize: function (evt) {
        var d = jQuery(document);
        jQuery('.dialog-modal-blackout').css('display', 'none').css({
            width: d.width(), height: d.height()
        }).css('display', 'block');
    },

    _handleDrag: function (evt) {
        var d;
        if (d = dialogHelper.dragging) {
            d[0].dialog.css({ left: evt.pageX - d[1], top: evt.pageY - d[2] });
        }
    },

    _nextZ: function () {
        return dialogHelper.zIndex++;
    },

    _viewport: function () {
        var d = document.documentElement, b = document.body, w = window;
        return jQuery.extend(
            jQuery.browser.msie ?
                { left: b.scrollLeft || d.scrollLeft, top: b.scrollTop || d.scrollTop} :
                { left: w.pageXOffset, top: w.pageYOffset },
            !dialogHelper._u(w.innerWidth) ?
                { width: w.innerWidth, height: w.innerHeight} :
                (!dialogHelper._u(d) && !dialogHelper._u(d.clientWidth) && d.clientWidth != 0 ?
                    { width: d.clientWidth, height: d.clientHeight} :
                    { width: b.clientWidth, height: b.clientHeight }));
    }



});

dialogHelper.prototype = {

    // Returns the size of this dialog instance without displaying it.
    // Do not use this method if dialog is already visible, use getSize() instead.
    estimateSize: function () {

        this.dialog.css({ visibility: 'hidden', display: 'block' });
        var dims = this.getSize();
        this.dialog.css('display', 'none').css('visibility', 'visible');
        return dims;
    },

    // Returns the dimensions of the entire dialog dialog as [width,height]
    getSize: function () {
        return [this.dialog.width(), this.dialog.height()];
    },

    // Returns the dimensions of the content region as [width,height]
    getContentSize: function () {
        var c = this.getContent();
        return [c.width(), c.height()];
    },

    // Returns the position of this dialog as [x,y]
    getPosition: function () {
        var b = this.dialog[0];
        return [b.offsetLeft, b.offsetTop];
    },

    // Returns the center point of this dialog as [x,y]
    getCenter: function () {
        var p = this.getPosition();
        var s = this.getSize();
        return [Math.floor(p[0] + s[0] / 2), Math.floor(p[1] + s[1] / 2)];
    },

    // Returns a jQuery object wrapping the inner dialog region.
    // Not much reason to use this, you're probably more interested in getContent()
    getInner: function () {
        return jQuery('.dialog-inner', this.dialog);
    },

    // Returns a jQuery object wrapping the dialog content region.
    // This is the user-editable content area (i.e. excludes titlebar)
    getContent: function () {
        return jQuery('.dialog-content', this.dialog);
    },

    // Replace dialog content
    setContent: function (newContent) {
        newContent = jQuery(newContent).css({ display: 'block' }).addClass('dialog-content');
        if (this.options.clone) newContent = newContent.clone(true);
        this.getContent().remove();
        this.getInner().append(newContent);
        this._setupDefaultBehaviours(newContent);
        this.options.behaviours.call(this, newContent);
        return this;
    },

    // Move this dialog to some position, funnily enough
    moveTo: function (x, y) {
        this.moveToX(x).moveToY(y);
        return this;
    },

    // Move this dialog (x-coord only)
    moveToX: function (x) {
        if (typeof x == 'number') this.dialog.css({ left: x });
        else this.centerX();
        return this;
    },

    // Move this dialog (y-coord only)
    moveToY: function (y) {
        if (typeof y == 'number') this.dialog.css({ top: y });
        else this.centerY();
        return this;
    },

    // Move this dialog so that it is centered at (x,y)
    centerAt: function (x, y) {
        var s = this[this.visible ? 'getSize' : 'estimateSize']();

        if (typeof x == 'number') this.moveToX(x - s[0] / 2);
        if (typeof y == 'number') this.moveToY(y - s[1] / 2);
        return this;
    },

    centerAtX: function (x) {
        return this.centerAt(x, null);
    },

    centerAtY: function (y) {
        return this.centerAt(null, y);
    },

    // Center this dialog in the viewport
    // axis is optional, can be 'x', 'y'.
    center: function (axis) {


        var v = dialogHelper._viewport();
        var o = this.options.fixed ? [0, 0] : [v.left, v.top];
        if (!axis || axis == 'x') this.centerAt(o[0] + v.width / 2, null);
        if (!axis || axis == 'y') this.centerAt(null, o[1] + v.height / 2);
        return this;
    },

    // Center this dialog in the viewport (x-coord only)
    centerX: function () {
        return this.center('x');
    },

    // Center this dialog in the viewport (y-coord only)
    centerY: function () {
        return this.center('y');
    },

    // Resize the content region to a specific size
    resize: function (width, height, after) {
        if (!this.visible) return;
        var bounds = this._getBoundsForResize(width, height);
        this.dialog.css({ left: bounds[0], top: bounds[1] });
        this.getContent().css({ width: bounds[2], height: bounds[3] });
        if (after) after(this);
        return this;
    },

    // Tween the content region to a specific size
    tween: function (width, height, after) {
        if (!this.visible) return;
        var bounds = this._getBoundsForResize(width, height);
        var self = this;
        this.dialog.stop().animate({ left: bounds[0], top: bounds[1] });
        this.getContent().stop().animate({ width: bounds[2], height: bounds[3] }, function () {
            if (after) after(self);
        });
        return this;
    },

    // Returns true if this dialog is visible, false otherwise
    isVisible: function () {
        return this.visible;
    },

    // Make this dialog instance visible
    show: function () {
        if (this.visible) return;
        if (this.options.modal) {
            var self = this;
            if (!dialogHelper.resizeConfigured) {
                dialogHelper.resizeConfigured = true;
                jQuery(window).resize(function () { dialogHelper._handleResize(); });

            }
            if (jQuery.browser.msie && jQuery.browser.version < 7) {

                $('embed, object, select').css('visibility', 'hidden');
            }
            this.modalBlackout = jQuery('<div class="dialog-modal-blackout"></div>')
                .css({ zIndex: dialogHelper._nextZ(),
                    opacity: 0.2,
                    width: jQuery(document).width(),
                    height: jQuery(document).height()
                })
                .appendTo(document.body);
            this.toTop();

            if (this.options.closeable) {
                jQuery(document.body).bind('keypress.dialog', function (evt) {
                    var key = evt.which || evt.keyCode;
                    if (key == 27) {
                        self.hide();
                        jQuery(document.body).unbind('keypress.dialog');
                    }
                });
            }
        }

        this.dialog.stop().css({ opacity: 1 }).show();
        this.visible = true;
        this._fire('afterShow');
        return this;
    },

    // Hide this dialog instance
    hide: function (after) {
        if (!this.visible) return;
        var self = this;

        if (this.options.modal) {
            jQuery(document.body).unbind('keypress.dialog');

            jQuery.removeData(self.dialog[0], 'dialog');
            if (jQuery.browser.msie && jQuery.browser.version < 7) {
                $('embed, object, select').css('visibility', 'visible');
            }
            this.modalBlackout.animate({ opacity: 0 }, function () {
                jQuery(this).remove();

            });
        }
        this.dialog.stop().animate({ opacity: 0 }, 200, function () {
            self.dialog.css({ display: 'none' });
            self.visible = false;

            self._fire('afterHide');
            if (after) after(self);


            if (self.options.unloadOnHide) self.unload();
        });
        return this;
    },

    toggle: function () {
        this[this.visible ? 'hide' : 'show']();
        return this;
    },

    hideAndUnload: function (after) {
        this.options.unloadOnHide = true;
        this.hide(after);
        return this;
    },

    unload: function () {
        this._fire('beforeUnload');
        this.dialog.remove();
        if (this.options.actuator) {
            jQuery.data(this.options.actuator, 'active.dialog', false);
        }
    },

    // Move this dialog box above all other dialog instances
    toTop: function () {
        this.dialog.css({ zIndex: dialogHelper._nextZ() });
        return this;
    },

    // Returns the title of this dialog
    getTitle: function () {
        return jQuery('> .dialog-title-bar h2', this.getInner()).html();
    },

    // Sets the title of this dialog
    setTitle: function (t) {
        jQuery('> .dialog-title-bar h2', this.getInner()).html(t);
        return this;
    },

    //
    // Don't touch these privates

    _getBoundsForResize: function (width, height) {
        var csize = this.getContentSize();
        var delta = [width - csize[0], height - csize[1]];
        var p = this.getPosition();
        return [Math.max(p[0] - delta[0] / 2, 0),
                Math.max(p[1] - delta[1] / 2, 0), width, height];
    },
    _setupTitleBar: function () {
        if (this.options.title) {
            var self = this;
            var tb = jQuery("<div class='dialog-title-bar'></div>").html("<div>" + this.options.title + "</div>");
            if (this.options.closeable) {
                tb.append(jQuery("<a href='#' class='dialog-close' alt='关闭' ></a>").html(this.options.closeText));
            }
            if (this.options.draggable) {

                tb[0].onselectstart = function () { return false; }
                tb[0].unselectable = 'on';
                tb[0].style.MozUserSelect = 'none';
                if (!dialogHelper.dragConfigured) {
                    jQuery(document).mousemove(dialogHelper._handleDrag);
                    dialogHelper.dragConfigured = true;
                }
                tb.mousedown(function (evt) {
                    self.toTop();
                    dialogHelper.dragging = [self, evt.pageX - self.dialog[0].offsetLeft, evt.pageY - self.dialog[0].offsetTop];
                    jQuery(this).addClass('dragging');
                }).mouseup(function () {
                    jQuery(this).removeClass('dragging');
                    dialogHelper.dragging = null;
                    self._fire('afterDrop');
                });
            }
            this.getInner().prepend(tb);
            this._setupDefaultBehaviours(tb);
        }
    },

    _setupDefaultBehaviours: function (root) {
        var self = this;
        if (this.options.clickToFront) {
            root.click(function () { self.toTop(); });
        }
        jQuery('.dialog-close', root).click(function () {
            self.frameCloseCommand = "cancel";
            self.hide();
            return false;
        }).mousedown(function (evt) { evt.stopPropagation(); });
    },

    _fire: function (event) {

        this.options[event].call(this);

    }

};
