using Microsoft.EntityFrameworkCore;
using SQLServer.Data.Metadata.Manager;
using System;
using System.Linq;

namespace SQLServer.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var sqlServerMetadataManager = new MetadataManager();
			sqlServerMetadataManager.RetrieveTableEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
			sqlServerMetadataManager.RetrieveDataSpaceEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
		}
	}
}

