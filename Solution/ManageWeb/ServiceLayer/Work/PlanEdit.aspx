<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/EditPage.master" AutoEventWireup="true"
    CodeFile="PlanEdit.aspx.cs" Inherits="ServiceLayer_Work_PlanEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <script type="text/javascript" src="../../App_Control/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        fieldset {
            margin-top: 5px;
            padding-left: 10px;
        }

        legend {
            font-weight: bold;
            color: #3E6AAA;
        }

        .pan {
            display: inline-block;
        }

        .pan {
            padding-left: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTopToolbar" runat="Server">
    <WTF:MyToolbar ID="MyTopToolBar" runat="server" ModuleCode="" OperatePlaceTypeValue="OperateTopBar"
        OnItemCommand="CurrentTool_ItemCommand" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="Server">
    <table class="tblContent">
        <colgroup>
            <col class="colTitle" width="120" />
            <col class="colContent" />
        </colgroup>
        <tr class="trCaption">
            <td colspan="2">计划信息
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>计划名称:
            </td>
            <td>
                <WTF:MyTextBox ID="txtPlanName" ValidationGroup="SaveGroup" CheckValueEmpty="true"
                    MaxLength="50" ErrorMessage="请输入计划名称" runat="server" Text="<%# objWork_Plan.PlanName %>"></WTF:MyTextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>计划类型:
            </td>
            <td>
                <WTF:MyEnumRadioButtonList ID="radPlanType" runat="server" RepeatColumns="2" AutoPostBack="true"
                    OnSelectedIndexChanged="radPlanType_SelectedIndexChanged">
                    <asp:ListItem Value="1" Text="执行一次"></asp:ListItem>
                    <asp:ListItem Value="2" Selected="True" Text="重复执行"></asp:ListItem>
                </WTF:MyEnumRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <span class="txtNoNull">*</span>计划配置:
            </td>
            <td>
                <div style="padding: 3px;">
                    <fieldset>
                        <legend>执行一次</legend>
                        <asp:Panel ID="panExecute" runat="server">
                            执行时间：
                            <WTF:MyTextBox ID="txtExecute" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                runat="server" ValidationGroup="SaveGroup" ErrorMessage="请输入执行时间"></WTF:MyTextBox>
                        </asp:Panel>
                    </fieldset>
                    <asp:Panel ID="panFrequency" runat="server">
                        <fieldset>
                            <legend>频率</legend>
                            <div>
                                <WTF:MyEnumRadioButtonList ID="radFrequency" runat="server" RepeatColumns="3" AutoPostBack="true"
                                    OnSelectedIndexChanged="radFrequency_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="每天"></asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True" Text="每周"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="每月"></asp:ListItem>
                                </WTF:MyEnumRadioButtonList>
                            </div>
                            <asp:Panel ID="panDay" runat="server" CssClass="pan">
                                <div>
                                    执行间隔:<WTF:MyTextBox ID="txtDayInterval" Width="30" ValidationExpression="[1-9]{1}\d*"
                                        runat="server" ValidationGroup="SaveGroup" ErrorMessage="请用数字输入执行间隔天"></WTF:MyTextBox>天
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panWeek" runat="server" CssClass="pan">
                                <div>
                                    执行间隔:<WTF:MyTextBox ID="txtWeekInterval" runat="server" Text="1" Width="30" ValidationGroup="SaveGroup"
                                        ErrorMessage="请用数字输入执行间隔周" ValidationExpression="[1-9]{1}\d*"></WTF:MyTextBox>周，在
                                    <WTF:MyCheckBoxList ID="chkWeek" runat="server" ValidationGroup="SaveGroup" ErrorMessage="请选择星期几"
                                        RepeatColumns="7" RepeatLayout="Flow">
                                        <asp:ListItem Value="1">星期一</asp:ListItem>
                                        <asp:ListItem Value="2">星期二</asp:ListItem>
                                        <asp:ListItem Value="3">星期三</asp:ListItem>
                                        <asp:ListItem Value="4">星期四</asp:ListItem>
                                        <asp:ListItem Value="5">星期五</asp:ListItem>
                                        <asp:ListItem Value="6">星期六</asp:ListItem>
                                        <asp:ListItem Value="0">星期日</asp:ListItem>
                                    </WTF:MyCheckBoxList>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panMonth" runat="server" Style="padding-left: 5px">
                                <div>
                                    <asp:RadioButton ID="radExecuteDay" runat="server" Text="第" GroupName="Execute" Checked="true"
                                        OnCheckedChanged="radExecute_CheckedChanged" AutoPostBack="true" />&nbsp;
                                    <asp:Panel ID="PanExecuteMonthDay" runat="server" CssClass="pan">
                                        <WTF:MyDropDownList ID="dropMonthDay" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                        </WTF:MyDropDownList>
                                        天&nbsp;-&nbsp;每
                                        <WTF:MyDropDownList ID="dropMonthDayInterval" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </WTF:MyDropDownList>
                                        个月
                                    </asp:Panel>
                                </div>
                                <div>
                                    <asp:RadioButton ID="radExecuteAt" runat="server" Text="在" GroupName="Execute" OnCheckedChanged="radExecute_CheckedChanged"
                                        AutoPostBack="true" />&nbsp;
                                    <asp:Panel ID="PanExecuteMonthWeek" runat="server" CssClass="pan">
                                        <WTF:MyDropDownList ID="dropWeekNumber" runat="server">
                                            <asp:ListItem Value="1">第一个</asp:ListItem>
                                            <asp:ListItem Value="2">第二个</asp:ListItem>
                                            <asp:ListItem Value="3">第三个</asp:ListItem>
                                            <asp:ListItem Value="4">第四个</asp:ListItem>
                                            <asp:ListItem Value="0">最后一个</asp:ListItem>
                                        </WTF:MyDropDownList>
                                        <WTF:MyDropDownList ID="dropWeek" runat="server">
                                            <asp:ListItem Value="1">星期一</asp:ListItem>
                                            <asp:ListItem Value="2">星期二</asp:ListItem>
                                            <asp:ListItem Value="3">星期三</asp:ListItem>
                                            <asp:ListItem Value="4">星期四</asp:ListItem>
                                            <asp:ListItem Value="5">星期五</asp:ListItem>
                                            <asp:ListItem Value="6">星期六</asp:ListItem>
                                            <asp:ListItem Value="0">星期日</asp:ListItem>
                                        </WTF:MyDropDownList>
                                        &nbsp;-&nbsp;每
                                        <WTF:MyDropDownList ID="dropMonthWeekInterval" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </WTF:MyDropDownList>
                                        个月
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </fieldset>
                        <fieldset>
                            <legend>每天频率</legend>
                            <div>
                                <asp:RadioButton ID="radExecuteTime" runat="server" Text="执行一次,时间为" GroupName="ExecuteTime"
                                    Checked="true" OnCheckedChanged="radExecuteTime_CheckedChanged" AutoPostBack="true" />
                                <asp:Panel ID="panExecuteTime" runat="server" CssClass="pan">
                                    <WTF:MyTextBox ID="txtExecuteTime" SkinID="Date" Width="80" onfocus="new WdatePicker({dateFmt:' HH:mm:ss'})"
                                        ValidationGroup="SaveGroup" ErrorMessage="请输入执行时间一次" runat="server" Text="">
                                    </WTF:MyTextBox>
                                </asp:Panel>
                            </div>
                            <asp:RadioButton ID="radExecuteTimeInterval" runat="server" Text="执行间隔" GroupName="ExecuteTime"
                                OnCheckedChanged="radExecuteTime_CheckedChanged" AutoPostBack="true" />
                            <asp:Panel ID="panExecuteTimeTimeInterval" runat="server" CssClass="pan">
                                <WTF:MyTextBox ID="txtHourCount" ValidationExpression="[1-9]{1}\d*" runat="server" Text="" Width="40" ValidationGroup="SaveGroup"
                                    ErrorMessage="请输入执行间隔"></WTF:MyTextBox>
                                <WTF:MyDropDownList ID="dropIntervalType" runat="server">
                                    <asp:ListItem Value="1">分钟</asp:ListItem>
                                    <asp:ListItem Value="2">小时</asp:ListItem>
                                </WTF:MyDropDownList>
                                &nbsp; &nbsp; 开始时间
                                <WTF:MyTextBox ID="txtIntervalStartTime" SkinID="Date" Width="80" onfocus="new WdatePicker({dateFmt:' HH:mm:ss'})"
                                    runat="server" Text="" ValidationGroup="SaveGroup" ErrorMessage="请输入开始时间"></WTF:MyTextBox>
                                结束时间
                                <WTF:MyTextBox ID="txtIntervalEndTime" SkinID="Date" Width="80" onfocus="new WdatePicker({dateFmt:'HH:mm:ss'})"
                                    runat="server" Text="" ValidationGroup="SaveGroup" ErrorMessage="请输入结束时间"></WTF:MyTextBox>
                            </asp:Panel>
                        </fieldset>
                        <fieldset>
                            <legend>持续时间</legend>开始日期:<WTF:MyTextBox ID="txtStartDate" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd '})"
                                runat="server" Text="" ValidationGroup="SaveGroup" ErrorMessage="请输入开始日期"></WTF:MyTextBox>
                            结束日期:
                            <WTF:MyTextBox ID="txtEndDate" SkinID="Date" onfocus="new WdatePicker({dateFmt:'yyyy-MM-dd'})"
                                runat="server" Text=""></WTF:MyTextBox>
                        </fieldset>
                    </asp:Panel>
                </div>
            </td>
        </tr>


        <tr>
            <td>作业说明:
            </td>
            <td>
                <%# objWork_Plan.PlanRemark %>
            </td>
        </tr>
        <tr>
            <td>作业配置参数:
            </td>
            <td>
                <WTF:MyTextBox ID="txtConfigInfo" ValidationGroup="SaveGroup" TextMode="MultiLine"
                    Width="800" Height="250" runat="server" Text="<%# objWork_Plan.ConfigInfo %>"></WTF:MyTextBox>

            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScriptcbar" runat="Server">
    <script type="text/javascript">

        //        $("#<%=txtWeekInterval.ClientID %>").change(function () {
        //            $("#<%=txtWeekInterval.ClientID %>").attr("CheckValueEmpty", true);

        //        });


    </script>
</asp:Content>
