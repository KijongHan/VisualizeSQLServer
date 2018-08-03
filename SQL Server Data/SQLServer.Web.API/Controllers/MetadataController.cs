using Microsoft.AspNetCore.Mvc;
using SQLServer.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Web.API.Controllers
{
	[Route("api/metadata")]
	public class MetadataController : Controller
	{
		private SQLServerMetadataManager sqlServerMetadataManager;

		public MetadataController(SQLServerMetadataManager sqlServerMetadataManager)
		{
			this.sqlServerMetadataManager = sqlServerMetadataManager;
		}

		[Route("tables")]
		[HttpGet]
		public IActionResult GetTables()
		{
			var tables = sqlServerMetadataManager.RetrieveTableEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
			return Json(tables);
		}
	}
}
