using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccCategoryData : Core.DataClass
	{
		#region constructor 
		public AccCategoryData() : base() {}
		#endregion

		#region Create
		public int Create(
			int UserId,
			string Description,
			short Rank,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (UserId > 0)
				command.Parameters.Add(new SqlParameter("@UserId", UserId));
			else
				command.Parameters.Add(new SqlParameter("@UserId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@Description", Description));
			command.Parameters.Add(new SqlParameter("@Rank", Rank));
			command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id, bool DeleteDependents)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_Delete", Connection);
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
		public AccCategoryList GetList()
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public AccCategoryTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public AccCategoryTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public AccCategoryTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserField
		public AccCategoryTable GetRecordsByUserField(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByUserField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserFieldP
		public AccCategoryTable GetRecordsByUserFieldP(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByUserFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserFieldPS
		public AccCategoryTable GetRecordsByUserFieldPS(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByUserFieldPS", Connection);
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

		#region GetRecordsByAccountLink
		public AccCategoryTable GetRecordsByAccountLink(int AccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByAccountLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkP
		public AccCategoryTable GetRecordsByAccountLinkP(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByAccountLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkPS
		public AccCategoryTable GetRecordsByAccountLinkPS(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetRecordsByAccountLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetAccountLinks
		public AccCategoryList GetAccountLinks(int AccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetAccountLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksP
		public AccCategoryList GetAccountLinksP(int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetAccountLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksPS
		public AccCategoryList GetAccountLinksPS(			int AccountId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_AccCategory_GetAccountLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region private

		private AccCategoryTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 8)
				includeSelect = true;

			while (reader.Read())
			{
				AccCategoryRow r = new AccCategoryRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Description = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.Rank = reader.GetInt16(3);

				if (reader[4] != System.DBNull.Value)
					r.Created = reader.GetDateTime(4);

				if (reader[5] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(5);

				if (reader[6] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.User_FullName = reader.GetString(7);

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
			return new AccCategoryTable(rows, totalCount);
		}

		private AccCategoryList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				AccCategoryListRow r = new AccCategoryListRow();
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
			return new AccCategoryList(rows, totalCount);
		}

		#endregion
	}
}