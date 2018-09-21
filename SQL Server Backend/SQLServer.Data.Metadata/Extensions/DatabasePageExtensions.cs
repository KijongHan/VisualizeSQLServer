using SQLServer.Data.Entities.Architecture;
using SQLServer.Data.Metadata.Entities.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Metadata.Extensions
{
	public static class DatabasePageExtensions
	{
		public static string GetDatabasePageUniqueKey(this DatabasePage databasePage)
		{
			return $"{databasePage.PageID}_{databasePage.FileID}";
		}

		public static string GetDatabasePageUniqueKey(this IndexPageMetadata indexPage)
		{
			return $"{indexPage.PageID}_{indexPage.FileID}";
		}
	}
}
