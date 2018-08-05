using SQLServer.Data.Entities;
using SQLServer.Data.Extensions;
using SQLServer.Data.Metadata.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class IndexMetadataQueryExtensions
	{
		public static IQueryable<IndexMetadata> GetIndexMetadataInTables(this IQueryable<IndexMetadata> indexMetadataQuery, IQueryable<TableMetadata> tableMetadataQuery)
		{
			indexMetadataQuery = indexMetadataQuery
				.Where((item) => tableMetadataQuery.Select((table) => table.TableObjectID).Contains(item.TableObjectID));
			
			return indexMetadataQuery;
		}

		public static IQueryable<IndexMetadata> GetIndexMetadataInTable(this IQueryable<IndexMetadata> indexMetadataQuery, int tableObjectID)
		{
			indexMetadataQuery = indexMetadataQuery
				.Where((item) => item.TableObjectID == tableObjectID);

			return indexMetadataQuery;
		}

		public static IQueryable<IndexMetadata> GetIndexMetadataInDataSpace(this IQueryable<IndexMetadata> indexMetadataQuery, int dataSpaceID)
		{
			indexMetadataQuery = indexMetadataQuery
				.Where((item) => item.DataSpaceID == dataSpaceID);

			return indexMetadataQuery;
		}

		public static IQueryable<Index> AsIndexes(this IQueryable<IndexMetadata> indexMetadataQuery)
		{
			return indexMetadataQuery
				.Select((item) => new Index
				{
					TableObjectID = item.TableObjectID,
					IndexID = item.IndexID,
					IndexName = item.IndexName,
					IndexType = item.IndexType.GetIndexType()
				});
		}
	}
}
