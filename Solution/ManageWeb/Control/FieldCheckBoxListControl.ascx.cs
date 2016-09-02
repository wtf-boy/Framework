using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
public partial class Control_FieldCheckBoxListControl : System.Web.UI.UserControl
{


    public string Condition
    {
        get
        {
            return ViewState.GetString("Condition");
        }
        set
        {
            ViewState["Condition"] = value;
        }
    }
    public void SetField(XmlNode objXmlNode)
    {

        lblFieldTitle.Text = objXmlNode.ReadXmlAttributeString("FieldTitle");
        lblHintMessage.Text = objXmlNode.ReadAttribute("HintMessage");
        chkboxValue.ErrorMessage = objXmlNode.ReadAttribute("ErrorMessage");
        chkboxValue.RepeatColumns = objXmlNode.ReadAttributeInt("RepeatColumns", 5);
        chkboxValue.CheckValueEmpty = objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false);
        if (objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false))
        {
            lblMustWrite.Text = " <span class=\"txtNoNull\">*</span>";

        }
        chkboxValue.FieldNodeBind(objXmlNode, Condition, HeaderType.None);
    }

    public string Value
    {
        get
        {
            return chkboxValue.SelectValueString;
        }
        set
        {
            chkboxValue.SetSelectValue(value);
        }
    }
}