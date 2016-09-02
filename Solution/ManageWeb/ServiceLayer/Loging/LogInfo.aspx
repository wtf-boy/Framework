<%@ Page Language="C#" MasterPageFile="~/App_Master/InfoPage.master" AutoEventWireup="true"
    CodeFile="LogInfo.aspx.cs" Inherits="ServiceLayer_Loging_LogInfo" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        .ShowMoreInfo {
        }

            .ShowMoreInfo li {
                word-break: break-all;
            }

        .CollectMoreInfo {
            height: 200px;
            overflow-y: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Loging_LogInfo"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" style="width: 80px;" />
            <col class="colContent" />
            <col class="colTitle" style="width: 80px;" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="4">日志基本信息  [  <%#  objLog_LogingInfo.LogID%>]
            </td>
        </tr>
        <tr>
            <td>程序名称：
            </td>
            <td>
                <%#  objLog_LogingInfo.ApplicationName%>
            </td>
            <td>消息标识：
            </td>
            <td>
                <%#  objLog_LogingInfo.MessageID%>
            </td>
        </tr>
        <tr>
            <td>模块代码：
            </td>
            <td>
                <%#  objLog_LogingInfo.ModuleTypeCode%>
            </td>
            <td>日志分类：
            </td>
            <td>
                <%#  objLog_LogingInfo.CategoryTypeCode%>
            </td>
        </tr>
        <tr>
            <td>运行主机：
            </td>
            <td>
                <%#  objLog_LogingInfo.ApplicationHost%>
            </td>
            <td>记录时间：
            </td>
            <td>
                <%#  objLog_LogingInfo.LogDate.ToString("yyyy-MM-dd HH:mm:ss")%>
            </td>

        </tr>
        <tr>

            <td>用户IP：
            </td>
            <td>
                <%#  objLog_LogingInfo.UserHostAddress%>
            </td>
            <td>用户帐号：
            </td>
            <td>
                <%#  objLog_LogingInfo.Account%>
            </td>
        </tr>

        <tr>
            <td>上次地址：
            </td>
            <td colspan="3">
                <%#    objLog_LogingInfo.UrlReferrer.EncodeHtml()%>
            </td>
        </tr>
        <tr>
            <td>请求地址：
            </td>
            <td colspan="3">
                <%#   objLog_LogingInfo.RawUrl.EncodeHtml()%>
            </td>
        </tr>
        <tr>
            <td>UserAgent：
            </td>
            <td colspan="3">
                <%#  objLog_LogingInfo.UserAgent%>
            </td>
        </tr>
        <tr>
            <td>标题：
            </td>
            <td colspan="3">
                <%#  objLog_LogingInfo.Title%>
            </td>
        </tr>

        <tr <%# objLog_LogingInfo.HeadersData=="[]" || objLog_LogingInfo.HeadersData.IsNull()?"style=' display:none'":"" %>>
            <td>HeadersData：
            </td>
            <td colspan="3">
                <div class="ShowMoreInfo CollectMoreInfo">
                    <asp:Repeater ID="rptHeadersData" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><%#Eval("DataKey") %>:<%#Eval("DataValue") %> </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr <%# objLog_LogingInfo.RequestData=="[]" || objLog_LogingInfo.RequestData.IsNull()?"style=' display:none'":"" %>>
            <td>RequestData：
            </td>
            <td colspan="3">
                <div class="ShowMoreInfo CollectMoreInfo">
                    <asp:Repeater ID="rptRequestData" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li><%#Eval("DataKey") %>:<%#Eval("DataValue") %> </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <%# objLog_LogingInfo.ResultMessage.IsNull()?"日志内容：":"请求内容" %>
            </td>
            <td colspan="3">
                <%#    objLog_LogingInfo.Message.EncodeHtml()%>
            </td>
        </tr>

        <tr <%# objLog_LogingInfo.ResultMessage.IsNull()?"style=' display:none'":"" %>>
            <td>调用结果：
            </td>
            <td colspan="3" style="">

                <%# objLog_LogingInfo.ResultMessage.EncodeHtml()%>

            </td>
        </tr>



    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">
        //$(".ShowMoreInfo").hover(
        //   function () {
        //       $(this).removeClass("CollectMoreInfo");
        //   },
        //   function () {
        //       $(this).addClass("CollectMoreInfo");
        //   }
        // );
    </script>

</asp:Content>
