using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class DepartmentTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField CollegeIdField 
		public static c.IDataField CollegeIdField { get { return new CollegeIdFieldClass(); } }
		#endregion

		#region static IDataField NameField 
		public static c.IDataField NameField { get { return new NameFieldClass(); } }
		#endregion

		#region static IDataField College_NameField 
		public static c.IDataField College_NameField { get { return new College_NameFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public DepartmentTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 5; } }
		#endregion

		#region DepartmentRow this[int row]
		public DepartmentRow this[int row] { get { return (DepartmentRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Department.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class CollegeIdFieldClass : c.IDataField
		{
			public string DataField { get { return "CollegeId"; } }
			public string SortExpression { get { return "Department.CollegeId"; } }
			public string Display { get { return "CollegeId"; } }
		}

		private class NameFieldClass : c.IDataField
		{
			public string DataField { get { return "Name"; } }
			public string SortExpression { get { return "Department.Name"; } }
			public string Display { get { return "Name"; } }
		}

		private class College_NameFieldClass : c.IDataField
		{
			public string DataField { get { return "College_Name"; } }
			public string SortExpression { get { return "College.Name"; } }
			public string Display { get { return "College Name"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class DepartmentRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int CollegeId
		public int CollegeId
		{
			get { return collegeId; }
			set { collegeId = value; }
		}
		#endregion

		#region string Name
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region string College_Name
		public string College_Name
		{
			get { return college_Name; }
			set { college_Name = value; }
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
		private int collegeId;
		private string name;
		private string college_Name;
		private bool selected;
		#endregion
	}
}