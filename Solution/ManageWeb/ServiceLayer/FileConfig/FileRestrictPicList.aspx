<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="FileRestrictPicList.aspx.cs" Inherits="ServiceLayer_FileConfig_FileRestrictPicList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode=""
        DataKeyNames="SystemFilePicID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="SortIndex" HeaderText="创建排序号" SortExpression="SortIndex" HeaderStyle-Width="100" ItemStyle-Width="100">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="水印" HeaderStyle-Width="80" ItemStyle-Width="80">
                <ItemTemplate>

                    <%#  Eval("IsCreateWaterMark").ConvertBool()?"是":"否"%>
                </ItemTemplate>
            </WTF:TemplateField>

            <WTF:TemplateField HeaderText="图片宽度" SortExpression="ImageWidth" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  Eval("ImageWidth")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="图片高度" SortExpression="ImageHeight" HeaderStyle-Width="200" ItemStyle-Width="200">
                <ItemTemplate>
                    <%#  Eval("ImageHeight")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="水平位置" SortExpression="HorizontalAlign">
                <ItemTemplate>
                    <%# Eval("HorizontalAlign")%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:TemplateField HeaderText="垂直位置" SortExpression="VerticalAlign">
                <ItemTemplate>
                    <%#  Eval("VerticalAlign")%>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

