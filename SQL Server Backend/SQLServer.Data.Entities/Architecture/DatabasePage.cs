using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public abstract class DatabasePage
	{
		public abstract DatabasePage.Type DatabasePageType { get; }

		public int? NextPageID { get; set; }
		public DatabasePage NextPage { get; set; }

		public int? PrevPageID { get; set; }
		public DatabasePage PrevPage { get; set; }

		public int PageID { get; set; }

		public short FileID { get; set; }

		public int IndexID { get; set; }

		public int TableObjectID { get; set; }

		public enum Type
		{
			Data, IAM, Index
		}
	}
}
