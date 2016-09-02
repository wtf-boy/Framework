<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WorkFrame_Default_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="HeaderControl.ascx" TagName="HeaderControl" TagPrefix="uc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../../Lib/JsBase/jquery.js"></script>
    <script src='../..//Lib/JqExtend/jquery.timers-1.1.3.js' type="text/javascript"></script>
    <script src='../..//Lib/JqExtend/jquery.message.js' type="text/javascript"></script>
    <script src="WorkDeskTop.js" type="text/javascript"></script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <table class="tbl_Desktop" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td colspan="3">
                        <div class="header_container">
                            <uc:HeaderControl ID="HeaderControl1" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="function_container" valign="top" style="width: 150px; background-color: #CBDAEE">
                        <iframe id="frmFunction" name="frmFunction" allowtransparency="true" style="height: 100%"
                            frameborder="0"></iframe>
                    </td>
                    <td id="switch_container" valign="middle">
                        <img id="switchPoint" src="../../App_Themes/Default/Image/ico_Arrow_Left.gif" alt="隐藏左栏" />
                    </td>
                    <td valign="top" >
                        <div id="desktop_task_container">
                            <div class="desktop_task_wrap">
                                <div class="desktop_task_content">
                                    <ul id="desktop_task_list">
                                    </ul>
                                </div>
                                <div class="desktop_task_menu">
                                    <span cmd="max" cmdtitle="最大化">□</span>
                                    <span cmd="close" cmdtitle="关闭">×</span>
                                </div>
                                <span class="clear"></span>
                            </div>
                        </div>
                        <div id="desktop_Work_container">
                            <div id="maxWindow" class="window" style="display: none;">
                                <div class="window_titleBar">
                                    <div class="window_titleButtonBar">
                                        <a class="window_close" title="关闭"></a><a class="window_restore" title="还原"></a>
                                    </div>
                                    <div class="window_title">
                                    </div>
                                </div>
                                <div class="windowframe">
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
    <script type="text/javascript" src="../../CMS/AjaxToDo/ToDoMessageHint.js"></script>
</body>
</html>
