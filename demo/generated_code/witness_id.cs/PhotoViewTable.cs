using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class PhotoViewTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField LineupViewIdField 
		public static c.IDataField LineupViewIdField { get { return new LineupViewIdFieldClass(); } }
		#endregion

		#region static IDataField PhotoIdField 
		public static c.IDataField PhotoIdField { get { return new PhotoIdFieldClass(); } }
		#endregion

		#region static IDataField ResultField 
		public static c.IDataField ResultField { get { return new ResultFieldClass(); } }
		#endregion

		#region static IDataField CertaintyField 
		public static c.IDataField CertaintyField { get { return new CertaintyFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public PhotoViewTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 6; } }
		#endregion

		#region PhotoViewRow this[int row]
		public PhotoViewRow this[int row] { get { return (PhotoViewRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "PhotoView.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class LineupViewIdFieldClass : c.IDataField
		{
			public string DataField { get { return "LineupViewId"; } }
			public string SortExpression { get { return "PhotoView.LineupViewId"; } }
			public string Display { get { return "LineupViewId"; } }
		}

		private class PhotoIdFieldClass : c.IDataField
		{
			public string DataField { get { return "PhotoId"; } }
			public string SortExpression { get { return "PhotoView.PhotoId"; } }
			public string Display { get { return "PhotoId"; } }
		}

		private class ResultFieldClass : c.IDataField
		{
			public string DataField { get { return "Result"; } }
			public string SortExpression { get { return "PhotoView.[Result]"; } }
			public string Display { get { return "Result"; } }
		}

		private class CertaintyFieldClass : c.IDataField
		{
			public string DataField { get { return "Certainty"; } }
			public string SortExpression { get { return "PhotoView.Certainty"; } }
			public string Display { get { return "Certainty"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class PhotoViewRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int LineupViewId
		public int LineupViewId
		{
			get { return lineupViewId; }
			set { lineupViewId = value; }
		}
		#endregion

		#region int PhotoId
		public int PhotoId
		{
			get { return photoId; }
			set { photoId = value; }
		}
		#endregion

		#region string Result
		public string Result
		{
			get { return result; }
			set { result = value; }
		}
		#endregion

		#region string Certainty
		public string Certainty
		{
			get { return certainty; }
			set { certainty = value; }
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
		private int lineupViewId;
		private int photoId;
		private string result;
		private string certainty;
		private bool selected;
		#endregion
	}
}