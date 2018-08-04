using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Data.Entities
{
	public class Table
	{
		public int TableObjectID { get; set; }

		public string TableName { get; set; }

		public Key PrimaryKey
		{
			get
			{
				return Keys.Where((item) => item.KeyType==KeyType.PrimaryKey).FirstOrDefault();
			}
		}

		public List<Key> Keys
		{
			get
			{
				return Indexes
					.Where((item) => item.GetType() == typeof(Key))
					.Select((item) => (Key)item)
					.ToList();
			}
		}

		public Schema Schema { get; set; }

		public List<Column> Columns { get; set; }

		public List<Index> Indexes { get; set; }

		public List<ForeignKey> ForeignKeys { get; set; }

		public Table()
		{
			Schema = new Schema();
			Columns = new List<Column>();
			Indexes = new List<Index>();
			ForeignKeys = new List<ForeignKey>();
		}
	}
}
