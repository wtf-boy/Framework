<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true" CodeFile="ThemeList.aspx.cs" Inherits="ServiceLayer_Module_ThemeList" %>

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
        DataKeyNames="ModuleThemeID" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand"
        OnPagerChangeCommand="CurrentPager_PagerChangeCommand">
        <Columns>
            <WTF:SelectField></WTF:SelectField>
            <WTF:OperateField DataTextField="ThemeConfigName" HeaderText="主题名称">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="预览图标">
                <ItemTemplate>
                    <a href='<%#    GetThemePreviewIco( Container.DataItem)%>' target="_blank">
                        <img border="0" title="预览图标" src='<%#  GetThemePreviewIco(Container.DataItem)%>' width="100" height="100" />
                    </a>
                </ItemTemplate>
            </WTF:TemplateField>


        </Columns>
    </WTF:MyGridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphScriptcbar" runat="Server">

    <script type="text/javascript">
        //用户成员
        function ThemeImport() {
            //打开页面
            var theDes = "dialogWidth:400px;dialogHeight:500px;edge:sunken;help:no;status:no;scroll:no;";
            window.showModalDialog('<%=EncryptModuleQuery(string.Format("ThemeImport.aspx?ThemeModuleTypeID={0}",ThemeModuleTypeID))%>', window, theDes);

            return false;
        }
    </script>
</asp:Content>

