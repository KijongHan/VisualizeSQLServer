using SQLServer.Data.Metadata.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata
{
	[SQLServerMetadata("SQLServer.Data.Metadata.Entities.SQL.TableMetadata.sql")]
	public class TableMetadata
	{
		[Column("object_id")]
		public int TableObjectID { get; set; }

		[Column("name")]
		public string TableName { get; set; }
		 
		[Column("schema_id")]
		public int SchemaID { get; set; }

		[Column("schema_name")]
		public string SchemaName { get; set; }
	}
}
