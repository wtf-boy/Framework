<%@ Page Language="C#" MasterPageFile="~/App_Master/InfoPage.master" AutoEventWireup="true"
    CodeFile="ModuleTypeInfo.aspx.cs" Inherits="ServiceLayer_Module_ModuleTypeInfo"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleTypeInfo"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">平台分类基本信息
            </td>
        </tr>
        <tr>
            <td>平台分类标识：
            </td>
            <td>
                <%# objModuleType.ModuleTypeID%>
            </td>
        </tr>
        <tr>
            <td>平台分类名称：
            </td>
            <td>
                <%# objModuleType.ModuleTypeName%>
            </td>
        </tr>
        <tr>
            <td>平台分类代码：
            </td>
            <td>
                <%# objModuleType.ModuleTypeCode%>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        //用户成员
        function ModuleSort() {
            //打开页面
            var theDes = "dialogWidth:400px;dialogHeight:350px;edge:sunken;help:no;status:no;scroll:no;";
            window.showModalDialog('<%=EncryptModuleQuery(string.Format("../../ServiceLayer/Module/ModuleSort.aspx?ModuleID={0}",ModuleID))%>', window, theDes);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
