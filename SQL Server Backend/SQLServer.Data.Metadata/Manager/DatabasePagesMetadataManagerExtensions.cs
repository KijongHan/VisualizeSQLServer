using SQLServer.Data.Entities;
using SQLServer.Data.Entities.Architecture;
using SQLServer.Data.Metadata.Entities.Definitions;
using SQLServer.Data.Metadata.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Manager
{
	public static class DatabasePagesMetadataManagerExtensions
	{
		public static IndexArchitecture RetrieveIndexArchitecture(
			this MetadataManager metadataManager, 
			string dbConnectionString, 
			string database, 
			string tableName, 
			string schema, 
			Index index,
			MetadataDbContext dbContext)
		{
			var dataPagesMetadata = dbContext
					.GetDataPagesMetadata(database, tableName, schema)
					.Where((item) => item.IndexID == index.IndexID);

			return null;
		}

		public static IndexArchitecture RetrieveIndexArchitecture(
			this MetadataManager metadataManager,
			string dbConnectionString,
			string database,
			string tableName,
			string schema,
			int indexID,
			MetadataDbContext dbContext)
		{
			var table = dbContext
				.GetTableMetadata()
				.GetTableMetadata(tableName, schema);

			var index = dbContext
				.GetIndexMetadata()
				.GetIndexMetadataInTables(table)
				.Where((item) => item.IndexID == indexID)
				.AsIndexes()
				.FirstOrDefault();

			return metadataManager.RetrieveIndexArchitecture(
				dbConnectionString,
				database,
				tableName,
				schema,
				index,
				dbContext);
		}
	}
}
