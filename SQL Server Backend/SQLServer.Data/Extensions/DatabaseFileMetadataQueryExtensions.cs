using SQLServer.Data.Entities;
using SQLServer.Data.Extensions;
using SQLServer.Data.Metadata.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class DatabaseFileMetadataQueryExtensions
	{
		public static IQueryable<DatabaseFileMetdata> GetDatabaseFileMetadataInDataSpace(this IQueryable<DatabaseFileMetdata> databaseFileMetadata, int dataSpaceID)
		{
			databaseFileMetadata = databaseFileMetadata
				.Where((item) => item.DataSpaceID == dataSpaceID);

			return databaseFileMetadata;
		}

		public static IQueryable<DatabaseFileMetdata> GetDataFileMetadata(this IQueryable<DatabaseFileMetdata> databaseFileMetadata)
		{
			databaseFileMetadata = databaseFileMetadata
				.Where((item) => item.Type.GetDatabaseFileType() == DatabaseFile.Type.Data);

			return databaseFileMetadata;
		}

		public static IQueryable<DatabaseFileMetdata> GetLogFileMetadata(this IQueryable<DatabaseFileMetdata> databaseFileMetadata)
		{
			databaseFileMetadata = databaseFileMetadata
				.Where((item) => item.Type.GetDatabaseFileType() == DatabaseFile.Type.Log);

			return databaseFileMetadata;
		}

		public static IQueryable<DatabaseFile> AsEntities(this IQueryable<DatabaseFileMetdata> databaseFileMetadata)
		{
			var entities = databaseFileMetadata
				.Select((item) =>
					new DatabaseFile
					{

					}
				);
			return entities;
		}

		public static IQueryable<DataFile> AsDataFiles(this IQueryable<DatabaseFileMetdata> databaseFileMetadata)
		{
			var entities = databaseFileMetadata
				.GetDataFileMetadata()
				.Select((item) =>
					new DataFile
					{

					}
				);
			return entities;
		}
	}
}
