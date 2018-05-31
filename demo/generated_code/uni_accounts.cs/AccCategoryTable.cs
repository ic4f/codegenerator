using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccCategoryTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField UserIdField 
		public static c.IDataField UserIdField { get { return new UserIdFieldClass(); } }
		#endregion

		#region static IDataField DescriptionField 
		public static c.IDataField DescriptionField { get { return new DescriptionFieldClass(); } }
		#endregion

		#region static IDataField RankField 
		public static c.IDataField RankField { get { return new RankFieldClass(); } }
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

		#region static IDataField User_FullNameField 
		public static c.IDataField User_FullNameField { get { return new User_FullNameFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccCategoryTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 9; } }
		#endregion

		#region AccCategoryRow this[int row]
		public AccCategoryRow this[int row] { get { return (AccCategoryRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "AccCategory.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class UserIdFieldClass : c.IDataField
		{
			public string DataField { get { return "UserId"; } }
			public string SortExpression { get { return "AccCategory.UserId"; } }
			public string Display { get { return "UserId"; } }
		}

		private class DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Description"; } }
			public string SortExpression { get { return "AccCategory.Description"; } }
			public string Display { get { return "Description"; } }
		}

		private class RankFieldClass : c.IDataField
		{
			public string DataField { get { return "Rank"; } }
			public string SortExpression { get { return "AccCategory.Rank"; } }
			public string Display { get { return "Rank"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "AccCategory.Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "AccCategory.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "AccCategory.ModifiedBy"; } }
			public string Display { get { return "Modified By"; } }
		}

		private class User_FullNameFieldClass : c.IDataField
		{
			public string DataField { get { return "User_FullName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "User Full Name"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class AccCategoryRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int UserId
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion

		#region short Rank
		public short Rank
		{
			get { return rank; }
			set { rank = value; }
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

		#region string User_FullName
		public string User_FullName
		{
			get { return user_FullName; }
			set { user_FullName = value; }
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
		private int userId;
		private string description;
		private short rank;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string user_FullName;
		private bool selected;
		#endregion
	}
}