using SQLServer.Data.Entities;
using SQLServer.Data.Extensions;
using SQLServer.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SQLServer.Data.Metadata.Manager
{
	public static class DbObjectMetadataManagerExtensions
	{
		public static List<Table> RetrieveTableEntities(this MetadataManager metadataManager, string dbConnString)
		{
			var tables = new List<Table>();
			using (var dbContext = new MetadataDbContext(dbConnString))
			{
				var tableMetadataList = dbContext.GetTableMetadata().ToList();
				var columnMetadataList = dbContext.GetColumnMetadata().ToList();
				var foreignKeyMetadataList = dbContext.GetForeignKeyMetadata().ToList();
				var foreignKeyColumnMetadataList = dbContext.GetForeignKeyColumnMetadata().ToList();
				var indexMetadataList = dbContext.GetIndexMetadata().ToList();
				var indexColumnMetadataList = dbContext.GetIndexColumnMetadata().ToList();
				var keyMetadataList = dbContext.GetKeyMetadata().ToList();

				var columns = new List<Column>();
				foreach(var column in columnMetadataList)
				{
					var indexColumn = indexColumnMetadataList.Where((item) => item.ColumnID == column.ColumnID && item.TableObjectID == column.TableObjectID).FirstOrDefault();
					if (indexColumn != null)
					{
						columns.Add(new IndexColumn()
						{
							TableObjectID = column.TableObjectID,
							ColumnID = column.ColumnID,
							ColumnName = column.ColumnName,
							ColumnOrdinalPosition = column.ColumnPosition,
							IndexID = indexColumn.IndexID,
							IndexColumnID = indexColumn.IndexColumnID,
							IndexOrdinal = indexColumn.IndexOrdinal,
							ColumnType = column.SystemTypeID.GetDbType()
						});
					}
					else
					{
						columns.Add(new Column()
						{
							TableObjectID = column.TableObjectID,
							ColumnID = column.ColumnID,
							ColumnName = column.ColumnName,
							ColumnOrdinalPosition = column.ColumnPosition,
							ColumnType = column.SystemTypeID.GetDbType()
						});
					}
				}

				var indexes = new List<Index>();
				foreach(var index in indexMetadataList)
				{
					var key = keyMetadataList.Where((item) => item.IndexID == index.IndexID && item.TableObjectID == index.TableObjectID).FirstOrDefault();
					if(key != null)
					{
						indexes.Add(new Key()
						{
							KeyObjectID = key.KeyObjectID,
							TableObjectID = key.TableObjectID,
							KeyType = key.Type.GetKeyType(),
							IndexID = key.IndexID,
							IndexName = index.IndexName,
							IndexType = index.IndexType.GetIndexType(),
							IndexColumns = columns.GetIndexColumns(index.IndexID, index.TableObjectID)
						});
					}
					else
					{
						indexes.Add(new Index()
						{
							TableObjectID = index.TableObjectID,
							IndexID = index.IndexID,
							IndexName = index.IndexName,
							IndexType = index.IndexType.GetIndexType(),
							IndexColumns = columns.GetIndexColumns(index.IndexID, index.TableObjectID)
						});
					}
				}

				foreach (var tableMetadata in tableMetadataList)
				{
					var tableEntity = new Table
					{
						TableName = tableMetadata.TableName,
						TableObjectID = tableMetadata.TableObjectID,
						Schema = new Schema { SchemaName = tableMetadata.SchemaName, SchemaID = tableMetadata.SchemaID }
					};

					tableEntity.Columns = columns.GetColumns(tableMetadata.TableObjectID);
					tableEntity.Indexes = indexes.GetIndexes(tableMetadata.TableObjectID);
					tables.Add(tableEntity);
				}
			}

			return tables;
		}
	}
}
