using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.SQL.KeyMetadata.sql")]
	public class KeyMetadata
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("object_id")]
		public int KeyObjectID { get; set; }

		[Column("parent_object_id")]
		public int TableObjectID { get; set; }

		[Column("type")]
		public string Type { get; set; }

		[Column("unique_index_id")]
		public int IndexID { get; set; }
	}
}
