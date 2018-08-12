using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class TableMetadataQueryExtensions
	{
		public static IQueryable<TableMetadata> GetTableMetadata(this IQueryable<TableMetadata> tableMetadataQuery, string tableName, string tableSchema)
		{
			tableMetadataQuery = tableMetadataQuery
				.Where((item) => item.TableName == tableName && item.SchemaName == tableSchema);

			return tableMetadataQuery;
		}
	}
}
