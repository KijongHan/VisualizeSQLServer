using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQLServer.Data.Metadata.Entities.Definitions
{
	public class IndexPageLeafMetadata : IndexPageMetadata
	{
		public List<Tuple<string, string>> KeyColumns { get; set; }
	}
}
