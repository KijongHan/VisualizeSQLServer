using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.ForeignKeyMetadata.sql")]
	public class ForeignKeyMetadata
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("object_id")]
		public int ForeignKeyObjectID { get; set; }

		[Column("parent_object_id")]
		public int TableObjectID { get; set; }

		[Column("referenced_object_id")]
		public int ReferencedTableObjectID { get; set; }

		[Column("key_index_id")]
		public int ReferencedKeyIndexID { get; set; }
	}
}
