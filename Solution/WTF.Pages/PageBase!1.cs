namespace WTF.Pages
{
    using WTF.Controls;
    using WTF.Framework;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class PageBase<T> : PageBase where T: class
    {
        public void CurrentBindData(MyGridView objGridView, IQueryable<T> objectQuery)
        {
            Expression<Func<T, bool>> condition = this.Condition;
            Expression<Func<T, bool>> predicate = base.QueryModel.ToConditioExpressionLinq<T>();
            int recordCount = 0;
            if (condition != null)
            {
                objectQuery = objectQuery.Where<T>(condition);
            }
            if (predicate != null)
            {
                objectQuery = objectQuery.Where<T>(predicate);
            }
            objGridView.DataSource = objectQuery.GetPage<T>(this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public override string SearchSortExpression()
        {
            return base.SearchSortExpression().Replace("it.", "");
        }

        public virtual Expression<Func<T, bool>> Condition
        {
            get
            {
                return null;
            }
        }
    }
}

