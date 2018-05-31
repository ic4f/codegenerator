using System;
using Far.Core;

namespace Far.Data
{
	public class RoleRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region Description 
		public class Description : IRecordField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "Description"; } }
			public string Display { get { return "Description"; } }
		}
		#endregion

		#region Rank 
		public class Rank : IRecordField
		{
			public string DataField { get { return "Rank"; } }
			public string SortExpression { get { return "Rank"; } }
			public string Display { get { return "Rank"; } }
		}
		#endregion

	}
}