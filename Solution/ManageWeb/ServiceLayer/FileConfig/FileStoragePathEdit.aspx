<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true" CodeFile="FileStoragePathEdit.aspx.cs" Inherits="ServiceLayer_FileConfig_FileStoragePathEdit" %>

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
            <td colspan="2">存储配置信息</td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>存储名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtStoragePathName" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="256" ErrorMessage="请输入存储名称" runat="server" Text="<%# objresource_filestoragepath.StoragePathName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>存储类型:
            </td>
            <td>
                <WTF:MyRadioButtonList ID="radStorageTypeID" RepeatColumns="3" ValidationGroup="SaveGroup" CheckValueEmpty="true" ErrorMessage="请选择存储类型" runat="server">

                    <asp:ListItem Value="1" Text="本地存储"></asp:ListItem>
                    <asp:ListItem Value="2" Text="FTP"></asp:ListItem>
                    <asp:ListItem Value="3" Selected="True" Text="文件系统"></asp:ListItem>
                </WTF:MyRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>虚拟目录:
            </td>
            <td>
                <WTF:MyTextBox ID="txtVirtualName" Width="500" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="1000" ErrorMessage="请输入虚拟目录" runat="server" Text="<%# objresource_filestoragepath.VirtualName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trStoragePath">
            <td>
                <span class="txtNoNull">*</span><label id="StoragePath">存储地址</label>
                :
            </td>
            <td>
                <WTF:MyTextBox ID="txtStoragePath" Width="500" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="1000" ErrorMessage="请输入存储地址" runat="server" Text="<%# objresource_filestoragepath.StoragePath %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="trStoragehConfig">
            <td>
                <span class="txtNoNull">*</span> 配置文件
                :
            </td>
            <td>
                <WTF:MyTextBox ID="txtStorageConfig" Width="500" TextMode="MultiLine" Height="300" ValidationGroup="SaveGroup" CheckValueEmpty="false" ErrorMessage="请输入配置文件" runat="server" Text="<%# objresource_filestoragepath.StorageConfig %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="traccount">
            <td>
                <span class="txtNoNull">*</span>IP:
            </td>
            <td>
                <WTF:MyTextBox ID="txtIPAddress" ValidationGroup="SaveGroup" CheckValueEmpty="true" MaxLength="100" ErrorMessage="请输入IP" runat="server" Text="<%# objresource_filestoragepath.IPAddress %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="traccount">
            <td>
                <span class="txtNoNull">*</span>帐号:
            </td>
            <td>
                <WTF:MyTextBox ID="txtAccount" ValidationGroup="SaveGroup" MaxLength="100" ErrorMessage="请输入帐号" runat="server" Text="<%# objresource_filestoragepath.Account %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="traccount">
            <td>
                <span class="txtNoNull">*</span>密码:
            </td>
            <td>
                <WTF:MyTextBox ID="txtPassword" ValidationGroup="SaveGroup" MaxLength="100" ErrorMessage="请输入密码" runat="server" Text="<%# objresource_filestoragepath.Password %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr class="traccount">
            <td>
                <span class="txtNoNull">*</span>端口:
            </td>
            <td>
                <WTF:MyTextBox ID="txtPort" ValidationGroup="SaveGroup" ValidationExpression="\d+" MaxLength="100" ErrorMessage="请输入端口" runat="server" Text="<%# objresource_filestoragepath.Port %>"></WTF:MyTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">

    <script type="text/javascript">


        $("#<%=radStorageTypeID.ClientID%>").change(function () {

            var StorageTypeValue = $("#<%=radStorageTypeID.ClientID%> :checked").val();
            $(".trStoragehConfig").hide();
            $(".trStoragehConfig input").removeAttr("CheckValueEmpty");
            if (StorageTypeValue == "2") {
                $(".traccount").show();

                $(".traccount input").attr("CheckValueEmpty", "true");

                $(".trStoragePath").show();
                $(".trStoragePath input").attr("CheckValueEmpty", "true");
                $("#StoragePath").text("存储地址");
                $(".trStoragePath input").attr("ErrorMessage", "请输入存储地址");
            }
            else {
                $(".traccount").hide();
                $(".traccount input").removeAttr("CheckValueEmpty");


                $(".trStoragePath input").attr("CheckValueEmpty", "true");
                $(".trStoragePath").show();

                if (StorageTypeValue == "3") {
                    $(".trStoragehConfig").show();
                    $(".trStoragehConfig input").attr("CheckValueEmpty", "true");
                    $("#StoragePath").text("文件组名");
                    $(".trStoragePath input").attr("ErrorMessage", "请输入文件组名");
                }
                else {
                    $("#StoragePath").text("存储地址");
                    $(".trStoragePath input").attr("ErrorMessage", "请输入存储地址");
                }
            }
        });

        jQuery(function () {


            var StorageTypeValue = $("#<%=radStorageTypeID.ClientID%> :checked").val();
            $(".trStoragehConfig").hide();
            $(".trStoragehConfig input").removeAttr("CheckValueEmpty");
            if (StorageTypeValue == "2") {
                $(".traccount").show();
                $(".traccount input").attr("CheckValueEmpty", "true");
                $(".trStoragePath").show();
                $(".trStoragePath input").attr("CheckValueEmpty", "true");
                $("#StoragePath").text("存储地址");
                $(".trStoragePath input").attr("ErrorMessage", "请输入存储地址");
            }
            else {
                $(".traccount").hide();

                $(".traccount input").removeAttr("CheckValueEmpty");

                $(".trStoragePath input").attr("CheckValueEmpty", "true");
                $(".trStoragePath").show();
                if (StorageTypeValue == "3") {
                    $(".trStoragehConfig").show();
                    $(".trStoragehConfig input").attr("CheckValueEmpty", "true");
                    $("#StoragePath").text("文件组名");
                    $(".trStoragePath input").attr("ErrorMessage", "请输入文件组名");
                }
                else {
                    $("#StoragePath").text("存储地址");
                    $(".trStoragePath input").attr("ErrorMessage", "请输入存储地址");
                }
            }

        });

    </script>
</asp:Content>

