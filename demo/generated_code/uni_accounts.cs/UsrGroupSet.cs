using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UsrGroupSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			string Name)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@Name", Name)};

			UsrGroupSet x = new UsrGroupSet();
			return Convert.ToInt32(x.ExecScalar("a_UsrGroup_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			UsrGroupSet x = new UsrGroupSet();
			return x.ExecNonQuery("a_UsrGroup_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecords", parameters);
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

			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecordsP", parameters);
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

			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecordsByUser", parameters);
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
			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecordsByUserP", parameters);
		}
		#endregion

		#region GetAllRecordsByUser
		public static DataSet GetAllRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetAllRecordsByUser", parameters);
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
			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetAllRecordsByUserP", parameters);
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

			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetRecordsByUserPS", parameters);
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

			UsrGroupSet x = new UsrGroupSet();
			return x.ExecDataSet("a_UsrGroup_GetAllRecordsByUserPS", parameters);
		}
		#endregion

		#region private

		private UsrGroupSet() : base("a_UsrGroup") {}

		#endregion
	}
}