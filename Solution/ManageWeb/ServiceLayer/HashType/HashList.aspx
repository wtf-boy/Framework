<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="HashList.aspx.cs" Inherits="ServiceLayer_HashType_HashList" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_HashType_HashList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_HashType_HashList"
        DataKeyNames="HashID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="HashKey" HeaderText="哈希名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="HashValue" HeaderText="哈希代码">
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
