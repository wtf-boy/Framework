<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="OperationLogList.aspx.cs" Inherits="ServiceLayer_Loging_OperationLogList" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode=""
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="Account" runat="server" QueryMethod="Equal" QueryTitle="操作帐号"></WTF:QueryTextBox>
     <WTF:QueryTextBox ID="TableName" runat="server" QueryMethod="Equal" QueryTitle="表名" ></WTF:QueryTextBox>
    <WTF:QueryDropDownList ID="ModuleTypeCode" runat="server" QueryDataType="String" QueryMethod="Equal" QueryTitle="模块分类"></WTF:QueryDropDownList>
    <WTF:QueryDropDownList ID="OperationTypeID" runat="server" QueryTitle="操作"></WTF:QueryDropDownList>

    <br />
    <WTF:QueryTextBox ID="CreateDateStart" QueryField="CreateDate" QueryTitle="记录日期" QueryMethod="GreaterThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
    -
    <WTF:QueryTextBox ID="CreateDateEnd" QueryField="CreateDate" QueryMethod="LessThanOrEqual"
        runat="server" QueryDataType="Date" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" DataKeyNames="OperationID" ModuleCode="" IsAutoSortFields="false"
        AutoGenerateColumns="false" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ApplicationName" HeaderText="程序名称" HeaderStyle-Width="120"
                ItemStyle-Width="120">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="模块名称" HeaderStyle-Width="100"
                ItemStyle-Width="100">
                <ItemTemplate>
                    <%#   GetLogModuleTypeName(Eval("ModuleTypeCode").ToString()) %>
                </ItemTemplate>
            </WTF:TemplateField>
               <WTF:BoundField DataField="TableName" HeaderText="表名" HeaderStyle-Width="120" ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="操作" HeaderStyle-Width="40"
                ItemStyle-Width="100">
                <ItemTemplate>
                    <%#   ((WTF.Logging.OperationType)((int)Eval("OperationTypeID"))).GetEnumDescription() %>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="Account" HeaderText="帐号" HeaderStyle-Width="120" ItemStyle-Width="120">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="Sql语句">
                <ItemTemplate>
                    <a href='<%# string.Format("OperationLogInfo.aspx?OperationID={0}&ApplicationID={1}", Eval("OperationID"),ApplicationID).EncryptModuleQuery()  %>'>
                        <%#   Eval("SqlQuery").ToString().CutText(20).CleanHtmlTags(true) %></a>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="CreateDate" HeaderText="记录时间" HeaderStyle-Width="120" ItemStyle-Width="120" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
            </WTF:BoundField>
        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
