using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class UserLogTableGetRecordsByDepartmentP : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField UserIdField 
		public static c.IDataField UserIdField { get { return new UserIdFieldClass(); } }
		#endregion

		#region static IDataField CreatedField 
		public static c.IDataField CreatedField { get { return new CreatedFieldClass(); } }
		#endregion

		#region static IDataField User_FullNameField 
		public static c.IDataField User_FullNameField { get { return new User_FullNameFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public UserLogTableGetRecordsByDepartmentP(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 4; } }
		#endregion

		#region UserLogRowGetRecordsByDepartmentP this[int row]
		public UserLogRowGetRecordsByDepartmentP this[int row] { get { return (UserLogRowGetRecordsByDepartmentP)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "UserLog.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class UserIdFieldClass : c.IDataField
		{
			public string DataField { get { return "UserId"; } }
			public string SortExpression { get { return "UserLog.UserId"; } }
			public string Display { get { return "UserId"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "UserLog.Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class User_FullNameFieldClass : c.IDataField
		{
			public string DataField { get { return "User_FullName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "User"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class UserLogRowGetRecordsByDepartmentP : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int UserId
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
			set { created = value; }
		}
		#endregion

		#region string User_FullName
		public string User_FullName
		{
			get { return user_FullName; }
			set { user_FullName = value; }
		}
		#endregion

		#region bool Selected
		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}
		#endregion

		#region private
		private int id;
		private int userId;
		private DateTime created;
		private string user_FullName;
		private bool selected;
		#endregion
	}
}