using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccountSet : Core.DbRecord
	{
		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecords", parameters);
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

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsP", parameters);
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

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByUser", parameters);
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
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByUserP", parameters);
		}
		#endregion

		#region GetAllRecordsByUser
		public static DataSet GetAllRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByUser", parameters);
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
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByUserP", parameters);
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

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByUserPS", parameters);
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

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByUserPS", parameters);
		}
		#endregion

		#region GetRecordsByAccCategory
		public static DataSet GetRecordsByAccCategory(int AccCategoryId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByAccCategory", parameters);
		}
		#endregion

		#region GetRecordsByAccCategoryP
		public static DataSet GetRecordsByAccCategoryP(
			int AccCategoryId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByAccCategoryP", parameters);
		}
		#endregion

		#region GetAllRecordsByAccCategory
		public static DataSet GetAllRecordsByAccCategory(int AccCategoryId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByAccCategory", parameters);
		}
		#endregion

		#region GetAllRecordsByAccCategoryP
		public static DataSet GetAllRecordsByAccCategoryP(
			int AccCategoryId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByAccCategoryP", parameters);
		}
		#endregion

		#region GetRecordsByAccCategoryPS
		public static DataSet GetRecordsByAccCategoryPS(
			int AccCategoryId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetRecordsByAccCategoryPS", parameters);
		}
		#endregion

		#region GetAllRecordsByAccCategoryPS
		public static DataSet GetAllRecordsByAccCategoryPS(
			int AccCategoryId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			AccountSet x = new AccountSet();
			return x.ExecDataSet("vw_Accounts_GetAllRecordsByAccCategoryPS", parameters);
		}
		#endregion

		#region private

		private AccountSet() : base("vw_Accounts") {}

		#endregion
	}
}