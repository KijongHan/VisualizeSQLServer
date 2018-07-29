using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class Column
	{
		public int TableObjectID { get; set; }

		public int ColumnID { get; set; }

		public SqlDbType ColumnType { get; set; }

		public string ColumnName { get; set; }

		public object ColumnValue { get; set; }

		public int ColumnOrdinalPosition { get; set; }

		public bool IsIdentity { get; set; }

		public int MaxLength { get; set; }

		public int Precision { get; set; }

		public int Scale { get; set; }
	}
}
