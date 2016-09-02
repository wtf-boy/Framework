<%@ Page Language="C#" MasterPageFile="~/App_Master/ListPage.master" AutoEventWireup="true"
    CodeFile="ResourceVerList.aspx.cs" Inherits="ServiceLayer_Resource_ResourceVerList"
    Title="�ޱ���ҳ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="ServiceLayer_Resource_ResourceVerList"
        OperatePlaceTypeValue="OperateTopBar" OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSearchBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <WTF:MyGridView ID="gdvContent" runat="server" ModuleCode="ServiceLayer_Resource_ResourceVerList"
        DataKeyNames="VerNo" AutoGenerateColumns="False" OnRowCommand="CurrentContent_RowCommand" OnPagerChangeCommand="CurrentPager_PagerChangeCommand" >
        <Columns>
            <WTF:SelectField>
            </WTF:SelectField>
            <WTF:OperateField DataTextField="ResourceFileName" HeaderText="�ļ�����">
            </WTF:OperateField>
            <WTF:TemplateField HeaderText="�ļ���С">
                <ItemTemplate>
                    <%# TypeHelper.RenderFileSize(Eval("ResourceSize").ConvertLong())%>
                </ItemTemplate>
            </WTF:TemplateField>
            <WTF:BoundField DataField="ContentType" HeaderText="�ļ�����">
            </WTF:BoundField>
            <WTF:BoundField DataField="CreateDateTime" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd HH:mm}">
            </WTF:BoundField>
            <WTF:BoundField DataField="Account" HeaderText="�û��ʺ�">
            </WTF:BoundField>
            <WTF:BoundField DataField="VerNo" HeaderText="�汾��">
            </WTF:BoundField>
            <WTF:BoundField DataField="RefCount" HeaderText="������">
            </WTF:BoundField>
            <WTF:TemplateField HeaderText="�鿴�ļ�">
                <ItemTemplate>
                    <a href='<%# Eval("ResourcePath") %>' target="_blank">�鿴�ļ�</a>
                </ItemTemplate>
            </WTF:TemplateField>
        </Columns>
    </WTF:MyGridView>
    

    <script type="text/javascript">

        function Remove() {
            if (window.confirm("ȷ��ɾ����?")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
