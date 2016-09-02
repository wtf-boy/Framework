<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ParameterList.aspx.cs" Inherits="ServiceLayer_EnumType_ParameterList"
    Title="无标题页" %>

<%@ Import Namespace="WTF.DataConfig.Entity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" runat="Server">
    <WTF:QueryTextBox ID="ParameterName" QueryTitle="参数名称" runat="server"></WTF:QueryTextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_EnumType_ParameterList"
        DataKeyNames="ParameterID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ParameterName" HeaderText="参数名称">
            </WTF:OperateField>
            <WTF:BoundField DataField="ParameterCode" HeaderText="参数代码">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="参数类型">
                <ItemTemplate>
                    <%# ((Sys_ParameterType)Eval("Sys_ParameterType")).ParameterTypeName%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="ParameterCodeID" HeaderText="参数标识">
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

        //用户成员
        function Sort() {

            //打开页面
            var theDes = "dialogWidth:400px;dialogHeight:350px;edge:sunken;help:no;status:no;scroll:no;";
            window.showModalDialog('<%=EncryptModuleQuery(string.Format("ParameterSort.aspx?ParameterTypeID={0}",ParameterTypeID))%>', window, theDes);
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
