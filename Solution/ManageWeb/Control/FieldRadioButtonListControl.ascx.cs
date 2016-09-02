using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
public partial class Control_FieldRadioButtonListControl : System.Web.UI.UserControl
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
        radList.ErrorMessage = objXmlNode.ReadAttribute("ErrorMessage");
        radList.RepeatColumns = objXmlNode.ReadAttributeInt("RepeatColumns", 5);
        radList.CheckValueEmpty = objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false);
        if (objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false))
        {

            lblMustWrite.Text = " <span class=\"txtNoNull\">*</span>";

        }
        radList.FieldNodeBind(objXmlNode, Condition, HeaderType.None);
    }

    public string Value
    {
        get
        {
            return radList.SelectedValue;
        }
        set
        {
            radList.SelectedValue = value;
        }
    }
}