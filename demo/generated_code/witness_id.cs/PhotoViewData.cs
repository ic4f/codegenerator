using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class PhotoViewData : Core.DataClass
	{
		#region constructor 
		public PhotoViewData() : base() {}
		#endregion

		#region Create
		public int Create(
			int LineupViewId,
			int PhotoId,
			string Result,
			string Certainty)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (LineupViewId > 0)
				command.Parameters.Add(new SqlParameter("@LineupViewId", LineupViewId));
			else
				command.Parameters.Add(new SqlParameter("@LineupViewId", System.DBNull.Value));

			if (PhotoId > 0)
				command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			else
				command.Parameters.Add(new SqlParameter("@PhotoId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@Result", Result));
			command.Parameters.Add(new SqlParameter("@Certainty", Certainty));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public PhotoViewList GetList()
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public PhotoViewTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public PhotoViewTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public PhotoViewTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupViewField
		public PhotoViewTable GetRecordsByLineupViewField(int LineupViewId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByLineupViewField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupViewId", LineupViewId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupViewFieldP
		public PhotoViewTable GetRecordsByLineupViewFieldP(
			int LineupViewId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByLineupViewFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupViewId", LineupViewId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByLineupViewFieldPS
		public PhotoViewTable GetRecordsByLineupViewFieldPS(
			int LineupViewId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByLineupViewFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LineupViewId", LineupViewId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByPhotoField
		public PhotoViewTable GetRecordsByPhotoField(int PhotoId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByPhotoField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByPhotoFieldP
		public PhotoViewTable GetRecordsByPhotoFieldP(
			int PhotoId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByPhotoFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@PhotoId", PhotoId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByPhotoFieldPS
		public PhotoViewTable GetRecordsByPhotoFieldPS(
			int PhotoId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecordsByPhotoFieldPS", Connection);
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

		#region private

		private PhotoViewTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 5)
				includeSelect = true;

			while (reader.Read())
			{
				PhotoViewRow r = new PhotoViewRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.LineupViewId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.PhotoId = reader.GetInt32(2);

				if (reader[3] != System.DBNull.Value)
					r.Result = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Certainty = reader.GetString(4);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(5))
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
			return new PhotoViewTable(rows, totalCount);
		}

		private PhotoViewList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				PhotoViewListRow r = new PhotoViewListRow();
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
			return new PhotoViewList(rows, totalCount);
		}

		#endregion
	}
}