using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
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
			string CampusCode,
			string UniId,
			bool ConAgree,
			bool OnlineAccess,
			string ModifiedBy)
		{
			SqlCommand command = new SqlCommand("a_User_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add(new SqlParameter("@Login", Login));
			command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
			command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
			command.Parameters.Add(new SqlParameter("@LastName", LastName));
			command.Parameters.Add(new SqlParameter("@CampusCode", CampusCode));
			command.Parameters.Add(new SqlParameter("@UniId", UniId));
			command.Parameters.Add(new SqlParameter("@ConAgree", ConAgree));
			command.Parameters.Add(new SqlParameter("@OnlineAccess", OnlineAccess));
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
			SqlCommand command = new SqlCommand("a_User_Delete", Connection);
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

		#region GetRecordsByDepartmentLink
		public UserTable GetRecordsByDepartmentLink(int DepartmentId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByDepartmentLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByDepartmentLinkP
		public UserTable GetRecordsByDepartmentLinkP(
			int DepartmentId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByDepartmentLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByDepartmentLinkPS
		public UserTable GetRecordsByDepartmentLinkPS(
			int DepartmentId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByDepartmentLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLink
		public UserTable GetRecordsByAccountLink(int AccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByAccountLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkP
		public UserTable GetRecordsByAccountLinkP(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByAccountLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkPS
		public UserTable GetRecordsByAccountLinkPS(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByAccountLinkPS", Connection);
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

		#region GetRecordsByEmailLink
		public UserTable GetRecordsByEmailLink(int EmailId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByEmailLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByEmailLinkP
		public UserTable GetRecordsByEmailLinkP(
			int EmailId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByEmailLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByEmailLinkPS
		public UserTable GetRecordsByEmailLinkPS(
			int EmailId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByEmailLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUsrGroupLink
		public UserTable GetRecordsByUsrGroupLink(int UsrGroupId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByUsrGroupLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUsrGroupLinkP
		public UserTable GetRecordsByUsrGroupLinkP(
			int UsrGroupId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByUsrGroupLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUsrGroupLinkPS
		public UserTable GetRecordsByUsrGroupLinkPS(
			int UsrGroupId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecordsByUsrGroupLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
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

		#region GetRoleLinksByDepartment
		public UserList GetRoleLinksByDepartment(int RoleId, int DepartmentId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByDepartment", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByDepartmentP
		public UserList GetRoleLinksByDepartmentP(int RoleId, int DepartmentId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByDepartmentP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByAccount
		public UserList GetRoleLinksByAccount(int RoleId, int AccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByAccountP
		public UserList GetRoleLinksByAccountP(int RoleId, int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByEmail
		public UserList GetRoleLinksByEmail(int RoleId, int EmailId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByEmail", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByEmailP
		public UserList GetRoleLinksByEmailP(int RoleId, int EmailId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByEmailP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByUsrGroup
		public UserList GetRoleLinksByUsrGroup(int RoleId, int UsrGroupId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByUsrGroup", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetRoleLinksByUsrGroupP
		public UserList GetRoleLinksByUsrGroupP(int RoleId, int UsrGroupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetRoleLinksByUsrGroupP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinks
		public UserList GetDepartmentLinks(int DepartmentId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksP
		public UserList GetDepartmentLinksP(int DepartmentId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksPS
		public UserList GetDepartmentLinksPS(			int DepartmentId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByRole
		public UserList GetDepartmentLinksByRole(int DepartmentId, int RoleId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByRole", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByRoleP
		public UserList GetDepartmentLinksByRoleP(int DepartmentId, int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByAccount
		public UserList GetDepartmentLinksByAccount(int DepartmentId, int AccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByAccountP
		public UserList GetDepartmentLinksByAccountP(int DepartmentId, int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByEmail
		public UserList GetDepartmentLinksByEmail(int DepartmentId, int EmailId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByEmail", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByEmailP
		public UserList GetDepartmentLinksByEmailP(int DepartmentId, int EmailId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByEmailP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByUsrGroup
		public UserList GetDepartmentLinksByUsrGroup(int DepartmentId, int UsrGroupId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByUsrGroup", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetDepartmentLinksByUsrGroupP
		public UserList GetDepartmentLinksByUsrGroupP(int DepartmentId, int UsrGroupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetDepartmentLinksByUsrGroupP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinks
		public UserList GetAccountLinks(int AccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksP
		public UserList GetAccountLinksP(int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksPS
		public UserList GetAccountLinksPS(			int AccountId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksPS", Connection);
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

		#region GetAccountLinksByRole
		public UserList GetAccountLinksByRole(int AccountId, int RoleId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByRole", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByRoleP
		public UserList GetAccountLinksByRoleP(int AccountId, int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByDepartment
		public UserList GetAccountLinksByDepartment(int AccountId, int DepartmentId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByDepartment", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByDepartmentP
		public UserList GetAccountLinksByDepartmentP(int AccountId, int DepartmentId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByDepartmentP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByEmail
		public UserList GetAccountLinksByEmail(int AccountId, int EmailId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByEmail", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByEmailP
		public UserList GetAccountLinksByEmailP(int AccountId, int EmailId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByEmailP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByUsrGroup
		public UserList GetAccountLinksByUsrGroup(int AccountId, int UsrGroupId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByUsrGroup", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksByUsrGroupP
		public UserList GetAccountLinksByUsrGroupP(int AccountId, int UsrGroupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetAccountLinksByUsrGroupP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinks
		public UserList GetEmailLinks(int EmailId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksP
		public UserList GetEmailLinksP(int EmailId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksPS
		public UserList GetEmailLinksPS(			int EmailId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByRole
		public UserList GetEmailLinksByRole(int EmailId, int RoleId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByRole", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByRoleP
		public UserList GetEmailLinksByRoleP(int EmailId, int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByDepartment
		public UserList GetEmailLinksByDepartment(int EmailId, int DepartmentId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByDepartment", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByDepartmentP
		public UserList GetEmailLinksByDepartmentP(int EmailId, int DepartmentId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByDepartmentP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByAccount
		public UserList GetEmailLinksByAccount(int EmailId, int AccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByAccountP
		public UserList GetEmailLinksByAccountP(int EmailId, int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByUsrGroup
		public UserList GetEmailLinksByUsrGroup(int EmailId, int UsrGroupId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByUsrGroup", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetEmailLinksByUsrGroupP
		public UserList GetEmailLinksByUsrGroupP(int EmailId, int UsrGroupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetEmailLinksByUsrGroupP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinks
		public UserList GetUsrGroupLinks(int UsrGroupId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksP
		public UserList GetUsrGroupLinksP(int UsrGroupId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksPS
		public UserList GetUsrGroupLinksPS(			int UsrGroupId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByRole
		public UserList GetUsrGroupLinksByRole(int UsrGroupId, int RoleId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByRole", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByRoleP
		public UserList GetUsrGroupLinksByRoleP(int UsrGroupId, int RoleId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByRoleP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByDepartment
		public UserList GetUsrGroupLinksByDepartment(int UsrGroupId, int DepartmentId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByDepartment", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByDepartmentP
		public UserList GetUsrGroupLinksByDepartmentP(int UsrGroupId, int DepartmentId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByDepartmentP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByAccount
		public UserList GetUsrGroupLinksByAccount(int UsrGroupId, int AccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByAccountP
		public UserList GetUsrGroupLinksByAccountP(int UsrGroupId, int AccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByEmail
		public UserList GetUsrGroupLinksByEmail(int UsrGroupId, int EmailId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByEmail", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUsrGroupLinksByEmailP
		public UserList GetUsrGroupLinksByEmailP(int UsrGroupId, int EmailId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_User_GetUsrGroupLinksByEmailP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UsrGroupId", UsrGroupId));
			command.Parameters.Add(new SqlParameter("@EmailId", EmailId));
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

		#region GetUserId
		public int GetUserId(
			string Login)
		{
			SqlCommand command = new SqlCommand("User_GetUserId", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Login", Login));
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

		#region CopyUserAccounts
		public int CopyUserAccounts(
			int SourceUserId,
			int DestinationUserId)
		{
			SqlCommand command = new SqlCommand("User_CopyUserAccounts", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SourceUserId", SourceUserId));
			command.Parameters.Add(new SqlParameter("@DestinationUserId", DestinationUserId));
			Connection.Open();
			object result = command.ExecuteScalar();
			Connection.Close();
			return Convert.ToInt32(result);
		}
		#endregion

		#region ReplaceUserAccounts
		public int ReplaceUserAccounts(
			int SourceUserId,
			int DestinationUserId)
		{
			SqlCommand command = new SqlCommand("User_ReplaceUserAccounts", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SourceUserId", SourceUserId));
			command.Parameters.Add(new SqlParameter("@DestinationUserId", DestinationUserId));
			Connection.Open();
			object result = command.ExecuteScalar();
			Connection.Close();
			return Convert.ToInt32(result);
		}
		#endregion

		#region GetRoleLinksWithQueryP
		public UserTableGetRoleLinksWithQueryP GetRoleLinksWithQueryP(
			string Query,
			int RoleId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("User_GetRoleLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserTableGetRoleLinksWithQueryP(command);
		}
		#endregion

		#region GetRoleLinksWithQuery
		public UserTableGetRoleLinksWithQuery GetRoleLinksWithQuery(
			string Query,
			int RoleId)
		{
			SqlCommand command = new SqlCommand("User_GetRoleLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
			return loadUserTableGetRoleLinksWithQuery(command);
		}
		#endregion

		#region GetUserGroupLinksWithQueryP
		public UserTableGetUserGroupLinksWithQueryP GetUserGroupLinksWithQueryP(
			string Query,
			int UserGroupId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("User_GetUserGroupLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@UserGroupId", UserGroupId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserTableGetUserGroupLinksWithQueryP(command);
		}
		#endregion

		#region GetUserGroupLinksWithQuery
		public UserTableGetUserGroupLinksWithQuery GetUserGroupLinksWithQuery(
			string Query,
			int UserGroupId)
		{
			SqlCommand command = new SqlCommand("User_GetUserGroupLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@UserGroupId", UserGroupId));
			return loadUserTableGetUserGroupLinksWithQuery(command);
		}
		#endregion

		#region GetAccountLinksWithQueryP
		public UserTableGetAccountLinksWithQueryP GetAccountLinksWithQueryP(
			string Query,
			int AccountId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("User_GetAccountLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserTableGetAccountLinksWithQueryP(command);
		}
		#endregion

		#region GetAccountLinksWithQuery
		public UserTableGetAccountLinksWithQuery GetAccountLinksWithQuery(
			string Query,
			int AccountId)
		{
			SqlCommand command = new SqlCommand("User_GetAccountLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			return loadUserTableGetAccountLinksWithQuery(command);
		}
		#endregion

		#region GetDepartmentLinksWithQueryP
		public UserTableGetDepartmentLinksWithQueryP GetDepartmentLinksWithQueryP(
			string Query,
			int DepartmentId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("User_GetDepartmentLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadUserTableGetDepartmentLinksWithQueryP(command);
		}
		#endregion

		#region GetDepartmentLinksWithQuery
		public UserTableGetDepartmentLinksWithQuery GetDepartmentLinksWithQuery(
			string Query,
			int DepartmentId)
		{
			SqlCommand command = new SqlCommand("User_GetDepartmentLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
			return loadUserTableGetDepartmentLinksWithQuery(command);
		}
		#endregion

		#region GetByCollege
		public UserTableGetByCollege GetByCollege(
			int CollegeId)
		{
			SqlCommand command = new SqlCommand("User_GetByCollege", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
			return loadUserTableGetByCollege(command);
		}
		#endregion

		#region private

		private UserTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 13)
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
					r.CampusCode = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.UniId = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.ConAgree = reader.GetBoolean(7);

				if (reader[8] != System.DBNull.Value)
					r.OnlineAccess = reader.GetBoolean(8);

				if (reader[9] != System.DBNull.Value)
					r.Created = reader.GetDateTime(9);

				if (reader[10] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(10);

				if (reader[11] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(11);

				if (reader[12] != System.DBNull.Value)
					r.FullName = reader.GetString(12);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(13))
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

		private UserTableGetRoleLinksWithQueryP loadUserTableGetRoleLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetRoleLinksWithQueryP r = new UserRowGetRoleLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetRoleLinksWithQueryP(rows, totalCount);
		}

		private UserTableGetRoleLinksWithQuery loadUserTableGetRoleLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetRoleLinksWithQuery r = new UserRowGetRoleLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetRoleLinksWithQuery(rows, totalCount);
		}

		private UserTableGetUserGroupLinksWithQueryP loadUserTableGetUserGroupLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetUserGroupLinksWithQueryP r = new UserRowGetUserGroupLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetUserGroupLinksWithQueryP(rows, totalCount);
		}

		private UserTableGetUserGroupLinksWithQuery loadUserTableGetUserGroupLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetUserGroupLinksWithQuery r = new UserRowGetUserGroupLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetUserGroupLinksWithQuery(rows, totalCount);
		}

		private UserTableGetAccountLinksWithQueryP loadUserTableGetAccountLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetAccountLinksWithQueryP r = new UserRowGetAccountLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetAccountLinksWithQueryP(rows, totalCount);
		}

		private UserTableGetAccountLinksWithQuery loadUserTableGetAccountLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetAccountLinksWithQuery r = new UserRowGetAccountLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetAccountLinksWithQuery(rows, totalCount);
		}

		private UserTableGetDepartmentLinksWithQueryP loadUserTableGetDepartmentLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetDepartmentLinksWithQueryP r = new UserRowGetDepartmentLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetDepartmentLinksWithQueryP(rows, totalCount);
		}

		private UserTableGetDepartmentLinksWithQuery loadUserTableGetDepartmentLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetDepartmentLinksWithQuery r = new UserRowGetDepartmentLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(2);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetDepartmentLinksWithQuery(rows, totalCount);
		}

		private UserTableGetByCollege loadUserTableGetByCollege(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				UserRowGetByCollege r = new UserRowGetByCollege();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullName = reader.GetString(1);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new UserTableGetByCollege(rows, totalCount);
		}

		#endregion
	}
}