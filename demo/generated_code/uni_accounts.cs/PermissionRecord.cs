using System;
using Far.Core;

namespace Far.Data
{
	public class PermissionRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region CategoryId 
		public class CategoryId : IRecordField
		{
			public string DataField { get { return "CategoryId"; } }
			public string SortExpression { get { return "CategoryId"; } }
			public string Display { get { return "CategoryId"; } }
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

		#region Code 
		public class Code : IRecordField
		{
			public string DataField { get { return "Code"; } }
			public string SortExpression { get { return "Code"; } }
			public string Display { get { return "Code"; } }
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

		#region PermissionCategory_Description 
		public class PermissionCategory_Description : IRecordField
		{
			public string DataField { get { return "PermissionCategory_Description"; } }
			public string SortExpression { get { return "Description"; } }
			public string Display { get { return "Description"; } }
		}
		#endregion

		#region PermissionCategory_Rank 
		public class PermissionCategory_Rank : IRecordField
		{
			public string DataField { get { return "PermissionCategory_Rank"; } }
			public string SortExpression { get { return "Rank"; } }
			public string Display { get { return "Rank"; } }
		}
		#endregion

	}
}