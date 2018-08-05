using SQLServer.Data.Entities;
using SQLServer.Data.Extensions;
using SQLServer.Data.Metadata.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Manager
{
	public static class DataSpaceMetadataManagerExtensions
	{
		public static List<DataSpace> RetrieveDataSpaceEntities(this MetadataManager metadataManager, string dbConnString)
		{
			var dataSpaces = new List<DataSpace>();
			using(var dbContext = new MetadataDbContext(dbConnString))
			{
				var dataSpacesMetadata = dbContext
					.GetDataSpaceMetadata()
					.ToList();
				var dataFilesMetadata = dbContext
					.GetDatabaseFileMetdata()
					.ToList();

				var indexMetadata = dbContext
					.GetIndexMetadata()
					.GetIndexMetadataInTables(dbContext.GetTableMetadata())
					.ToList();
				var columnMetadata = dbContext
					.GetColumnMetadata()
					.AsQueryable()
					.GetColumnsInIndexColumns(dbContext.GetIndexColumnMetadata())
					.ToList();
				var indexColumnMetadata = dbContext
					.GetIndexColumnMetadata()
					.ToList();

				foreach(var dataSpaceMetadata in dataSpacesMetadata)
				{
					DataSpace dataSpaceEntity = null;
					if(dataSpaceMetadata.Type.GetDataSpaceType() == DataSpace.Type.FileGroup)
					{
						dataSpaceEntity = new FileGroup
						{
							Name = dataSpaceMetadata.Name,
							DataSpaceID = dataSpaceMetadata.DataSpaceID
						};

						var dataFiles = dataFilesMetadata
							.AsQueryable()
							.GetDatabaseFileMetadataInDataSpace(dataSpaceEntity.DataSpaceID);
						dataSpaceEntity.DataFiles = dataFiles
							.AsDataFiles()
							.ToList();

						var indexes = indexMetadata
							.AsQueryable()
							.GetIndexMetadataInDataSpace(dataSpaceEntity.DataSpaceID)
							.AsIndexes()
							.ToList();
						dataSpaceEntity.Indexes = indexes;
					}
					dataSpaces.Add(dataSpaceEntity);
				}
			}

			return dataSpaces;
		}
	}
}
