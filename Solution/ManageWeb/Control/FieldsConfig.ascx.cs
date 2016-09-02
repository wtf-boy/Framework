using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using System.Xml;
public partial class Control_FieldsConfig : System.Web.UI.UserControl
{

    public string FieldsXmlPath
    {
        get
        {
            return ViewState.GetString("FieldsXmlPath");
        }
        set
        {
            ViewState["FieldsXmlPath"] = value;
        }
    }
    Dictionary<string, string> _FieldValues = new Dictionary<string, string>();
    Dictionary<string, string> _ConditionValues = new Dictionary<string, string>();
    public void SetFieldValues(Dictionary<string, string> fieldValues)
    {

        _FieldValues = fieldValues;

    }

    public void SetFieldValues(string fieldValueJson)
    {

        _FieldValues = fieldValueJson.JsonJsDeserialize<Dictionary<string, string>>();

    }
    public void SetConditionValues(Dictionary<string, string> conditionValues)
    {

        _ConditionValues = conditionValues;
    }
    public string GetFieldJsonValues()
    {
        return GetFieldValues().JsonJsSerialize();
    }
    public Dictionary<string, string> GetFieldValues()
    {

        if (string.IsNullOrWhiteSpace(FieldsXmlPath))
        {
            Response.Write("请设置FieldXmlPath字段路经");
            return new Dictionary<string, string>();
        }

        string xmlFilePath = SysVariable.CurrentContext.Server.MapPath(FieldsXmlPath);
        XmlDocument _FileConfigXml = new XmlDocument();
        _FileConfigXml.Load(xmlFilePath);
        foreach (XmlNode objXmlNode in _FileConfigXml.SelectNodes("//FieldConfig/Field"))
        {
            string WriteType = objXmlNode.ReadAttribute("WriteType", "TextBox");
            if (WriteType == "Caption")
            {
                continue;
            }

            string KeyName = objXmlNode.ReadAttribute("KeyName");

            string KeyValue = "";


            if (WriteType == "TextBox")
            {
                Control_FieldTextBoxControl objFieldControl = (Control_FieldTextBoxControl)this.FindControl(objXmlNode.ReadAttribute("Name"));
                KeyValue = objFieldControl.Value;
            }
            else if (WriteType == "DropDown")
            {
                Control_FieldDropDownControl objFieldControl = (Control_FieldDropDownControl)this.FindControl(objXmlNode.ReadAttribute("Name"));
                KeyValue = objFieldControl.Value;
            }
            else if (WriteType == "CheckBoxList")
            {
                Control_FieldCheckBoxListControl objFieldControl = (Control_FieldCheckBoxListControl)this.FindControl(objXmlNode.ReadAttribute("Name"));
                KeyValue = objFieldControl.Value;
            }

            else if (WriteType == "RadioList")
            {
                Control_FieldRadioButtonListControl objFieldControl = (Control_FieldRadioButtonListControl)this.FindControl(objXmlNode.ReadAttribute("Name"));
                KeyValue = objFieldControl.Value;
            }
            else if (WriteType == "CheckBox")
            {
                Control_FieldCheckBoxControl objFieldControl = (Control_FieldCheckBoxControl)this.FindControl(objXmlNode.ReadAttribute("Name"));
                KeyValue = objFieldControl.Value;
            }

            if (!string.IsNullOrWhiteSpace(KeyValue))
            {
                _FieldValues.Add(KeyName, KeyValue);
            }


        }
        return _FieldValues;

    }

    private void CreateFieldConfigNode()
    {
        if (string.IsNullOrWhiteSpace(FieldsXmlPath))
        {
            Response.Write("请设置FieldXmlPath字段路经");
            return;
        }

        string xmlFilePath = SysVariable.CurrentContext.Server.MapPath(FieldsXmlPath);
        XmlDocument _FileConfigXml = new XmlDocument();
        _FileConfigXml.Load(xmlFilePath);
        foreach (XmlNode objXmlNode in _FileConfigXml.SelectNodes("//FieldConfig/Field"))
        {

            string WriteType = objXmlNode.ReadAttribute("WriteType", "TextBox");
            if (WriteType == "Caption")
            {
                Literal objLiteral = new Literal();
                objLiteral.Text = "<tr class=\"trCaption\"><td colspan=\"2\">" + objXmlNode.InnerText.Replace("\r\n", "").Trim() + " </td>  </tr>";
                panControl.Controls.Add(objLiteral);
            }
            else if (WriteType == "TextBox")
            {
                Control_FieldTextBoxControl objFieldControl = (Control_FieldTextBoxControl)this.LoadControl("/Control/FieldTextBoxControl.ascx");
                string Name = objXmlNode.ReadAttribute("Name");
                string KeyName = objXmlNode.ReadAttribute("KeyName", Name);
                objFieldControl.ID = Name;
                panControl.Controls.Add(objFieldControl);

                objFieldControl.SetField(objXmlNode);

                if (_FieldValues != null && _FieldValues.ContainsKey(KeyName))
                {
                    objFieldControl.Value = _FieldValues[KeyName];
                }
            }
            else if (WriteType == "DropDown")
            {
                Control_FieldDropDownControl objFieldControl = (Control_FieldDropDownControl)this.LoadControl("/Control/FieldDropDownControl.ascx");

                string Name = objXmlNode.ReadAttribute("Name");
                string KeyName = objXmlNode.ReadAttribute("KeyName", Name);
                objFieldControl.ID = Name;
                panControl.Controls.Add(objFieldControl);
                if (_ConditionValues != null && _ConditionValues.ContainsKey(Name))
                {
                    objFieldControl.Condition = _ConditionValues[Name];
                }
                objFieldControl.SetField(objXmlNode);

                if (_FieldValues != null && _FieldValues.ContainsKey(KeyName))
                {
                    objFieldControl.Value = _FieldValues[KeyName];
                }
            }
            else if (WriteType == "CheckBoxList")
            {
                Control_FieldCheckBoxListControl objFieldControl = (Control_FieldCheckBoxListControl)this.LoadControl("/Control/FieldCheckBoxListControl.ascx");
                string Name = objXmlNode.ReadAttribute("Name");
                string KeyName = objXmlNode.ReadAttribute("KeyName", Name);
                objFieldControl.ID = Name;
                panControl.Controls.Add(objFieldControl);
                if (_ConditionValues != null && _ConditionValues.ContainsKey(Name))
                {
                    objFieldControl.Condition = _ConditionValues[Name];
                }
                objFieldControl.SetField(objXmlNode);

                if (_FieldValues != null && _FieldValues.ContainsKey(KeyName))
                {
                    objFieldControl.Value = _FieldValues[KeyName];
                }
            }

            else if (WriteType == "RadioList")
            {
                Control_FieldRadioButtonListControl objFieldControl = (Control_FieldRadioButtonListControl)this.LoadControl("/Control/FieldRadioButtonListControl.ascx");

                string Name = objXmlNode.ReadAttribute("Name");
                string KeyName = objXmlNode.ReadAttribute("KeyName", Name);
                objFieldControl.ID = Name;
                panControl.Controls.Add(objFieldControl);

                if (_ConditionValues != null && _ConditionValues.ContainsKey(Name))
                {
                    objFieldControl.Condition = _ConditionValues[Name];
                }
                objFieldControl.SetField(objXmlNode);

                if (_FieldValues != null && _FieldValues.ContainsKey(KeyName))
                {
                    objFieldControl.Value = _FieldValues[KeyName];
                }
            }
            else if (WriteType == "CheckBox")
            {
                Control_FieldCheckBoxControl objFieldControl = (Control_FieldCheckBoxControl)this.LoadControl("/Control/FieldCheckBoxControl.ascx");
                string Name = objXmlNode.ReadAttribute("Name");
                string KeyName = objXmlNode.ReadAttribute("KeyName", Name);
                objFieldControl.ID = Name;
                panControl.Controls.Add(objFieldControl);
                objFieldControl.SetField(objXmlNode);

                if (_FieldValues != null && _FieldValues.ContainsKey(KeyName))
                {
                    objFieldControl.Value = _FieldValues[KeyName];
                }
            }


        }




    }
    protected void Page_Load(object sender, EventArgs e)
    {

        CreateFieldConfigNode();

    }
}