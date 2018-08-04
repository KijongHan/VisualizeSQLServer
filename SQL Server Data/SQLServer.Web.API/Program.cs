using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SQLServer.Web.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			if(args!=null && args.Length==2)
			{
				var baseAddress = args[0];
				var port = args[1];
				var url = $"{baseAddress}:{port}";

				var host = new WebHostBuilder()
					.UseKestrel()
					.UseContentRoot(Directory.GetCurrentDirectory())
					.UseIISIntegration()
					.UseStartup<Startup>()
					.UseUrls(url)
					.Build();
				return host;
			}

			return WebHost.CreateDefaultBuilder()
				.UseStartup<Startup>()
				.Build();
		}
	}
}
