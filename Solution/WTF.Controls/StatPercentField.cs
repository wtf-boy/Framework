namespace WTF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class StatPercentField : DataControlField
    {
        private bool IsBindData = false;
        private List<DataControlFieldCell> objDataControlFieldCellList = new List<DataControlFieldCell>();

        private void cell_DataBinding(object sender, EventArgs e)
        {
            DataControlFieldCell item = (DataControlFieldCell) sender;
            this.objDataControlFieldCellList.Add(item);
            GridViewRow namingContainer = (GridViewRow) item.NamingContainer;
            item.Text = Convert.ToDouble(DataBinder.Eval(namingContainer.DataItem, this.StatField)).ToString();
        }

        protected override DataControlField CreateField()
        {
            return new StatPercentField();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (!string.IsNullOrWhiteSpace(this.StatField))
            {
                if (cellType == DataControlCellType.DataCell)
                {
                    cell.DataBinding += new EventHandler(this.cell_DataBinding);
                }
                else if (cellType == DataControlCellType.Header)
                {
                    cell.Text = this.HeaderText;
                }
                else if (cellType == DataControlCellType.Footer)
                {
                    double num = 0.0;
                    foreach (DataControlFieldCell cell2 in this.objDataControlFieldCellList)
                    {
                        num += Convert.ToDouble(cell2.Text);
                    }
                    if (num != 0.0)
                    {
                        foreach (DataControlFieldCell cell2 in this.objDataControlFieldCellList)
                        {
                            cell2.Text = string.Format("{0:F}", (Convert.ToDouble(cell2.Text) / num) * 100.0) + "%";
                        }
                    }
                }
            }
        }

        public string StatField
        {
            get
            {
                if (base.ViewState["StatField"] == null)
                {
                    return string.Empty;
                }
                return (string) base.ViewState["StatField"];
            }
            set
            {
                base.ViewState["StatField"] = value;
            }
        }
    }
}

