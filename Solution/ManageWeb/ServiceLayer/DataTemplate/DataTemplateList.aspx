<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="DataTemplateList.aspx.cs" Inherits="ServiceLayer_DataTemplate_DataTemplateList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_DataTemplate_DataTemplateList"
        DataKeyNames="DataTemplateID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="TemplateName" HeaderText="模板名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="TemplateCode" HeaderText="模板代码">
            </WTF:BoundField>
            <WTF:BoundField DataField="Remark" HeaderText="备注">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
    <script type="text/javascript">
        function Remove() {
            if (window.confirm("确认删除吗?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
