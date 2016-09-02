namespace WTF.Controls
{
    using WTF.DataConfig;
    using WTF.DataConfig.Entity;
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyEnumCheckBoxList : MyCheckBoxList
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
                return this.ViewState.GetString("EnumTypeCode", "");
            }
            set
            {
                this.ViewState["EnumTypeCode"] = value;
            }
        }

        [Browsable(true), Category("Seven：枚举属性"), Description("是否启动缓存")]
        public bool IsCache
        {
            get
            {
                return this.ViewState.GetBool("IsCache", true);
            }
            set
            {
                this.ViewState["IsCache"] = value;
            }
        }
    }
}

