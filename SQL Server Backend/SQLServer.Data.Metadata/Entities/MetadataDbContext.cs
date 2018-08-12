﻿using Microsoft.EntityFrameworkCore;
using SQLServer.Data.Metadata.Definitions;
using SQLServer.Data.Metadata.Entities.Definitions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SQLServer.Data.Metadata
{
	public class MetadataDbContext : DbContext
	{
		public MetadataDbContext(string connString) : base(
			new DbContextOptionsBuilder()
			.UseSqlServer(connString)
			.Options
			)
		{}

		public MetadataDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{}

		protected DbQuery<TableMetadata> TableMetadata { get; set; }
		protected DbQuery<ColumnMetadata> ColumnMetadata { get; set; }
		protected DbQuery<KeyMetadata> KeyMetadata { get; set; }
		protected DbQuery<IndexMetadata> IndexMetadata { get; set; }
		protected DbQuery<IndexColumnMetadata> IndexColumnMetadata { get; set; }
		protected DbQuery<ForeignKeyMetadata> ForeignKeyMetadata { get; set; }
		protected DbQuery<ForeignKeyColumnMetadata> ForeignKeyColumnMetadata { get; set; }
		
		protected DbQuery<DatabaseFileMetdata> DatabaseFileMetdata { get; set; }
		protected DbQuery<DataSpaceMetadata> DataSpaceMetadata { get; set; }

		protected DbQuery<DataPagesMetadata> DataPagesMetadata { get; set; }

		public List<DataPageRawMetadata> GetDataPageRawMetadata(string database, short fileID, int pageID, string dbConnectionString)
		{
			var data = new List<DataPageRawMetadata>();
			using (var connection = new SqlConnection(dbConnectionString))
			{
				connection.Open();
				var sqlCommand = new SqlCommand($"DBCC PAGE('{database}',{fileID},{pageID},3) WITH TABLERESULTS", connection);
				using (var reader = sqlCommand.ExecuteReader())
				{
					while (reader.Read())
					{
						var raw = new DataPageRawMetadata
						{
							ParentObjectName = reader.GetString(0),
							Object = reader.GetString(1),
							Field = reader.GetString(2),
							Value = reader.GetString(3)
						};
						data.Add(raw);
					}
				}
			}
			return data;
		}

		public List<DataPageFormattedMetadata> GetDataPageFormattedMetadata(string database, short fileID, int pageID, string dbConnectionString)
		{
			var data = new List<DataPageFormattedMetadata>();
			using (var connection = new SqlConnection(dbConnectionString))
			{
				connection.Open();
				var sqlCommand = new SqlCommand($"DBCC PAGE('{database}',{fileID},{pageID},3) WITH TABLERESULTS", connection);
				using (var reader = sqlCommand.ExecuteReader())
				{
					if (reader.NextResult())
					{
						while (reader.Read())
						{
							string keyHashValue = null;
							try
							{
								keyHashValue = (string)reader.GetValue(7);
							}
							catch (Exception e) { }

							string key = null;
							try
							{
								key = (string)reader.GetValue(6);
							}
							catch (Exception e) { }

							var formatted = new DataPageFormattedMetadata
							{
								FileID = (short)reader.GetValue(0),
								PageID = (int)reader.GetValue(1),
								Row = (short)reader.GetValue(2),
								Level = (short)reader.GetValue(3),
								ChildFileID = (short)reader.GetValue(4),
								ChildPageID = (int)reader.GetValue(5),
								Key = key,
								KeyHashValue = keyHashValue,
								RowSize = (short)reader.GetValue(8)
							};
							data.Add(formatted);
						}
					}
				}
			}

			return data;
		}

		public List<DataPagesMetadata> GetDataPagesMetadata(string database, string table, string schema)
		{
			var sqlDefinition = $"DBCC IND('{database}','{schema}.{table}',-1)";
			return DataPagesMetadata
				.FromSql(sqlDefinition)
				.ToList();
		}

		public IQueryable<DataSpaceMetadata> GetDataSpaceMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(DataSpaceMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return DataSpaceMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<DatabaseFileMetdata> GetDatabaseFileMetdata()
		{
			var sqlDefinition = getSQLDefinition(typeof(DatabaseFileMetdata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return DatabaseFileMetdata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<TableMetadata> GetTableMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(TableMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return TableMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<ColumnMetadata> GetColumnMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(ColumnMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return ColumnMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<IndexMetadata> GetIndexMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(IndexMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return IndexMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<IndexColumnMetadata> GetIndexColumnMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(IndexColumnMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return IndexColumnMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<KeyMetadata> GetKeyMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(KeyMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return KeyMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<ForeignKeyMetadata> GetForeignKeyMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(ForeignKeyMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return ForeignKeyMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		public IQueryable<ForeignKeyColumnMetadata> GetForeignKeyColumnMetadata()
		{
			var sqlDefinition = getSQLDefinition(typeof(ForeignKeyColumnMetadata).GetCustomAttribute<SQLServerMetadataAttribute>().SQLDefinitionResource);
			return ForeignKeyColumnMetadata
				.FromSql(sqlDefinition)
				.AsQueryable();
		}

		private string getSQLDefinition(string resourceName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string result = reader.ReadToEnd();
					result = Regex.Replace(result, @"\t|\n|\r", " ");
					return result;
				}
			}
		}
	}
}
