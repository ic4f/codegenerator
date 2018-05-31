using System;
using Far.Core;

namespace Far.Data
{
	public class UserRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region Login 
		public class Login : IRecordField
		{
			public string DataField { get { return "Login"; } }
			public string SortExpression { get { return "Login"; } }
			public string Display { get { return "Login"; } }
		}
		#endregion

		#region Password 
		public class Password : IRecordField
		{
			public string DataField { get { return "Password"; } }
			public string SortExpression { get { return "Password"; } }
			public string Display { get { return "Password"; } }
		}
		#endregion

		#region FirstName 
		public class FirstName : IRecordField
		{
			public string DataField { get { return "FirstName"; } }
			public string SortExpression { get { return "FirstName"; } }
			public string Display { get { return "First Name"; } }
		}
		#endregion

		#region LastName 
		public class LastName : IRecordField
		{
			public string DataField { get { return "LastName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "Last Name"; } }
		}
		#endregion

		#region CampusCode 
		public class CampusCode : IRecordField
		{
			public string DataField { get { return "CampusCode"; } }
			public string SortExpression { get { return "CampusCode"; } }
			public string Display { get { return "Campus Code"; } }
		}
		#endregion

		#region UniId 
		public class UniId : IRecordField
		{
			public string DataField { get { return "UniId"; } }
			public string SortExpression { get { return "UniId"; } }
			public string Display { get { return "UNI ID"; } }
		}
		#endregion

		#region ConAgree 
		public class ConAgree : IRecordField
		{
			public string DataField { get { return "ConAgree"; } }
			public string SortExpression { get { return "ConAgree"; } }
			public string Display { get { return "Confidentiality Agreement"; } }
		}
		#endregion

		#region OnlineAccess 
		public class OnlineAccess : IRecordField
		{
			public string DataField { get { return "OnlineAccess"; } }
			public string SortExpression { get { return "OnlineAccess"; } }
			public string Display { get { return "Online Access"; } }
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

		#region Modified 
		public class Modified : IRecordField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Modified"; } }
			public string Display { get { return "Modified"; } }
		}
		#endregion

		#region ModifiedBy 
		public class ModifiedBy : IRecordField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "ModifiedBy"; } }
			public string Display { get { return "Modified By"; } }
		}
		#endregion

		#region FullName 
		public class FullName : IRecordField
		{
			public string DataField { get { return "FullName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "FullName"; } }
		}
		#endregion

	}
}