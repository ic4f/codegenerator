using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class UserTableGetByCollege : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField FullNameField 
		public static c.IDataField FullNameField { get { return new FullNameFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public UserTableGetByCollege(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 2; } }
		#endregion

		#region UserRowGetByCollege this[int row]
		public UserRowGetByCollege this[int row] { get { return (UserRowGetByCollege)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "[User].Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class FullNameFieldClass : c.IDataField
		{
			public string DataField { get { return "FullName"; } }
			public string SortExpression { get { return "[User].LastName"; } }
			public string Display { get { return "User Name"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class UserRowGetByCollege : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string FullName
		public string FullName
		{
			get { return fullName; }
			set { fullName = value; }
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
		private string fullName;
		private bool selected;
		#endregion
	}
}