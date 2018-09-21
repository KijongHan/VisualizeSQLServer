using Microsoft.EntityFrameworkCore;
using SQLServer.Data.Metadata.Definitions;
using SQLServer.Data.Metadata.Entities.Definitions;
using System;
using System.Collections.Generic;
using System.Data;
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
		private string ConnectionString;

		public MetadataDbContext(string connString) : base(
			new DbContextOptionsBuilder()
			.UseSqlServer(connString)
			.Options
			)
		{
			ConnectionString = connString;
		}
		
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

		public List<DataPageMetadata> GetDataPageMetadata(short fileID, int pageID)
		{
			var data = new List<DataPageMetadata>();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				var sqlCommand = new SqlCommand($"DBCC PAGE(@database, @fileID, @pageID,3) WITH TABLERESULTS", connection);
				sqlCommand.Parameters.AddWithValue("@database", connection.Database);
				sqlCommand.Parameters.AddWithValue("@fileID", fileID);
				sqlCommand.Parameters.AddWithValue("@pageID", pageID);

				using (var reader = sqlCommand.ExecuteReader())
				{
					while (reader.Read())
					{
						string parentObjectName = null;
						string objectColumn = null;
						string field = null;
						string value = null;

						try
						{
							parentObjectName = reader.GetString(0);
						}
						catch (InvalidCastException e) { }
						try
						{
							objectColumn = reader.GetString(1);
						}
						catch (InvalidCastException e) { }
						try
						{
							field = reader.GetString(2);
						}
						catch (InvalidCastException e) { }
						try
						{
							value = reader.GetString(3);
						}
						catch (InvalidCastException e) { }

						var raw = new DataPageMetadata
						{
							ParentObjectName = parentObjectName,
							Object = objectColumn,
							Field = field,
							Value = value
						};
						data.Add(raw);
					}
				}
			}
			return data;
		}

		public List<IndexPageIntermediateMetadata> GetIndexPageIntermediateMetadata(short fileID, int pageID)
		{
			var data = new List<IndexPageIntermediateMetadata>();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				var sqlCommand = new SqlCommand($"DBCC PAGE(@database, @fileID, @pageID,3) WITH TABLERESULTS", connection);
				sqlCommand.Parameters.AddWithValue("@database", connection.Database);
				sqlCommand.Parameters.AddWithValue("@fileID", fileID);
				sqlCommand.Parameters.AddWithValue("@pageID", pageID);

				using (var reader = sqlCommand.ExecuteReader())
				{
					if(!reader.NextResult())
					{
						return null;
					}

					while (reader.Read())
					{
						string keyHashValue = null;
						string key = null;
						try
						{
							keyHashValue = (string)reader.GetValue(7);
						}
						catch(InvalidCastException e) { }
						try
						{
							key = (string)reader.GetValue(6);
						}
						catch (InvalidCastException e) { }


						var formatted = new IndexPageIntermediateMetadata
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
			return data;
		}

		public List<IndexPageLeafMetadata> GetIndexPageLeafMetadata(short fileID, int pageID)
		{
			var data = new List<IndexPageLeafMetadata>();
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				var sqlCommand = new SqlCommand($"DBCC PAGE(@database, @fileID, @pageID,3) WITH TABLERESULTS", connection);
				sqlCommand.Parameters.AddWithValue("@database", connection.Database);
				sqlCommand.Parameters.AddWithValue("@fileID", fileID);
				sqlCommand.Parameters.AddWithValue("@pageID", pageID);

				using (var reader = sqlCommand.ExecuteReader())
				{
					if (!reader.NextResult())
					{
						return null;
					}

					List<Tuple<string, int>> nameOrdinalPairs = null;
					while (reader.Read())
					{
						if (nameOrdinalPairs == null)
						{
							nameOrdinalPairs = new List<Tuple<string, int>>();

							int ordinal = 0;
							while(ordinal < reader.FieldCount)
							{
								var name = reader.GetName(ordinal);
								if(name.Contains("(key)"))
								{
									nameOrdinalPairs.Add(new Tuple<string, int>(name, ordinal));
								}
							}
						}
						var formatted = new IndexPageLeafMetadata
						{
							FileID = (short)reader.GetValue(0),
							PageID = (int)reader.GetValue(1),
							Row = (short)reader.GetValue(2),
							Level = (short)reader.GetValue(3)
						};

						formatted.KeyColumns = nameOrdinalPairs
							.Select((pair) =>
							{
								var key = pair.Item1;
								string value = null;

								try
								{
									value = (string)reader.GetValue(pair.Item2);
								}
								catch (InvalidCastException e) { }
								
								return new Tuple<string, string>(key, value);
							})
							.ToList();
						data.Add(formatted);
					}
				}
			}
			return data;
		}

		public List<DataPagesMetadata> GetDataPagesMetadata(string table, string schema)
		{
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				var schemaTable = $"{schema}.{table}";
				var database = connection.Database;

				var sqlDefinition = "DBCC IND({0},{1},-1)";
				return DataPagesMetadata
					.FromSql(sqlDefinition, database, schemaTable)
					.ToList();
			}
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
