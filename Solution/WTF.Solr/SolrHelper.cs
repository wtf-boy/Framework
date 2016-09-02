using EasyNet.Solr;
using EasyNet.Solr.Commons;
using EasyNet.Solr.Impl;
using System;
using System.Collections.Generic;
using System.Data;

namespace WTF.Solr
{
	public static class SolrHelper
	{
		public static IDictionary<string, ICollection<string>> Merge(this IDictionary<string, ICollection<string>> options, IDictionary<string, ICollection<string>> mergeoptions)
		{
			IDictionary<string, ICollection<string>> result;
			if (mergeoptions == null || mergeoptions.Count == 0)
			{
				result = options;
			}
			else
			{
				foreach (KeyValuePair<string, ICollection<string>> current in mergeoptions)
				{
					if (!options.ContainsKey(current.Key))
					{
						options[current.Key] = current.Value;
					}
				}
				result = options;
			}
			return result;
		}

		public static IDictionary<string, ICollection<string>> CreateCommon(string fl, string sort, int start, int rows, ICollection<string> fiterQuery = null)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddCommon(fl, sort, start, rows, fiterQuery);
		}

		public static IDictionary<string, ICollection<string>> AddCommon(this IDictionary<string, ICollection<string>> options, string fl, string sort, int start, int rows, ICollection<string> fiterQuery = null)
		{
			if (fl != "*" && !string.IsNullOrWhiteSpace(fl))
			{
				options.AddOptions("fl", new string[]
				{
					fl
				});
			}
			if (fiterQuery != null && fiterQuery.Count > 0)
			{
				options.AddOptions("fq", fiterQuery);
			}
			options.AddOptions("start", new string[]
			{
				start.ToString()
			});
			options.AddOptions("rows", new string[]
			{
				rows.ToString()
			});
			sort = (string.IsNullOrWhiteSpace(sort) ? "score desc" : sort);
			options.AddOptions("sort", new string[]
			{
				sort
			});
			return options;
		}

		public static IDictionary<string, ICollection<string>> CreateOptions(this string key, params string[] values)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddOptions(key, values);
		}

		public static IDictionary<string, ICollection<string>> CreateOptions(this string key, ICollection<string> value)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddOptions(key, value);
		}

		public static IDictionary<string, ICollection<string>> AddOptions(this IDictionary<string, ICollection<string>> options, string key, params string[] values)
		{
			options.AddOptions(key, SolrHelper.CreateCollection(values));
			return options;
		}

		public static IDictionary<string, ICollection<string>> AddOptions(this IDictionary<string, ICollection<string>> options, string key, ICollection<string> value)
		{
			if (!options.ContainsKey(key) && value != null && value.Count > 0)
			{
				options[key] = value;
			}
			return options;
		}

		public static ICollection<string> CreateCollection(params string[] values)
		{
			List<string> list = new List<string>();
			ICollection<string> result;
			if (values == null || values.Length == 0)
			{
				result = list;
			}
			else
			{
				for (int i = 0; i < values.Length; i++)
				{
					string text = values[i];
					if (!string.IsNullOrWhiteSpace(text))
					{
						list.Add(text);
					}
				}
				result = list;
			}
			return result;
		}

		public static ICollection<string> CreateCollection(this string value)
		{
			ICollection<string> result;
			if (string.IsNullOrWhiteSpace(value))
			{
				result = new List<string>();
			}
			else
			{
				result = SolrHelper.CreateCollection(new string[]
				{
					value
				});
			}
			return result;
		}

		public static IDictionary<string, ICollection<string>> CreateFacetCommon(ICollection<string> fields, ICollection<string> facetquery)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddFacetCommon(fields, facetquery);
		}

		public static IDictionary<string, ICollection<string>> AddFacetCommon(this IDictionary<string, ICollection<string>> options, ICollection<string> fields, ICollection<string> facetquery)
		{
			options.AddOptions("facet", new string[]
			{
				"true"
			});
			options.AddOptions("facet.field", fields);
			if (facetquery != null && facetquery.Count > 0)
			{
				options.AddOptions("facet.query", facetquery);
			}
			return options;
		}

		public static IDictionary<string, ICollection<string>> CreateDismax(string qf, string bf = "", DefTypeType defTypeType = DefTypeType.edismax)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddDismax(qf, bf, defTypeType);
		}

		public static IDictionary<string, ICollection<string>> AddDismax(this IDictionary<string, ICollection<string>> options, string qf, string bf = "", DefTypeType defTypeType = DefTypeType.edismax)
		{
			options.AddOptions("q.alt", new string[]
			{
				"*:*"
			});
			options.AddOptions("defType", new string[]
			{
				defTypeType.ToString()
			});
			options.AddOptions("qf", new string[]
			{
				qf
			});
			options.AddOptions("bf", new string[]
			{
				bf
			});
			if (defTypeType == DefTypeType.edismax)
			{
				options.AddOptions("stopwords", new string[]
				{
					"true"
				});
				options.AddOptions("lowercaseOperators", new string[]
				{
					"true"
				});
			}
			return options;
		}

		public static IDictionary<string, ICollection<string>> CreateHL(string hlFields, string hlSimplePre = "<em class='highlight'>", string hlSimplePost = "</em>")
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddHL(hlFields, hlSimplePre, hlSimplePost);
		}

		public static IDictionary<string, ICollection<string>> AddHL(this IDictionary<string, ICollection<string>> options, string hlFields, string hlSimplePre = "<em class='highlight'>", string hlSimplePost = "</em>")
		{
			if (!string.IsNullOrWhiteSpace(hlFields))
			{
				options.AddOptions("hl", new string[]
				{
					"true"
				});
				options.AddOptions("hl.fl", new string[]
				{
					hlFields
				});
				options.AddOptions("hl.simple.pre", new string[]
				{
					hlSimplePre
				});
				options.AddOptions("hl.simple.post", new string[]
				{
					hlSimplePost
				});
			}
			return options;
		}

		public static IDictionary<string, ICollection<string>> CreateSpatial(string sfield, float longitude, float latitude, float d)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddSpatial(sfield, longitude, latitude, d);
		}

		public static IDictionary<string, ICollection<string>> AddSpatial(this IDictionary<string, ICollection<string>> options, string sfield, float longitude, float latitude, float d)
		{
			options["sfield"] = new string[]
			{
				sfield
			};
			options["pt"] = new string[]
			{
				latitude.ToString() + "," + longitude.ToString()
			};
			options["d"] = new string[]
			{
				d.ToString()
			};
			return options;
		}

		public static IDictionary<string, ICollection<string>> CreateGroup(string groupField, string groupSort, int groupLimit)
		{
			IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
			return options.AddGroup(groupField, groupSort, groupLimit);
		}

		public static IDictionary<string, ICollection<string>> AddGroup(this IDictionary<string, ICollection<string>> options, string groupField, string groupSort, int groupLimit)
		{
			options.AddOptions("group", new string[]
			{
				"true"
			});
			options.AddOptions("group.field", new string[]
			{
				groupField
			});
			options.AddOptions("group.ngroups", new string[]
			{
				"true"
			});
			if (groupLimit <= 0)
			{
				groupLimit = 0;
			}
			options.AddOptions("group.limit", new string[]
			{
				groupLimit.ToString()
			});
			groupSort = (string.IsNullOrWhiteSpace(groupSort) ? "score desc" : groupSort);
			options.AddOptions("group.sort", new string[]
			{
				groupSort
			});
			return options;
		}

		public static IEnumerable<GroupResultInfo<T>> ParserResultsGroup<T>(this NamedList namedList, string fields, out int matchesCount, string tableName = "") where T : class, new()
		{
			ObjectDeserializeT<T> objectDeserializeT = new ObjectDeserializeT<T>(tableName, fields);
			matchesCount = 0;
			ISolrResponseParser<NamedList, IList<GroupQueryResults<T>>> solrResponseParser = new BinaryGroupQueryResultsParser<T>(objectDeserializeT);
			BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
			IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
			objectDeserializeT.Highlight = highlight;
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

		public static IEnumerable<T> ParserResults<T>(this NamedList namedList, string fields, out long recordCount, string tableName = "") where T : class, new()
		{
			ObjectDeserializeT<T> objectDeserializeT = new ObjectDeserializeT<T>(tableName, fields);
			ISolrResponseParser<NamedList, QueryResults<T>> solrResponseParser = new BinaryQueryResultsParser<T>(objectDeserializeT);
			BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
			IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
			objectDeserializeT.Highlight = highlight;
			QueryResults<T> queryResults = solrResponseParser.Parse(namedList);
			recordCount = queryResults.NumFound;
			return queryResults;
		}

		public static FacetResult<T> ParserFacetResults<T>(this NamedList namedList, string fields, out long recordCount, string tableName = "") where T : class, new()
		{
			ObjectDeserializeT<T> objectDeserializeT = new ObjectDeserializeT<T>(tableName, fields);
			ISolrResponseParser<NamedList, QueryResults<T>> solrResponseParser = new BinaryQueryResultsParser<T>(objectDeserializeT);
			BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
			IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
			objectDeserializeT.Highlight = highlight;
			QueryResults<T> queryResults = solrResponseParser.Parse(namedList);
			BinaryFacetFieldsParser binaryFacetFieldsParser = new BinaryFacetFieldsParser();
			IDictionary<string, IList<FacetField>> dictionary = binaryFacetFieldsParser.Parse(namedList);
			IDictionary<string, IList<FacetFieldInfo>> dictionary2 = new Dictionary<string, IList<FacetFieldInfo>>();
			if (dictionary != null && dictionary.Count > 0)
			{
				foreach (KeyValuePair<string, IList<FacetField>> current in dictionary)
				{
					List<FacetFieldInfo> list = new List<FacetFieldInfo>();
					foreach (FacetField current2 in current.Value)
					{
						list.Add(new FacetFieldInfo
						{
							Name = current2.Name,
							Count = current2.Count
						});
					}
					dictionary2.Add(current.Key, list);
				}
			}
			FacetResult<T> facetResult = new FacetResult<T>();
			facetResult.Data = queryResults;
			facetResult.Facet = dictionary2;
			recordCount = queryResults.NumFound;
			return facetResult;
		}

		public static DataSet ParserResultsDataSet<T>(this NamedList namedList, string fields, out long recordCount, string tableName = "") where T : class, new()
		{
			ObjectDeserializeTable<T> objectDeserializeTable = new ObjectDeserializeTable<T>(tableName, fields);
			ISolrResponseParser<NamedList, QueryResults<DataRow>> solrResponseParser = new BinaryQueryResultsParser<DataRow>(objectDeserializeTable);
			BinaryHighlightingParser binaryHighlightingParser = new BinaryHighlightingParser();
			IDictionary<string, IDictionary<string, IList<string>>> highlight = binaryHighlightingParser.Parse(namedList);
			objectDeserializeTable.Highlight = highlight;
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
	}
}
