using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UserLogData : Core.DataClass
	{
		#region constructor 
		public UserLogData() : base() {}
		#endregion

		#region Create
		public int Create(
			int UserId)
		{
			SqlCommand command = new SqlCommand("a_UserLog_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (UserId > 0)
				command.Parameters.Add(new SqlParameter("@UserId", UserId));
			else
				command.Parameters.Add(new SqlParameter("@UserId", System.DBNull.Value));

			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id, bool DeleteDependents)
		{
			SqlCommand command = new SqlCommand("a_UserLog_Delete", Connection);
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
		public UserLogList GetList()
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public UserLogTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public UserLogTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public UserLogTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecordsPS", Connection);
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
		public UserLogTable GetRecordsByUserField(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecordsByUserField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserFieldP
		public UserLogTable GetRecordsByUserFieldP(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecordsByUserFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserFieldPS
		public UserLogTable GetRecordsByUserFieldPS(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecordsByUserFieldPS", Connection);
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

		#region GetRecordsByRoleP
		public UserLogTableGetRecordsByRoleP GetRecordsByRoleP(
			int RoleId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("UserLog_GetRecordsByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserLogTableGetRecordsByRoleP(command);
		}
		#endregion

		#region GetRecordsByDepartmentP
		public UserLogTableGetRecordsByDepartmentP GetRecordsByDepartmentP(
			int DepartmentId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("UserLog_GetRecordsByDepartmentP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserLogTableGetRecordsByDepartmentP(command);
		}
		#endregion

		#region GetRecordsByGroupP
		public UserLogTableGetRecordsByGroupP GetRecordsByGroupP(
			int GroupId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("UserLog_GetRecordsByGroupP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@GroupId", GroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserLogTableGetRecordsByGroupP(command);
		}
		#endregion

		#region GetRecordsByAccountP
		public UserLogTableGetRecordsByAccountP GetRecordsByAccountP(
			int AccountId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("UserLog_GetRecordsByAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserLogTableGetRecordsByAccountP(command);
		}
		#endregion

		#region private

		private UserLogTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 4)
				includeSelect = true;

			while (reader.Read())
			{
				UserLogRow r = new UserLogRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Created = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.User_FullName = reader.GetString(3);

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
			return new UserLogTable(rows, totalCount);
		}

		private UserLogList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				UserLogListRow r = new UserLogListRow();
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
			return new UserLogList(rows, totalCount);
		}

		private UserLogTableGetRecordsByRoleP loadUserLogTableGetRecordsByRoleP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserLogRowGetRecordsByRoleP r = new UserLogRowGetRecordsByRoleP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Created = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.User_FullName = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserLogTableGetRecordsByRoleP(rows, totalCount);
		}

		private UserLogTableGetRecordsByDepartmentP loadUserLogTableGetRecordsByDepartmentP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserLogRowGetRecordsByDepartmentP r = new UserLogRowGetRecordsByDepartmentP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Created = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.User_FullName = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserLogTableGetRecordsByDepartmentP(rows, totalCount);
		}

		private UserLogTableGetRecordsByGroupP loadUserLogTableGetRecordsByGroupP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserLogRowGetRecordsByGroupP r = new UserLogRowGetRecordsByGroupP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Created = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.User_FullName = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserLogTableGetRecordsByGroupP(rows, totalCount);
		}

		private UserLogTableGetRecordsByAccountP loadUserLogTableGetRecordsByAccountP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserLogRowGetRecordsByAccountP r = new UserLogRowGetRecordsByAccountP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.UserId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Created = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.User_FullName = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserLogTableGetRecordsByAccountP(rows, totalCount);
		}

		#endregion
	}
}