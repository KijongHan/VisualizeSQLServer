using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLServer.Data.Metadata;
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
			using(var dbContext = new MetadataDbContext(connstring))
			{
				var tables = sqlServerMetadataManager.RetrieveTableEntities(dbContext);
				var dataspaces = sqlServerMetadataManager.RetrieveDataSpaceEntities(dbContext);
				var indexArchitecture = sqlServerMetadataManager.RetrieveIndexArchitecture("MyTable2", "test", 1, dbContext);
			}
		}
	}
}
