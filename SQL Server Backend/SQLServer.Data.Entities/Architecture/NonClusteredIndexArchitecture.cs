using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class NonClusteredIndexArchitecture : IndexArchitecture
	{
		public IAMPage RootNode { get; set; }

		public List<IndexPage> IntermediateNodes { get; set; }

		public List<IndexPage> LeafNodes { get; set; }
	}
}
