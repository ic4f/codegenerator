using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UserLogSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId)};

			UserLogSet x = new UserLogSet();
			return Convert.ToInt32(x.ExecScalar("a_UserLog_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			UserLogSet x = new UserLogSet();
			return x.ExecNonQuery("a_UserLog_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			UserLogSet x = new UserLogSet();
			return x.ExecDataSet("a_UserLog_GetRecords", parameters);
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

			UserLogSet x = new UserLogSet();
			return x.ExecDataSet("a_UserLog_GetRecordsP", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			UserLogSet x = new UserLogSet();
			return x.ExecDataSet("a_UserLog_GetRecordsByUser", parameters);
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
			UserLogSet x = new UserLogSet();
			return x.ExecDataSet("a_UserLog_GetRecordsByUserP", parameters);
		}
		#endregion

		#region private

		private UserLogSet() : base("a_UserLog") {}

		#endregion
	}
}