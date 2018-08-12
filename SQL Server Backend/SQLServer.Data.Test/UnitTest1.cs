using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLServer.Data.Metadata.Manager;

namespace SQLServer.Data.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var connstring = "Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;Trusted_Connection=true;";
			var sqlServerMetadataManager = new MetadataManager();
			var tables = sqlServerMetadataManager.RetrieveTableEntities(connstring);
			var dataspaces = sqlServerMetadataManager.RetrieveDataSpaceEntities(connstring);
			sqlServerMetadataManager.RetrieveEntities(connstring);
		}
	}
}
