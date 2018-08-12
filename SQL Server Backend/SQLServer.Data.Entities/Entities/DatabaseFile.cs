using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public abstract class DatabaseFile
	{
		public abstract DatabaseFile.Type DatabaseFileType { get; }

		public enum Type
		{
			Data, Log
		}
	}
}
