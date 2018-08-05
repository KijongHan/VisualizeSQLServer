using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class TableMetadataQueryExtensions
	{
		public static List<int> GetTableMetadataTableObjectIDs(this IQueryable<TableMetadata> tableMetadataQuery)
		{
			return tableMetadataQuery.Select((item) => item.TableObjectID).ToList();
		}
	}
}
