using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Entities
{
	public class LogFile : DatabaseFile
	{
		public override Type DatabaseFileType
		{
			get
			{
				return DatabaseFile.Type.Log;
			}
		}
	}
}
