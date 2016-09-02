using EasyNet.Solr;
using EasyNet.Solr.Commons;
using EasyNet.Solr.Impl;
using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using WTF.Logging;

namespace WTF.Solr
{
	public class SolrQuery<T> : SolrUpdate<T> where T : class, new()
	{
		public enum QueryResultType
		{
			json,
			xml
		}

		private string _PrimaryKeyName = "";

		private bool _IsEdismax = true;

		private string _HlSimplePre = "<em class='highlight'>";

		private string _HlSimplePost = "</em>";

		public bool IsEdismax
		{
			get
			{
				return this._IsEdismax;
			}
			set
			{
				this._IsEdismax = value;
			}
		}

		public string PrimaryKeyName
		{
			get
			{
				return this._PrimaryKeyName;
			}
			set
			{
				this._PrimaryKeyName = value;
			}
		}

		public string HlSimplePre
		{
			get
			{
				return this._HlSimplePre;
			}
			set
			{
				this._HlSimplePre = value;
			}
		}

		public string HlSimplePost
		{
			get
			{
				return this._HlSimplePost;
			}
			set
			{
				this._HlSimplePost = value;
			}
		}

		public SolrQuery(string tableName, string serverUrlOrServerKey = "") : this(tableName, "", serverUrlOrServerKey)
		{
		}

		public SolrQuery(string tableName, string primaryKeyName, string serverUrlOrServerKey) : base(tableName, serverUrlOrServerKey)
		{
			this._PrimaryKeyName = primaryKeyName;
		}

		private ISolrQueryOperations<NamedList> GetQueryOperations()
		{
			return new SolrQueryOperations<NamedList>(new SolrQueryConnection<NamedList>
			{
				ServerUrl = base.SolrServerUrl,
				ResponseCodecFactory = new BinaryCodecFactory()
			});
		}

		public int GetCount(string condition)
		{
			NamedList result = this.QueryLimit("*", condition, "", "", 0, 0, "", "");
			ISolrResponseParser<NamedList, QueryResults<NullResult>> solrResponseParser = new BinaryQueryResultsParser<NullResult>(new NullResultObjectDeserialize());
			QueryResults<NullResult> queryResults = solrResponseParser.Parse(result);
			return Convert.ToInt32(queryResults.NumFound);
		}

		private NamedList QueryPage(string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, string bf = "")
		{
			return this.QueryLimit(fields, condition, qf, highlightFields, pageSize * pageIndex, pageSize, sortExpression, bf);
		}

		private NamedList QueryLimit(string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "")
		{
			IDictionary<string, ICollection<string>> dictionary = new Dictionary<string, ICollection<string>>();
			StringBuilder stringBuilder = new StringBuilder();
			condition = (string.IsNullOrWhiteSpace(condition) ? "*:*" : condition);
			stringBuilder.AppendFormat("?q={0}", condition);
			if (this.IsEdismax)
			{
				dictionary["defType"] = new string[]
				{
					"edismax"
				};
				stringBuilder.AppendFormat("&defType=edismax", new object[0]);
				dictionary["stopwords"] = new string[]
				{
					"true"
				};
				stringBuilder.AppendFormat("&stopwords=true", new object[0]);
				dictionary["lowercaseOperators"] = new string[]
				{
					"true"
				};
				stringBuilder.AppendFormat("&lowercaseOperators=true", new object[0]);
			}
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				dictionary["hl"] = new string[]
				{
					"true"
				};
				dictionary["hl.fl"] = new string[]
				{
					highlightFields
				};
				dictionary["hl.simple.pre"] = new string[]
				{
					this.HlSimplePre
				};
				dictionary["hl.simple.post"] = new string[]
				{
					this.HlSimplePost
				};
			}
			if (!string.IsNullOrEmpty(qf))
			{
				dictionary["qf"] = new string[]
				{
					qf
				};
				stringBuilder.AppendFormat("&qf={0}", qf);
			}
			if (fields != "*" && !string.IsNullOrWhiteSpace(fields))
			{
				dictionary["fl"] = new string[]
				{
					fields
				};
				stringBuilder.AppendFormat("&fl={0}", fields);
			}
			dictionary["start"] = new string[]
			{
				offset.ToString()
			};
			dictionary["rows"] = new string[]
			{
				limit.ToString()
			};
			stringBuilder.AppendFormat("&start={0}&rows={1}", offset, limit);
			string text = string.IsNullOrWhiteSpace(sortExpression) ? "score desc" : sortExpression;
			dictionary["sort"] = new string[]
			{
				text
			};
			stringBuilder.AppendFormat("&sort={0}", text);
			if (!string.IsNullOrEmpty(bf))
			{
				dictionary["bf"] = new string[]
				{
					bf
				};
				stringBuilder.AppendFormat("&bf={0}", bf);
			}
			if (base.IsRecordSolrSql)
			{
                base.Log.WriteLog(LogCategory.SolrInfo, stringBuilder.ToString(), base.SolrServerUrl + "/select" + stringBuilder.ToString() + "&wt=json&indent=true");
			}
			return this.GetQueryOperations().Query("/select", new SolrQuery(condition), dictionary);
		}

		private NamedList QueryGroupLimit(string GroupField, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string groupExpression, int groupLimit = 0, string bf = "")
		{
			if (string.IsNullOrWhiteSpace(GroupField))
			{
				throw new ArgumentNullException("参数GroupField不能为空");
			}
			IDictionary<string, ICollection<string>> dictionary = new Dictionary<string, ICollection<string>>();
			StringBuilder stringBuilder = new StringBuilder();
			condition = (string.IsNullOrWhiteSpace(condition) ? "*:*" : condition);
			stringBuilder.AppendFormat("?q={0}", condition);
			dictionary["group"] = new string[]
			{
				"true"
			};
			stringBuilder.AppendFormat("&group=true", new object[0]);
			dictionary["group.field"] = new string[]
			{
				GroupField
			};
			stringBuilder.AppendFormat("&group.field=" + GroupField, new object[0]);
			dictionary["group.ngroups"] = new string[]
			{
				"true"
			};
			stringBuilder.AppendFormat("&group.ngroups=true", new object[0]);
			if (groupLimit <= 0)
			{
				groupLimit = 0;
			}
			dictionary["group.limit"] = new string[]
			{
				groupLimit.ToString()
			};
			string text = string.IsNullOrWhiteSpace(groupExpression) ? "score desc" : groupExpression;
			dictionary["group.sort"] = new string[]
			{
				text
			};
			stringBuilder.AppendFormat("&group.limit=" + groupLimit, new object[0]);
			if (this.IsEdismax)
			{
				dictionary["defType"] = new string[]
				{
					"edismax"
				};
				stringBuilder.AppendFormat("&defType=edismax", new object[0]);
				dictionary["stopwords"] = new string[]
				{
					"true"
				};
				stringBuilder.AppendFormat("&stopwords=true", new object[0]);
				dictionary["lowercaseOperators"] = new string[]
				{
					"true"
				};
				stringBuilder.AppendFormat("&lowercaseOperators=true", new object[0]);
			}
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				dictionary["hl"] = new string[]
				{
					"true"
				};
				dictionary["hl.fl"] = new string[]
				{
					highlightFields
				};
				dictionary["hl.simple.pre"] = new string[]
				{
					"<em class='highlight'>"
				};
				dictionary["hl.simple.post"] = new string[]
				{
					"</em>"
				};
			}
			if (!string.IsNullOrEmpty(qf))
			{
				dictionary["qf"] = new string[]
				{
					qf
				};
				stringBuilder.AppendFormat("&qf={0}", qf);
			}
			if (fields != "*" && !string.IsNullOrWhiteSpace(fields))
			{
				dictionary["fl"] = new string[]
				{
					fields + ",score,_version_"
				};
				stringBuilder.AppendFormat("&fl={0}", fields + ",score,_version_");
			}
			dictionary["start"] = new string[]
			{
				offset.ToString()
			};
			dictionary["rows"] = new string[]
			{
				limit.ToString()
			};
			stringBuilder.AppendFormat("&start={0}&rows={1}", offset, limit);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				dictionary["sort"] = new string[]
				{
					sortExpression
				};
				stringBuilder.AppendFormat("&sort={0}", sortExpression);
			}
			if (!string.IsNullOrEmpty(bf))
			{
				dictionary["bf"] = new string[]
				{
					bf
				};
				stringBuilder.AppendFormat("&bf={0}", bf);
			}
			if (base.IsRecordSolrSql)
			{
                base.Log.WriteLog(LogCategory.SolrInfo, stringBuilder.ToString(), base.SolrServerUrl + "/select" + stringBuilder.ToString() + "&wt=json&indent=true");
			}
			return this.GetQueryOperations().Query("/select", new SolrQuery(condition), dictionary);
		}

		private NamedList Query(string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "")
		{
			return this.QueryLimit(fields, condition, qf, highlightFields, 0, limit, sortExpression, bf);
		}

		public List<string> QueryAnalysis(string fieldValue, string fieldName)
		{
			List<string> list = new List<string>();
			string text = this.QueryAnalysisUrl(fieldValue, fieldName, SolrQuery<T>.QueryResultType.xml);
			string pageText = RequestHelper.GetPageText(text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(pageText);
			foreach (XmlNode xmlNode in xmlDocument.SelectNodes("//str[@name='text']"))
			{
				list.Add(xmlNode.InnerText);
			}
			return list;
		}

		public string QueryAnalysisUrl(string fieldValue, string fieldName, SolrQuery<T>.QueryResultType queryResultType = SolrQuery<T>.QueryResultType.json)
		{
			Dictionary<string, ICollection<string>> dictionary = new Dictionary<string, ICollection<string>>();
			dictionary.AddOptions("wt", new string[]
			{
				queryResultType.ToString()
			});
			dictionary.AddOptions("analysis.showmatch", new string[]
			{
				"true"
			});
			dictionary.AddOptions("indent", new string[]
			{
				"true"
			});
			dictionary.AddOptions("analysis.fieldvalue", new string[]
			{
				fieldValue
			});
			dictionary.AddOptions("analysis.fieldname", new string[]
			{
				fieldName
			});
			return base.SolrServerUrl + "/analysis/field?" + dictionary.BuildParams();
		}

		public virtual DataSet QueryDataSet(string fields, string condition, string qf, int limit, string sortExpression, string bf = "")
		{
			return this.QueryDataSet(fields, condition, qf, "", limit, sortExpression, bf);
		}

		public virtual DataSet QueryDataSet(string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryDataSetLimit(fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public virtual DataSet QueryDataSetLimit(string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "")
		{
			NamedList namedList = this.QueryLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, bf);
			return this.QueryResultsParseDataSet(fields, namedList, highlightFields, out recordCount);
		}

		public virtual DataSet QueryDataSetLimit(string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "")
		{
			return this.QueryDataSetLimit(fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public virtual DataSet QueryDataSetLimit(string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryDataSetLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupList(string GroupField, string fields, string condition, string qf, int limit, string sortExpression, string groupExpression, int groupLimit = 0, string bf = "")
		{
			return this.QueryGroupList(GroupField, fields, condition, qf, "", limit, sortExpression, groupExpression, groupLimit, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupList(string GroupField, string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string groupExpression, int groupLimit = 0, string bf = "")
		{
			int num = 0;
			return this.QueryGroupListLimit(GroupField, fields, condition, qf, highlightFields, 0, limit, sortExpression, groupExpression, out num, groupLimit, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupListLimit(string GroupField, string fields, string condition, string qf, int offset, int limit, string sortExpression, string groupExpression, out int matchesCount, int groupLimit = 0, string bf = "")
		{
			return this.QueryGroupListLimit(GroupField, fields, condition, qf, "", offset, limit, sortExpression, groupExpression, out matchesCount, groupLimit, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupPage(string GroupField, string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, string groupExpression, out int matchesCount, int groupLimit = 0, string bf = "")
		{
			return this.QueryGroupPage(GroupField, fields, condition, qf, "", pageSize, pageIndex, sortExpression, groupExpression, out matchesCount, groupLimit, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupPage(string GroupField, string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, string groupExpression, out int matchesCount, int groupLimit = 0, string bf = "")
		{
			return this.QueryGroupListLimit(GroupField, fields, condition, qf, highlightFields, pageSize * pageIndex, pageSize, sortExpression, groupExpression, out matchesCount, groupLimit, bf);
		}

		public virtual IEnumerable<GroupResultInfo<T>> QueryGroupListLimit(string GroupField, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string groupExpression, out int matchesCount, int groupLimit = 0, string bf = "")
		{
			NamedList namedList = this.QueryGroupLimit(GroupField, fields, condition, qf, highlightFields, offset, limit, sortExpression, groupExpression, groupLimit, bf);
			return this.QueryGroupResultsParseT(fields, namedList, highlightFields, out matchesCount);
		}

		private IEnumerable<GroupResultInfo<T>> QueryGroupResultsParseT(string fields, NamedList namedList, string highlightFields, out int matchesCount)
		{
			ObjectDeserializeT<T> objectDeserializeT = new ObjectDeserializeT<T>(this._TableName, fields);
			matchesCount = 0;
			ISolrResponseParser<NamedList, IList<GroupQueryResults<T>>> solrResponseParser = new BinaryGroupQueryResultsParser<T>(objectDeserializeT);
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
				IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
				objectDeserializeT.Highlight = highlight;
			}
			List<GroupResultInfo<T>> list = new List<GroupResultInfo<T>>();
			IList<GroupQueryResults<T>> list2 = solrResponseParser.Parse(namedList);
			IEnumerable<GroupResultInfo<T>> result;
			if (list2 == null || list2.Count == 0)
			{
				result = list;
			}
			else
			{
				NamedList namedList2 = (NamedList)namedList.Get("grouped");
				int num = 0;
				foreach (GroupQueryResults<T> current in list2)
				{
					matchesCount = current.Matches;
					NamedList namedList3 = (NamedList)namedList2.GetVal(num);
					object obj = namedList3.Get("ngroups");
					if (obj != null)
					{
						matchesCount = Convert.ToInt32(obj);
					}
					foreach (GroupQueryResult<T> current2 in current.Groups)
					{
						list.Add(new GroupResultInfo<T>
						{
							Data = current2.QueryResults,
							RecordCount = current2.QueryResults.NumFound,
							MatchesCount = current.Matches,
							GroupValue = current2.GroupValue
						});
					}
					num++;
				}
				result = list;
			}
			return result;
		}

		public string GetFieldInCondition<I>(string KeyFieldName, IEnumerable<I> IDList)
		{
			return this.GetFieldInCondition<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList), false);
		}

		public string GetFieldInCondition<I>(string KeyFieldName, string IDString, bool IsSort = false)
		{
			string text = "";
			string text2 = (typeof(I) == typeof(string)) ? "\"" : "";
			string[] array = IDString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				throw new ArgumentNullException("FieldInCondition的参数IDList不能长度为0");
			}
			int num = array.Length + 1;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text3 = array2[i];
				if (IsSort)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						" ",
						KeyFieldName,
						":",
						text2,
						text3,
						text2,
						"^",
						num,
						" OR"
					});
				}
				else
				{
					string text4 = text;
					text = string.Concat(new string[]
					{
						text4,
						" ",
						KeyFieldName,
						":",
						text2,
						text3,
						text2,
						" OR"
					});
				}
				num--;
			}
			text = StringHelper.TrimEnd(text, "OR");
			string result;
			if (array.Length == 1)
			{
				result = text;
			}
			else
			{
				text = "( " + text + " )";
				result = text;
			}
			return result;
		}

		public string GetFieldInConditionInt(string KeyFieldName, string IDString)
		{
			return this.GetFieldInCondition<int>(KeyFieldName, IDString, false);
		}

		public string GetFieldInCondition(string KeyFieldName, string IDString)
		{
			return this.GetFieldInCondition<string>(KeyFieldName, StringHelper.ConvertListString(IDString));
		}

		public string GetFieldInConditionAnd<I>(string KeyFieldName, IEnumerable<I> IDList)
		{
			return this.GetFieldInConditionAnd<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList));
		}

		public string GetFieldInConditionAnd<I>(string KeyFieldName, string IDString)
		{
			string text = "";
			string text2 = (typeof(I) == typeof(string)) ? "\"" : "";
			string[] array = IDString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				throw new ArgumentNullException("FieldInCondition的参数IDList不能长度为0");
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text3 = array2[i];
				string text4 = text;
				text = string.Concat(new string[]
				{
					text4,
					" ",
					KeyFieldName,
					":",
					text2,
					text3,
					text2,
					" AND"
				});
			}
			return StringHelper.TrimEnd(text, "AND");
		}

		public string GetFieldInConditionAnd(string KeyFieldName, string IDString)
		{
			return this.GetFieldInConditionAnd<string>(KeyFieldName, StringHelper.ConvertListString(IDString));
		}

		public string GetFieldInConditionIntAnd(string KeyFieldName, string IDString)
		{
			return this.GetFieldInConditionAnd<int>(KeyFieldName, IDString);
		}

		public string FiterKeyWord(string keyWord)
		{
			return keyWord.Replace(" ", ",").Replace(":", ",");
		}

		public string GetFieldInConditionSort<I>(string KeyFieldName, IEnumerable<I> IDList)
		{
			return this.GetFieldInConditionSort<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList));
		}

		public string GetFieldInConditionSort<I>(string KeyFieldName, string IDString)
		{
			return this.GetFieldInCondition<I>(KeyFieldName, IDString, true);
		}

		public string GetFieldInConditionIntSort(string KeyFieldName, string IDString)
		{
			return this.GetFieldInConditionSort<int>(KeyFieldName, IDString);
		}

		public string GetFieldInConditionSort(string KeyFieldName, string IDString)
		{
			return this.GetFieldInConditionSort<string>(KeyFieldName, StringHelper.ConvertListString(IDString));
		}

		public virtual DataSet QueryPageDataSet(string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "")
		{
			NamedList namedList = this.QueryPage(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, bf);
			return this.QueryResultsParseDataSet(fields, namedList, highlightFields, out recordCount);
		}

		public virtual DataSet QueryPageDataSet(string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "")
		{
			return this.QueryPageDataSet(fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public virtual DataSet QueryPageDataSet(string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryPageDataSet(fields, condition, qf, "", pageSize, pageIndex, sortExpression, out num, bf);
		}

		public string GetQueryUrl(bool isedismax, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, SolrQuery<T>.QueryResultType queryResultType, string bf = "")
		{
			StringBuilder stringBuilder = new StringBuilder();
			condition = (string.IsNullOrWhiteSpace(condition) ? "*:*" : condition);
			stringBuilder.AppendFormat("q={0}", HttpUtilityHelper.EncodeUrl(condition));
			if (isedismax)
			{
				stringBuilder.AppendFormat("&defType=edismax", new object[0]);
				stringBuilder.AppendFormat("&stopwords=true", new object[0]);
				stringBuilder.AppendFormat("&lowercaseOperators=true", new object[0]);
			}
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				stringBuilder.AppendFormat("&hl=true&hl.fl={0}&hl.simple.pre={1}&hl.simple.post={2}", HttpUtilityHelper.EncodeUrl(highlightFields), HttpUtilityHelper.EncodeUrl("<em class='highlight'>"), HttpUtilityHelper.EncodeUrl("</em>"));
			}
			if (!string.IsNullOrEmpty(qf))
			{
				stringBuilder.AppendFormat("&qf={0}", qf);
			}
			if (fields != "*" && !string.IsNullOrWhiteSpace(fields))
			{
				stringBuilder.AppendFormat("&fl={0}", fields);
			}
			stringBuilder.AppendFormat("&start={0}&rows={1}", offset, limit);
			string arg = string.IsNullOrWhiteSpace(sortExpression) ? "score desc" : sortExpression;
			stringBuilder.AppendFormat("&sort={0}", arg);
			if (!string.IsNullOrEmpty(bf))
			{
				stringBuilder.AppendFormat("&bf={0}", bf);
			}
			stringBuilder.AppendFormat("&wt={0}", queryResultType.ToString());
			stringBuilder.Append("&indent=true");
			return base.SolrServerUrl + "/select?" + stringBuilder.ToString();
		}

		public string QueryLimit(bool isedismax, string fields, string condition, string qf, int offset, int limit, string sortExpression, SolrQuery<T>.QueryResultType queryResultType, string bf = "")
		{
			return this.QueryLimit(isedismax, fields, condition, qf, "", offset, limit, sortExpression, queryResultType, bf);
		}

		public string QueryLimit(bool isedismax, string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, SolrQuery<T>.QueryResultType queryResultType, string bf = "")
		{
			string queryUrl = this.GetQueryUrl(isedismax, fields, condition, qf, highlightFields, offset, limit, sortExpression, queryResultType, bf);
			return RequestHelper.GetPageText(queryUrl);
		}

		private DataSet QueryResultsParseDataSet(string fields, NamedList namedList, string highlightFields, out long recordCount)
		{
			ObjectDeserializeTable<T> objectDeserializeTable = new ObjectDeserializeTable<T>(this._TableName, fields);
			ISolrResponseParser<NamedList, QueryResults<DataRow>> solrResponseParser = new BinaryQueryResultsParser<DataRow>(objectDeserializeTable);
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
				IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
				objectDeserializeTable.Highlight = highlight;
			}
			QueryResults<DataRow> queryResults = solrResponseParser.Parse(namedList);
			recordCount = queryResults.NumFound;
			DataSet dataSet = new DataSet();
			DataTable dataTable = objectDeserializeTable.CreateDataTable();
			foreach (DataRow current in queryResults)
			{
				dataTable.Rows.Add(current.ItemArray);
			}
			dataSet.Tables.Add(dataTable);
			DataTable dataTable2 = new DataTable();
			dataTable2.TableName = "RecordCount";
			dataTable2.Columns.Add("RecordCount", typeof(long));
			DataRow dataRow = dataTable2.NewRow();
			dataRow["RecordCount"] = recordCount;
			dataTable2.Rows.Add(dataRow);
			dataSet.Tables.Add(dataTable2);
			return dataSet;
		}

		private IEnumerable<T> QueryResultsParseT(string fields, NamedList namedList, string highlightFields, out long recordCount)
		{
			ObjectDeserializeT<T> objectDeserializeT = new ObjectDeserializeT<T>(this._TableName, fields);
			ISolrResponseParser<NamedList, QueryResults<T>> solrResponseParser = new BinaryQueryResultsParser<T>(objectDeserializeT);
			if (!string.IsNullOrWhiteSpace(highlightFields))
			{
				BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
				IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
				objectDeserializeT.Highlight = highlight;
			}
			QueryResults<T> queryResults = solrResponseParser.Parse(namedList);
			recordCount = queryResults.NumFound;
			return queryResults;
		}

		public virtual IEnumerable<T> QueryList(string fields, string condition, string qf, int limit, string sortExpression, string bf = "")
		{
			return this.QueryList(fields, condition, qf, "", limit, sortExpression, bf);
		}

		public virtual IEnumerable<T> QueryList(string fields, string condition, string qf, string highlightFields, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryListLimit(fields, condition, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, string condition, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "")
		{
			return this.QueryListLimit(fields, condition, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "")
		{
			NamedList namedList = this.QueryLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, bf);
			return this.QueryResultsParseT(fields, namedList, highlightFields, out recordCount);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, string condition, string qf, int offset, int limit, string sortExpression, string bf = "")
		{
			return this.QueryListLimit(fields, condition, qf, "", offset, limit, sortExpression, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, string condition, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryListLimit(fields, condition, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public virtual IEnumerable<T> QueryPage(string fields, string condition, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "")
		{
			return this.QueryPage(fields, condition, qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}

		public virtual IEnumerable<T> QueryPage(string fields, string condition, string qf, string highlightFields, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "")
		{
			NamedList namedList = this.QueryPage(fields, condition, qf, highlightFields, pageSize, pageIndex, sortExpression, bf);
			return this.QueryResultsParseT(fields, namedList, highlightFields, out recordCount);
		}

		public string Condiition(Expression<Func<T, bool>> predicate)
		{
			SolrConditionBuilder solrConditionBuilder = new SolrConditionBuilder();
			solrConditionBuilder.Build(predicate);
			return solrConditionBuilder.Condition;
		}

		public static string CreateCondiition(Expression<Func<T, bool>> predicate)
		{
			SolrConditionBuilder solrConditionBuilder = new SolrConditionBuilder();
			solrConditionBuilder.Build(predicate);
			return solrConditionBuilder.Condition;
		}

		public virtual IEnumerable<T> QueryList(string fields, Expression<Func<T, bool>> predicate, string qf, int limit, string sortExpression, string bf = "")
		{
			return this.QueryList(fields, predicate, qf, "", limit, sortExpression, bf);
		}

		public virtual IEnumerable<T> QueryList(string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryListLimit(fields, predicate, qf, highlightFields, 0, limit, sortExpression, out num, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, out long recordCount, string bf = "")
		{
			return this.QueryListLimit(fields, predicate, qf, "", offset, limit, sortExpression, out recordCount, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int offset, int limit, string sortExpression, out long recordCount, string bf = "")
		{
			NamedList namedList = this.QueryLimit(fields, this.Condiition(predicate), qf, highlightFields, offset, limit, sortExpression, bf);
			return this.QueryResultsParseT(fields, namedList, highlightFields, out recordCount);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, Expression<Func<T, bool>> predicate, string qf, int offset, int limit, string sortExpression, string bf = "")
		{
			return this.QueryListLimit(fields, predicate, qf, "", offset, limit, sortExpression, bf);
		}

		public virtual IEnumerable<T> QueryListLimit(string fields, Expression<Func<T, bool>> predicate, string qf, string highlightFields, int offset, int limit, string sortExpression, string bf = "")
		{
			long num = 0L;
			return this.QueryListLimit(fields, predicate, qf, highlightFields, offset, limit, sortExpression, out num, bf);
		}

		public virtual IEnumerable<T> QueryPage(string fields, Expression<Func<T, bool>> predicate, string qf, int pageSize, int pageIndex, string sortExpression, out long recordCount, string bf = "")
		{
			return this.QueryPage(fields, this.Condiition(predicate), qf, "", pageSize, pageIndex, sortExpression, out recordCount, bf);
		}
	}
}
