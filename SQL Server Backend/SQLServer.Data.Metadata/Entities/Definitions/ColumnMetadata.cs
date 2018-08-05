using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.ColumnMetadata.sql")]
	public class ColumnMetadata
	{
		[Column("object_id")]
		public int TableObjectID { get; set; }

		[Column("column_id")]
		public int ColumnID { get; set; }

		[Column("name")]
		public string ColumnName { get; set; }

		[Column("column_id")]
		public int ColumnPosition { get; set; }

		[Column("system_type_id")]
		public byte SystemTypeID { get; set; }

		[Column("max_length")]
		public short MaxLength { get; set; }

		[Column("precision")]
		public byte Precision { get; set; }

		[Column("scale")]
		public byte Scale { get; set; }

		[Column("is_nullable")]
		public bool IsNullable { get; set; }

		[Column("is_identity")]
		public bool IsIdentity { get; set; }
	}
}
