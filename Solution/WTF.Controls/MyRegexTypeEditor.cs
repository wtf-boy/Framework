namespace WTF.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class MyRegexTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value == null)
            {
                value = "";
            }
            if (value.GetType() == typeof(string))
            {
                this.edSvc = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
                if ((this.edSvc != null) && (context.Instance is MyTextBox))
                {
                    RegexTypeEditor dialog = new RegexTypeEditor(value.ToString());
                    if (this.edSvc.ShowDialog(dialog) == DialogResult.OK)
                    {
                        return dialog.RegexExpression;
                    }
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}

