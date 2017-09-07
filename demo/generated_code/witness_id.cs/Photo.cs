using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class Photo : Core.DataClass
	{
		#region constructor
		public Photo(int recordId) : base()
		{
			loadRecord(recordId);
		}
		#endregion

		#region int Id
		public int Id
		{
			get { return id; }
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
		}
		#endregion

		#region DateTime Modified
		public DateTime Modified
		{
			get { return modified; }
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
		}
		#endregion

		#region string Hair_Description
		public string Hair_Description
		{
			get { return hair_Description; }
		}
		#endregion

		#region string Age_Description
		public string Age_Description
		{
			get { return age_Description; }
		}
		#endregion

		#region string Weight_Description
		public string Weight_Description
		{
			get { return weight_Description; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			SqlCommand command = new SqlCommand("a_Photo_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (ExternalId != null)
				command.Parameters.Add(new SqlParameter("@ExternalId", ExternalId));
			else
				command.Parameters.Add(new SqlParameter("@ExternalId", System.DBNull.Value));

			if (Gender != null)
				command.Parameters.Add(new SqlParameter("@Gender", Gender));
			else
				command.Parameters.Add(new SqlParameter("@Gender", System.DBNull.Value));

			if (RaceId > 0)
				command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			else
				command.Parameters.Add(new SqlParameter("@RaceId", System.DBNull.Value));

			if (HairId > 0)
				command.Parameters.Add(new SqlParameter("@HairId", HairId));
			else
				command.Parameters.Add(new SqlParameter("@HairId", System.DBNull.Value));

			if (AgeId > 0)
				command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			else
				command.Parameters.Add(new SqlParameter("@AgeId", System.DBNull.Value));

			if (WeightId > 0)
				command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			else
				command.Parameters.Add(new SqlParameter("@WeightId", System.DBNull.Value));

			if (ModifiedBy != null)
				command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
			else
				command.Parameters.Add(new SqlParameter("@ModifiedBy", System.DBNull.Value));

			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();

			if (result >= 0)
				loadRecord(id); //some values might have been changed by the db code
			return result;
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecord", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", recordId));

			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			if (!reader.HasRows)
				throw new Core.AppException("Record with id = " + recordId + " not found.");
			reader.Read();

			if (reader[0] != System.DBNull.Value)
				id = reader.GetInt32(0);

			if (reader[1] != System.DBNull.Value)
				externalId = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				gender = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				raceId = reader.GetInt32(3);

			if (reader[4] != System.DBNull.Value)
				hairId = reader.GetInt32(4);

			if (reader[5] != System.DBNull.Value)
				ageId = reader.GetInt32(5);

			if (reader[6] != System.DBNull.Value)
				weightId = reader.GetInt32(6);

			if (reader[7] != System.DBNull.Value)
				created = reader.GetDateTime(7);

			if (reader[8] != System.DBNull.Value)
				modified = reader.GetDateTime(8);

			if (reader[9] != System.DBNull.Value)
				modifiedBy = reader.GetString(9);

			if (reader[10] != System.DBNull.Value)
				race_Description = reader.GetString(10);

			if (reader[11] != System.DBNull.Value)
				hair_Description = reader.GetString(11);

			if (reader[12] != System.DBNull.Value)
				age_Description = reader.GetString(12);

			if (reader[13] != System.DBNull.Value)
				weight_Description = reader.GetString(13);

			reader.Close();
			Connection.Close();
		}
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

		#endregion
	}
}