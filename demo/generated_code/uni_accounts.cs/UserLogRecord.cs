using System;
using Far.Core;

namespace Far.Data
{
	public class UserLogRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region UserId 
		public class UserId : IRecordField
		{
			public string DataField { get { return "UserId"; } }
			public string SortExpression { get { return "UserId"; } }
			public string Display { get { return "UserId"; } }
		}
		#endregion

		#region Created 
		public class Created : IRecordField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "Created"; } }
			public string Display { get { return "Created"; } }
		}
		#endregion

		#region User_Login 
		public class User_Login : IRecordField
		{
			public string DataField { get { return "User_Login"; } }
			public string SortExpression { get { return "Login"; } }
			public string Display { get { return "Login"; } }
		}
		#endregion

		#region User_Password 
		public class User_Password : IRecordField
		{
			public string DataField { get { return "User_Password"; } }
			public string SortExpression { get { return "Password"; } }
			public string Display { get { return "Password"; } }
		}
		#endregion

		#region User_FirstName 
		public class User_FirstName : IRecordField
		{
			public string DataField { get { return "User_FirstName"; } }
			public string SortExpression { get { return "FirstName"; } }
			public string Display { get { return "First Name"; } }
		}
		#endregion

		#region User_LastName 
		public class User_LastName : IRecordField
		{
			public string DataField { get { return "User_LastName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "Last Name"; } }
		}
		#endregion

		#region User_CampusCode 
		public class User_CampusCode : IRecordField
		{
			public string DataField { get { return "User_CampusCode"; } }
			public string SortExpression { get { return "CampusCode"; } }
			public string Display { get { return "Campus Code"; } }
		}
		#endregion

		#region User_UniId 
		public class User_UniId : IRecordField
		{
			public string DataField { get { return "User_UniId"; } }
			public string SortExpression { get { return "UniId"; } }
			public string Display { get { return "UNI ID"; } }
		}
		#endregion

		#region User_ConAgree 
		public class User_ConAgree : IRecordField
		{
			public string DataField { get { return "User_ConAgree"; } }
			public string SortExpression { get { return "ConAgree"; } }
			public string Display { get { return "Confidentiality Agreement"; } }
		}
		#endregion

		#region User_OnlineAccess 
		public class User_OnlineAccess : IRecordField
		{
			public string DataField { get { return "User_OnlineAccess"; } }
			public string SortExpression { get { return "OnlineAccess"; } }
			public string Display { get { return "Online Access"; } }
		}
		#endregion

		#region User_Created 
		public class User_Created : IRecordField
		{
			public string DataField { get { return "User_Created"; } }
			public string SortExpression { get { return "Created"; } }
			public string Display { get { return "Created"; } }
		}
		#endregion

		#region User_Modified 
		public class User_Modified : IRecordField
		{
			public string DataField { get { return "User_Modified"; } }
			public string SortExpression { get { return "Modified"; } }
			public string Display { get { return "Modified"; } }
		}
		#endregion

		#region User_ModifiedBy 
		public class User_ModifiedBy : IRecordField
		{
			public string DataField { get { return "User_ModifiedBy"; } }
			public string SortExpression { get { return "ModifiedBy"; } }
			public string Display { get { return "Modified By"; } }
		}
		#endregion

		#region User_FullName 
		public class User_FullName : IRecordField
		{
			public string DataField { get { return "User_FullName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "FullName"; } }
		}
		#endregion

	}
}