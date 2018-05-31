using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class EmailList : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField SubjectField 
		public static c.IDataField SubjectField { get { return new SubjectFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public EmailList(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 5; } }
		#endregion

		#region EmailListRow this[int row]
		public EmailListRow this[int row] { get { return (EmailListRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Email.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class SubjectFieldClass : c.IDataField
		{
			public string DataField { get { return "Subject"; } }
			public string SortExpression { get { return "Email.Subject"; } }
			public string Display { get { return "Subject"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class EmailListRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string Subject
		public string Subject
		{
			get { return subject; }
			set { subject = value; }
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
		private string subject;
		private bool selected;
		#endregion
	}
}