<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ResourceRestrictPicList.aspx.cs" Inherits="ServiceLayer_Resource_ResourceRestrictPicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceRestrictPicList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" ModuleCode="ServiceLayer_Resource_ResourceRestrictPicList"
        DataKeyNames="ResourceRestrictPicID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand" runat="server">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField HeaderText="版本号" DataTextField="VerNo" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="是否水印" HeaderStyle-Width="60"
                ItemStyle-Width="60">
                <ItemTemplate>
                    <%#  Eval("CreateWaterMark").ConvertBool()?"是":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField HeaderText="水平对齐方式" DataField="HorizontalAlign"  >
            </WTF:BoundField>
            <WTF:BoundField HeaderText="垂直对齐方式" DataField="VerticalAlign" >
            </WTF:BoundField>
            <WTF:BoundField HeaderText="宽度限制" DataField="ImageWidth" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
            <WTF:BoundField HeaderText="高度限制" DataField="ImageHeight" HeaderStyle-Width="100"
                ItemStyle-Width="100">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
