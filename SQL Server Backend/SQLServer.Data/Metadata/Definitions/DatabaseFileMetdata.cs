using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[SQLServerMetadata("SQLServer.Data.Metadata.SQL.DatabaseFileMetdata.sql")]
	public class DatabaseFileMetdata
	{
		[Column("file_id")]
		public int FileID { get; set; }

		[Column("type")]
		public string Type { get; set; }

		[Column("type_desc")]
		public string TypeDescription { get; set; }

	}
}
