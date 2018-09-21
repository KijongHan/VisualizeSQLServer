using SQLServer.Data.Entities;
using SQLServer.Data.Entities.Architecture;
using SQLServer.Data.Extensions;
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
			string tableName, 
			string schema, 
			Index index,
			MetadataDbContext dbContext)
		{
			var pagesMetadata = dbContext
					.GetDataPagesMetadata(tableName, schema)
					.Where((item) => item.IndexID == index.IndexID);
			var indexPagesMetadata = pagesMetadata
				.Where((item) => item.PageType.GetDatabasePageType() == DatabasePage.Type.Index);
			var dataPagesMetadata = pagesMetadata
				.Where((item) => item.PageType.GetDatabasePageType() == DatabasePage.Type.Data);

			if (index.IndexType == IndexType.Clustered)
			{
				var pagefileIndexpagePairs = indexPagesMetadata
					.Select((item) =>
					{
						return new IndexPage()
						{
							PageID = item.PageID,
							FileID = item.FileID,
							IndexID = item.IndexID,
							TableObjectID = item.ObjectID,
							NextPageID = item.NextPagePageID,
							PrevPageID = item.PreviousPagePageID
						};
					})
					.ToDictionary((keySelector) => keySelector.GetDatabasePageUniqueKey());
				var indexPages = new List<IndexPage>();

				foreach(var pagefileIndexpagePair in pagefileIndexpagePairs)
				{
					var indexPage = pagefileIndexpagePair.Value;
					if(indexPage.NextPageID.GetValueOrDefault() > 0)
					{
						indexPage.NextPage = pagefileIndexpagePairs[indexPage.GetDatabasePageUniqueKey()];
					}
					if (indexPage.PrevPageID.GetValueOrDefault() > 0)
					{
						indexPage.PrevPage = pagefileIndexpagePairs[indexPage.GetDatabasePageUniqueKey()];
					}
					indexPages.Add(indexPage);
				}

				var pagefileDatapagePairs = dataPagesMetadata
					.Select((item) =>
					{
						return new DataPage()
						{
							PageID = item.PageID,
							FileID = item.FileID,
							IndexID = item.IndexID,
							TableObjectID = item.ObjectID,
							NextPageID = item.NextPagePageID,
							PrevPageID = item.PreviousPagePageID
						};
					})
					.ToDictionary((keySelector) => keySelector.GetDatabasePageUniqueKey());
				var dataPages = new List<DataPage>();

				foreach (var pagefileDatapagePair in pagefileDatapagePairs)
				{
					var dataPage = pagefileDatapagePair.Value;
					if (dataPage.NextPageID.GetValueOrDefault() > 0)
					{
						dataPage.NextPage = pagefileDatapagePairs[dataPage.GetDatabasePageUniqueKey()];
					}
					if (dataPage.PrevPageID.GetValueOrDefault() > 0)
					{
						dataPage.PrevPage = pagefileDatapagePairs[dataPage.GetDatabasePageUniqueKey()];
					}
					dataPages.Add(dataPage);
				}

				foreach (var indexPage in indexPages)
				{
					var indexPageMetadata = dbContext.GetIndexPageIntermediateMetadata(indexPage.FileID, indexPage.PageID);
					
					var childrenPages = new List<DatabasePage>();
					indexPageMetadata.ForEach((item) =>
					{
						var childrenPageKey = item.GetDatabasePageUniqueKey();
						if (pagefileIndexpagePairs.ContainsKey(childrenPageKey))
						{
							childrenPages.Add(pagefileIndexpagePairs[childrenPageKey]);
						}
						if (pagefileDatapagePairs.ContainsKey(childrenPageKey))
						{
							childrenPages.Add(pagefileDatapagePairs[childrenPageKey]);
						}
					});
					indexPage.Children = childrenPages;
				}

				var tableColumns = new HashSet<string>();
				dbContext
					.GetColumnMetadata()
					.Where((item) => item.TableObjectID == index.TableObjectID)
					.ToList()
					.ForEach((item) =>
					{
						tableColumns.Add(item.ColumnName);
					});

				var rowDatas = new List<RowData>();
				var columnDatas = new List<ColumnData>();
				foreach (var dataPage in dataPages)
				{
					var datapageMetadata = dbContext.GetDataPageMetadata(dataPage.FileID, dataPage.PageID);
					
					datapageMetadata.ForEach((item) =>
					{
						if(item.Field == "KeyHashValue")
						{
							var rowData = new RowData()
							{
								ColumnDatas = columnDatas,
								KeyHashValue = item.Value
							};
							rowDatas.Add(rowData);
							columnDatas = new List<ColumnData>();
						}
						else if(tableColumns.Contains(item.Field))
						{
							var columnData = new ColumnData()
							{
								ColumnName = item.Field,
								RawColumnData = item.Value
							};
							columnDatas.Add(columnData);
						}
					});

					dataPage.RowDatas = rowDatas;
				}

				var indexPagesSet = new HashSet<IndexPage>();
				indexPages.ForEach((item) =>
				{
					indexPagesSet.Add(item);
				});
				var dataPagesSet = new HashSet<DataPage>();
				dataPages.ForEach((item) =>
				{
					dataPagesSet.Add(item);
				});

				return new ClusteredIndexArchitecture()
				{
					IndexPages = indexPagesSet,
					DataPages = dataPagesSet
				};
			}
			else if (index.IndexType == IndexType.Heap)
			{
				return null;
			}
			else if (index.IndexType == IndexType.NonClustered)
			{
				return null;
			}
			else
			{
				throw new Exception();
			}
		}

		public static IndexArchitecture RetrieveIndexArchitecture(
			this MetadataManager metadataManager,
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
				tableName,
				schema,
				index,
				dbContext);
		}
	}
}
