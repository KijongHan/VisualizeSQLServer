using Microsoft.EntityFrameworkCore;
using SQLServer.Data.Manager;
using SQLServer.Data.Metadata;
using System;
using System.Linq;

namespace SQLServer.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var sqlServerMetadataManager = new SQLServerMetadataManager();
			sqlServerMetadataManager.RetrieveTableEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
		}
	}
}

