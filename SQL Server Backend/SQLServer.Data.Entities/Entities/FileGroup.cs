using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class FileGroup : DataSpace
	{
		public override Type DataSpaceType
		{
			get
			{
				return DataSpace.Type.FileGroup;
			}
		}
		public override string Description
		{
			get
			{
				return "ROWS_FILEGROUP";
			}
		}

		public int IsDefault { get; set; }


	}
}
