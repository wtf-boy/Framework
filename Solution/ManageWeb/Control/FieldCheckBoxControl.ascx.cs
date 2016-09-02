using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
public partial class Control_FieldCheckBoxControl : System.Web.UI.UserControl
{

    public void SetField(XmlNode objXmlNode)
    {
        lblFieldTitle.Text = objXmlNode.ReadXmlAttributeString("FieldTitle");
        chkValue.Text = objXmlNode.ReadXmlAttributeString("FieldTitle");

        lblHintMessage.Text = objXmlNode.ReadAttribute("HintMessage");

    }

    public string Value
    {
        get
        {
            return chkValue.Checked.ToString();
        }
        set
        {
            chkValue.Checked = value.ToLower() == "true" || value.ToLower() == "1";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}