using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.DatabaseFileMetadata.sql")]
	public class DatabaseFileMetdata
	{
		[Column("file_id")]
		public int FileID { get; set; }

		[Column("type")]
		public byte Type { get; set; }

		[Column("type_desc")]
		public string TypeDescription { get; set; }

		[Column("data_space_id")]
		public int DataSpaceID { get; set; }

		[Column("name")]
		public string LogicalName { get; set; }

		[Column("physical_name")]
		public string PhysicalName { get; set; }

		[Column("state")]
		public byte State { get; set; }

		[Column("state_desc")]
		public string StateDescription { get; set; }

		[Column("size")]
		public int Size { get; set; }

		[Column("max_size")]
		public int MaxSize { get; set; }

		[Column("growth")]
		public int Growth { get; set; }
	}
}
