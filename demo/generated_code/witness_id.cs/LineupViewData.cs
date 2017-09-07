using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class LineupViewData : Core.DataClass
	{
		#region constructor 
		public LineupViewData() : base() {}
		#endregion

		#region Create
		public int Create(
			int LineupId,
			string WitnessFirstName,
			string WitnessLastName,
			string Relevance,
			string CreatedBy,
			bool IsCompleted)
		{
			SqlCommand command = new SqlCommand("a_LineupView_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (LineupId > 0)
				command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			else
				command.Parameters.Add(new SqlParameter("@LineupId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@WitnessFirstName", WitnessFirstName));
			command.Parameters.Add(new SqlParameter("@WitnessLastName", WitnessLastName));
			command.Parameters.Add(new SqlParameter("@Relevance", Relevance));
			command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
			command.Parameters.Add(new SqlParameter("@IsCompleted", IsCompleted));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id)
		{
			SqlCommand command = new SqlCommand("a_LineupView_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public LineupViewList GetList()
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public LineupViewTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public LineupViewTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public LineupViewTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupField
		public LineupViewTable GetRecordsByLineupField(int LineupId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecordsByLineupField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupFieldP
		public LineupViewTable GetRecordsByLineupFieldP(
			int LineupId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecordsByLineupFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupId", LineupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupFieldPS
		public LineupViewTable GetRecordsByLineupFieldPS(
			int LineupId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecordsByLineupFieldPS", Connection);
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

		#region Finalize
		public int Finalize(
			int LineupViewId,
			string Relevance)
		{
			SqlCommand command = new SqlCommand("LineupView_Finalize", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupViewId", LineupViewId));
			command.Parameters.Add(new SqlParameter("@Relevance", Relevance));
			Connection.Open();
			object result = command.ExecuteScalar();
			Connection.Close();
			return Convert.ToInt32(result);
		}
		#endregion

		#region private

		private LineupViewTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 8)
				includeSelect = true;

			while (reader.Read())
			{
				LineupViewRow r = new LineupViewRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.LineupId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.WitnessFirstName = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.WitnessLastName = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Relevance = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.Created = reader.GetDateTime(5);

				if (reader[6] != System.DBNull.Value)
					r.CreatedBy = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.IsCompleted = reader.GetBoolean(7);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(8))
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
			return new LineupViewTable(rows, totalCount);
		}

		private LineupViewList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				LineupViewListRow r = new LineupViewListRow();
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
			return new LineupViewList(rows, totalCount);
		}

		#endregion
	}
}