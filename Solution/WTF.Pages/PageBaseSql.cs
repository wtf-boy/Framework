namespace WTF.Pages
{
    using WTF.Controls;
    using WTF.Framework;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PageBaseSql : PageBase
    {
        public virtual DataSet BindDataing(DataSet bindData)
        {
            return bindData;
        }

        public void CurrentBindData(MyGridView objGridView, PageBindData objPageBindData, string fields = "*")
        {
            int recordCount = 0;
            objGridView.DataSource = this.BindDataing(objPageBindData(this.SearchConditionSql(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields));
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, PageBindData objPageBindData, string fields = "*") where T: class
        {
            int recordCount = 0;
            objGridView.DataSource = this.BindDataing(objPageBindData(this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields));
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData(MyGridView objGridView, PageSolrBindData objPageBindData, string fields = "*")
        {
            long recordCount = 0L;
            objGridView.DataSource = this.BindDataing(objPageBindData(fields, this.SearchConditionSolrSql(), "", this.PageSize, this.PageIndex, this.SearchSolrSortExpression(), out recordCount, ""));
            objGridView.RecordCount = int.Parse(recordCount.ToString());
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData(MyGridView objGridView, PageBindData objPageBindData, Action<DataSet> OnDataBinding, string fields = "*")
        {
            int recordCount = 0;
            DataSet set = objPageBindData(this.SearchConditionSql(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields);
            objGridView.DataSource = set;
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            if (OnDataBinding != null)
            {
                OnDataBinding(set);
            }
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, PageBindData objPageBindData, Action<DataSet> OnDataBinding, string fields = "*") where T: class
        {
            int recordCount = 0;
            DataSet set = objPageBindData(this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields);
            objGridView.DataSource = set;
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            if (OnDataBinding != null)
            {
                OnDataBinding(set);
            }
            objGridView.DataBind();
        }

        public void CurrentBindData(MyGridView objGridView, PageSolrBindData objPageBindData, Action<DataSet> OnDataBinding, string fields = "*")
        {
            long recordCount = 0L;
            DataSet set = objPageBindData(fields, this.SearchConditionSolrSql(), "", this.PageSize, this.PageIndex, this.SearchSolrSortExpression(), out recordCount, "");
            objGridView.DataSource = set;
            objGridView.RecordCount = int.Parse(recordCount.ToString());
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            if (OnDataBinding != null)
            {
                OnDataBinding(set);
            }
            objGridView.DataBind();
        }

        public void CurrentBindData(MyGridView objGridView, string tableNameOrCommandText, PageBindTableData objPageBindData, string fields = "*")
        {
            int recordCount = 0;
            objGridView.DataSource = this.BindDataing(objPageBindData(tableNameOrCommandText, this.SearchConditionSql(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields));
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, string tableNameOrCommandText, PageBindTableData objPageBindData, string fields = "*") where T: class
        {
            int recordCount = 0;
            objGridView.DataSource = this.BindDataing(objPageBindData(tableNameOrCommandText, this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields));
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData(MyGridView objGridView, string tableNameOrCommandText, PageBindTableData objPageBindData, Action<DataSet> OnDataBinding, string fields = "*")
        {
            int recordCount = 0;
            DataSet set = objPageBindData(tableNameOrCommandText, this.SearchConditionSql(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields);
            objGridView.DataSource = set;
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            if (OnDataBinding != null)
            {
                OnDataBinding(set);
            }
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, string tableNameOrCommandText, PageBindTableData objPageBindData, Action<DataSet> OnDataBinding, string fields = "*") where T: class
        {
            int recordCount = 0;
            DataSet set = objPageBindData(tableNameOrCommandText, this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount, fields);
            objGridView.DataSource = set;
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            if (OnDataBinding != null)
            {
                OnDataBinding(set);
            }
            objGridView.DataBind();
        }

        public override string SearchCondition<T>()
        {
            string condition = this.Condition;
            string str2 = base.QueryModel.ToConditionSql<T>();
            string str3 = this.DataConditon(this.ModuleTypeID, this.PowerPageCode);
            if (str3.IsNoNull())
            {
                str3 = str3.Replace("{", "(").Replace("}", ")").Replace("Guid", "");
            }
            if (condition.IsNoNull())
            {
                condition = condition + (str2.IsNull() ? "" : (" and " + str2));
            }
            else
            {
                condition = str2.IsNull() ? "" : str2;
            }
            if (condition.IsNoNull())
            {
                return (condition + (str3.IsNull() ? "" : (" and " + str3)));
            }
            return (str3.IsNull() ? "" : str3);
        }

        public string SearchConditionSolrSql()
        {
            string conditionSolr = this.ConditionSolr;
            string str2 = base.QueryModel.ToConditionSolrSql();
            if (conditionSolr.IsNoNull())
            {
                return (conditionSolr + (str2.IsNull() ? "" : (" AND " + str2)));
            }
            return (str2.IsNull() ? "" : str2);
        }

        public string SearchConditionSql()
        {
            string condition = this.Condition;
            string str2 = base.QueryModel.ToConditionSql();
            string str3 = this.DataConditon(this.ModuleTypeID, this.PowerPageCode);
            if (str3.IsNoNull())
            {
                str3 = str3.Replace("{", "(").Replace("}", ")").Replace("Guid", "");
            }
            if (condition.IsNoNull())
            {
                condition = condition + (str2.IsNull() ? "" : (" and " + str2));
            }
            else
            {
                condition = str2.IsNull() ? "" : str2;
            }
            if (condition.IsNoNull())
            {
                return (condition + (str3.IsNull() ? "" : (" and " + str3)));
            }
            return (str3.IsNull() ? "" : str3);
        }

        public string SearchSolrSortExpression()
        {
            string str = base.SearchSortExpression().Replace("it.", "");
            return (str.IsNull() ? "score desc" : str);
        }

        public override string SearchSortExpression()
        {
            return base.SearchSortExpression().Replace("it.", "");
        }

        public delegate DataSet PageBindData(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*");

        public delegate DataSet PageBindTableData(string tableNameOrcommandText, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*");

        public delegate DataSet PageSolrBindData(string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "");
    }
}

