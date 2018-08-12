using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class LinkedList<T>
	{
		public int Count { get; private set; }

		private LinkedListItem<T> root;

		private LinkedListItem<T> last;

		public LinkedList()
		{
			root = null;
			last = null;
			Count = 0;
		}

		public void Add(T item)
		{
			var newLinkedListItem = new LinkedListItem<T>
			{
				Prev = last,
				Item = item
			};

			if (Count == 0)
			{
				root = newLinkedListItem;
			}
			if (last != null)
			{
				last.Next = newLinkedListItem;
			}

			last = newLinkedListItem;
			Count += 1;
		}
		
		public T Get(int index)
		{
			if(index >= Count)
			{
				throw new IndexOutOfRangeException();
			}

			int i = 0;
			var item = root;
			while(i < index)
			{
				item = item.Next;
				i++;
			}
			return item.Item;
		}
	}

	public class LinkedListItem<T>
	{
		public LinkedListItem<T> Next { get; set; }

		public LinkedListItem<T> Prev { get; set; }

		public T Item { get; set; }
	}
}
