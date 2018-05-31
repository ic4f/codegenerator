using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UserSet : Core.DbRecord
	{
		#region Create
		public static int Create(
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
			SqlParameter[] parameters = { 
				new SqlParameter("@Login", Login),
				new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)),
				new SqlParameter("@FirstName", FirstName),
				new SqlParameter("@LastName", LastName),
				new SqlParameter("@CampusCode", CampusCode),
				new SqlParameter("@UniId", UniId),
				new SqlParameter("@ConAgree", ConAgree),
				new SqlParameter("@OnlineAccess", OnlineAccess),
				new SqlParameter("@ModifiedBy", ModifiedBy)};

			UserSet x = new UserSet();
			return Convert.ToInt32(x.ExecScalar("a_User_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			UserSet x = new UserSet();
			return x.ExecNonQuery("a_User_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecords", parameters);
		}
		#endregion

		#region GetRecordsP
		public static DataSet GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsP", parameters);
		}
		#endregion

		#region GetRecordsPS
		public static DataSet GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByUsrGroup
		public static DataSet GetRecordsByUsrGroup(int UsrGroupId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByUsrGroup", parameters);
		}
		#endregion

		#region GetRecordsByUsrGroupP
		public static DataSet GetRecordsByUsrGroupP(
			int UsrGroupId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByUsrGroupP", parameters);
		}
		#endregion

		#region GetAllRecordsByUsrGroup
		public static DataSet GetAllRecordsByUsrGroup(int UsrGroupId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByUsrGroup", parameters);
		}
		#endregion

		#region GetAllRecordsByUsrGroupP
		public static DataSet GetAllRecordsByUsrGroupP(
			int UsrGroupId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByUsrGroupP", parameters);
		}
		#endregion

		#region GetRecordsByUsrGroupPS
		public static DataSet GetRecordsByUsrGroupPS(
			int UsrGroupId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByUsrGroupPS", parameters);
		}
		#endregion

		#region GetAllRecordsByUsrGroupPS
		public static DataSet GetAllRecordsByUsrGroupPS(
			int UsrGroupId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByUsrGroupPS", parameters);
		}
		#endregion

		#region GetRecordsByDepartment
		public static DataSet GetRecordsByDepartment(int DepartmentId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByDepartment", parameters);
		}
		#endregion

		#region GetRecordsByDepartmentP
		public static DataSet GetRecordsByDepartmentP(
			int DepartmentId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByDepartmentP", parameters);
		}
		#endregion

		#region GetAllRecordsByDepartment
		public static DataSet GetAllRecordsByDepartment(int DepartmentId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByDepartment", parameters);
		}
		#endregion

		#region GetAllRecordsByDepartmentP
		public static DataSet GetAllRecordsByDepartmentP(
			int DepartmentId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByDepartmentP", parameters);
		}
		#endregion

		#region GetRecordsByDepartmentPS
		public static DataSet GetRecordsByDepartmentPS(
			int DepartmentId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByDepartmentPS", parameters);
		}
		#endregion

		#region GetAllRecordsByDepartmentPS
		public static DataSet GetAllRecordsByDepartmentPS(
			int DepartmentId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByDepartmentPS", parameters);
		}
		#endregion

		#region GetRecordsByAccount
		public static DataSet GetRecordsByAccount(string AccountId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByAccount", parameters);
		}
		#endregion

		#region GetRecordsByAccountP
		public static DataSet GetRecordsByAccountP(
			string AccountId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByAccountP", parameters);
		}
		#endregion

		#region GetAllRecordsByAccount
		public static DataSet GetAllRecordsByAccount(string AccountId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByAccount", parameters);
		}
		#endregion

		#region GetAllRecordsByAccountP
		public static DataSet GetAllRecordsByAccountP(
			string AccountId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByAccountP", parameters);
		}
		#endregion

		#region GetRecordsByAccountPS
		public static DataSet GetRecordsByAccountPS(
			string AccountId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByAccountPS", parameters);
		}
		#endregion

		#region GetAllRecordsByAccountPS
		public static DataSet GetAllRecordsByAccountPS(
			string AccountId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByAccountPS", parameters);
		}
		#endregion

		#region GetRecordsByRole
		public static DataSet GetRecordsByRole(int RoleId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByRole", parameters);
		}
		#endregion

		#region GetRecordsByRoleP
		public static DataSet GetRecordsByRoleP(
			int RoleId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByRoleP", parameters);
		}
		#endregion

		#region GetAllRecordsByRole
		public static DataSet GetAllRecordsByRole(int RoleId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByRole", parameters);
		}
		#endregion

		#region GetAllRecordsByRoleP
		public static DataSet GetAllRecordsByRoleP(
			int RoleId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByRoleP", parameters);
		}
		#endregion

		#region GetRecordsByRolePS
		public static DataSet GetRecordsByRolePS(
			int RoleId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetRecordsByRolePS", parameters);
		}
		#endregion

		#region GetAllRecordsByRolePS
		public static DataSet GetAllRecordsByRolePS(
			int RoleId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			UserSet x = new UserSet();
			return x.ExecDataSet("a_User_GetAllRecordsByRolePS", parameters);
		}
		#endregion

		#region ValidateUser
		public static int ValidateUser(
			string Login,
			string Password)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@Login", Login),
				new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password))};

			UserSet x = new UserSet();
			return Convert.ToInt32(x.ExecScalar("User_ValidateUser", parameters));
		}
		#endregion

		#region LogUser
		public static void LogUser(
			int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId)};

			UserSet x = new UserSet();
		}
		#endregion

		#region private

		private UserSet() : base("a_User") {}

		#endregion
	}
}