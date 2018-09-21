using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class IndexArchitectureBuilder
	{
		private IndexType indexType;

		public IndexArchitectureBuilder SetIndexType(IndexType indexType)
		{
			this.indexType = indexType;
			return this;
		}

		public IndexArchitecture Build()
		{
			return null;
		}
	}
}
