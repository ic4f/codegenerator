using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class EmailData : Core.DataClass
	{
		#region constructor 
		public EmailData() : base() {}
		#endregion

		#region Create
		public int Create(
			string Subject,
			string Message,
			string SentBy)
		{
			SqlCommand command = new SqlCommand("a_Email_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add(new SqlParameter("@Subject", Subject));
			command.Parameters.Add(new SqlParameter("@Message", Message));
			command.Parameters.Add(new SqlParameter("@SentBy", SentBy));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id, bool DeleteDependents)
		{
			SqlCommand command = new SqlCommand("a_Email_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			command.Parameters.Add(new SqlParameter("@DeleteDependents", DeleteDependents));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public EmailList GetList()
		{
			SqlCommand command = new SqlCommand("a_Email_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public EmailTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public EmailTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public EmailTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLink
		public EmailTable GetRecordsByUserLink(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecordsByUserLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkP
		public EmailTable GetRecordsByUserLinkP(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecordsByUserLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkPS
		public EmailTable GetRecordsByUserLinkPS(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Email_GetRecordsByUserLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetUserLinks
		public EmailList GetUserLinks(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Email_GetUserLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksP
		public EmailList GetUserLinksP(int UserId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Email_GetUserLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksPS
		public EmailList GetUserLinksPS(			int UserId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Email_GetUserLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region private

		private EmailTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 4)
				includeSelect = true;

			while (reader.Read())
			{
				EmailRow r = new EmailRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.Subject = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Sent = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.SentBy = reader.GetString(3);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(4))
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
			return new EmailTable(rows, totalCount);
		}

		private EmailList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				EmailListRow r = new EmailListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.Subject = reader.GetString(1);

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
			return new EmailList(rows, totalCount);
		}

		#endregion
	}
}