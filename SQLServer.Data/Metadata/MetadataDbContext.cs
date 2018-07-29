using Microsoft.EntityFrameworkCore;
using SQLServer.Data.Metadata.Definitions;
using System;
using System.Collections.Generic;
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
