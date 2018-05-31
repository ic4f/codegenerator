using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class PermissionSet : Core.DbRecord
	{
		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecords", parameters);
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

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsP", parameters);
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

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByPermissionCategory
		public static DataSet GetRecordsByPermissionCategory(int CategoryId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CategoryId", CategoryId),
				new SqlParameter("@SortExp", SortExp)};
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByPermissionCategory", parameters);
		}
		#endregion

		#region GetRecordsByPermissionCategoryP
		public static DataSet GetRecordsByPermissionCategoryP(
			int CategoryId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CategoryId", CategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByPermissionCategoryP", parameters);
		}
		#endregion

		#region GetRecordsByPermissionCategoryPS
		public static DataSet GetRecordsByPermissionCategoryPS(
			int CategoryId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CategoryId", CategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByPermissionCategoryPS", parameters);
		}
		#endregion

		#region GetRecordsByRole
		public static DataSet GetRecordsByRole(int RoleId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp)};
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByRole", parameters);
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
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByRoleP", parameters);
		}
		#endregion

		#region GetAllRecordsByRole
		public static DataSet GetAllRecordsByRole(int RoleId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId),
				new SqlParameter("@SortExp", SortExp)};
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetAllRecordsByRole", parameters);
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
			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetAllRecordsByRoleP", parameters);
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

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetRecordsByRolePS", parameters);
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

			PermissionSet x = new PermissionSet();
			return x.ExecDataSet("a_Permission_GetAllRecordsByRolePS", parameters);
		}
		#endregion

		#region GetPermissionCodesByUser
		public static ArrayList GetPermissionCodesByUser(
			int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId)};

			PermissionSet x = new PermissionSet();
			return x.ExecFirstColumn("Permission_GetPermissionCodesByUser", parameters);
		}
		#endregion

		#region private

		private PermissionSet() : base("a_Permission") {}

		#endregion
	}
}