using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class LineupViewTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField LineupIdField 
		public static c.IDataField LineupIdField { get { return new LineupIdFieldClass(); } }
		#endregion

		#region static IDataField WitnessFirstNameField 
		public static c.IDataField WitnessFirstNameField { get { return new WitnessFirstNameFieldClass(); } }
		#endregion

		#region static IDataField WitnessLastNameField 
		public static c.IDataField WitnessLastNameField { get { return new WitnessLastNameFieldClass(); } }
		#endregion

		#region static IDataField RelevanceField 
		public static c.IDataField RelevanceField { get { return new RelevanceFieldClass(); } }
		#endregion

		#region static IDataField CreatedField 
		public static c.IDataField CreatedField { get { return new CreatedFieldClass(); } }
		#endregion

		#region static IDataField CreatedByField 
		public static c.IDataField CreatedByField { get { return new CreatedByFieldClass(); } }
		#endregion

		#region static IDataField IsCompletedField 
		public static c.IDataField IsCompletedField { get { return new IsCompletedFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public LineupViewTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 9; } }
		#endregion

		#region LineupViewRow this[int row]
		public LineupViewRow this[int row] { get { return (LineupViewRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "LineupView.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class LineupIdFieldClass : c.IDataField
		{
			public string DataField { get { return "LineupId"; } }
			public string SortExpression { get { return "LineupView.LineupId"; } }
			public string Display { get { return "LineupId"; } }
		}

		private class WitnessFirstNameFieldClass : c.IDataField
		{
			public string DataField { get { return "WitnessFirstName"; } }
			public string SortExpression { get { return "LineupView.WitnessFirstName"; } }
			public string Display { get { return "WitnessFirstName"; } }
		}

		private class WitnessLastNameFieldClass : c.IDataField
		{
			public string DataField { get { return "WitnessLastName"; } }
			public string SortExpression { get { return "LineupView.WitnessLastName"; } }
			public string Display { get { return "WitnessLastName"; } }
		}

		private class RelevanceFieldClass : c.IDataField
		{
			public string DataField { get { return "Relevance"; } }
			public string SortExpression { get { return "LineupView.Relevance"; } }
			public string Display { get { return "Relevance"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "LineupView.Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class CreatedByFieldClass : c.IDataField
		{
			public string DataField { get { return "CreatedBy"; } }
			public string SortExpression { get { return "LineupView.CreatedBy"; } }
			public string Display { get { return "CreatedBy"; } }
		}

		private class IsCompletedFieldClass : c.IDataField
		{
			public string DataField { get { return "IsCompleted"; } }
			public string SortExpression { get { return "LineupView.IsCompleted"; } }
			public string Display { get { return "IsCompleted"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class LineupViewRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int LineupId
		public int LineupId
		{
			get { return lineupId; }
			set { lineupId = value; }
		}
		#endregion

		#region string WitnessFirstName
		public string WitnessFirstName
		{
			get { return witnessFirstName; }
			set { witnessFirstName = value; }
		}
		#endregion

		#region string WitnessLastName
		public string WitnessLastName
		{
			get { return witnessLastName; }
			set { witnessLastName = value; }
		}
		#endregion

		#region string Relevance
		public string Relevance
		{
			get { return relevance; }
			set { relevance = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
			set { created = value; }
		}
		#endregion

		#region string CreatedBy
		public string CreatedBy
		{
			get { return createdBy; }
			set { createdBy = value; }
		}
		#endregion

		#region bool IsCompleted
		public bool IsCompleted
		{
			get { return isCompleted; }
			set { isCompleted = value; }
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
		private int lineupId;
		private string witnessFirstName;
		private string witnessLastName;
		private string relevance;
		private DateTime created;
		private string createdBy;
		private bool isCompleted;
		private bool selected;
		#endregion
	}
}