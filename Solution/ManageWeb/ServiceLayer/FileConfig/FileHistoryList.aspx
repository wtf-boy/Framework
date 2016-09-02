<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="FileHistoryList.aspx.cs" Inherits="ServiceLayer_FileConfig_FileHistoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryDropDownList ID="FileResourceID" runat="server" QueryMethod="Equal"></WTF:QueryDropDownList>
    <WTF:QueryTextBox ID="QueryStartDate" QueryTitle="添加时间" QueryField="CreateDate" QueryMethod="GreaterThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd 00:00'})"></WTF:QueryTextBox>
    -
    <WTF:QueryTextBox ID="QueryEndDate" QueryField="CreateDate" QueryMethod="LessThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd 23:59'})"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContent" runat="Server">

    <div style="background-color: white; padding: 5px;">
        <asp:Repeater ID="rtpPicHistoryList" runat="server" OnItemCommand="rtpPicHistoryList_ItemCommand">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li style="width: 120px; float: left; height: 120px; text-align: center">
                    <a href="<%#Eval("PicUrl") %>" target="_blank">
                        <img alt="" height="100" width="100" src="<%#Eval("PicUrl") %>" /></a>

                    <%# Eval("CreateDate").ConvertDate("yyyy-MM-dd") %><asp:ImageButton ID="RemoveImg" OnClientClick="return Remove();" runat="server" CommandName="Reomve" CommandArgument='<%# Eval("FileHistoryID") %>' ImageUrl="~/App_Themes/Default/ButtonIco/delete.png" />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>

        <span class="clear"></span>
    </div>
    <WTF:MyPager ID="myPager" runat="server" OnPagerChangeCommand="CurrentPager_PagerChangeCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>

