<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="HashTypeList.aspx.cs" Inherits="ServiceLayer_HashType_HashTypeList"
    Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_HashType_HashTypeList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="HashTypeName" QueryTitle="类型名称" runat="server"></WTF:QueryTextBox>
    <WTF:QueryTextBox ID="HashTypeCode" QueryTitle="类型代码" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_HashType_HashTypeList"
        DataKeyNames="HashTypeID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="HashTypeName" HeaderText="参数类型名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="HashTypeCode" HeaderText="参数类型代码">
            </WTF:BoundField>
            <WTF:BoundField DataField="Remark" HeaderText="参数类型备注"  IsAutoSort="false">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
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
