using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class ClusteredIndexArchitecture : IndexArchitecture
	{
		public IAMPage RootNode { get; set; }

		public List<IndexPage> IntermediateNodes { get; set; }

		public List<DataPage> LeafNodes { get; set; }
	}
}
