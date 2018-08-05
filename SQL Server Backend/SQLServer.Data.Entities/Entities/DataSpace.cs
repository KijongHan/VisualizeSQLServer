using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public abstract class DataSpace
	{
		public string Name { get; set; }

		public int DataSpaceID { get; set; }

		public abstract DataSpace.Type DataSpaceType { get; }

		public abstract string Description { get; }

		public List<DataFile> DataFiles { get; set; }

		public List<Index> Indexes { get; set; }

		public enum Type
		{
			FileGroup, PartitionScheme
		}
	}
}
