using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class RowData
	{
		public List<ColumnData> ColumnDatas { get; set; }

		public string KeyHashValue { get; set; }
	}
}
