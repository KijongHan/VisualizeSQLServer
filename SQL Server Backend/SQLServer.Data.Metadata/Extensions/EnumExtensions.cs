using SQLServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SQLServer.Data.Extensions
{
	public static class EnumExtensions
	{
		public static SqlDbType GetDbType(this byte systemTypeID)
		{
			if(systemTypeID == 34)
			{
				return SqlDbType.Image;
			}
			if (systemTypeID == 35)
			{
				return SqlDbType.Text;
			}
			if (systemTypeID == 36)
			{
				return SqlDbType.UniqueIdentifier;
			}
			if (systemTypeID == 40)
			{
				return SqlDbType.Date;
			}
			if (systemTypeID == 41)
			{
				return SqlDbType.Time;
			}
			if (systemTypeID == 42)
			{
				return SqlDbType.DateTime2;
			}
			if (systemTypeID == 43)
			{
				return SqlDbType.DateTimeOffset;
			}
			if (systemTypeID == 48)
			{
				return SqlDbType.TinyInt;
			}
			if (systemTypeID == 52)
			{
				return SqlDbType.SmallInt;
			}
			if (systemTypeID == 56)
			{
				return SqlDbType.Int;
			}
			if (systemTypeID == 58)
			{
				return SqlDbType.SmallDateTime;
			}
			if (systemTypeID == 59)
			{
				return SqlDbType.Real;
			}
			if (systemTypeID == 60)
			{
				return SqlDbType.Money;
			}
			if (systemTypeID == 61)
			{
				return SqlDbType.DateTime;
			}
			if (systemTypeID == 62)
			{
				return SqlDbType.Float;
			}
			if (systemTypeID == 99)
			{
				return SqlDbType.NText;
			}
			if (systemTypeID == 104)
			{
				return SqlDbType.Bit;
			}
			if (systemTypeID == 106)
			{
				return SqlDbType.Decimal;
			}
			if (systemTypeID == 122)
			{
				return SqlDbType.SmallMoney;
			}
			if (systemTypeID == 127)
			{
				return SqlDbType.BigInt;
			}
			if (systemTypeID == 165)
			{
				return SqlDbType.VarBinary;
			}
			if (systemTypeID == 167)
			{
				return SqlDbType.VarChar;
			}
			if (systemTypeID == 173)
			{
				return SqlDbType.Binary;
			}
			if (systemTypeID == 175)
			{
				return SqlDbType.Char;
			}
			if (systemTypeID == 189)
			{
				return SqlDbType.Timestamp;
			}
			if (systemTypeID == 231)
			{
				return SqlDbType.NVarChar;
			}
			return SqlDbType.Xml;
		}

		public static IndexType GetIndexType(this byte indexType)
		{
			if (indexType == 0)
			{
				return IndexType.Heap;
			}
			if (indexType == 1)
			{
				return IndexType.Clustered;
			}
			return IndexType.NonClustered;
		}

		public static KeyType GetKeyType(this string keyType)
		{
			if(keyType == "UQ")
			{
				return KeyType.UniqueKey;
			}
			if(keyType == "PK")
			{
				return KeyType.PrimaryKey;
			}
			throw new Exception($"Not recognized key type {keyType}");
		}

		public static DataSpace.Type GetDataSpaceType(this string dataSpaceType)
		{
			if(dataSpaceType == "FG")
			{
				return DataSpace.Type.FileGroup;
			}
			if (dataSpaceType == "PS")
			{
				return DataSpace.Type.PartitionScheme;
			}
			throw new Exception($"Not recognized data space type {dataSpaceType}");
		}

		public static DatabaseFile.Type GetDatabaseFileType(this byte databaseFileType)
		{
			if(databaseFileType == 0)
			{
				return DatabaseFile.Type.Data;
			}
			if(databaseFileType == 1)
			{
				return DatabaseFile.Type.Log;
			}
			throw new Exception($"Not recognized database file type {databaseFileType}");
		}
	}
}
