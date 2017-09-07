using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class LineupTextTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField NameField 
		public static c.IDataField NameField { get { return new NameFieldClass(); } }
		#endregion

		#region static IDataField ContentField 
		public static c.IDataField ContentField { get { return new ContentFieldClass(); } }
		#endregion

		#region static IDataField RankField 
		public static c.IDataField RankField { get { return new RankFieldClass(); } }
		#endregion

		#region static IDataField ModifiedField 
		public static c.IDataField ModifiedField { get { return new ModifiedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedByField 
		public static c.IDataField ModifiedByField { get { return new ModifiedByFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public LineupTextTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 7; } }
		#endregion

		#region LineupTextRow this[int row]
		public LineupTextRow this[int row] { get { return (LineupTextRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "LineupText.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class NameFieldClass : c.IDataField
		{
			public string DataField { get { return "Name"; } }
			public string SortExpression { get { return "LineupText.Name"; } }
			public string Display { get { return "Name"; } }
		}

		private class ContentFieldClass : c.IDataField
		{
			public string DataField { get { return "Content"; } }
			public string SortExpression { get { return "LineupText.Content"; } }
			public string Display { get { return "Content"; } }
		}

		private class RankFieldClass : c.IDataField
		{
			public string DataField { get { return "Rank"; } }
			public string SortExpression { get { return "LineupText.Rank"; } }
			public string Display { get { return "Rank"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "LineupText.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "LineupText.ModifiedBy"; } }
			public string Display { get { return "ModifiedBy"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class LineupTextRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string Name
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region string Content
		public string Content
		{
			get { return content; }
			set { content = value; }
		}
		#endregion

		#region int Rank
		public int Rank
		{
			get { return rank; }
			set { rank = value; }
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

		#region bool Selected
		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}
		#endregion

		#region private
		private int id;
		private string name;
		private string content;
		private int rank;
		private DateTime modified;
		private string modifiedBy;
		private bool selected;
		#endregion
	}
}