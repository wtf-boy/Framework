namespace WTF.Pages
{
    using WTF.Controls;
    using WTF.Framework;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PageBaseESearch<T> : PageBase where T: class, new()
    {
        public virtual PageInfo<T> BindDataing(PageInfo<T> bindData)
        {
            return bindData;
        }

        //public void CurrentBindData(MyGridView objGridView, PageBindData<T> objPageBindData, string fields = "*")
        //{
        //    PageInfo<T> info = this.BindDataing(objPageBindData(this.SearchConditionESearch(), this.SearchSortExpression(), this.PageSize, this.PageIndex, fields));
        //    objGridView.DataSource = info.Data;
        //    objGridView.RecordCount = info.RecordCount;
        //    objGridView.PageSize = this.PageSize;
        //    objGridView.PageIndex = this.PageIndex;
        //    objGridView.DataBind();
        //}

        //public void CurrentBindData(MyGridView objGridView, string sql, PageBindSqlData<T> objPageBindData)
        //{
        //    PageInfo<T> info = this.BindDataing(objPageBindData(sql, this.PageSize, this.PageIndex));
        //    objGridView.DataSource = info.Data;
        //    objGridView.RecordCount = info.RecordCount;
        //    objGridView.PageSize = this.PageSize;
        //    objGridView.PageIndex = this.PageIndex;
        //    objGridView.DataBind();
        //}

        public string SearchConditionESearch()
        {
            string condition = this.Condition;
            string str2 = base.QueryModel.ToConditionESearch();
            if (condition.IsNoNull())
            {
                return (condition + (str2.IsNull() ? "" : (" AND " + str2)));
            }
            return (str2.IsNull() ? "" : str2);
        }

        public override string SearchSortExpression()
        {
            return base.SearchSortExpression().Replace("it.", "");
        }

        public delegate PageInfo<T> PageBindData(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*");

        public delegate PageInfo<T> PageBindSqlData(string sql, int pageSize, int pageIndex);

    }
}

