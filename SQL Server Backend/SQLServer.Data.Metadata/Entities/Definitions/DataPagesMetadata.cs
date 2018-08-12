using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Entities.Definitions
{
	public class DataPagesMetadata
	{
		[Column("PageFID")]
		public short FileID { get; set; }

		[Column("PagePID")]
		public int PageID { get; set; }

		[Column("IAMFID")]
		public short? IAMFileID { get; set; }

		[Column("IAMPID")]
		public int? IAMPageID { get; set; }

		[Column("ObjectID")]
		public int ObjectID { get; set; }

		[Column("IndexID")]
		public int IndexID { get; set; }

		[Column("PartitionNumber")]
		public int PartitionNumber { get; set; }

		[Column("PageType")]
		public short PageType { get; set; }

		[Column("IndexLevel")]
		public short? IndexLevel { get; set; }

		[Column("NextPageFID")]
		public short? NextPageFileID { get; set; }

		[Column("NextPagePID")]
		public int? NextPagePageID { get; set; }

		[Column("PrevPageFID")]
		public short? PreviousPageFileID { get; set; }

		[Column("PrevPagePID")]
		public int? PreviousPagePageID { get; set; }
	}
}