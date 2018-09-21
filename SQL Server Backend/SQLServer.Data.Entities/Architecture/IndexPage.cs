﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class IndexPage : DatabasePage
	{
		public override Type DatabasePageType
		{
			get
			{
				return DatabasePage.Type.Index;
			}
		}

		public List<DatabasePage> Children { get; set; }
	}
}
