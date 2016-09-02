namespace WTF.Controls
{
    using WTF.DataConfig;
    using WTF.DataConfig.Entity;
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyEnumRadioButtonList : MyRadioButtonList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.EnumTypeCode.IsNoNull())
            {
                List<Sys_Parameter> list = new ParameterRule().GetParameter(this.EnumTypeCode, this.IsCache);
                foreach (Sys_Parameter parameter in list)
                {
                    this.Items.Add(new ListItem(parameter.ParameterName, parameter.ParameterCodeID.ToString()));
                }
            }
        }

        [Description("枚举类型"), Browsable(true), Category("Seven：枚举属性")]
        public string EnumTypeCode
        {
            get
            {
                if (this.ViewState["EnumTypeCode"] != null)
                {
                    return (string) this.ViewState["EnumTypeCode"];
                }
                return "";
            }
            set
            {
                this.ViewState["EnumTypeCode"] = value;
            }
        }

        [Description("是否启动缓存"), Browsable(true), Category("Seven：枚举属性")]
        public bool IsCache
        {
            get
            {
                return ((this.ViewState["IsCache"] != null) && ((bool) this.ViewState["IsCache"]));
            }
            set
            {
                this.ViewState["IsCache"] = value;
            }
        }
    }
}

