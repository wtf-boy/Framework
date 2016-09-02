namespace WTF.Pages
{
    using WTF.Controls;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.UI.WebControls;

    public class UserControlBase<T> : UserControlBase where T: class
    {
        public void CurrentBindData(MyGridView objGridView, IQueryable<T> objectQuery)
        {
            Expression<Func<T, bool>> condition = this.Condition;
            int num = 0;
            if (condition != null)
            {
                objectQuery = objectQuery.Where<T>(condition);
            }
            num = objectQuery.Count<T>();
            objGridView.RecordCount = num;
            objGridView.PageSize = base.PageSize;
            objGridView.PageIndex = base.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData(Repeater objRepeater, IQueryable<T> objectQuery)
        {
            int num = 0;
            Expression<Func<T, bool>> condition = this.Condition;
            if (condition != null)
            {
                objectQuery = objectQuery.Where<T>(condition);
            }
            num = objectQuery.Count<T>();
            base.RecordCount = num;
            objRepeater.DataBind();
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

