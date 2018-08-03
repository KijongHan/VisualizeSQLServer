using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities
{
	public class Key : Index
	{
		public int KeyObjectID { get; set; }
		
		public KeyType KeyType { get; set; }
	}

	public enum KeyType
	{
		PrimaryKey, UniqueKey
	}
}
