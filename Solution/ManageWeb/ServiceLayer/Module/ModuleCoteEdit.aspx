<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="ModuleCoteEdit.aspx.cs" Inherits="ServiceLayer_Module_ModuleCoteEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">基础表结构
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>栏目名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCoteTitle" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxCharLength="50" ErrorMessage="请输入栏目名称" runat="server" Text="<%# objSys_ModuleCote.CoteTitle %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>栏目表名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCoteTableName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxCharLength="100" ErrorMessage="请输入栏目表名" runat="server" Text="<%# objSys_ModuleCote.CoteTableName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>ID字段名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtIDName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxCharLength="100" ErrorMessage="请输入ID字段名" runat="server" Text="<%# objSys_ModuleCote.IDName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>名称字段名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxCharLength="100" ErrorMessage="请输入名称字段名" runat="server" Text="<%# objSys_ModuleCote.Name %>"></WTF:MyTextBox>
            </td>
        </tr>
      
        <tr>
            <td>
                <span class="txtNoNull">*</span>连接字符串名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConnectionStringName" ValidationGroup="SaveGroup" Width="500" CheckValueEmpty="true" MaxCharLength="200" ErrorMessage="请输入连接字符串名" runat="server" Text="<%# objSys_ModuleCote.ConnectionStringName %>"></WTF:MyTextBox>
            </td>
        </tr>
      

        <tr>
            <td>条件表达式:
            </td>
            <td>
                <WTF:MyTextBox ID="txtCondition" TextMode="MultiLine" ValidationGroup="SaveGroup" Width="600" Height="50" MaxCharLength="200" runat="server" Text="<%# objSys_ModuleCote.Condtion %>"></WTF:MyTextBox>
            </td>
        </tr>

        <tr>
            <td>排序表达式:
            </td>
            <td>
                <WTF:MyTextBox ID="txtSortExpression" ValidationGroup="SaveGroup" Width="500" MaxCharLength="200" runat="server" Text="<%# objSys_ModuleCote.SortExpression %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>ID类型1整型2字符串:
            </td>
            <td>

                <WTF:MyRadioButtonList ID="radIDDataType" CssClass=" radTableBase" RepeatColumns="2" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择ID类型1整型2字符串" runat="server">
                    <asp:ListItem Value="1" Text="整形"></asp:ListItem>
                    <asp:ListItem Value="2" Text="字符串"></asp:ListItem>
                </WTF:MyRadioButtonList>
            </td>
        </tr>

        <tr class="trCaption">
            <td colspan="2">地址配置
            </td>
        </tr>
        <tr>
            <td>父节点是否地址:
            </td>
            <td>
                <asp:CheckBox ID="chkIsParentUrl" runat="server" Checked="true" Text="是否父节点地址" />
            </td>
        </tr>
          <tr class="trCaption">
            <td colspan="2">树结构 
            </td>
        </tr>
        <tr>
            <td>
             父节点字段名:
            </td>
            <td>
                <WTF:MyTextBox ID="txtParentIDName" ValidationGroup="SaveGroup" MaxCharLength="100" ErrorMessage="请输入父节点字段名" runat="server" Text="<%# objSys_ModuleCote.ParentIDName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>ID路经名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtIDPathName" ValidationGroup="SaveGroup" MaxCharLength="100" ErrorMessage="请输入ID路经名称" runat="server" Text="<%# objSys_ModuleCote.IDPathName %>"></WTF:MyTextBox>
            </td>
        </tr>
          <tr>
            <td>
              根节点ID值:
            </td>
            <td>
                <WTF:MyTextBox ID="txtRootIDValue" ValidationGroup="SaveGroup" Width="500" MaxCharLength="200" ErrorMessage="请输入根节点ID值" runat="server" Text="<%# objSys_ModuleCote.RootIDValue %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
</asp:Content>
