using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class RoleSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			string Description,
			short Rank)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@Description", Description),
				new SqlParameter("@Rank", Rank)};

			RoleSet x = new RoleSet();
			return Convert.ToInt32(x.ExecScalar("a_Role_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			RoleSet x = new RoleSet();
			return x.ExecNonQuery("a_Role_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecords", parameters);
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

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsP", parameters);
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

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByPermission
		public static DataSet GetRecordsByPermission(int PermissionId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByPermission", parameters);
		}
		#endregion

		#region GetRecordsByPermissionP
		public static DataSet GetRecordsByPermissionP(
			int PermissionId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByPermissionP", parameters);
		}
		#endregion

		#region GetAllRecordsByPermission
		public static DataSet GetAllRecordsByPermission(int PermissionId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByPermission", parameters);
		}
		#endregion

		#region GetAllRecordsByPermissionP
		public static DataSet GetAllRecordsByPermissionP(
			int PermissionId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByPermissionP", parameters);
		}
		#endregion

		#region GetRecordsByPermissionPS
		public static DataSet GetRecordsByPermissionPS(
			int PermissionId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByPermissionPS", parameters);
		}
		#endregion

		#region GetAllRecordsByPermissionPS
		public static DataSet GetAllRecordsByPermissionPS(
			int PermissionId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@PermissionId", PermissionId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByPermissionPS", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByUser", parameters);
		}
		#endregion

		#region GetRecordsByUserP
		public static DataSet GetRecordsByUserP(
			int UserId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByUserP", parameters);
		}
		#endregion

		#region GetAllRecordsByUser
		public static DataSet GetAllRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByUser", parameters);
		}
		#endregion

		#region GetAllRecordsByUserP
		public static DataSet GetAllRecordsByUserP(
			int UserId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByUserP", parameters);
		}
		#endregion

		#region GetRecordsByUserPS
		public static DataSet GetRecordsByUserPS(
			int UserId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetRecordsByUserPS", parameters);
		}
		#endregion

		#region GetAllRecordsByUserPS
		public static DataSet GetAllRecordsByUserPS(
			int UserId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			RoleSet x = new RoleSet();
			return x.ExecDataSet("a_Role_GetAllRecordsByUserPS", parameters);
		}
		#endregion

		#region private

		private RoleSet() : base("a_Role") {}

		#endregion
	}
}