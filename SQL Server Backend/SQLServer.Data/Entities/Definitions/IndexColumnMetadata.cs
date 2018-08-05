using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.IndexColumnMetadata.sql")]
	public class IndexColumnMetadata
	{
		[Column("object_id")]
		public int TableObjectID { get; set; }

		[Column("index_id")]
		public int IndexID { get; set; }

		[Column("index_column_id")]
		public int IndexColumnID { get; set; }

		[Column("key_ordinal")]
		public byte IndexOrdinal { get; set; }

		[Column("column_id")]
		public int ColumnID { get; set; }

		[Column("is_descending_key")]
		public bool IsDescendingKey { get; set; }
	}
}
