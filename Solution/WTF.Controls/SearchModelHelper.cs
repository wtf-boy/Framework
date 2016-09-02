namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Web.UI;

    public class SearchModelHelper
    {
        public static void CreateControl(QueryModel objQueryModel, ControlCollection objControls)
        {
            foreach (Control control in objControls)
            {
                ConditionItem item;
                if (control is QueryTextBox)
                {
                    QueryTextBox box = (QueryTextBox) control;
                    if (box.Text.IsNoNull())
                    {
                        item = new ConditionItem {
                            Field = box.SearchQueryField,
                            Value = box.TextSqlFilter,
                            Prefix = box.QueryPrefix,
                            QueryUnite = box.QueryUnite,
                            Method = box.QueryMethod,
                            QueryDataType = box.QueryDataType,
                            DefaultQueryDataType = QueryDataType.String
                        };
                        objQueryModel.Items.Add(item);
                    }
                }
                else if (control is QueryHiddenField)
                {
                    QueryHiddenField field = (QueryHiddenField) control;
                    if (field.Value.IsNoNull())
                    {
                        item = new ConditionItem {
                            Field = field.SearchQueryField,
                            Value = field.Value,
                            Prefix = field.QueryPrefix,
                            QueryUnite = field.QueryUnite,
                            Method = field.QueryMethod,
                            QueryDataType = field.QueryDataType,
                            DefaultQueryDataType = QueryDataType.Int
                        };
                        objQueryModel.Items.Add(item);
                    }
                }
                else if (control is QueryCheckBoxList)
                {
                    QueryCheckBoxList list = (QueryCheckBoxList) control;
                    if (list.SelectValueString.IsNoNull())
                    {
                        item = new ConditionItem {
                            Field = list.SearchQueryField,
                            Value = list.SelectValueString,
                            Prefix = list.QueryPrefix,
                            QueryUnite = list.QueryUnite,
                            Method = list.QueryMethod,
                            QueryDataType = list.QueryDataType,
                            DefaultQueryDataType = QueryDataType.Int
                        };
                        objQueryModel.Items.Add(item);
                    }
                }
                else if (control is QueryDropDownList)
                {
                    QueryDropDownList list2 = (QueryDropDownList) control;
                    if (list2.SelectedValue.IsNoNull())
                    {
                        item = new ConditionItem {
                            Field = list2.SearchQueryField,
                            Value = list2.SelectedValue,
                            Prefix = list2.QueryPrefix,
                            QueryUnite = list2.QueryUnite,
                            Method = list2.QueryMethod,
                            QueryDataType = list2.QueryDataType,
                            DefaultQueryDataType = QueryDataType.Int
                        };
                        objQueryModel.Items.Add(item);
                    }
                }
                else if (control is QueryRadioButtonList)
                {
                    QueryRadioButtonList list3 = (QueryRadioButtonList) control;
                    if (list3.SelectedValue.IsNoNull())
                    {
                        item = new ConditionItem {
                            Field = list3.ID,
                            Value = list3.SelectedValue,
                            Prefix = list3.QueryPrefix,
                            QueryUnite = list3.QueryUnite,
                            Method = list3.QueryMethod,
                            QueryDataType = list3.QueryDataType,
                            DefaultQueryDataType = QueryDataType.Int
                        };
                        objQueryModel.Items.Add(item);
                    }
                }
                if (control.HasControls())
                {
                    CreateControl(objQueryModel, control.Controls);
                }
            }
        }

        public static QueryModel CreateQueryModel()
        {
            QueryModel objQueryModel = new QueryModel();
            CreateControl(objQueryModel, SysVariable.CurrentPage.Controls);
            return objQueryModel;
        }
    }
}

