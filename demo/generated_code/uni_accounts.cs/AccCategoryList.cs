using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccCategoryList : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField DescriptionField 
		public static c.IDataField DescriptionField { get { return new DescriptionFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccCategoryList(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 9; } }
		#endregion

		#region AccCategoryListRow this[int row]
		public AccCategoryListRow this[int row] { get { return (AccCategoryListRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "AccCategory.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "AccCategory.Description"; } }
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

	public class AccCategoryListRow : c.IDataRow
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