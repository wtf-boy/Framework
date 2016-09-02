var editor;
$(resourceInit);
function resourceInit() {
    var resourcePlugin = {
        resourceImg: {  t: '测试7：showIframeModal (Ctrl+7)', s: 'ctrl+7', e: function () {
            var _this = this;
            _this.saveBookmark();
            _this.showIframeModal('测试showIframeModal接口', 'uploadgui.php', function (v) { _this.loadBookmark(); _this.pasteText('返回值：\r\n' + v); }, 500, 300);
        } 
        }
};
editor = $('.xhtml').xheditor({ plugins: resourcePlugin, tools: 'Italic,resourceImg' });
}