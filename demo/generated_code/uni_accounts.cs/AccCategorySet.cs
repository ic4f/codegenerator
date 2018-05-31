using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccCategorySet : Core.DbRecord
	{
		#region Create
		public static int Create(
			int UserId,
			string Description,
			short Rank,
			string ModifiedBy)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@Description", Description),
				new SqlParameter("@Rank", Rank),
				new SqlParameter("@ModifiedBy", ModifiedBy)};

			AccCategorySet x = new AccCategorySet();
			return Convert.ToInt32(x.ExecScalar("a_AccCategory_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			AccCategorySet x = new AccCategorySet();
			return x.ExecNonQuery("a_AccCategory_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecords", parameters);
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

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsP", parameters);
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

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByUser", parameters);
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
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByUserP", parameters);
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

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByUserPS", parameters);
		}
		#endregion

		#region GetRecordsByAccount
		public static DataSet GetRecordsByAccount(string AccountId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp)};
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByAccount", parameters);
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
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByAccountP", parameters);
		}
		#endregion

		#region GetAllRecordsByAccount
		public static DataSet GetAllRecordsByAccount(string AccountId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccountId", AccountId),
				new SqlParameter("@SortExp", SortExp)};
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetAllRecordsByAccount", parameters);
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
			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetAllRecordsByAccountP", parameters);
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

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetRecordsByAccountPS", parameters);
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

			AccCategorySet x = new AccCategorySet();
			return x.ExecDataSet("a_AccCategory_GetAllRecordsByAccountPS", parameters);
		}
		#endregion

		#region private

		private AccCategorySet() : base("a_AccCategory") {}

		#endregion
	}
}