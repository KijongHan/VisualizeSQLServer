using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Data.Entities.Architecture
{
	public class IAMPage : DatabasePage
	{
		public override Type DatabasePageType
		{
			get
			{
				return DatabasePage.Type.IAM;
			}
		}
	}
}
