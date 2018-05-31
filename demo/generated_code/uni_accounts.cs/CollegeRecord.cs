using System;
using Far.Core;

namespace Far.Data
{
	public class CollegeRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region Name 
		public class Name : IRecordField
		{
			public string DataField { get { return "Name"; } }
			public string SortExpression { get { return "Name"; } }
			public string Display { get { return "Name"; } }
		}
		#endregion

	}
}