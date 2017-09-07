using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class LineupTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField SuspectIdField 
		public static c.IDataField SuspectIdField { get { return new SuspectIdFieldClass(); } }
		#endregion

		#region static IDataField SuspectPhotoPositionField 
		public static c.IDataField SuspectPhotoPositionField { get { return new SuspectPhotoPositionFieldClass(); } }
		#endregion

		#region static IDataField CaseIdField 
		public static c.IDataField CaseIdField { get { return new CaseIdFieldClass(); } }
		#endregion

		#region static IDataField DescriptionField 
		public static c.IDataField DescriptionField { get { return new DescriptionFieldClass(); } }
		#endregion

		#region static IDataField CreatedField 
		public static c.IDataField CreatedField { get { return new CreatedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedField 
		public static c.IDataField ModifiedField { get { return new ModifiedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedByField 
		public static c.IDataField ModifiedByField { get { return new ModifiedByFieldClass(); } }
		#endregion

		#region static IDataField Case_NumberField 
		public static c.IDataField Case_NumberField { get { return new Case_NumberFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public LineupTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 10; } }
		#endregion

		#region LineupRow this[int row]
		public LineupRow this[int row] { get { return (LineupRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Lineup.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class SuspectIdFieldClass : c.IDataField
		{
			public string DataField { get { return "SuspectId"; } }
			public string SortExpression { get { return "Lineup.SuspectId"; } }
			public string Display { get { return "SuspectId"; } }
		}

		private class SuspectPhotoPositionFieldClass : c.IDataField
		{
			public string DataField { get { return "SuspectPhotoPosition"; } }
			public string SortExpression { get { return "Lineup.SuspectPhotoPosition"; } }
			public string Display { get { return "SuspectPhotoPosition"; } }
		}

		private class CaseIdFieldClass : c.IDataField
		{
			public string DataField { get { return "CaseId"; } }
			public string SortExpression { get { return "Lineup.CaseId"; } }
			public string Display { get { return "CaseId"; } }
		}

		private class DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "Lineup.Description"; } }
			public string Display { get { return "Description"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "Lineup.Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Lineup.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "Lineup.ModifiedBy"; } }
			public string Display { get { return "ModifiedBy"; } }
		}

		private class Case_NumberFieldClass : c.IDataField
		{
			public string DataField { get { return "Case_Number"; } }
			public string SortExpression { get { return "[Case].Number"; } }
			public string Display { get { return "Case Number"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class LineupRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int SuspectId
		public int SuspectId
		{
			get { return suspectId; }
			set { suspectId = value; }
		}
		#endregion

		#region int SuspectPhotoPosition
		public int SuspectPhotoPosition
		{
			get { return suspectPhotoPosition; }
			set { suspectPhotoPosition = value; }
		}
		#endregion

		#region int CaseId
		public int CaseId
		{
			get { return caseId; }
			set { caseId = value; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
			set { created = value; }
		}
		#endregion

		#region DateTime Modified
		public DateTime Modified
		{
			get { return modified; }
			set { modified = value; }
		}
		#endregion

		#region string ModifiedBy
		public string ModifiedBy
		{
			get { return modifiedBy; }
			set { modifiedBy = value; }
		}
		#endregion

		#region string Case_Number
		public string Case_Number
		{
			get { return case_Number; }
			set { case_Number = value; }
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
		private int suspectId;
		private int suspectPhotoPosition;
		private int caseId;
		private string description;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string case_Number;
		private bool selected;
		#endregion
	}
}