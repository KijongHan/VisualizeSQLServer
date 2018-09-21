using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Entities.Definitions
{
	public class IndexPageIntermediateMetadata : IndexPageMetadata
	{
		[Column("ChildFileId")]
		public short ChildFileID { get; set; }

		[Column("ChildPageId")]
		public int ChildPageID { get; set; }
		
		public string Key { get; set; }
	}
}
