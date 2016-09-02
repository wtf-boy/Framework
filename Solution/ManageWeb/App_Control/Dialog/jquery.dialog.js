
document.write('<script src="../../App_Control/Jqueryui/jquery-ui.min.js" type="text/javascript"></script>');
document.write('<link href="../../App_Control/Jqueryui/css/ui-lightness/jquery-ui.min.css" rel="Stylesheet" />');
document.write('<link href="../../App_Control/Dialog/jquery.dialog.ui.css" rel="stylesheet" />');
var dialogIframeCallbackEvent = {};
jQuery.extend({
    dialogIframe: function (url, title, width, height, callback)
    {
        var timestamp = Math.round(new Date().getTime() / 1000);
        var dialogID = "dialog" + timestamp;
        var newFrame = " <iframe id='dialogframe" + timestamp + "' class='dialog_window_frame' src='" + url + "' width='" + width + "'  height='" + height + "' title=''   name='dialogframe" + timestamp + "' frameborder='0' scrolling='yes'></iframe>";
        $("<div class='self_Iframe' id='dialog" + timestamp + "' style=' margin:0px; padding:1px; padding-bottom:5px;'></div>").append($(newFrame)).dialog({
            autoOpen: true,
            title: title,
            width: width + 5,
            height: height + 100,
            modal: true
        }).height(height + 5).width(width);
        if (callback != undefined)
        {

            dialogIframeCallbackEvent[dialogID] = callback;
        }
    }
});
function closedialogIframe(dialogID)
{
    $("#" + dialogID).dialog("close");
}
function dialogIframeReturn(dialogID, returnValue)
{

    var CallbackEvent = dialogIframeCallbackEvent[dialogID];
    if (CallbackEvent != undefined)
    {
        dialogIframeCallbackEvent[dialogID].call(this, returnValue);
    }
}

function dialogReturn(returnValue)
{
    var dialogID = window.name.replace("frame", "");
    window.parent.dialogIframeReturn(dialogID, returnValue);
}
function closedialog()
{
    var dialogID = window.name.replace("frame", "");
    window.parent.closedialogIframe(dialogID);
}