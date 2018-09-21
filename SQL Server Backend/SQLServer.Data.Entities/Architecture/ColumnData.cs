using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class ColumnData
	{
		public string ColumnName { get; set; }

		public object RawColumnData { get; set; }
	}
}
