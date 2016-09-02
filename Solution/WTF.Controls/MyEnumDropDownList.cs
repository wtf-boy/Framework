namespace WTF.Controls
{
    using WTF.DataConfig;
    using WTF.DataConfig.Entity;
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyEnumDropDownList : MyDropDownList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.EnumTypeCode.IsNoNull())
            {
                this.Items.Clear();
                if (this.IsSelectAll)
                {
                    this.Items.Insert(0, new ListItem(this.DefaultText, ""));
                }
                ParameterRule rule = new ParameterRule();
                foreach (Sys_Parameter parameter in rule.GetParameter(this.EnumTypeCode, this.IsCache))
                {
                    this.Items.Add(new ListItem(parameter.ParameterName, parameter.ParameterCodeID.ToString()));
                }
            }
            else if (this.IsSelectAll)
            {
                this.Items.Insert(0, new ListItem(this.DefaultText, ""));
            }
        }

        [Category("Seven：全部默认标题"), Description("枚举类型"), Browsable(true)]
        public string DefaultText
        {
            get
            {
                return this.ViewState.GetString("DefaultText", "--全部--");
            }
            set
            {
                this.ViewState["DefaultText"] = value;
            }
        }

        [Category("Seven：枚举属性"), Browsable(true), Description("枚举类型")]
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

        [Category("Seven：枚举属性"), Browsable(true), Description("是否启动缓存")]
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

        [Browsable(true), Category("Seven：枚举属性"), Description("是否添加全部选择")]
        public bool IsSelectAll
        {
            get
            {
                return this.ViewState.GetBool("IsSelectAll", false);
            }
            set
            {
                this.ViewState["IsSelectAll"] = value;
            }
        }
    }
}

