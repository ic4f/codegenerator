using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class UserData : Core.DataClass
	{
		#region constructor 
		public UserData() : base() {}
		#endregion

		#region Create
		public int Create(
			string Login,
			string Password,
			string FirstName,
			string LastName,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_User_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add(new SqlParameter("@Login", Login));
			command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
			command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
			command.Parameters.Add(new SqlParameter("@LastName", LastName));
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
			SqlCommand command = new SqlCommand("a_User_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public UserList GetList()
		{
			SqlCommand command = new SqlCommand("a_User_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public UserTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public UserTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public UserTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseLink
		public UserTable GetRecordsByCaseLink(int CaseId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByCaseLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseLinkP
		public UserTable GetRecordsByCaseLinkP(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByCaseLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCaseLinkPS
		public UserTable GetRecordsByCaseLinkPS(
			int CaseId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByCaseLinkPS", Connection);
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

		#region GetRecordsByRoleLink
		public UserTable GetRecordsByRoleLink(int RoleId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByRoleLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRoleLinkP
		public UserTable GetRecordsByRoleLinkP(
			int RoleId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByRoleLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByRoleLinkPS
		public UserTable GetRecordsByRoleLinkPS(
			int RoleId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByRoleLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetCaseLinks
		public UserList GetCaseLinks(int CaseId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetCaseLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetCaseLinksP
		public UserList GetCaseLinksP(int CaseId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetCaseLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetCaseLinksPS
		public UserList GetCaseLinksPS(			int CaseId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetCaseLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetCaseLinksByRole
		public UserList GetCaseLinksByRole(int CaseId, int RoleId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetCaseLinksByRole", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetCaseLinksByRoleP
		public UserList GetCaseLinksByRoleP(int CaseId, int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetCaseLinksByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinks
		public UserList GetRoleLinks(int RoleId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksP
		public UserList GetRoleLinksP(int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksPS
		public UserList GetRoleLinksPS(			int RoleId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByCase
		public UserList GetRoleLinksByCase(int RoleId, int CaseId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByCase", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByCaseP
		public UserList GetRoleLinksByCaseP(int RoleId, int CaseId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByCaseP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region ValidateUser
		public int ValidateUser(
			string Login,
			string Password)
		{
			SqlCommand command = new SqlCommand("User_ValidateUser", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Login", Login));
			command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
			Connection.Open();
			object result = command.ExecuteScalar();
			Connection.Close();
			return Convert.ToInt32(result);
		}
		#endregion

		#region LogUser
		public void LogUser(
			int UserId)
		{
			SqlCommand command = new SqlCommand("User_LogUser", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			Connection.Open();
			command.ExecuteNonQuery();
			Connection.Close();
		}
		#endregion

		#region private

		private UserTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 9)
				includeSelect = true;

			while (reader.Read())
			{
				UserRow r = new UserRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.Login = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Password = (byte[])reader.GetValue(2);

				if (reader[3] != System.DBNull.Value)
					r.FirstName = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.LastName = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.Created = reader.GetDateTime(5);

				if (reader[6] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(6);

				if (reader[7] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(7);

				if (reader[8] != System.DBNull.Value)
					r.FullName = reader.GetString(8);

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
			return new UserTable(rows, totalCount);
		}

		private UserList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				UserListRow r = new UserListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

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
			return new UserList(rows, totalCount);
		}

		#endregion
	}
}