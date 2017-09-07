using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class PhotoTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField ExternalIdField 
		public static c.IDataField ExternalIdField { get { return new ExternalIdFieldClass(); } }
		#endregion

		#region static IDataField GenderField 
		public static c.IDataField GenderField { get { return new GenderFieldClass(); } }
		#endregion

		#region static IDataField RaceIdField 
		public static c.IDataField RaceIdField { get { return new RaceIdFieldClass(); } }
		#endregion

		#region static IDataField HairIdField 
		public static c.IDataField HairIdField { get { return new HairIdFieldClass(); } }
		#endregion

		#region static IDataField AgeIdField 
		public static c.IDataField AgeIdField { get { return new AgeIdFieldClass(); } }
		#endregion

		#region static IDataField WeightIdField 
		public static c.IDataField WeightIdField { get { return new WeightIdFieldClass(); } }
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

		#region static IDataField Race_DescriptionField 
		public static c.IDataField Race_DescriptionField { get { return new Race_DescriptionFieldClass(); } }
		#endregion

		#region static IDataField Hair_DescriptionField 
		public static c.IDataField Hair_DescriptionField { get { return new Hair_DescriptionFieldClass(); } }
		#endregion

		#region static IDataField Age_DescriptionField 
		public static c.IDataField Age_DescriptionField { get { return new Age_DescriptionFieldClass(); } }
		#endregion

		#region static IDataField Weight_DescriptionField 
		public static c.IDataField Weight_DescriptionField { get { return new Weight_DescriptionFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public PhotoTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 15; } }
		#endregion

		#region PhotoRow this[int row]
		public PhotoRow this[int row] { get { return (PhotoRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Photo.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class ExternalIdFieldClass : c.IDataField
		{
			public string DataField { get { return "ExternalId"; } }
			public string SortExpression { get { return "Photo.ExternalId"; } }
			public string Display { get { return "ExternalId"; } }
		}

		private class GenderFieldClass : c.IDataField
		{
			public string DataField { get { return "Gender"; } }
			public string SortExpression { get { return "Photo.Gender"; } }
			public string Display { get { return "Gender"; } }
		}

		private class RaceIdFieldClass : c.IDataField
		{
			public string DataField { get { return "RaceId"; } }
			public string SortExpression { get { return "Photo.RaceId"; } }
			public string Display { get { return "RaceId"; } }
		}

		private class HairIdFieldClass : c.IDataField
		{
			public string DataField { get { return "HairId"; } }
			public string SortExpression { get { return "Photo.HairId"; } }
			public string Display { get { return "HairId"; } }
		}

		private class AgeIdFieldClass : c.IDataField
		{
			public string DataField { get { return "AgeId"; } }
			public string SortExpression { get { return "Photo.AgeId"; } }
			public string Display { get { return "AgeId"; } }
		}

		private class WeightIdFieldClass : c.IDataField
		{
			public string DataField { get { return "WeightId"; } }
			public string SortExpression { get { return "Photo.WeightId"; } }
			public string Display { get { return "WeightId"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "Photo.Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Photo.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "Photo.ModifiedBy"; } }
			public string Display { get { return "ModifiedBy"; } }
		}

		private class Race_DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Race_Description"; } }
			public string SortExpression { get { return "Race.Description"; } }
			public string Display { get { return "Race Description"; } }
		}

		private class Hair_DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Hair_Description"; } }
			public string SortExpression { get { return "Hair.Description"; } }
			public string Display { get { return "Hair Color"; } }
		}

		private class Age_DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Age_Description"; } }
			public string SortExpression { get { return "Age.Description"; } }
			public string Display { get { return "Age Range"; } }
		}

		private class Weight_DescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Weight_Description"; } }
			public string SortExpression { get { return "Weight.Description"; } }
			public string Display { get { return "Weight Range"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class PhotoRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string ExternalId
		public string ExternalId
		{
			get { return externalId; }
			set { externalId = value; }
		}
		#endregion

		#region string Gender
		public string Gender
		{
			get { return gender; }
			set { gender = value; }
		}
		#endregion

		#region int RaceId
		public int RaceId
		{
			get { return raceId; }
			set { raceId = value; }
		}
		#endregion

		#region int HairId
		public int HairId
		{
			get { return hairId; }
			set { hairId = value; }
		}
		#endregion

		#region int AgeId
		public int AgeId
		{
			get { return ageId; }
			set { ageId = value; }
		}
		#endregion

		#region int WeightId
		public int WeightId
		{
			get { return weightId; }
			set { weightId = value; }
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

		#region string Race_Description
		public string Race_Description
		{
			get { return race_Description; }
			set { race_Description = value; }
		}
		#endregion

		#region string Hair_Description
		public string Hair_Description
		{
			get { return hair_Description; }
			set { hair_Description = value; }
		}
		#endregion

		#region string Age_Description
		public string Age_Description
		{
			get { return age_Description; }
			set { age_Description = value; }
		}
		#endregion

		#region string Weight_Description
		public string Weight_Description
		{
			get { return weight_Description; }
			set { weight_Description = value; }
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
		private string externalId;
		private string gender;
		private int raceId;
		private int hairId;
		private int ageId;
		private int weightId;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string race_Description;
		private string hair_Description;
		private string age_Description;
		private string weight_Description;
		private bool selected;
		#endregion
	}
}