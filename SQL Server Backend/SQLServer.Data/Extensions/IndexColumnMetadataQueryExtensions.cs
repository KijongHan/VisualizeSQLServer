using SQLServer.Data.Metadata.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Repository
{
	public static class IndexColumnMetadataQueryExtensions
	{
		public static IQueryable<IndexColumnMetadata> GetIndexColumnMetadataInTableAndIndex(this IQueryable<IndexColumnMetadata> indexColumnMetadataQuery, int tableObjectID, int indexID)
		{
			indexColumnMetadataQuery = indexColumnMetadataQuery
				.Where((item) => item.IndexID == indexID && item.TableObjectID == tableObjectID);

			return indexColumnMetadataQuery;
		}

		public static IQueryable<int> GetIndexColumnMetadataColumnID(this IQueryable<IndexColumnMetadata> indexColumnMetadata)
		{
			return indexColumnMetadata.Select((item) => item.ColumnID);
		}
	}
}
