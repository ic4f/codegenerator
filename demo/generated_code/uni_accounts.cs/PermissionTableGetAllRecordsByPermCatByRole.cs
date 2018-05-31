using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class PermissionTableGetAllRecordsByPermCatByRole : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField DescriptionField 
		public static c.IDataField DescriptionField { get { return new DescriptionFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedFieldClass(); } }
		#endregion

		#region constructor 
		public PermissionTableGetAllRecordsByPermCatByRole(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 3; } }
		#endregion

		#region PermissionRowGetAllRecordsByPermCatByRole this[int row]
		public PermissionRowGetAllRecordsByPermCatByRole this[int row] { get { return (PermissionRowGetAllRecordsByPermCatByRole)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Permission.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "Permission.Description"; } }
			public string Display { get { return "Description"; } }
		}

		private class SelectedFieldClass : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class PermissionRowGetAllRecordsByPermCatByRole : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
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

		#region private
		private int id;
		private string description;
		private bool selected;
		#endregion
	}
}