using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class CaseList : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField NumberField 
		public static c.IDataField NumberField { get { return new NumberFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public CaseList(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 7; } }
		#endregion

		#region CaseListRow this[int row]
		public CaseListRow this[int row] { get { return (CaseListRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "[Case].Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class NumberFieldClass : c.IDataField
		{
			public string DataField { get { return "Number"; } }
			public string SortExpression { get { return "[Case].Number"; } }
			public string Display { get { return "Number"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class CaseListRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string Number
		public string Number
		{
			get { return number; }
			set { number = value; }
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
		private string number;
		private bool selected;
		#endregion
	}
}