namespace WTF.Framework
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public class ConditionItem
    {
        private string _Prefix;
        private WTF.Framework.QueryDataType _QueryDataType;

        public ConditionItem()
        {
            this._QueryDataType = WTF.Framework.QueryDataType.ObjectT;
            this._Prefix = "it";
            this.QueryUnite = WTF.Framework.QueryUnite.AND;
        }

        public ConditionItem(string field, QueryMethod method, object val)
        {
            this._QueryDataType = WTF.Framework.QueryDataType.ObjectT;
            this._Prefix = "it";
            this.Field = field;
            this.Method = method;
            this.Value = val;
        }

        private string MethodToStringESearchSql()
        {
            string str = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Field + " like '{Value}%'");

                    case QueryMethod.EndsWith:
                        return (this.Field + " like '%{Value}'");

                    case QueryMethod.Contains:
                        return (this.Field + " like '%{Value}%'");

                    case QueryMethod.Like:
                        return (this.Field + " like '%{Value}%'");
                }
                return str;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                throw new ArgumentNullException("查询字段" + this.Field + "请设置字段查询类型");
            }
            if (((this.QueryDataType == WTF.Framework.QueryDataType.Guid) || (this.QueryDataType == WTF.Framework.QueryDataType.String)) || (this.QueryDataType == WTF.Framework.QueryDataType.Date))
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Field + "={Value}");

                case QueryMethod.GreaterThan:
                    return (this.Field + ">{Value}");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Field + ">={Value}");

                case QueryMethod.LessThan:
                    return (this.Field + "<{Value}");

                case QueryMethod.LessThanOrEqual:
                    return (this.Field + "<={Value}");

                case QueryMethod.NotEqual:
                    return (this.Field + "<>{Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return str;

                case QueryMethod.StdIn:
                    return (this.Field + " in ({Value})");

                case QueryMethod.In:
                    return (this.Field + " in ({Value})");
            }
            return str;
        }

        private string MethodToStringLinq()
        {
            string str = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Prefix + "." + this.Field + " like '{Value}%'");

                    case QueryMethod.EndsWith:
                        return (this.Prefix + "." + this.Field + " like '%{Value}'");

                    case QueryMethod.Contains:
                        return (this.Prefix + "." + this.Field + " like '%{Value}%'");

                    case QueryMethod.Like:
                        return (this.Prefix + "." + this.Field + " like '%{Value}%'");
                }
                return str;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                throw new ArgumentNullException("查询字段" + this.Field + "请设置字段查询类型");
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.Guid)
            {
                this.Value = this.Value.ToString().ConvertGuidID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.String)
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.Date)
            {
                this.Value = string.Format(" cast('{0}' as System.DateTime) ", this.Value);
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Prefix + "." + this.Field + "={Value}");

                case QueryMethod.GreaterThan:
                    return (this.Prefix + "." + this.Field + ">{Value}");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Prefix + "." + this.Field + ">={Value}");

                case QueryMethod.LessThan:
                    return (this.Prefix + "." + this.Field + "<{Value}");

                case QueryMethod.LessThanOrEqual:
                    return (this.Prefix + "." + this.Field + "<={Value}");

                case QueryMethod.NotEqual:
                    return (this.Prefix + "." + this.Field + "!={Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return str;

                case QueryMethod.StdIn:
                    return (this.Prefix + "." + this.Field + " in {{Value}}");

                case QueryMethod.In:
                    return (this.Prefix + "." + this.Field + " in {{Value}}");
            }
            return str;
        }

        private string MethodToStringLinq<T>()
        {
            PropertyInfo property;
            string str = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
                {
                    Type type = typeof(T);
                    property = type.GetProperty(this.Field);
                    if (property == null)
                    {
                        throw new ArgumentNullException("数据模型找不到此" + this.Field + "字段请设置字段查询类型");
                    }
                    if (property.PropertyType != typeof(string))
                    {
                        throw new ArgumentNullException("字段" + this.Field + "不是字符串类型无法进行LIKE查询");
                    }
                }
                else if (this.QueryDataType != WTF.Framework.QueryDataType.String)
                {
                    throw new ArgumentNullException("字段" + this.Field + "不是字符串类型无法进行LIKE查询");
                }
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Prefix + "." + this.Field + " like '{Value}%'");

                    case QueryMethod.EndsWith:
                        return (this.Prefix + "." + this.Field + " like '%{Value}'");

                    case QueryMethod.Contains:
                        return (this.Prefix + "." + this.Field + " like '%{Value}%'");

                    case QueryMethod.Like:
                        return (this.Prefix + "." + this.Field + " like '%{Value}%'");
                }
                return str;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                property = typeof(T).GetProperty(this.Field);
                if (property == null)
                {
                    throw new ArgumentNullException("数据模型找不到此" + this.Field + "字段请设置字段查询类型");
                }
                if ((property.PropertyType == typeof(Guid)) || (property.PropertyType == typeof(Guid?)))
                {
                    this.Value = this.Value.ToString().ConvertGuidID();
                }
                else if (property.PropertyType == typeof(string))
                {
                    this.Value = this.Value.ToString().ConvertStringID();
                }
                else if ((property.PropertyType == typeof(DateTime)) || (property.PropertyType == typeof(DateTime?)))
                {
                    this.Value = string.Format("cast('{0}' as System.DateTime)", this.Value);
                }
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.Guid)
            {
                this.Value = this.Value.ToString().ConvertGuidID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.String)
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.Date)
            {
                this.Value = string.Format(" cast('{0}' as System.DateTime) ", this.Value);
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Prefix + "." + this.Field + "={Value}");

                case QueryMethod.GreaterThan:
                    return (this.Prefix + "." + this.Field + ">{Value}");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Prefix + "." + this.Field + ">={Value}");

                case QueryMethod.LessThan:
                    return (this.Prefix + "." + this.Field + "<{Value}");

                case QueryMethod.LessThanOrEqual:
                    return (this.Prefix + "." + this.Field + "<={Value}");

                case QueryMethod.NotEqual:
                    return (this.Prefix + "." + this.Field + "!={Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return str;

                case QueryMethod.StdIn:
                    return (this.Prefix + "." + this.Field + " in{{Value}}");

                case QueryMethod.In:
                    return (this.Prefix + "." + this.Field + " in{{Value}}");
            }
            return str;
        }

        private string MethodToStringSolrSql()
        {
            string str4;
            string source = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Field + ":*{Value}");

                    case QueryMethod.EndsWith:
                        return (this.Field + ":{Value}*");

                    case QueryMethod.Contains:
                        return (this.Field + ":{Value}");

                    case QueryMethod.Like:
                        return (this.Field + ":*{Value}*");
                }
                return source;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                throw new ArgumentNullException("查询字段" + this.Field + "请设置字段查询类型");
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.Date)
            {
                this.Value = DateTime.Parse(this.Value.ToString()).AddHours(-8.0).ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else if ((this.QueryDataType == WTF.Framework.QueryDataType.Guid) || (this.QueryDataType == WTF.Framework.QueryDataType.String))
            {
                this.Value = this.Value.ToString().ConvertIDString('"');
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Field + ":{Value}");

                case QueryMethod.GreaterThan:
                    return (this.Field + ":{{Value} TO *]");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Field + ":[{Value} TO *]");

                case QueryMethod.LessThan:
                    return (this.Field + ":[* TO {Value})");

                case QueryMethod.LessThanOrEqual:
                    return (this.Field + ":[* TO {Value}]");

                case QueryMethod.NotEqual:
                    return ("-" + this.Field + ":{Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return source;

                case QueryMethod.StdIn:
                    foreach (string str2 in this.Value.ToString().Split(new char[] { ',' }))
                    {
                        str4 = source;
                        source = str4 + " " + this.Field + ":" + str2 + " OR";
                    }
                    source = source.TrimEnd("OR");
                    return ("(" + source + ")");

                case QueryMethod.In:
                    foreach (string str2 in this.Value.ToString().Split(new char[] { ',' }))
                    {
                        str4 = source;
                        source = str4 + " " + this.Field + ":" + str2 + " OR";
                    }
                    source = source.TrimEnd("OR");
                    return ("( " + source + " )");
            }
            return source;
        }

        private string MethodToStringSql()
        {
            string str = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Field + " like '{Value}%'");

                    case QueryMethod.EndsWith:
                        return (this.Field + " like '%{Value}'");

                    case QueryMethod.Contains:
                        return (this.Field + " like '%{Value}%'");

                    case QueryMethod.Like:
                        return (this.Field + " like '%{Value}%'");
                }
                return str;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                throw new ArgumentNullException("查询字段" + this.Field + "请设置字段查询类型");
            }
            if (((this.QueryDataType == WTF.Framework.QueryDataType.Guid) || (this.QueryDataType == WTF.Framework.QueryDataType.String)) || (this.QueryDataType == WTF.Framework.QueryDataType.Date))
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Field + "={Value}");

                case QueryMethod.GreaterThan:
                    return (this.Field + ">{Value}");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Field + ">={Value}");

                case QueryMethod.LessThan:
                    return (this.Field + "<{Value}");

                case QueryMethod.LessThanOrEqual:
                    return (this.Field + "<={Value}");

                case QueryMethod.NotEqual:
                    return (this.Field + "!={Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return str;

                case QueryMethod.StdIn:
                    return (this.Field + " in ({Value})");

                case QueryMethod.In:
                    return (this.Field + " in ({Value})");
            }
            return str;
        }

        private string MethodToStringSql<T>()
        {
            PropertyInfo property;
            string str = "";
            if ((((this.Method == QueryMethod.Contains) || (this.Method == QueryMethod.EndsWith)) || (this.Method == QueryMethod.StartsWith)) || (this.Method == QueryMethod.Like))
            {
                if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
                {
                    Type type = typeof(T);
                    property = type.GetProperty(this.Field);
                    if (property == null)
                    {
                        throw new ArgumentNullException("数据模型找不到此" + this.Field + "字段请设置字段查询类型");
                    }
                    if (property.PropertyType != typeof(string))
                    {
                        throw new ArgumentNullException("字段" + this.Field + "不是字符串类型无法进行LIKE查询");
                    }
                }
                else if (this.QueryDataType != WTF.Framework.QueryDataType.String)
                {
                    throw new ArgumentNullException("字段" + this.Field + "不是字符串类型无法进行LIKE查询");
                }
                switch (this.Method)
                {
                    case QueryMethod.StartsWith:
                        return (this.Field + " like '{Value}%'");

                    case QueryMethod.EndsWith:
                        return (this.Field + " like '%{Value}'");

                    case QueryMethod.Contains:
                        return (this.Field + " like '%{Value}%'");

                    case QueryMethod.Like:
                        return (this.Field + " like '%{Value}%'");
                }
                return str;
            }
            if (this.QueryDataType == WTF.Framework.QueryDataType.ObjectT)
            {
                property = typeof(T).GetProperty(this.Field);
                if (property == null)
                {
                    throw new ArgumentNullException("数据模型找不到此" + this.Field + "字段请设置字段查询类型");
                }
                if ((property.PropertyType == typeof(Guid)) || (property.PropertyType == typeof(Guid?)))
                {
                    this.Value = this.Value.ToString().ConvertStringID();
                }
                else if (property.PropertyType == typeof(string))
                {
                    this.Value = this.Value.ToString().ConvertStringID();
                }
                else if ((property.PropertyType == typeof(DateTime)) || (property.PropertyType == typeof(DateTime?)))
                {
                    this.Value = this.Value.ToString().ConvertStringID();
                }
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.Guid)
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.String)
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.Date)
            {
                this.Value = this.Value.ToString().ConvertStringID();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.SecondStamp)
            {
                this.Value = this.Value.ToString().ConvertToSecondStamp();
            }
            else if (this.QueryDataType == WTF.Framework.QueryDataType.TimeStamp)
            {
                this.Value = this.Value.ToString().ConvertToTimeStamp();
            }
            switch (this.Method)
            {
                case QueryMethod.Equal:
                    return (this.Field + "={Value}");

                case QueryMethod.GreaterThan:
                    return (this.Field + ">{Value}");

                case QueryMethod.GreaterThanOrEqual:
                    return (this.Field + ">={Value}");

                case QueryMethod.LessThan:
                    return (this.Field + "<{Value}");

                case QueryMethod.LessThanOrEqual:
                    return (this.Field + "<={Value}");

                case QueryMethod.NotEqual:
                    return (this.Field + "!={Value}");

                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:
                case QueryMethod.Contains:
                case QueryMethod.Like:
                    return str;

                case QueryMethod.StdIn:
                    return (this.Field + " in ({Value})");

                case QueryMethod.In:
                    return (this.Field + " in ({Value})");
            }
            return str;
        }

        public string ToConditionESearch()
        {
            return (this.QueryUnite.ToString() + " " + this.MethodToStringESearchSql().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public string ToConditionLinq()
        {
            return (this.QueryUnite.ToString() + " " + this.MethodToStringLinq().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public string ToConditionLinq<T>()
        {
            return (this.QueryUnite.ToString() + " " + this.MethodToStringLinq<T>().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public string ToConditionSolrSql()
        {
            return (this.QueryUnite.ToString().ToUpper() + " " + this.MethodToStringSolrSql().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public string ToConditionSql()
        {
            return (this.QueryUnite.ToString() + " " + this.MethodToStringSql().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public string ToConditionSql<T>()
        {
            return (this.QueryUnite.ToString() + " " + this.MethodToStringSql<T>().ToString().Replace("{Value}", this.Value.ToString(), RegexOptions.IgnoreCase) + "  ");
        }

        public WTF.Framework.QueryDataType DefaultQueryDataType { get; set; }

        public string Field { get; set; }

        public QueryMethod Method { get; set; }

        public string OrGroup { get; set; }

        public string Prefix
        {
            get
            {
                return this._Prefix;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this._Prefix = value;
                }
                else
                {
                    this._Prefix = "it";
                }
            }
        }

        public WTF.Framework.QueryDataType QueryDataType
        {
            get
            {
                return this._QueryDataType;
            }
            set
            {
                this._QueryDataType = value;
            }
        }

        public WTF.Framework.QueryUnite QueryUnite { get; set; }

        public object Value { get; set; }
    }
}

