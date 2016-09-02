<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ProcessList.aspx.cs" Inherits="ServiceLayer_Work_ProcessList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="" DataKeyNames="ProcessID"
        AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:NumberField></WTF:NumberField>
            <WTF:OperateField DataTextField="ProcessName" HeaderText="处理名称" HeaderStyle-Width="240"
                ItemStyle-Width="240">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="程序集" HeaderStyle-Width="240"
                ItemStyle-Width="240">
                <ItemTemplate>
                    <%#  Eval("AssemblyName").CutText(100)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="类全名">
                <ItemTemplate>
                    <%#  Eval("TypeName").CutText(100)%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="CreateDate" HeaderText="创建时间" HeaderStyle-Width="120"
                ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm}">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
   
</asp:Content>
