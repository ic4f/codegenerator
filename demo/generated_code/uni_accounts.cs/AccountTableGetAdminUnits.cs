using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccountTableGetAdminUnits : c.AbstractDataTable
	{
		#region static IDataField AdminUnitField 
		public static c.IDataField AdminUnitField { get { return new AdminUnitFieldClass(); } }
		#endregion

		#region static IDataField DescriptionField 
		public static c.IDataField DescriptionField { get { return new DescriptionFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccountTableGetAdminUnits(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 2; } }
		#endregion

		#region AccountRowGetAdminUnits this[int row]
		public AccountRowGetAdminUnits this[int row] { get { return (AccountRowGetAdminUnits)Rows[row]; } }
		#endregion

		#region private
		private class AdminUnitFieldClass : c.IDataField
		{
			public string DataField { get { return "AdminUnit"; } }
			public string SortExpression { get { return "Account.AdminUnit"; } }
			public string Display { get { return "Administrative Unit"; } }
		}

		private class DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "AdminUnit"; } }
			public string Display { get { return "Description"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class AccountRowGetAdminUnits : c.IDataRow
	{
		#region string AdminUnit
		public string AdminUnit
		{
			get { return adminUnit; }
			set { adminUnit = value; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion

		#region bool Selected
		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}
		#endregion

		#region int Id
		public int Id
		{
			get { return -1; }
		}
		#endregion

		#region private
		private string adminUnit;
		private string description;
		private bool selected;
		#endregion
	}
}