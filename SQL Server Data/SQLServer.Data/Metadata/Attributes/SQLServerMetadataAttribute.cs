using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Metadata.Definitions
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class SQLServerMetadataAttribute : Attribute
	{
		public string SQLDefinitionResource { get; set; }

		public SQLServerMetadataAttribute(string sqlDefinitionResource)
		{
			SQLDefinitionResource = sqlDefinitionResource;
		}
	}
}
