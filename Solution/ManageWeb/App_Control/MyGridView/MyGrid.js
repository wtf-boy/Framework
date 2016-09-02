//注册关闭事件
document.onclick = new Function("closeMenu()");
//关闭所有菜单
function closeMenu() {
    $(".gridList .tbdOperateMenu ol").hide();
}
function MyGrid() {
}
MyGrid.prototype = {
    grid: null,
    defaultoption: {},
    initGrid: function (options) {
        this.options = jQuery.extend({}, this.defaultoption, options || {});
        this._addAllSelectEvent();
        this._addSelectEvent();
        this._addRowDataHoverEvent();
       // this._addRowDataClickEvent();
        this._addRightOperate();
        if (!$.browser.msie) {
            this.grid.find(".tbdOperateMenu ol a[disabled='disabled']").css(
					"color", "#A0A0A0");
        }
    },
    _addRightOperate: function () {
        // 操作菜单
        this.grid.find("tr").has(".tbdOperateMenu").mouseleave(function () {
            $(this).find(".tbdOperateMenu ol").hide();
        });
        this.grid
				.find(".trclist")
				.has(".tbdOperateMenu ol")
				.bind(
						'mousedown',
						this,
						function (event) {

						    if (event.button == 2) {

						        event.data.grid.find(".tbdOperateMenu ol")
										.hide();
						        $(this).find(".tbdOperateMenu ol").show();
						        $(this).find(".tbdOperateMenu ol").css("zIndex");

						        var top = event.pageY;
						        var left = event.pageX;
						        if ($(this).find(".tbdOperateMenu ol").height()
										+ event.pageY > document.body.clientHeight) {
						            top = event.pageY
											- $(this).find(".tbdOperateMenu ol")
													.height();
						            if (top < 0) {
						                var offectheight = $(this).find(
												".tbdOperateMenu ol").height()
												+ event.pageY
												- document.body.clientHeight;
						                top = event.pageY - offectheight - 10;
						            }
						        }

						        if ($(this).find(".tbdOperateMenu ol").width()
										+ event.pageX > document.body.clientWidth) {
						            left = event.pageX
											- $(this).find(".tbdOperateMenu ol")
													.width();
						        }

						        $(this).find(".tbdOperateMenu ol").css({
						            left: left,
						            top: top
						        });
						        this.oncontextmenu = function () {
						            return false;
						        };
						    }

						});

        this.grid.find(".tbdOperateMenu  ol a").click(this, function (event) {
            var $trc = $(this).parents(".trclist");
            if ($trc.find(".colSelect").length > 0) {
                $trc.find(".colSelect :checkbox").prop("checked", true);
            } else if ($trc.find(".colRadioSelect").length > 0) {
                event.data.grid.find(".colRadioSelect :radio").prop("checked", false);
                $trc.find(".colRadioSelect :radio").prop(
                                      "checked", true);
            }
            $trc.addClass("rowSelect");

        });
        this.grid.find(".tbdOperate   a").click(this, function (event) {
            var $trc = $(this).parents(".trclist");
            if ($trc.find(".colSelect").length > 0) {
                $trc.find(".colSelect :checkbox").prop("checked", true);
            } else if ($trc.find(".colRadioSelect").length > 0) {
                event.data.grid.find(".colRadioSelect :radio").prop("checked", false);
                $trc.find(".colRadioSelect :radio").prop(
                                      "checked", true);

            }
            $trc.addClass("rowSelect");
        });

    },
    _addRowDataClickEvent: function () {
        // 数据行点击
        this.grid.find(".trclist").click(
				this,
				function (event) {

				    if ($(this).find(".colSelect :checkbox")
                            .prop("checked") || $(this).find(".colRadioSelect :radio")
                                .prop("checked")) {

				    } else {
				        if ($(this).hasClass("rowSelect")) {
				            $(this).removeClass("rowSelect");
				        } else {
				            $(this).addClass("rowSelect");
				        }
				    }

				});
    },
    _addRowDataHoverEvent: function () {
        // 数据行鼠标移支
        this.grid.find(".trclist").hover(function () {
            if (!$(this).find(".colSelect :checkbox").prop("checked"))
                $(this).addClass("rowOver");
        }, function () {

            if (!$(this).find(".colSelect :checkbox").prop("checked"))
                $(this).removeClass("rowOver");
        });
    },
    _addSelectEvent: function () {
        // 点选选择
        this.grid.find(".trclist .colSelect :checkbox").click(this,
				function (event) {
				    if ($(this).prop("checked")) {
				        $(this).parent().parent().addClass("rowSelect");
				    } else {
				        $(this).parent().parent().removeClass("rowSelect");
				    }
				    event.stopPropagation();
				});

        // 点选选择
        this.grid.find(".trclist .colRadioSelect :radio").click(this,
				function (event) {
				    event.data.grid.find(".colRadioSelect :radio").prop("checked", false);
				    event.data.grid.find(".trclist").removeClass("rowSelect");
				    $(this).prop("checked", true);
				    $(this).parent().parent().addClass("rowSelect");
				    event.stopPropagation();
				});

    },
    _addAllSelectEvent: function () {
        // 多选选择
        this.grid.find(".trhList .colSelect :checkbox").click(
				this,
				function (event) {
				    event.data.grid.find(".trclist .colSelect :checkbox").prop(
							"checked", $(this).prop("checked"));
				    if ($(this).prop("checked")) {
				        event.data.grid.find(".trclist").addClass("rowSelect");
				    } else {
				        event.data.grid.find(".trclist")
								.removeClass("rowSelect");
				        event.data.grid.find(".trclist").removeClass("rowOver");
				    }
				    event.stopPropagation();

				});
    },


    SelectID: function () {
        var valueID = "";
        // 多选选择
        this.grid.find(".trclist .colSelect input:checked").each(function () {
            valueID += $(this).parent().attr("SelectID") + ",";
        });
        if (valueID.length > 1) {
            valueID = valueID.substring(0, valueID.length - 1);
        }
        return valueID;

    }
};
jQuery.fn.extend({
    createmyGrid: function (initoptions) {

        var objmyGrid = new MyGrid();
        objmyGrid.grid = this;
        objmyGrid.initGrid(initoptions);
        return objmyGrid;
    }
});