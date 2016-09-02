<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="FileStoragePathList.aspx.cs" Inherits="ServiceLayer_FileConfig_FileStoragePathList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">

    <WTF:QueryTextBox ID="StoragePathName" QueryTitle="存储名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">





    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="FileStoragePathID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="StoragePathName" HeaderText="存储名称" HeaderStyle-Width="200" ItemStyle-Width="200">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="存储类型" HeaderStyle-Width="200" ItemStyle-Width="200" SortExpression="StorageTypeID">
                <ItemTemplate>
                    <%#  Eval("StorageTypeID").ConvertInt()==1?"本地存储":Eval("StorageTypeID").ConvertInt()==1?"FTP":"文件系统"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="虚目录">
                <ItemTemplate>
                    <%#  Eval("VirtualName")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="StoragePath" HeaderText="存储地址">
            </WTF:BoundField>

        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

