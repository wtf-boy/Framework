using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
public partial class Control_FieldDropDownControl : System.Web.UI.UserControl
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
        dropValue.HintMessage = objXmlNode.ReadAttribute("HintMessage");
        lblHintMessage.Text = objXmlNode.ReadAttribute("HintMessage");
        dropValue.ErrorMessage = objXmlNode.ReadAttribute("ErrorMessage");
        dropValue.CheckValueEmpty = objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false);
        if (objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false))
        {
            lblMustWrite.Text = " <span class=\"txtNoNull\">*</span>";

        }
        dropValue.FieldNodeBind(objXmlNode, Condition, HeaderType.Select);
    }

    public string Value
    {
        get
        {
            return dropValue.SelectedValue;
        }
        set
        {
            dropValue.SetSelectValue(value);
        }
    }
}