using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class IndexColumn : Column
	{
		public int IndexID { get; set; }

		public bool IsDescending { get; set; }

		public int IndexColumnID { get; set; }

		public int IndexOrdinal { get; set; }
	}
}