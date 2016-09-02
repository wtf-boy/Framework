<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleCopy.aspx.cs" Inherits="ServiceLayer_Module_ModuleCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Module_ModuleCopy"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
      <div>
       复制个数： <WTF:MyDropDownList ID="dropCopyCount" runat="server">

            <asp:ListItem Value="1" Selected="True" Text="1"></asp:ListItem>
            <asp:ListItem Value="2" Text="2"></asp:ListItem>
            <asp:ListItem Value="3" Text="3"></asp:ListItem>
            <asp:ListItem Value="4" Text="4"></asp:ListItem>
            <asp:ListItem Value="5" Text="5"></asp:ListItem>

        </WTF:MyDropDownList>
    </div>
    <WTF:MyUlTreeView ID="tvwModule" runat="server" DataSourceID="XmlDataSource" ExpandDepth="1" ShowType="CheckBox" ViewStateData="false" RefFather="false"
        ShowLines="True">
        <DataBindings>
            <asp:TreeNodeBinding DataMember="Module" TextField="ModuleName" ValueField="ModuleId" />
        </DataBindings>
    </WTF:MyUlTreeView>

    <asp:XmlDataSource ID="XmlDataSource" runat="server" XPath='//Module[@NavigateUrl="ModuleType"]'
        EnableCaching="false"></asp:XmlDataSource>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
