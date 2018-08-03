using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.SQL.IndexMetadata.sql")]
	public class IndexMetadata
	{
		[Column("object_id")]
		public int TableObjectID { get; set; }

		[Column("name")]
		public string IndexName { get; set; }

		[Column("index_id")]
		public int IndexID { get; set; }

		[Column("type")]
		public byte IndexType { get; set; }

		[Column("type_desc")]
		public string TypeDesc { get; set; }

		[Column("is_unique")]
		public bool IsUnique { get; set; }

		[Column("is_primary_key")]
		public bool IsPrimaryKey { get; set; }
	}
}
