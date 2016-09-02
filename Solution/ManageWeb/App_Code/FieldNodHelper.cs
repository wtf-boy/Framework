using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using WTF.Framework;
/// <summary>
/// FieldNodHelper 的摘要说明
/// </summary>
public static class FieldNodHelper
{

    public static void FieldNodeBind(this  ListControl bindControl, XmlNode objXmlNode, string condition, HeaderType headerType = HeaderType.None)
    {
        string ConnectionKey = objXmlNode.ReadAttribute("ConnectionKey");
        string TableName = objXmlNode.ReadAttribute("TableName");
        string TextField = objXmlNode.ReadAttribute("TextField");
        string ValueField = objXmlNode.ReadAttribute("ValueField");
        string NodeCondition = objXmlNode.ReadAttribute("Condition");
        string Sort = objXmlNode.ReadAttribute("Sort");

        if (!string.IsNullOrWhiteSpace(condition))
        {
            if (string.IsNullOrWhiteSpace(NodeCondition))
            {
                NodeCondition = condition;
            }
            else
            {
                NodeCondition += " and " + condition;
            }
        }

        if (ConnectionKey.IsNoNullOrWhiteSpace() && TableName.IsNoNullOrWhiteSpace() && TextField.IsNoNullOrWhiteSpace() && ValueField.IsNoNullOrWhiteSpace())
        {
            bindControl.BindControl(ConnectionKey, TableName, NodeCondition, Sort, TextField, ValueField, headerType);
        }
        else
        {
            string ListValue = objXmlNode.InnerText.Replace("\r\n", "").Trim();
            if (ListValue.IsNoNullOrWhiteSpace())
            {
                bindControl.Items.Clear();
                if (headerType != HeaderType.None)
                {
                    bindControl.Items.Add(new ListItem(headerType.GetEnumDescription(), ""));
                }
                foreach (string item in ListValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string itemValue = item.Replace("\r\n", "").Trim();
                    bindControl.Items.Add(new ListItem(itemValue.Split(':', '：')[0], itemValue.Split(':', '：')[1]));

                }

            }
        }

    }
}