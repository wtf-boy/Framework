using EasyNet.Solr;
using EasyNet.Solr.Commons;
using EasyNet.Solr.Impl;
using MySql.Data.MySqlClient;
using WTF.DAL;
using WTF.Framework;
using WTF.Logging;
using WTF.Solr.Deserializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WTF.Solr
{
	public class SolrUpdate
	{
		private Logger _Logger = new Logger("Application");

		public string _ServerUrl = "";

		protected string _TableName = "";

		public Logger Log
		{
			get
			{
				return this._Logger;
			}
		}

		public bool IsRecordSolrSql
		{
			get
			{
				return LogSectionHelper.IsRecordSolrSql;
			}
		}

		protected string SolrServerUrl
		{
			get
			{
				string text = "";
				string[] array = this._ServerUrl.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					text = text + string.Format("{0}/{1}", text2.TrimEnd(new char[]
					{
						'/'
					}), this._TableName) + ",";
				}
				return text.Trim(new char[]
				{
					','
				});
			}
		}

		public string TableName
		{
			get
			{
				return this._TableName;
			}
			set
			{
				this._TableName = value;
			}
		}

		public SolrUpdate(string tableName, string serverUrlOrServerKey = "")
		{
			if (string.IsNullOrEmpty(tableName))
			{
				throw new ArgumentNullException("tableName参数不能为空");
			}
			this._TableName = tableName;
			if (!string.IsNullOrWhiteSpace(serverUrlOrServerKey))
			{
				if (serverUrlOrServerKey.ToLower().Contains("http"))
				{
					this._ServerUrl = serverUrlOrServerKey;
				}
				else
				{
					this._ServerUrl = ConfigHelper.GetValue(serverUrlOrServerKey);
				}
			}
			else
			{
				this._ServerUrl = ConfigHelper.GetValue("WTF.Solr.ServerUrl");
			}
		}

		protected ISolrUpdateOperations<NamedList> GetUpdateOperations()
		{
			IUpdateParametersConvert<NamedList> updateParametersConvert = new BinaryUpdateParametersConvert();
			ISolrUpdateConnection<NamedList, NamedList> connection = new SolrUpdateConnection<NamedList, NamedList>
			{
				ServerUrl = this.SolrServerUrl,
				ContentType = "application/javabin"
			};
			return new SolrUpdateOperations<NamedList, NamedList>(connection, updateParametersConvert);
		}

		protected virtual ResponseHeader Update(UpdateOptions objUpdateOptions)
		{
			ISolrResponseParser<NamedList, ResponseHeader> solrResponseParser = new BinaryResponseHeaderParser();
			NamedList result = this.GetUpdateOperations().Update("/update", objUpdateOptions);
			return solrResponseParser.Parse(result);
		}

		private object GetConvertValue(Type objType, object value)
		{
			object result;
			if (objType == typeof(uint))
			{
				result = Convert.ToInt32(value);
			}
			else if (objType == typeof(ushort))
			{
				result = Convert.ToInt32(value);
			}
			else if (objType == typeof(ulong))
			{
				result = Convert.ToInt64(value);
			}
			else if (objType == typeof(ulong))
			{
				result = Convert.ToInt64(value);
			}
			else if (objType == typeof(Guid))
			{
				result = value.ToString();
			}
			else
			{
				result = value;
			}
			return result;
		}

		private object GetDefaultValue(Type objType)
		{
			object result;
			if (objType == typeof(string))
			{
				result = string.Empty;
			}
			else if (objType == typeof(int))
			{
				result = 0;
			}
			else if (objType == typeof(DateTime))
			{
				result = DateTime.Now;
			}
			else if (objType == typeof(double))
			{
				result = 0.0;
			}
			else if (objType == typeof(bool))
			{
				result = false;
			}
			else if (objType == typeof(decimal))
			{
				result = 0;
			}
			else if (objType == typeof(float))
			{
				result = 0.0;
			}
			else if (objType == typeof(long))
			{
				result = 0;
			}
			else if (objType == typeof(short))
			{
				result = 0;
			}
			else if (objType == typeof(uint))
			{
				result = 0;
			}
			else if (objType == typeof(ushort))
			{
				result = 0;
			}
			else if (objType == typeof(ulong))
			{
				result = 0;
			}
			else if (objType == typeof(ulong))
			{
				result = 0;
			}
			else if (objType == typeof(byte))
			{
				result = 0;
			}
			else if (objType == typeof(char))
			{
				result = string.Empty;
			}
			else
			{
				result = "";
			}
			return result;
		}

		public IList<int> Update(DataTable objUpdateTable)
		{
			IList<int> list = new List<int>();
			List<SolrInputDocument> list2 = new List<SolrInputDocument>();
			foreach (DataRow dataRow in objUpdateTable.Rows)
			{
				SolrInputDocument solrInputDocument = new SolrInputDocument();
				foreach (DataColumn dataColumn in objUpdateTable.Columns)
				{
					if (dataRow[dataColumn.ColumnName] != DBNull.Value)
					{
						solrInputDocument.Add(dataColumn.ColumnName, new SolrInputField(dataColumn.ColumnName, this.GetConvertValue(dataColumn.DataType, dataRow[dataColumn.ColumnName])));
					}
					else
					{
						solrInputDocument.Add(dataColumn.ColumnName, new SolrInputField(dataColumn.ColumnName, this.GetDefaultValue(dataColumn.DataType)));
					}
				}
				list2.Add(solrInputDocument);
			}
			IList<int> result;
			if (list2.Count <= 0)
			{
				result = list;
			}
			else
			{
				CommitOptions value = default(CommitOptions);
				IObjectSerializer<SolrInputDocument> objectSerializer = new ObjectSerializerTable();
				ResponseHeader responseHeader = this.Update(new UpdateOptions
				{
					Docs = objectSerializer.Serialize(list2),
					CommitOptions = new CommitOptions?(value)
				});
				if (responseHeader != null)
				{
					list.Add(responseHeader.Status);
					list.Add(responseHeader.QTime);
					ConsoleHelper.WriteLine(string.Concat(new object[]
					{
						"更新Solr:",
						this.SolrServerUrl,
						"成功,状态：",
						responseHeader.Status,
						"执行时间:",
						responseHeader.QTime
					}), ConsoleColor.Yellow, "");
				}
				result = list;
			}
			return result;
		}

		public IList<int> Update<T>(IEnumerable<T> idList, string sourceTable, string primaryKey, string foreignkey, string connectionKeyOrConnectionString, string fields = "*")
		{
			IList<int> list = new List<int>();
			IList<int> result;
			if (idList == null || idList.Count<T>() == 0)
			{
				result = list;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(fields))
				{
					fields = "*";
				}
				DalBase dalBase = new DalBase(connectionKeyOrConnectionString);
				string str = string.IsNullOrWhiteSpace(foreignkey) ? primaryKey : foreignkey;
				string text;
				if (typeof(T) == typeof(string))
				{
					text = str + " in (" + StringHelper.ConvertStringID<T>(idList) + ")";
				}
				else
				{
					text = str + " in (" + StringHelper.ConvertListToString<T>(idList) + ")";
				}
				ConsoleHelper.WriteLine("进入更新数据条件:" + text, "");
				if (string.IsNullOrWhiteSpace(foreignkey))
				{
					if (string.IsNullOrWhiteSpace(sourceTable))
					{
						ConsoleHelper.WriteLine("进入主键Sql读取数据更新", "");
						DataTable dataTable = dalBase.ExecuteDataSet(string.Format("SELECT * FROM ({0}) as CommandTextTable where {1}", fields, text), new MySqlParameter[0]).Tables[0];
						result = this.Update(dataTable);
					}
					else
					{
						ConsoleHelper.WriteLine("进入主键源表读取数据更新", "");
						DataTable dataTable = dalBase.ExecuteDataSet(sourceTable, text, "", fields).Tables[0];
						result = this.Update(dataTable);
					}
				}
				else
				{
					int num = 0;
					if (string.IsNullOrWhiteSpace(sourceTable))
					{
						while (true)
						{
							ConsoleHelper.WriteLine("进入Sql外键读取数据第" + num + "数据", "");
							DataTable dataTable = dalBase.ExecutePageCommandLimit(fields, text, primaryKey + " asc", 20, num, "*").Tables[0];
							if (dataTable.Rows.Count <= 0)
							{
								break;
							}
							this.Update(dataTable);
							num++;
						}
						ConsoleHelper.WriteLine("进入Sql外键读取数据处理完成", "");
					}
					else
					{
						while (true)
						{
							ConsoleHelper.WriteLine("进入源表外键读取数据第" + num + "数据", "");
							DataTable dataTable = dalBase.ExecutePageLimit(sourceTable, text, primaryKey + " asc", 20, num, fields).Tables[0];
							if (dataTable.Rows.Count <= 0)
							{
								break;
							}
							this.Update(dataTable);
							num++;
						}
						ConsoleHelper.WriteLine("进入源表外键读取数据处理完成", "");
					}
					result = list;
				}
			}
			return result;
		}

		public IList<int> Delete(string idList)
		{
			IList<int> list = new List<int>();
			IList<int> result;
			if (string.IsNullOrWhiteSpace(idList))
			{
				result = list;
			}
			else
			{
				result = this.Delete(StringHelper.ConvertListString(idList));
			}
			return result;
		}

		public IList<int> Delete(IEnumerable<string> idList)
		{
			IList<int> list = new List<int>();
			IList<int> result;
			if (idList == null && idList.Count<string>() == 0)
			{
				result = list;
			}
			else
			{
				CommitOptions value = default(CommitOptions);
				ResponseHeader responseHeader = this.Update(new UpdateOptions
				{
					DelById = idList.ToList<string>(),
					CommitOptions = new CommitOptions?(value)
				});
				if (responseHeader != null)
				{
					list.Add(responseHeader.Status);
					list.Add(responseHeader.QTime);
					ConsoleHelper.WriteLine(string.Concat(new object[]
					{
						"删除Solr:",
						this.SolrServerUrl,
						"成功,状态：",
						responseHeader.Status,
						"执行时间:",
						responseHeader.QTime
					}), ConsoleColor.Yellow, "");
				}
				result = list;
			}
			return result;
		}

		public IList<int> UpdateDelete(IEnumerable<string> idList, string sourceTable, string primaryKey, string foreignkey, string connectionKeyOrConnectionString, string fields = "*")
		{
			IList<int> list = new List<int>();
			IList<int> result;
			if (idList == null && idList.Count<string>() == 0)
			{
				result = list;
			}
			else if (string.IsNullOrWhiteSpace(foreignkey))
			{
				ConsoleHelper.WriteLine("进入主键源表读取数据删除", "");
				result = this.Delete(idList);
			}
			else
			{
				DalBase dalBase = new DalBase(connectionKeyOrConnectionString);
				string text = foreignkey + " in (" + StringHelper.ConvertListToString<string>(idList) + ")";
				ConsoleHelper.WriteLine("进入删除数据条件:" + text, "");
				int num = 0;
				if (string.IsNullOrWhiteSpace(sourceTable))
				{
					while (true)
					{
						ConsoleHelper.WriteLine("进入Sql外键读取数据第" + num + "数据", "");
						DataTable dataTable = dalBase.ExecutePageCommandLimit(fields, text, primaryKey + " asc", 20, num, primaryKey).Tables[0];
						if (dataTable.Rows.Count <= 0)
						{
							break;
						}
						List<string> list2 = new List<string>();
						foreach (DataRow dataRow in dataTable.Rows)
						{
							list2.Add(dataRow[primaryKey].ToString());
						}
						this.Delete(list2);
						num++;
					}
					ConsoleHelper.WriteLine("进入Sql外键读取数据处理完成", "");
				}
				else
				{
					while (true)
					{
						ConsoleHelper.WriteLine("进入源表外键读取数据第" + num + "页", "");
						DataTable dataTable = dalBase.ExecutePageLimit(sourceTable, text, primaryKey + " asc", 20, num, primaryKey).Tables[0];
						if (dataTable.Rows.Count <= 0)
						{
							break;
						}
						List<string> list2 = new List<string>();
						foreach (DataRow dataRow in dataTable.Rows)
						{
							list2.Add(dataRow[primaryKey].ToString());
						}
						this.Delete(list2);
						num++;
					}
					ConsoleHelper.WriteLine("进入源表外键读取数据处理完成", "");
				}
				result = list;
			}
			return result;
		}

		public IList<int> Optimize()
		{
			OptimizeOptions value = default(OptimizeOptions);
			ResponseHeader responseHeader = this.Update(new UpdateOptions
			{
				OptimizeOptions = new OptimizeOptions?(value)
			});
			IList<int> list = new List<int>();
			if (responseHeader != null)
			{
				list.Add(responseHeader.Status);
				list.Add(responseHeader.QTime);
				ConsoleHelper.WriteLine(string.Concat(new object[]
				{
					"优化Solr:",
					this.SolrServerUrl,
					"成功,状态：",
					responseHeader.Status,
					"执行时间:",
					responseHeader.QTime
				}), ConsoleColor.Yellow, "");
			}
			return list;
		}

		public string DataImport()
		{
			IDictionary<string, ICollection<string>> dictionary = UriPathHelper.CreateQueryParam("command", new string[]
			{
				"full-import"
			});
			UriPathHelper.AddQueryParam(dictionary, "clean", new string[]
			{
				"true"
			});
			UriPathHelper.AddQueryParam(dictionary, "commit", new string[]
			{
				"true"
			});
			UriPathHelper.AddQueryParam(dictionary, "optimize", new string[]
			{
				"true"
			});
			UriPathHelper.AddQueryParam(dictionary, "wt", new string[]
			{
				"json"
			});
			UriPathHelper.AddQueryParam(dictionary, "indent", new string[]
			{
				"true"
			});
			UriPathHelper.AddQueryParam(dictionary, "entity", new string[]
			{
				this.TableName
			});
			UriPathHelper.AddQueryParam(dictionary, "verbose", new string[]
			{
				"false"
			});
			UriPathHelper.AddQueryParam(dictionary, "debug", new string[]
			{
				"false"
			});
			string text = this.SolrServerUrl + "/dataimport";
			return RequestHelper.GetPageText(RequestHelper.CreateHttpPost(text, dictionary), Encoding.UTF8);
		}
	}
	public class SolrUpdate<T> : SolrUpdate where T : class, new()
	{
		public SolrUpdate(string tableName, string serverUrlOrServerKey = "") : base(tableName, serverUrlOrServerKey)
		{
		}

		public virtual IList<int> Update(T value, string correctfield = "")
		{
			return this.Update(new List<T>
			{
				value
			}, correctfield);
		}

		public virtual IList<int> Update(IEnumerable<T> valueList, string correctfield = "")
		{
			IList<int> list = new List<int>();
			IList<int> result;
			if (valueList == null)
			{
				result = list;
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(correctfield))
				{
					correctfield = "," + correctfield + ",";
				}
				List<SolrInputDocument> list2 = new List<SolrInputDocument>();
				Type typeFromHandle = typeof(T);
				PropertyInfo[] properties = typeFromHandle.GetProperties();
				foreach (T current in valueList)
				{
					SolrInputDocument solrInputDocument = new SolrInputDocument();
					PropertyInfo[] array = properties;
					for (int i = 0; i < array.Length; i++)
					{
						PropertyInfo propertyInfo = array[i];
						if (string.IsNullOrWhiteSpace(correctfield) || !RegexHelper.IsMatch(correctfield, "," + propertyInfo.Name + ","))
						{
							solrInputDocument.Add(propertyInfo.Name, new SolrInputField(propertyInfo.Name, propertyInfo.GetValue(current, null)));
						}
						else
						{
							string text = StringHelper.TrimComma(RegexHelper.MatchValue(correctfield, "," + propertyInfo.Name + ","));
							solrInputDocument.Add(text, new SolrInputField(text, propertyInfo.GetValue(current, null)));
						}
					}
					list2.Add(solrInputDocument);
				}
				CommitOptions value = default(CommitOptions);
				IObjectSerializer<SolrInputDocument> objectSerializer = new ObjectSerializerTable();
				ResponseHeader responseHeader = this.Update(new UpdateOptions
				{
					Docs = objectSerializer.Serialize(list2),
					CommitOptions = new CommitOptions?(value)
				});
				if (responseHeader != null)
				{
					list.Add(responseHeader.Status);
					list.Add(responseHeader.QTime);
					ConsoleHelper.WriteLine(string.Concat(new object[]
					{
						"更新Solr:",
						base.SolrServerUrl,
						"成功,状态：",
						responseHeader.Status,
						"执行时间:",
						responseHeader.QTime
					}), ConsoleColor.Yellow, "");
				}
				result = list;
			}
			return result;
		}
	}
}
