using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class EmailTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField SubjectField 
		public static c.IDataField SubjectField { get { return new SubjectFieldClass(); } }
		#endregion

		#region static IDataField SentField 
		public static c.IDataField SentField { get { return new SentFieldClass(); } }
		#endregion

		#region static IDataField SentByField 
		public static c.IDataField SentByField { get { return new SentByFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public EmailTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 5; } }
		#endregion

		#region EmailRow this[int row]
		public EmailRow this[int row] { get { return (EmailRow)Rows[row]; } }
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

		private class SentFieldClass : c.IDataField
		{
			public string DataField { get { return "Sent"; } }
			public string SortExpression { get { return "Email.Sent"; } }
			public string Display { get { return "Sent"; } }
		}

		private class SentByFieldClass : c.IDataField
		{
			public string DataField { get { return "SentBy"; } }
			public string SortExpression { get { return "Email.SentBy"; } }
			public string Display { get { return "Sent By"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class EmailRow : c.IDataRow
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

		#region DateTime Sent
		public DateTime Sent
		{
			get { return sent; }
			set { sent = value; }
		}
		#endregion

		#region string SentBy
		public string SentBy
		{
			get { return sentBy; }
			set { sentBy = value; }
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
		private DateTime sent;
		private string sentBy;
		private bool selected;
		#endregion
	}
}