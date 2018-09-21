using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class DataPage : DatabasePage
	{
		public override Type DatabasePageType
		{
			get
			{
				return DatabasePage.Type.Data;
			}
		}

		public List<RowData> RowDatas { get; set; }
	}
}
