using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.SQL.DataSpaceMetadata.sql")]
	public class DataSpaceMetadata
	{
		[Column("name")]
		public string Name { get; set; }

		[Column("data_space_id")]
		public int DataSpaceID { get; set; }

		[Column("type")]
		public string Type { get; set; }

		[Column("type_desc")]
		public string TypeDescription { get; set; }

		[Column("is_default")]
		public bool IsDefault { get; set; }

		[Column("is_system")]
		public bool IsSystem { get; set; }
	}
}
