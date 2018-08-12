using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class DataFile : DatabaseFile
	{
		public override Type DatabaseFileType
		{
			get
			{
				return DatabaseFile.Type.Data;
			}
		}

		public int FileID { get; set; }

		public int DataSpaceID { get; set; }

		public string LogicalName { get; set; }

		public string PhysicalName { get; set; }
	}
}
