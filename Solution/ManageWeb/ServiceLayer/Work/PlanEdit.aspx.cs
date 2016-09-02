using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WTF.Framework;
using System.Xml;
using System.Data;
using WTF.Work.Entity;
using WTF.Work;
using System.Data.SqlTypes;
public partial class ServiceLayer_Work_PlanEdit : SupportPageBase
{
    /// <summary>
    /// 获取计划标识
    /// </summary>
    public int PlanID
    {
        get
        {
            return GetInt("PlanID");
        }

    }

    /// <summary>
    /// 获取作业标识
    /// </summary>
    public int WorkInfoID
    {
        get
        {
            return GetInt("WorkInfoID");
        }

    }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark = "";
    /// <summary>
    /// 变量
    /// </summary>

    public Work_Plan objWork_Plan = new Work_Plan();
    WorkRule objWorkRule = new WorkRule();

    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {



        if (PlanID.IsNoNull())
        {
            objWork_Plan = objWorkRule.Work_Plan.First(s => s.PlanID == PlanID);

            ReadXml(objWork_Plan.PlanConfig);
            Page.DataBind();
        }
        else
        {

            panExecute.Enabled = false;
            panFrequency.Enabled = true;
            panDay.Visible = false;
            panMonth.Visible = false;
            PanExecuteMonthWeek.Enabled = false;
            panExecuteTimeTimeInterval.Enabled = false;
            txtDayInterval.CheckValueEmpty = true;
            txtWeekInterval.CheckValueEmpty = true;
            txtExecuteTime.CheckValueEmpty = true; ;
            txtStartDate.CheckValueEmpty = true; ;
            chkWeek.SelectedValue = "0";

        }

    }
    /// <summary>
    /// 保存信息DateTime.Now.DayOfWeek

    /// </summary>
    public void SaveInfo()
    {

        if (PlanID.IsNull())
        {
            objWork_Plan.WorkInfoID = WorkInfoID;
            //是否启用
            objWork_Plan.IsEnable = false;

            //计划名称
            objWork_Plan.PlanName = txtPlanName.TextCutWord(50);
            //计划配置
            objWork_Plan.PlanConfig = GetConfigXML();

            if (radPlanType.SelectValueInt == 1)
            {
                objWork_Plan.StartDate = txtExecute.TextDateTime.AddDays(-1);
                objWork_Plan.EndDate = txtExecute.TextDateTime;


            }
            else
            {
                objWork_Plan.StartDate = txtStartDate.TextDateTime.AddDays(-1);
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    objWork_Plan.EndDate = DateTime.Parse(txtEndDate.Text + " " + (radExecuteTime.Checked ? txtExecuteTime.Text : txtIntervalEndTime.Text));
                }
                else
                {
                    objWork_Plan.EndDate = DateTime.Parse(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

            objWork_Plan.LastRunDate = DateTime.Parse(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
            //计划说明
            objWork_Plan.PlanRemark = Remark;
            objWork_Plan.ConfigInfo = txtConfigInfo.Text;
            objWorkRule.InsertPlan(objWork_Plan);

            MessageDialog("新增成功", "PlanList.aspx?WorkInfoID=" + WorkInfoID);
        }
        else
        {
            objWork_Plan = objWorkRule.Work_Plan.First(p => p.PlanID == PlanID);

            //计划名称
            objWork_Plan.PlanName = txtPlanName.TextCutWord(50);

            if (radPlanType.SelectValueInt == 1)
            {
                objWork_Plan.StartDate = txtExecute.TextDateTime.AddDays(-1);
                objWork_Plan.EndDate = txtExecute.TextDateTime;


            }
            else
            {
                objWork_Plan.StartDate = txtStartDate.TextDateTime.AddDays(-1);
                if (!string.IsNullOrEmpty(txtEndDate.Text))
                {
                    objWork_Plan.EndDate = DateTime.Parse(txtEndDate.Text + " " + (radExecuteTime.Checked ? txtExecuteTime.Text : txtIntervalEndTime.Text));
                }
                else
                {
                    objWork_Plan.EndDate = DateTime.Parse(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

            objWork_Plan.LastRunDate = DateTime.Parse(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss"));



            //计划配置
            objWork_Plan.PlanConfig = GetConfigXML();

            //计划说明
            objWork_Plan.PlanRemark = Remark;
            objWork_Plan.ConfigInfo = txtConfigInfo.Text;
            objWorkRule.UpdatePlan(objWork_Plan);
            MessageDialog("修改成功", "PlanList.aspx?WorkInfoID=" + WorkInfoID);
        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("PlanList.aspx?WorkInfoID=" + WorkInfoID);
                break;
        }

    }

    protected void radPlanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetValidate();
        panExecute.Enabled = radPlanType.SelectValueInt == 1;
        panFrequency.Enabled = radPlanType.SelectValueInt == 2;
        if (radPlanType.SelectValueInt == 1)
        {
            txtExecute.CheckValueEmpty = true;

        }
        else
        {
            switch (radFrequency.SelectValueInt)
            {
                case 1:
                    txtDayInterval.CheckValueEmpty = true;
                    break;
                case 2:
                    radExecuteTime_CheckedChanged(sender, e);

                    break;

                default:
                    break;
            }
        }



    }
    protected void radFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {

        panDay.Visible = radFrequency.SelectValueInt == 1;

        panWeek.Visible = radFrequency.SelectValueInt == 2;

        panMonth.Visible = radFrequency.SelectValueInt == 3;
        switch (radFrequency.SelectValueInt)
        {
            case 1:
                txtDayInterval.CheckValueEmpty = true; ;
                txtWeekInterval.CheckValueEmpty = false;
                break;
            case 2:
                txtWeekInterval.CheckValueEmpty = true; ;
                txtDayInterval.CheckValueEmpty = false;

                break;
            case 3:
                txtDayInterval.CheckValueEmpty = false;
                txtWeekInterval.CheckValueEmpty = false;
                break;
            default:
                break;
        }
    }
    protected void radExecute_CheckedChanged(object sender, EventArgs e)
    {

        PanExecuteMonthWeek.Enabled = !radExecuteDay.Checked;
        PanExecuteMonthDay.Enabled = radExecuteDay.Checked;

    }

    protected void radExecuteTime_CheckedChanged(object sender, EventArgs e)
    {

        panExecuteTime.Enabled = radExecuteTime.Checked;
        txtExecuteTime.CheckValueEmpty = radExecuteTime.Checked;
        txtHourCount.CheckValueEmpty = !radExecuteTime.Checked;
        txtIntervalStartTime.CheckValueEmpty = !radExecuteTime.Checked;
        txtIntervalEndTime.CheckValueEmpty = !radExecuteTime.Checked;
        panExecuteTimeTimeInterval.Enabled = !radExecuteTime.Checked;
        txtStartDate.CheckValueEmpty = true; ;
        txtWeekInterval.CheckValueEmpty = true; ;
    }
    void ResetValidate()
    {

        txtExecute.CheckValueEmpty = false;
        txtDayInterval.CheckValueEmpty = false;
        txtWeekInterval.CheckValueEmpty = false;
        txtExecuteTime.CheckValueEmpty = false;
        txtHourCount.CheckValueEmpty = false;
        txtIntervalStartTime.CheckValueEmpty = false;
        txtIntervalEndTime.CheckValueEmpty = false;
        txtStartDate.CheckValueEmpty = false;

    }
    /// <summary>
    /// 生成配置信息
    /// </summary>
    /// <returns></returns>
    public string GetConfigXML()
    {

        string HeadRemark = "";
        string MidRemark = "";
        string EndRemark = "";

        XmlDocument objXmlDocument = new XmlDocument();
        XmlElement objPlanXmlElement = objXmlDocument.CreateElement("Plan");
        if (radPlanType.SelectValueInt == 1)
        {
            //执行一次
            objPlanXmlElement.SetAttribute("Type", "1");
            objPlanXmlElement.SetAttribute("ExecuteDate", "");
            objXmlDocument.AppendChild(objPlanXmlElement);

            XmlElement objExecuteXmlElement = objXmlDocument.CreateElement("Execute");
            objExecuteXmlElement.InnerText = txtExecute.TextDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            objPlanXmlElement.AppendChild(objExecuteXmlElement);


            HeadRemark = string.Format("在 {0} 执行", txtExecute.TextDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            MidRemark = "";
            EndRemark = "";
            Remark = HeadRemark + MidRemark + EndRemark;


        }
        else
        {
            //重复执行
            objPlanXmlElement.SetAttribute("Type", "2");
            objPlanXmlElement.SetAttribute("ExecuteDate", "");
            objPlanXmlElement.SetAttribute("StartDate", txtStartDate.TextDateTime.ToString("yyyy-MM-dd"));
            if (txtEndDate.Text.IsNoNull())
            {
                objPlanXmlElement.SetAttribute("EndDate", txtEndDate.TextDateTime.ToString("yyyy-MM-dd"));

                EndRemark = string.Format("将在 {0} 到 {1}之间使用计划", txtStartDate.TextDateTime.ToString("yyyy-MM-dd"), txtEndDate.TextDateTime.ToString("yyyy-MM-dd"));


            }
            else
            {
                objPlanXmlElement.SetAttribute("EndDate", DateTime.MaxValue.ToString("yyyy-MM-dd"));

                EndRemark = string.Format("将从 {0} 开始使用计划。", txtStartDate.TextDateTime.ToString("yyyy-MM-dd"));
            }

            objXmlDocument.AppendChild(objPlanXmlElement);


            //频率
            XmlElement objFrequencyXmlElement = objXmlDocument.CreateElement("Frequency");
            if (radFrequency.SelectValueInt == 1)
            {
                objFrequencyXmlElement.SetAttribute("Type", "1");

                XmlElement objDayXmlElement = objXmlDocument.CreateElement("Day");
                objDayXmlElement.SetAttribute("Interval", txtDayInterval.Text);

                objFrequencyXmlElement.AppendChild(objDayXmlElement);
                HeadRemark = string.Format("每{0}天", txtDayInterval.Text);


            }
            else if (radFrequency.SelectValueInt == 2)
            {
                objFrequencyXmlElement.SetAttribute("Type", "2");
                XmlElement objWeekXmlElement = objXmlDocument.CreateElement("Week");

                objWeekXmlElement.SetAttribute("Interval", txtWeekInterval.Text);
                objWeekXmlElement.InnerText = chkWeek.SelectValueString;
                objFrequencyXmlElement.AppendChild(objWeekXmlElement);

                string weekText = "";
                for (int i = 0; i < this.chkWeek.Items.Count; i++)
                {
                    if (this.chkWeek.Items[i].Selected)
                    {
                        weekText += this.chkWeek.Items[i].Text + ",";  //获取所有Text值

                    }
                }



                HeadRemark = string.Format("在每{0}周 {1} ", txtWeekInterval.Text == "1" ? "" : txtWeekInterval.Text, weekText.TrimEnd(','));
            }
            else
            {
                objFrequencyXmlElement.SetAttribute("Type", "3");
                XmlElement objMonthXmlElement = objXmlDocument.CreateElement("Month");
                if (radExecuteDay.Checked)
                {
                    objMonthXmlElement.SetAttribute("MonthType", "1");
                    objMonthXmlElement.SetAttribute("Interval", dropMonthDayInterval.SelectedValue);

                    XmlElement objMonthDayXmlElement = objXmlDocument.CreateElement("Day");
                    objMonthDayXmlElement.InnerText = dropMonthDay.SelectedValue;

                    objMonthXmlElement.AppendChild(objMonthDayXmlElement);

                    HeadRemark = string.Format("每{0}个月于当月第 {1} 天的", dropMonthDayInterval.SelectedValue == "1" ? "" : dropMonthDayInterval.SelectedValue, dropMonthDay.SelectedValue);

                }
                else
                {

                    objMonthXmlElement.SetAttribute("MonthType", "2");
                    objMonthXmlElement.SetAttribute("Interval", dropMonthWeekInterval.SelectedValue);

                    XmlElement objMonthWeekXmlElement = objXmlDocument.CreateElement("Week");

                    objMonthWeekXmlElement.SetAttribute("WeekNumber", dropWeekNumber.SelectedValue);

                    objMonthWeekXmlElement.InnerText = dropWeek.SelectedValue;
                    objMonthXmlElement.AppendChild(objMonthWeekXmlElement);

                    HeadRemark = string.Format("每 {0} 个月于 第{1}个 {2} ", dropMonthWeekInterval.SelectedValue, dropWeekNumber.SelectedValue, dropWeek.SelectedItem.Text);
                }

                objFrequencyXmlElement.AppendChild(objMonthXmlElement);
            }
            objPlanXmlElement.AppendChild(objFrequencyXmlElement);

            XmlElement objDayFrequencyXmlElement = objXmlDocument.CreateElement("DayFrequency");
            objPlanXmlElement.AppendChild(objDayFrequencyXmlElement);
            objDayFrequencyXmlElement.SetAttribute("Type", radExecuteTime.Checked ? "1" : "2");
            if (radExecuteTime.Checked)
            {

                XmlElement objDayFrequencyExecuteTimeXmlElement = objXmlDocument.CreateElement("ExecuteTime");
                objDayFrequencyExecuteTimeXmlElement.InnerText = txtExecuteTime.TextDateTime.ToString("HH:mm:ss");
                objDayFrequencyXmlElement.AppendChild(objDayFrequencyExecuteTimeXmlElement);

                MidRemark = string.Format("的{0}执行。", txtExecuteTime.TextDateTime.ToString("HH:mm:ss"));
            }
            else
            {
                XmlElement objDayFrequencyIntervalXmlElement = objXmlDocument.CreateElement("Interval");
                objDayFrequencyIntervalXmlElement.SetAttribute("Type", dropIntervalType.SelectedValue);
                objDayFrequencyIntervalXmlElement.SetAttribute("Number", txtHourCount.Text);
                objDayFrequencyIntervalXmlElement.SetAttribute("StartTime", txtIntervalStartTime.TextDateTime.ToString("HH:mm:ss"));
                objDayFrequencyIntervalXmlElement.SetAttribute("EndTime", txtIntervalEndTime.TextDateTime.ToString("HH:mm:ss"));
                objDayFrequencyXmlElement.AppendChild(objDayFrequencyIntervalXmlElement);

                MidRemark = string.Format(" {0} 和 {1} 之间、每 {2} {3} 执行。", txtIntervalStartTime.TextDateTime.ToString("HH:mm:ss"), txtIntervalEndTime.TextDateTime.ToString("HH:mm:ss"), txtHourCount.Text, dropIntervalType.SelectedItem.Text);
            }
            Remark = HeadRemark + MidRemark + EndRemark;

        }
        return objXmlDocument.InnerXml;

    }
    /// <summary>
    /// 修改读取配置数据
    /// </summary>
    /// <param name="PlanConfig">配置信息</param>
    void ReadXml(string PlanConfig)
    {
        XmlDocument objXmlDocument = new XmlDocument();
        objXmlDocument.LoadXml(PlanConfig);

        XmlNode objPlanXmlNode = objXmlDocument.SelectSingleNode("Plan");

        //执行单次
        if (objPlanXmlNode.Attributes["Type"].Value == "1")
        {
            radPlanType.SelectedValue = "1";
            txtExecute.Text = objPlanXmlNode.SelectSingleNode("Execute").InnerText;
            txtExecute.CheckValueEmpty = true; ;
            panFrequency.Enabled = false;
            panDay.Visible = false;
            panMonth.Visible = false;
            PanExecuteMonthWeek.Enabled = false;
            panExecuteTimeTimeInterval.Enabled = false;

        }
        else
        {
            //重复多次
            radPlanType.SelectedValue = "2";
            panExecute.Enabled = false;
            txtStartDate.Text = objPlanXmlNode.Attributes["StartDate"].Value;
            txtStartDate.CheckValueEmpty = true; ;
            txtEndDate.Text = objPlanXmlNode.Attributes["EndDate"].Value == DateTime.MaxValue.ToString("yyyy-MM-dd") ? "" : objPlanXmlNode.Attributes["EndDate"].Value;
            XmlNode objFrequencyXmlNode = objPlanXmlNode.SelectSingleNode("Frequency");
            string FrequencyType = objFrequencyXmlNode.Attributes["Type"].Value;
            panDay.Visible = FrequencyType == "1";
            panWeek.Visible = FrequencyType == "2";
            panMonth.Visible = FrequencyType == "3";
            radFrequency.SelectedValue = FrequencyType;
            //频率
            if (FrequencyType == "1")
            {
                txtDayInterval.Text = objFrequencyXmlNode.SelectSingleNode("Day").Attributes["Interval"].Value;
                txtDayInterval.CheckValueEmpty = true; ;
            }
            else if (FrequencyType == "2")
            {

                XmlNode objWeek = objFrequencyXmlNode.SelectSingleNode("Week");
                txtWeekInterval.Text = objWeek.Attributes["Interval"].Value;
                txtWeekInterval.CheckValueEmpty = true; ;
                chkWeek.SetSelectValue(objWeek.InnerText);
                chkWeek.CheckValueEmpty = true; ;

            }
            else
            {
                XmlNode objMonth = objFrequencyXmlNode.SelectSingleNode("Month");
                string Interval = objMonth.Attributes["Interval"].Value;
                string MonthType = objMonth.Attributes["MonthType"].Value;
                radExecuteDay.Checked = PanExecuteMonthDay.Enabled = MonthType == "1";
                radExecuteAt.Checked = PanExecuteMonthWeek.Enabled = MonthType == "2";
                if (MonthType == "1")
                {
                    dropMonthDay.SelectedValue = objMonth.SelectSingleNode("Day").InnerText;
                    dropMonthDayInterval.SelectedValue = Interval;
                }
                else
                {
                    dropMonthWeekInterval.SelectedValue = Interval;
                    dropWeekNumber.SelectedValue = objMonth.SelectSingleNode("Week").Attributes["WeekNumber"].Value;
                    dropWeek.SelectedValue = objMonth.SelectSingleNode("Week").InnerText;
                }


            }


            //每天

            XmlNode objDayFrequencyXmlNode = objPlanXmlNode.SelectSingleNode("DayFrequency");
            string DayFrequencyType = objDayFrequencyXmlNode.Attributes["Type"].Value;
            radExecuteTime.Checked = panExecuteTime.Enabled = DayFrequencyType == "1";
            radExecuteTimeInterval.Checked = panExecuteTimeTimeInterval.Enabled = DayFrequencyType == "2";

            if (DayFrequencyType == "1")
            {
                txtExecuteTime.Text = objDayFrequencyXmlNode.SelectSingleNode("ExecuteTime").InnerText;
                txtExecuteTime.CheckValueEmpty = true; ;
            }
            else
            {
                XmlNode objInterval = objDayFrequencyXmlNode.SelectSingleNode("Interval");
                dropIntervalType.SelectedValue = objInterval.Attributes["Type"].Value;
                txtHourCount.Text = objInterval.Attributes["Number"].Value;
                txtHourCount.CheckValueEmpty = true; ;
                txtIntervalStartTime.Text = objInterval.Attributes["StartTime"].Value;
                txtIntervalStartTime.CheckValueEmpty = true; ;
                txtIntervalEndTime.Text = objInterval.Attributes["EndTime"].Value;
                txtIntervalEndTime.CheckValueEmpty = true; ;

            }


        }



    }




}