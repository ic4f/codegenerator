using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class LineupData : Core.DataClass
	{
		#region constructor 
		public LineupData() : base() {}
		#endregion

		#region Create
		public int Create(
			int SuspectId,
			int SuspectPhotoPosition,
			int CaseId,
			string Description,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_Lineup_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (SuspectId > 0)
				command.Parameters.Add(new SqlParameter("@SuspectId", SuspectId));
			else
				command.Parameters.Add(new SqlParameter("@SuspectId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@SuspectPhotoPosition", SuspectPhotoPosition));
			if (CaseId > 0)
				command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			else
				command.Parameters.Add(new SqlParameter("@CaseId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@Description", Description));
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
			SqlCommand command = new SqlCommand("a_Lineup_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public LineupList GetList()
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public LineupTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public LineupTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public LineupTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsBySuspectField
		public LineupTable GetRecordsBySuspectField(int SuspectId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsBySuspectField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SuspectId", SuspectId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsBySuspectFieldP
		public LineupTable GetRecordsBySuspectFieldP(
			int SuspectId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsBySuspectFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SuspectId", SuspectId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsBySuspectFieldPS
		public LineupTable GetRecordsBySuspectFieldPS(
			int SuspectId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsBySuspectFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SuspectId", SuspectId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseField
		public LineupTable GetRecordsByCaseField(int CaseId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByCaseField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseFieldP
		public LineupTable GetRecordsByCaseFieldP(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByCaseFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseFieldPS
		public LineupTable GetRecordsByCaseFieldPS(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByCaseFieldPS", Connection);
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

		#region GetRecordsByPhotoLink
		public LineupTable GetRecordsByPhotoLink(int PhotoId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByPhotoLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByPhotoLinkP
		public LineupTable GetRecordsByPhotoLinkP(
			int PhotoId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByPhotoLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByPhotoLinkPS
		public LineupTable GetRecordsByPhotoLinkPS(
			int PhotoId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetRecordsByPhotoLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetPhotoLinks
		public LineupList GetPhotoLinks(int PhotoId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetPhotoLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetPhotoLinksP
		public LineupList GetPhotoLinksP(int PhotoId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetPhotoLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetPhotoLinksPS
		public LineupList GetPhotoLinksPS(			int PhotoId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Lineup_GetPhotoLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region private

		private LineupTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 9)
				includeSelect = true;

			while (reader.Read())
			{
				LineupRow r = new LineupRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.SuspectId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.SuspectPhotoPosition = reader.GetInt32(2);

				if (reader[3] != System.DBNull.Value)
					r.CaseId = reader.GetInt32(3);

				if (reader[4] != System.DBNull.Value)
					r.Description = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.Created = reader.GetDateTime(5);

				if (reader[6] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(6);

				if (reader[7] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(7);

				if (reader[8] != System.DBNull.Value)
					r.Case_Number = reader.GetString(8);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(9))
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
			return new LineupTable(rows, totalCount);
		}

		private LineupList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				LineupListRow r = new LineupListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.Description = reader.GetString(1);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(2))
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
			return new LineupList(rows, totalCount);
		}

		#endregion
	}
}