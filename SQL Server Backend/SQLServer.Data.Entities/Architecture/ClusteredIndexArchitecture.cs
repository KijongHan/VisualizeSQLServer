using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class ClusteredIndexArchitecture : IndexArchitecture
	{
		public IAMPage IAMPage { get; set; }

		public HashSet<IndexPage> IndexPages { get; set; }

		public HashSet<DataPage> DataPages { get; set; }
	}
}
