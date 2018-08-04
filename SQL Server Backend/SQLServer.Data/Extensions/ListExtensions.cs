using SQLServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Extensions
{
	public static class ListExtensions
	{
		public static List<Index> GetIndexes(this List<Index> indexes, int tableObjectID)
		{
			return indexes
				.Where((item) => item.TableObjectID == tableObjectID)
				.ToList();
		}

		public static List<Column> GetColumns(this List<Column> columns, int tableObjectID)
		{
			return columns
				.Where((item) => item.TableObjectID == tableObjectID)
				.ToList();
		}

		public static List<IndexColumn> GetIndexColumns(this List<Column> indexes)
		{
			return indexes
				.Where((item) => item.GetType() == typeof(IndexColumn))
				.Select((item) => { return (IndexColumn)item; })
				.ToList();
		}

		public static List<IndexColumn> GetIndexColumns(this List<Column> indexes, int indexID, int tableObjectID)
		{
			return indexes
				.Where((item) => item.GetType() == typeof(IndexColumn))
				.Select((item) => { return (IndexColumn)item; })
				.Where((item) => item.IndexID == indexID && item.TableObjectID == tableObjectID)
				.ToList();
		}
	}
}
