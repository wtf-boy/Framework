

var bbs = 'Bold,Italic,Underline,Strikethrough,|,resourceImg,resourceFlash,resourceMedia,resourceFile,Googlemap';
var mymini = 'Bold,Italic,Underline,Strikethrough,|,Align,List,|,resourceFile,resourceImg';
var mysimple = 'Blocktag,Fontface,FontSize,Bold,Italic,Underline,Strikethrough,FontColor,BackColor,|,Align,List,Outdent,Indent,|,resourceFile,resourceImg,Emot';
var myfull = 'Cut,Copy,Paste,Pastetext,|,Blocktag,Fontface,FontSize,Bold,Italic,Underline,Strikethrough,FontColor,BackColor,SelectAll,Removeformat,|,Align,List,Outdent,Indent,|,resourceFile,Link,Unlink,Anchor,resourceImg,resourceFlash,resourceMedia,Googlemap,Hr,Emot,Table,|,Source,Preview,Print,Fullscreen';
function updateResourceClientValue(ResourceClientID, value) {
    $("#" + ResourceClientID).val(value);

}
var resourcePlugin = {
    resourceImg: { t: '图片上传：图片上传', e: function () {
        var _this = this;
        var RestrictCode = _this.settings.RestrictImgCode;
        if (!RestrictCode) {
            alert("请设置图片限制码");
            return;
        }
        RestrictCode.ResourceID = $("#" + RestrictCode.ResourceClientID).val();
        RestrictCode = jQuery.param(RestrictCode);

        _this.saveBookmark();
        var uploadUrl = '{editorRoot}xheditor_Seven/resource/ImageDialog.aspx?' + RestrictCode;
        _this.showIframeModal('图片上传', uploadUrl, function (v) { _this.loadBookmark(); _this.pasteHTML(v); }, 400, 300);
    }
    },
    resourceMedia: { t: '多媒体文件：多媒体文件', e: function () {
        var _this = this;
        var RestrictCode = _this.settings.RestrictMediaCode;
        if (!RestrictCode) {
            alert("请设置多媒体限制码");
            return;
        }
        RestrictCode.ResourceID = $("#" + RestrictCode.ResourceClientID).val();
        RestrictCode = jQuery.param(RestrictCode);

        _this.saveBookmark();
        var uploadUrl = '{editorRoot}xheditor_Seven/resource/MediaDialog.aspx?' + RestrictCode;
        _this.showIframeModal('多媒体文件', uploadUrl, function (v) { _this.loadBookmark(); _this.pasteHTML(v); }, 400, 250);
    }
    },
    resourceFlash: { t: 'Flash动画：Flash动画', e: function () {
        var _this = this;
        var RestrictCode = _this.settings.RestrictFlashCode;
        if (!RestrictCode) {
            alert("请设置Flash动画限制码");
            return;
        }
        RestrictCode.ResourceID = $("#" + RestrictCode.ResourceClientID).val();
        RestrictCode = jQuery.param(RestrictCode);
        _this.saveBookmark();
        var uploadUrl = '{editorRoot}xheditor_Seven/resource/FlashDialog.aspx?' + RestrictCode;

        _this.showIframeModal('Flash动画', uploadUrl, function (v) { _this.loadBookmark(); _this.pasteHTML(v); }, 400, 250);
    }
    },

    resourceFile: { t: '文件链接：文件链接', e: function () {
        var _this = this;
        var RestrictCode = _this.settings.RestrictFileCode;
        if (!RestrictCode) {
            alert("请设置文件限制码");
            return;
        }
        RestrictCode.ResourceID = $("#" + RestrictCode.ResourceClientID).val();
        RestrictCode = jQuery.param(RestrictCode);
        _this.saveBookmark();
        var uploadUrl = '{editorRoot}xheditor_Seven/resource/DialogFile.aspx?' + RestrictCode;

        _this.showIframeModal('文件链接', uploadUrl, function (v) { _this.loadBookmark(); _this.pasteHTML(v); }, 400, 250);
    }
    },
    Googlemap: {  t: '插入Google地图', e: function () {
        var _this = this;
        _this.showIframeModal('Google 地图', '{editorRoot}xheditor_Seven/googlemap/googlemap.html', function (v) { _this.pasteHTML('<img src="' + v + '" />'); }, 538, 404);
    }
    }
};
