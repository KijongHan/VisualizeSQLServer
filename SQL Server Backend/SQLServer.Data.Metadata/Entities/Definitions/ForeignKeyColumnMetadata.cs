using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.ForeignKeyColumnMetadata.sql")]
	public class ForeignKeyColumnMetadata
	{
		[Column("constraint_object_id")]
		public int ForeignKeyObjectID { get; set; }

		[Column("constraint_column_id")]
		public int ForeignKeyColumnID { get; set; }

		[Column("parent_object_id")]
		public int TableObjectID { get; set; }

		[Column("parent_column_id")]
		public int TableColumnID { get; set; }

		[Column("referenced_object_id")]
		public int ReferencedTableObjectID { get; set; }

		[Column("referenced_column_id")]
		public int ReferencedTableColumnID { get; set; }
	}
}
