<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ResourceRestrictList.aspx.cs" Inherits="ServiceLayer_Resource_ResourceRestrictList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceRestrictList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" ModuleCode="ServiceLayer_Resource_ResourceRestrictList"
        DataKeyNames="ResourceRestrictID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" runat="server">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:BoundField HeaderText="限制代码" DataField="RestrictCode"  HeaderStyle-Width="200"
                ItemStyle-Width="200">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="限制名称" DataField="RestrictName"  HeaderStyle-Width="200"
                ItemStyle-Width="200">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="扩展名" DataField="FileExtension">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="类型" HeaderStyle-Width="60"
                ItemStyle-Width="60">
                <ItemTemplate>
                    <%# GetRestrictType(Eval("RestrictType").ConvertInt())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="大小" HeaderStyle-Width="60"
                ItemStyle-Width="60">
                <ItemTemplate>
                    <%# TypeHelper.RenderFileSize((((int)Eval("FileMaxSize")) * 1024))%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:OperateField HeaderText="版本号" DataTextField="VerNo" HeaderStyle-Width="60"
                ItemStyle-Width="60">
            </WTF:OperateField>
            <WTF:BoundField HeaderText="开始版本号" DataField="BeginVerNo" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="结束版本号" DataField="EndVerNo" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
