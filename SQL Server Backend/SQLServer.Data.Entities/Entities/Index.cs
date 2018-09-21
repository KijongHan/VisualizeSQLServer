using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class Index
	{
		public string TableName { get; set; }

		public int TableObjectID { get; set; }

		public int IndexID { get; set; }

		public string IndexName { get; set; }

		public List<IndexColumn> IndexColumns { get; set; }

		public IndexType IndexType { get; set; }
	}

	public enum IndexType
	{
		Clustered, NonClustered, Heap
	}
}
