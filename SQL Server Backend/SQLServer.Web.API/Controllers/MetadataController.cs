using Microsoft.AspNetCore.Mvc;
using SQLServer.Data.Metadata.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Web.API.Controllers
{
	[Route("api/metadata")]
	public class MetadataController : Controller
	{
		private MetadataManager metadataManager;

		public MetadataController(MetadataManager metadataManager)
		{
			this.metadataManager = metadataManager;
		}

		[Route("tables")]
		[HttpGet]
		public IActionResult GetTables()
		{
			var tables = metadataManager.RetrieveTableEntities("Server=DESKTOP-DIU834E\\SQLEXPRESS;Database=TaccomStrike;User Id=Thomas_Han;Password=159789Qaz");
			return Json(tables);
		}
	}
}
