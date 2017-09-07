using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class SuspectData : Core.DataClass
	{
		#region constructor 
		public SuspectData() : base() {}
		#endregion

		#region Create
		public int Create(
			int CaseId,
			string Gender,
			int RaceId,
			int HairId,
			int AgeId,
			int WeightId,
			string Notes,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_Suspect_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (CaseId > 0)
				command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			else
				command.Parameters.Add(new SqlParameter("@CaseId", System.DBNull.Value));

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

			command.Parameters.Add(new SqlParameter("@Notes", Notes));
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
			SqlCommand command = new SqlCommand("a_Suspect_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public SuspectList GetList()
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public SuspectTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public SuspectTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public SuspectTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseField
		public SuspectTable GetRecordsByCaseField(int CaseId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByCaseField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseFieldP
		public SuspectTable GetRecordsByCaseFieldP(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByCaseFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseFieldPS
		public SuspectTable GetRecordsByCaseFieldPS(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByCaseFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceField
		public SuspectTable GetRecordsByRaceField(int RaceId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByRaceField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceFieldP
		public SuspectTable GetRecordsByRaceFieldP(
			int RaceId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByRaceFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RaceId", RaceId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRaceFieldPS
		public SuspectTable GetRecordsByRaceFieldPS(
			int RaceId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByRaceFieldPS", Connection);
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
		public SuspectTable GetRecordsByHairField(int HairId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByHairField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@HairId", HairId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByHairFieldP
		public SuspectTable GetRecordsByHairFieldP(
			int HairId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByHairFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@HairId", HairId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByHairFieldPS
		public SuspectTable GetRecordsByHairFieldPS(
			int HairId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByHairFieldPS", Connection);
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
		public SuspectTable GetRecordsByAgeField(int AgeId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByAgeField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAgeFieldP
		public SuspectTable GetRecordsByAgeFieldP(
			int AgeId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByAgeFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AgeId", AgeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAgeFieldPS
		public SuspectTable GetRecordsByAgeFieldPS(
			int AgeId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByAgeFieldPS", Connection);
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
		public SuspectTable GetRecordsByWeightField(int WeightId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByWeightField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByWeightFieldP
		public SuspectTable GetRecordsByWeightFieldP(
			int WeightId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByWeightFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@WeightId", WeightId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByWeightFieldPS
		public SuspectTable GetRecordsByWeightFieldPS(
			int WeightId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Suspect_GetRecordsByWeightFieldPS", Connection);
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

		#region private

		private SuspectTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 16)
				includeSelect = true;

			while (reader.Read())
			{
				SuspectRow r = new SuspectRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.CaseId = reader.GetInt32(1);

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
					r.Notes = reader.GetString(7);

				if (reader[8] != System.DBNull.Value)
					r.Created = reader.GetDateTime(8);

				if (reader[9] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(9);

				if (reader[10] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(10);

				if (reader[11] != System.DBNull.Value)
					r.Case_Number = reader.GetString(11);

				if (reader[12] != System.DBNull.Value)
					r.Race_Description = reader.GetString(12);

				if (reader[13] != System.DBNull.Value)
					r.Hair_Description = reader.GetString(13);

				if (reader[14] != System.DBNull.Value)
					r.Age_Description = reader.GetString(14);

				if (reader[15] != System.DBNull.Value)
					r.Weight_Description = reader.GetString(15);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(16))
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
			return new SuspectTable(rows, totalCount);
		}

		private SuspectList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				SuspectListRow r = new SuspectListRow();
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
			return new SuspectList(rows, totalCount);
		}

		#endregion
	}
}