using System;
using Far.Core;

namespace Far.Data
{
	public class DepartmentRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region CollegeId 
		public class CollegeId : IRecordField
		{
			public string DataField { get { return "CollegeId"; } }
			public string SortExpression { get { return "CollegeId"; } }
			public string Display { get { return "CollegeId"; } }
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

		#region College_Name 
		public class College_Name : IRecordField
		{
			public string DataField { get { return "College_Name"; } }
			public string SortExpression { get { return "Name"; } }
			public string Display { get { return "Name"; } }
		}
		#endregion

	}
}