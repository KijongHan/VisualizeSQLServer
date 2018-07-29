using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class ForeignKey
	{
		public int ObjectID { get; set; }

		public int TableObjectID { get; set; }

		public int ReferencedObjectID { get; set; }

		public List<Column> Columns { get; set; }

		public List<Column> ReferencedColumns { get; set; }
	}
}
