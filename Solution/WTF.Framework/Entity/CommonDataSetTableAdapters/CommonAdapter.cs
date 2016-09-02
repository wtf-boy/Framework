namespace WTF.Framework.Entity.CommonDataSetTableAdapters
{
    using WTF.Framework.Properties;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;

    [HelpKeyword("vs.data.TableAdapter"), ToolboxItem(true), DesignerCategory("code"), DataObject(true), Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class CommonAdapter : Component
    {
        private IDbCommand[] _commandCollection;

        [HelpKeyword("vs.data.TableAdapter"), DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public virtual object ExecuteNonQuery(string SqlExpression)
        {
            object obj2;
            SqlCommand command = (SqlCommand) this.CommandCollection[1];
            if (SqlExpression == null)
            {
                command.Parameters[1].Value = DBNull.Value;
            }
            else
            {
                command.Parameters[1].Value = SqlExpression;
            }
            ConnectionState state = command.Connection.State;
            if ((command.Connection.State & ConnectionState.Open) != ConnectionState.Open)
            {
                command.Connection.Open();
            }
            try
            {
                obj2 = command.ExecuteScalar();
            }
            finally
            {
                if (state == ConnectionState.Closed)
                {
                    command.Connection.Close();
                }
            }
            if ((obj2 == null) || (obj2.GetType() == typeof(DBNull)))
            {
                return null;
            }
            return obj2;
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), HelpKeyword("vs.data.TableAdapter"), DebuggerNonUserCode]
        public virtual object GetTableRecordCount(string DataTableCode, string Condition)
        {
            object obj2;
            SqlCommand command = (SqlCommand) this.CommandCollection[0];
            if (DataTableCode == null)
            {
                command.Parameters[1].Value = DBNull.Value;
            }
            else
            {
                command.Parameters[1].Value = DataTableCode;
            }
            if (Condition == null)
            {
                command.Parameters[2].Value = DBNull.Value;
            }
            else
            {
                command.Parameters[2].Value = Condition;
            }
            ConnectionState state = command.Connection.State;
            if ((command.Connection.State & ConnectionState.Open) != ConnectionState.Open)
            {
                command.Connection.Open();
            }
            try
            {
                obj2 = command.ExecuteScalar();
            }
            finally
            {
                if (state == ConnectionState.Closed)
                {
                    command.Connection.Close();
                }
            }
            if ((obj2 == null) || (obj2.GetType() == typeof(DBNull)))
            {
                return null;
            }
            return obj2;
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0"), DebuggerNonUserCode]
        private void InitCommandCollection()
        {
            this._commandCollection = new IDbCommand[2];
            this._commandCollection[0] = new SqlCommand();
            ((SqlCommand) this._commandCollection[0]).Connection = new SqlConnection(Settings.Default.SevenConnectionString);
            ((SqlCommand) this._commandCollection[0]).CommandText = "dbo.Sys_GetTableRecordCount";
            ((SqlCommand) this._commandCollection[0]).CommandType = CommandType.StoredProcedure;
            ((SqlCommand) this._commandCollection[0]).Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, 10, 0, null, DataRowVersion.Current, false, null, "", "", ""));
            ((SqlCommand) this._commandCollection[0]).Parameters.Add(new SqlParameter("@DataTableCode", SqlDbType.VarChar, 100, ParameterDirection.Input, 0, 0, null, DataRowVersion.Current, false, null, "", "", ""));
            ((SqlCommand) this._commandCollection[0]).Parameters.Add(new SqlParameter("@Condition", SqlDbType.VarChar, 0x3e8, ParameterDirection.Input, 0, 0, null, DataRowVersion.Current, false, null, "", "", ""));
            this._commandCollection[1] = new SqlCommand();
            ((SqlCommand) this._commandCollection[1]).Connection = new SqlConnection(Settings.Default.SevenConnectionString);
            ((SqlCommand) this._commandCollection[1]).CommandText = "dbo.Sys_ExecuteNonQuery";
            ((SqlCommand) this._commandCollection[1]).CommandType = CommandType.StoredProcedure;
            ((SqlCommand) this._commandCollection[1]).Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, 10, 0, null, DataRowVersion.Current, false, null, "", "", ""));
            ((SqlCommand) this._commandCollection[1]).Parameters.Add(new SqlParameter("@SqlExpression", SqlDbType.VarChar, 0xbb8, ParameterDirection.Input, 0, 0, null, DataRowVersion.Current, false, null, "", "", ""));
        }

        [DebuggerNonUserCode, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected IDbCommand[] CommandCollection
        {
            get
            {
                if (this._commandCollection == null)
                {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
    }
}

