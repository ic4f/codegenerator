using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class PhotoData : Core.DataClass
	{
		#region constructor 
		public PhotoData() : base() {}
		#endregion

		#region Create
		public int Create(
			string ExternalId,
			string Gender,
			int RaceId,
			int HairId,
			int AgeId,
			int WeightId,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_Photo_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add(new SqlParameter("@ExternalId", ExternalId));
			command.Parameters.Add(new SqlParameter("@Gender", Gender));
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

			command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id)
		{
			SqlCommand command = new SqlCommand("a_Photo_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public PhotoList GetList()
		{
			SqlCommand command = new SqlCommand("a_Photo_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public PhotoTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public PhotoTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public PhotoTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceField
		public PhotoTable GetRecordsByRaceField(int RaceId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByRaceField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceFieldP
		public PhotoTable GetRecordsByRaceFieldP(
			int RaceId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByRaceFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceFieldPS
		public PhotoTable GetRecordsByRaceFieldPS(
			int RaceId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByRaceFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByHairField
		public PhotoTable GetRecordsByHairField(int HairId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByHairField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@HairId", HairId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByHairFieldP
		public PhotoTable GetRecordsByHairFieldP(
			int HairId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByHairFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@HairId", HairId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByHairFieldPS
		public PhotoTable GetRecordsByHairFieldPS(
			int HairId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByHairFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@HairId", HairId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAgeField
		public PhotoTable GetRecordsByAgeField(int AgeId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByAgeField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAgeFieldP
		public PhotoTable GetRecordsByAgeFieldP(
			int AgeId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByAgeFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAgeFieldPS
		public PhotoTable GetRecordsByAgeFieldPS(
			int AgeId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByAgeFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByWeightField
		public PhotoTable GetRecordsByWeightField(int WeightId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByWeightField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByWeightFieldP
		public PhotoTable GetRecordsByWeightFieldP(
			int WeightId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByWeightFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByWeightFieldPS
		public PhotoTable GetRecordsByWeightFieldPS(
			int WeightId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByWeightFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupLink
		public PhotoTable GetRecordsByLineupLink(int LineupId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByLineupLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupLinkP
		public PhotoTable GetRecordsByLineupLinkP(
			int LineupId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByLineupLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupLinkPS
		public PhotoTable GetRecordsByLineupLinkPS(
			int LineupId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetRecordsByLineupLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetLineupLinks
		public PhotoList GetLineupLinks(int LineupId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetLineupLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetLineupLinksP
		public PhotoList GetLineupLinksP(int LineupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetLineupLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetLineupLinksPS
		public PhotoList GetLineupLinksPS(			int LineupId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Photo_GetLineupLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetByCriteria
		public PhotoTableGetByCriteria GetByCriteria(
			string Query)
		{
			SqlCommand command = new SqlCommand("Photo_GetByCriteria", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			return loadPhotoTableGetByCriteria(command);
		}
		#endregion

		#region private

		private PhotoTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 14)
				includeSelect = true;

			while (reader.Read())
			{
				PhotoRow r = new PhotoRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.ExternalId = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Gender = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.RaceId = reader.GetInt32(3);

				if (reader[4] != System.DBNull.Value)
					r.HairId = reader.GetInt32(4);

				if (reader[5] != System.DBNull.Value)
					r.AgeId = reader.GetInt32(5);

				if (reader[6] != System.DBNull.Value)
					r.WeightId = reader.GetInt32(6);

				if (reader[7] != System.DBNull.Value)
					r.Created = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(8);

				if (reader[9] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(9);

				if (reader[10] != System.DBNull.Value)
					r.Race_Description = reader.GetString(10);

				if (reader[11] != System.DBNull.Value)
					r.Hair_Description = reader.GetString(11);

				if (reader[12] != System.DBNull.Value)
					r.Age_Description = reader.GetString(12);

				if (reader[13] != System.DBNull.Value)
					r.Weight_Description = reader.GetString(13);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(14))
					r.Selected = true;

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new PhotoTable(rows, totalCount);
		}

		private PhotoList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				PhotoListRow r = new PhotoListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(1))
					r.Selected = true;

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new PhotoList(rows, totalCount);
		}

		private PhotoTableGetByCriteria loadPhotoTableGetByCriteria(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				PhotoRowGetByCriteria r = new PhotoRowGetByCriteria();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new PhotoTableGetByCriteria(rows, totalCount);
		}

		#endregion
	}
}