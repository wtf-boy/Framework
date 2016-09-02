<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DeskTop_FoldTree_Default" %>

<%@ Register Src="HeaderControl.ascx" TagName="HeaderControl" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="../../Lib/JsBase/jquery.js"></script>
    <script src="WorkDeskTop.js" type="text/javascript"></script>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <div class="header_container">
            <uc:HeaderControl ID="HeaderControl1" runat="server" />
        </div>
        <table class="tbl_Desktop" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td id="function_container" valign="top" style="width:200px; background-color: #CBDAEE;">
                        <iframe id="frmFunction" name="frmFunction" allowtransparency="true" style="height: 100%; width: 200px;" scrolling="no"
                            frameborder="0" src="FunctionMenu.aspx"></iframe>
                    </td>
                    <td id="switch_container" valign="middle">
                        <img id="switchPoint" src="../../App_Themes/Default/Image/ico_Arrow_Left.gif" alt="隐藏左栏" />
                    </td>
                    <td valign="top">
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
</body>
</html>
