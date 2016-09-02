<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="ServiceLayer_Authorize_GroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="AuthorizeGroupName" QueryTitle="授权组名" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="AuthorizeGroupID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" CommandExpandArgumentFields="IsRevertPower">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="AuthorizeGroupName" HeaderText="授权组名" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="是否超管组" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsSupertGroup").ConvertBool()?"<span class='txtNoNull'>超管</span>":"授权"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="回收授权" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsRevertPower").ConvertBool()?"<span class='txtNoNull'>是</span>":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="授权自己" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>
                    <%#  Eval("IsAllowPowerSelf").ConvertBool()?"<span class='txtNoNull'>是</span>":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="平台类型" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%# GetModuleTypenName( Eval("ModuleTypeID").ToString())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <%#  Eval("Remark")%>
                </ItemTemplate>
            </WTF:TemplateField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">


        function RevertPower() {

            if (window.confirm("是否确定全部收回所有的权限，很危险哦?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

