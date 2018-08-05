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
			var sqlServerMetadataManager = new MetadataManager();
			var tables = sqlServerMetadataManager.RetrieveTableEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
			var dataspaces = sqlServerMetadataManager.RetrieveDataSpaceEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
		}
	}
}
