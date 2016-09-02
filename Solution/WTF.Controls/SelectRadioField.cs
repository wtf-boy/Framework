namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Web.UI.WebControls;

    public class SelectRadioField : WTF.Controls.TemplateField
    {
        protected override DataControlField CreateField()
        {
            return new SelectRadioField();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            cell.Attributes.Add("class", "colRadioSelect");
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            switch (cellType)
            {
                case DataControlCellType.Header:
                {
                    Literal child = new Literal {
                        Text = "单选"
                    };
                    cell.Controls.Add(child);
                    break;
                }
                case DataControlCellType.DataCell:
                {
                    RadioButton button = new RadioButton {
                        ID = "chkSelect"
                    };
                    string str = ((MyGridView) base.Control).DataKeys[rowIndex].Value.ToString();
                    cell.Attributes.Add("SelectID", str);
                    string alreadySelectedRowKeys = ((MyGridView) base.Control).AlreadySelectedRowKeys;
                    if (alreadySelectedRowKeys.IsNoNull())
                    {
                        button.Checked = alreadySelectedRowKeys.ConvertListString().IndexOf(str) != -1;
                    }
                    cell.Controls.Add(button);
                    break;
                }
            }
        }
    }
}

