using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Entities.Definitions
{
	public class DataPageRawMetadata
	{
		[Column("ParentObject")]
		public string ParentObjectName { get; set; }

		[Column("Object")]
		public string Object { get; set; }

		[Column("Field")]
		public string Field { get; set; }

		[Column("VALUE")]
		public string Value { get; set; }
	}
}
