using EasyNet.Solr;
using EasyNet.Solr.Commons;
using EasyNet.Solr.Impl;
using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using WTF.Logging;

namespace WTF.Solr
{
	public class SolrSelect<T> : SolrUpdate<T> where T : class, new()
	{
		public enum QueryResultType
		{
			json,
			xml
		}

		private string _HlSimplePre = "<em class='highlight'>";

		private string _HlSimplePost = "</em>";

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

		public SolrSelect(string tableName, string serverUrlOrServerKey = "") : base(tableName, serverUrlOrServerKey)
		{
		}

		private ISolrQueryOperations<NamedList> GetQueryOperations()
		{
			return new SolrQueryOperations<NamedList>(new SolrQueryConnection<NamedList>
			{
				ServerUrl = base.SolrServerUrl,
				ResponseCodecFactory = new BinaryCodecFactory()
			});
		}

		public NamedList Query(string query, IDictionary<string, ICollection<string>> options = null)
		{
			options = (options ?? new Dictionary<string, ICollection<string>>());
			StringBuilder stringBuilder = new StringBuilder();
			query = (string.IsNullOrWhiteSpace(query) ? "*:*" : query);
			stringBuilder.AppendFormat("?q={0}", query);
			if (options.Count > 0)
			{
				stringBuilder.Append("&" + options.BuildParams());
			}
			if (base.IsRecordSolrSql)
			{
                base.Log.WriteLog(LogCategory.SolrInfo, stringBuilder.ToString(), base.SolrServerUrl + "/select" + stringBuilder.ToString() + "&wt=json&indent=true");
			}
			return this.GetQueryOperations().Query("/select", new SolrQuery(query), options);
		}

		public string QueryUrl(string query, IDictionary<string, ICollection<string>> options = null, SolrSelect<T>.QueryResultType queryResultType = SolrSelect<T>.QueryResultType.json)
		{
			options = (options ?? new Dictionary<string, ICollection<string>>());
			StringBuilder stringBuilder = new StringBuilder();
			query = (string.IsNullOrWhiteSpace(query) ? "*:*" : query);
			stringBuilder.AppendFormat("?q={0}", query);
			if (options.Count > 0)
			{
				stringBuilder.Append("&" + options.BuildParams());
			}
			stringBuilder.AppendFormat("&wt={0}", queryResultType.ToString());
			stringBuilder.Append("&indent=true");
			return base.SolrServerUrl + "/select" + stringBuilder.ToString();
		}

		public string QueryString(string query, IDictionary<string, ICollection<string>> options = null, SolrSelect<T>.QueryResultType queryResultType = SolrSelect<T>.QueryResultType.json)
		{
			string text = this.QueryUrl(query, options, queryResultType);
			return RequestHelper.GetPageText(text);
		}

		public List<string> QueryAnalysis(string fieldValue, string fieldName)
		{
			List<string> list = new List<string>();
			string text = this.QueryAnalysisUrl(fieldValue, fieldName, SolrSelect<T>.QueryResultType.xml);
			string pageText = RequestHelper.GetPageText(text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(pageText);
			foreach (XmlNode xmlNode in xmlDocument.SelectNodes("//str[@name='text']"))
			{
				list.Add(xmlNode.InnerText);
			}
			return list;
		}

		public string QueryAnalysisUrl(string fieldValue, string fieldName, SolrSelect<T>.QueryResultType queryResultType = SolrSelect<T>.QueryResultType.json)
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

		public NamedList QueryNamedLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			IDictionary<string, ICollection<string>> options2 = this.CreateCommon(fields, sort, start, rows, fiterQuery);
			options2.AddDismax(qf, bf, DefTypeType.edismax);
			options2.Merge(options);
			return this.Query(query, options2);
		}

		public NamedList QueryNamedGroupLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string groupField, string sort, string groupSort, int start, int rows, int groupLimit = 0, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			IDictionary<string, ICollection<string>> options2 = this.CreateCommon(fields, sort, start, rows, fiterQuery);
			options2.AddDismax(qf, bf, DefTypeType.edismax);
			options2.AddGroup(groupField, groupSort, groupLimit);
			options2.Merge(options);
			return this.Query(query, options2);
		}

		public NamedList QueryNamedSpatialLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sfield, float longitude, float latitude, float d, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			IDictionary<string, ICollection<string>> options2 = this.CreateCommon(fields, sort, start, rows, fiterQuery);
			options2.AddDismax(qf, bf, DefTypeType.edismax);
			options2.AddSpatial(sfield, longitude, latitude, d);
			options2.Merge(options);
			return this.Query(query, options2);
		}

		public NamedList QueryNamedFacetLimit(string query, string qf, ICollection<string> fiterQuery, string fields, ICollection<string> facetFields, ICollection<string> facetquery, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			IDictionary<string, ICollection<string>> options2 = this.CreateCommon(fields, sort, start, rows, fiterQuery);
			options2.AddDismax(qf, bf, DefTypeType.edismax);
			options2.AddFacetCommon(facetFields, facetquery);
			options2.Merge(options);
			return this.Query(query, options2);
		}

		public DataSet QueryDataSetLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			long num = 0L;
			return this.QueryDataSetLimit(query, qf, fiterQuery, fields, sort, start, rows, out num, bf, options);
		}

		public DataSet QueryDataSetLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int start, int rows, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			NamedList namedList = this.QueryNamedLimit(query, qf, fiterQuery, fields, sort, start, rows, bf, options);
            return namedList.ParserResultsDataSet<T>(fields, out recordCount, this._TableName);
		}

		public DataSet QueryDataSetPage(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int pageSize, int pageIndex, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			return this.QueryDataSetLimit(query, qf, fiterQuery, fields, sort, pageSize * pageIndex, pageSize, out recordCount, bf, options);
		}

		public IEnumerable<T> QueryLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			long num = 0L;
			return this.QueryLimit(query, qf, fiterQuery, fields, sort, start, rows, out num, bf, options);
		}

		public IEnumerable<T> QueryLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int start, int rows, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			NamedList namedList = this.QueryNamedLimit(query, qf, fiterQuery, fields, sort, start, rows, bf, options);
            return namedList.ParserResults<T>(fields, out recordCount, this._TableName);
		}

		public IEnumerable<T> QueryPage(string query, string qf, ICollection<string> fiterQuery, string fields, string sort, int pageSize, int pageIndex, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			return this.QueryLimit(query, qf, fiterQuery, fields, sort, pageSize * pageIndex, pageSize, out recordCount, bf, options);
		}

		public IEnumerable<GroupResultInfo<T>> QueryGroupLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string groupField, string sort, string groupSort, int start, int rows, int groupLimit = 0, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			int num = 0;
			return this.QueryGroupLimit(query, qf, fiterQuery, fields, groupField, sort, groupSort, start, rows, out num, groupLimit, bf, options);
		}

		public IEnumerable<GroupResultInfo<T>> QueryGroupLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string groupField, string sort, string groupSort, int start, int rows, out int matchesCount, int groupLimit = 0, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			NamedList namedList = this.QueryNamedGroupLimit(query, qf, fiterQuery, fields, groupField, sort, groupSort, start, rows, groupLimit, bf, options);
            return namedList.ParserResultsGroup<T>(fields, out matchesCount, this._TableName);
		}

		public IEnumerable<GroupResultInfo<T>> QueryGroupPage(string query, string qf, ICollection<string> fiterQuery, string fields, string groupField, string sort, string groupSort, int pageSize, int pageIndex, out int matchesCount, int groupLimit = 0, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			return this.QueryGroupLimit(query, qf, fiterQuery, fields, groupField, sort, groupSort, pageSize * pageIndex, pageSize, out matchesCount, groupLimit, bf, options);
		}

		public IEnumerable<T> QuerySpatialLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sfield, float longitude, float latitude, float d, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			long num = 0L;
			return this.QuerySpatialLimit(query, qf, fiterQuery, fields, sfield, longitude, latitude, d, sort, start, rows, out num, bf, options);
		}

		public IEnumerable<T> QuerySpatialLimit(string query, string qf, ICollection<string> fiterQuery, string fields, string sfield, float longitude, float latitude, float d, string sort, int start, int rows, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			NamedList namedList = this.QueryNamedSpatialLimit(query, qf, fiterQuery, fields, sfield, longitude, latitude, d, sort, start, rows, bf, options);
            return namedList.ParserResults<T>(fields, out recordCount, this._TableName);
		}

		public IEnumerable<T> QuerySpatialPage(string query, string qf, ICollection<string> fiterQuery, string fields, string sfield, float longitude, float latitude, float d, string sort, int pageSize, int pageIndex, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			return this.QuerySpatialLimit(query, qf, fiterQuery, fields, sfield, longitude, latitude, d, sort, pageSize * pageIndex, pageSize, out recordCount, bf, options);
		}

		public string GetFieldInCondition<I>(string KeyFieldName, IEnumerable<I> IDList, bool IsSort = false)
		{
			return this.GetFieldInCondition<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList), IsSort);
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

		public string GetFieldInConditionAnd<I>(string KeyFieldName, IEnumerable<I> IDList)
		{
			return this.GetFieldInConditionAnd<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList));
		}

        public string GetFieldInConditionAndLike<I>(string KeyFieldName, string IDString, string addSplit = "", QueryMethod queryMethod = QueryMethod.Contains)
		{
			string text = "";
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
				string text2 = array2[i];
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					" ",
					KeyFieldName,
					":",
					(queryMethod != QueryMethod.EndsWith) ? "*" : "",
					addSplit,
					text2,
					addSplit,
					(queryMethod != QueryMethod.StartsWith) ? "*" : "",
					" AND"
				});
			}
			return StringHelper.TrimEnd(text, "AND");
		}

		public string GetFieldInConditionAndLike<I>(string KeyFieldName, IEnumerable<I> IDList, string addSplit = "", QueryMethod queryMethod = QueryMethod.Contains)
		{
			return this.GetFieldInConditionAndLike<I>(KeyFieldName, StringHelper.ConvertListToString<I>(IDList), addSplit, queryMethod);
		}

		public IDictionary<string, ICollection<string>> CreateCommon(string fl, string sort, int start, int rows, ICollection<string> fiterQuery = null)
		{
			return SolrHelper.CreateCommon(fl, sort, start, rows, fiterQuery);
		}

		public IDictionary<string, ICollection<string>> CreateOptions(string key, params string[] values)
		{
			return key.CreateOptions(values);
		}

		public IDictionary<string, ICollection<string>> CreateOptions(string key, ICollection<string> value)
		{
			return key.CreateOptions(value);
		}

		public ICollection<string> CreateCollection(params string[] values)
		{
			return SolrHelper.CreateCollection(values);
		}

		public IDictionary<string, ICollection<string>> CreateDismax(string qf, string bf = "", DefTypeType defTypeType = DefTypeType.edismax)
		{
			return SolrHelper.CreateDismax(qf, bf, defTypeType);
		}

		public IDictionary<string, ICollection<string>> CreateHL(string hlFields, string hlSimplePre = "<em class='highlight'>", string hlSimplePost = "</em>")
		{
			return SolrHelper.CreateHL(hlFields, hlSimplePre, hlSimplePost);
		}

		public IDictionary<string, ICollection<string>> CreateGroup(string groupField, string groupSort, int groupLimit)
		{
			return SolrHelper.CreateGroup(groupField, groupSort, groupLimit);
		}

		public IDictionary<string, ICollection<string>> CreateSpatial(string sfield, float longitude, float latitude, float d)
		{
			return SolrHelper.CreateSpatial(sfield, longitude, latitude, d);
		}

		public FacetResult<T> QueryFacetLimit(string query, string qf, ICollection<string> fiterQuery, string fields, ICollection<string> facetFields, ICollection<string> facetquery, string sort, int start, int rows, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			long num = 0L;
			return this.QueryFacetLimit(query, qf, fiterQuery, fields, facetFields, facetquery, sort, start, rows, out num, bf, options);
		}

		public FacetResult<T> QueryFacetLimit(string query, string qf, ICollection<string> fiterQuery, string fields, ICollection<string> facetFields, ICollection<string> facetquery, string sort, int start, int rows, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			NamedList namedList = this.QueryNamedFacetLimit(query, qf, fiterQuery, fields, facetFields, facetquery, sort, start, rows, bf, options);
            return namedList.ParserFacetResults<T>(fields, out recordCount, this._TableName);
		}

		public FacetResult<T> QueryFacetPage(string query, string qf, ICollection<string> fiterQuery, string fields, ICollection<string> facetFields, ICollection<string> facetquery, string sort, int pageSize, int pageIndex, out long recordCount, string bf = "", IDictionary<string, ICollection<string>> options = null)
		{
			return this.QueryFacetLimit(query, qf, fiterQuery, fields, facetFields, facetquery, sort, pageSize * pageIndex, pageSize, out recordCount, bf, options);
		}
	}
}
