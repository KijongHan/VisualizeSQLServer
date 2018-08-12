using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public abstract class DatabasePage
	{
		public DatabasePage Next { get; set; }

		public DatabasePage Prev { get; set; }

		public int PageID { get; set; }

		public int IndexID { get; set; }

		public int TableObjectID { get; set; }

		public int DatabaseFileID { get; set; }
	}
}
