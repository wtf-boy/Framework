namespace WTF.Framework
{
    using System;
    using System.Text;

    public class ConditionWhere
    {
        private StringBuilder objWhere;

        public ConditionWhere()
        {
            this.objWhere = new StringBuilder();
        }

        public ConditionWhere(string condition)
        {
            this.objWhere = new StringBuilder();
            this.objWhere.Append(condition);
        }

        public void AddCondition(string condition)
        {
            if (this.objWhere.ToString().IsNoNull())
            {
                this.objWhere.Append(" AND ");
            }
            this.objWhere.Append(condition);
        }

        public void AddCondition(bool isCondition, string condition)
        {
            if (isCondition)
            {
                this.AddCondition(condition);
            }
        }

        public void AddCondition(string formantCondition, params object[] objects)
        {
            string condition = string.Format(formantCondition, objects);
            this.AddCondition(condition);
        }

        public void AddCondition(string formantCondition, string value)
        {
            this.AddCondition(value.IsNoNull(), formantCondition, new object[] { value });
        }

        public void AddCondition(bool isCondition, string formantCondition, params object[] objects)
        {
            if (isCondition)
            {
                this.AddCondition(formantCondition, objects);
            }
        }

        public override string ToString()
        {
            return this.objWhere.ToString();
        }
    }
}

