namespace WTF.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class NumberField : DataControlField
    {
        protected override DataControlField CreateField()
        {
            return new NumberField();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            cell.Attributes.Add("class", "colNumber");
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            int pageIndex = ((MyGridView) base.Control).PageIndex;
            int pageSize = ((MyGridView) base.Control).PageSize;
            int num3 = ((pageIndex * pageSize) + rowIndex) + 1;
            if (cellType == DataControlCellType.DataCell)
            {
                cell.Text = num3.ToString();
            }
            else if (cellType == DataControlCellType.Header)
            {
                cell.Text = "序号";
            }
            else if (cellType == DataControlCellType.Footer)
            {
                cell.Text = "合计";
            }
        }

        protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState, int rowIndex)
        {
        }
    }
}

