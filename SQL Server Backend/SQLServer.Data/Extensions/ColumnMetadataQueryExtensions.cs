using SQLServer.Data.Metadata.Definitions;
using SQLServer.Data.Metadata.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class ColumnMetadataQueryExtensions
	{
		public static IQueryable<ColumnMetadata> GetColumnsInIndexColumns(this IQueryable<ColumnMetadata> columnMetadata, IQueryable<IndexColumnMetadata> indexColumnMetadata)
		{
			columnMetadata = columnMetadata
				.Where((column) => indexColumnMetadata.Select((indexColumn) => indexColumn.ColumnID).Contains(column.ColumnID));

			return columnMetadata;
		}
	}
}
