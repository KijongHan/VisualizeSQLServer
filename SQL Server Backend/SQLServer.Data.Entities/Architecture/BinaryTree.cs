using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class BinaryTree<T>
	{
		private BinaryTreeNode<T> root;
	}

	public class BinaryTreeNode<T>
	{
		public T Item { get; set; }

		public List<BinaryTreeNode<T>> Children { get; set; }

		public BinaryTreeNode<T> Parent { get; set; }
	}
}
