namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Web.UI.WebControls;

    public class SelectField : WTF.Controls.TemplateField
    {
        protected override DataControlField CreateField()
        {
            return new SelectField();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            CheckBox box;
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.Header)
            {
                cell.Attributes.Add("class", "colSelect");
                box = new CheckBox {
                    ID = "chkSelect"
                };
                cell.Controls.Add(box);
            }
            else if (cellType == DataControlCellType.DataCell)
            {
                cell.Attributes.Add("class", "colSelect");
                box = new CheckBox {
                    ID = "chkSelect"
                };
                cell.Controls.Add(box);
                string str = ((MyGridView) base.Control).DataKeys[rowIndex].Value.ToString();
                cell.Attributes.Add("SelectID", str);
                string alreadySelectedRowKeys = ((MyGridView) base.Control).AlreadySelectedRowKeys;
                if (alreadySelectedRowKeys.IsNoNull())
                {
                    box.Checked = alreadySelectedRowKeys.ConvertListString().IndexOf(str) != -1;
                }
            }
            else if (cellType == DataControlCellType.Footer)
            {
                cell.Text = "合计";
            }
        }
    }
}

