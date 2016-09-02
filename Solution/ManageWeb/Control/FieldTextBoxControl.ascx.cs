using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
public partial class Control_FieldTextBoxControl : System.Web.UI.UserControl
{

    public void SetField(XmlNode objXmlNode)
    {
        lblFieldTitle.Text = objXmlNode.ReadXmlAttributeString("FieldTitle");
        txtValue.Text = objXmlNode.InnerText;
        txtValue.ValidationExpression = objXmlNode.ReadAttribute("ValidationExpression");
        txtValue.HintMessage = objXmlNode.ReadAttribute("HintMessage");
        lblHintMessage.Text = objXmlNode.ReadAttribute("HintMessage");
        txtValue.ErrorMessage = objXmlNode.ReadAttribute("ErrorMessage");
        txtValue.Text = objXmlNode.ReadAttribute("Value");
        int width = objXmlNode.ReadAttributeInt("Width", 0);
        if (width > 0)
        {
            txtValue.Width = width;
        }

        txtValue.CheckValueEmpty = objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false);
        if (objXmlNode.ReadAttributeBoolean("CheckValueEmpty", false))
        {

            lblMustWrite.Text = " <span class=\"txtNoNull\">*</span>";

        }
    }

    public string Value
    {
        get
        {
            return txtValue.Text;
        }
        set
        {
            txtValue.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}