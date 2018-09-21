using Microsoft.AspNetCore.Mvc;
using SQLServer.Data.Metadata;
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
		public IActionResult GetTables(string connString)
		{
			if (string.IsNullOrEmpty(connString))
			{
				return StatusCode(404);
			}

			using (var dbContext = new MetadataDbContext(connString))
			{
				var tables = metadataManager.RetrieveTableEntities(dbContext);
				return Json(tables);
			}
		}

		[Route("dataspaces")]
		[HttpGet]
		public IActionResult GetDataSpaces(string connString)
		{
			if (string.IsNullOrEmpty(connString))
			{
				return StatusCode(404);
			}

			using (var dbContext = new MetadataDbContext(connString))
			{
				var dataSpaces = metadataManager.RetrieveDataSpaceEntities(dbContext);
				return Json(dataSpaces);
			}
		}

		[Route("indexarchitecture")]
		[HttpGet]
		public IActionResult GetIndexArchitecture(string tableName, string schemaName, int indexID)
		{

		}
	}
}
