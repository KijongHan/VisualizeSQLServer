using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Entities.Definitions
{
	public class IndexPageMetadata
	{
		[Column("FileId")]
		public short FileID { get; set; }

		[Column("PageId")]
		public int PageID { get; set; }

		[Column("Row")]
		public int Row { get; set; }

		[Column("Level")]
		public int Level { get; set; }

		[Column("KeyHashValue")]
		public string KeyHashValue { get; set; }

		[Column("Row Size")]
		public short RowSize { get; set; }
	}
}
