using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class EmailSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			string Subject,
			string Message,
			DateTime Sent,
			string SentBy)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@Subject", Subject),
				new SqlParameter("@Message", Message),
				new SqlParameter("@Sent", Sent),
				new SqlParameter("@SentBy", SentBy)};

			EmailSet x = new EmailSet();
			return Convert.ToInt32(x.ExecScalar("a_Email_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			EmailSet x = new EmailSet();
			return x.ExecNonQuery("a_Email_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			EmailSet x = new EmailSet();
			return x.ExecDataSet("a_Email_GetRecords", parameters);
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

			EmailSet x = new EmailSet();
			return x.ExecDataSet("a_Email_GetRecordsP", parameters);
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

			EmailSet x = new EmailSet();
			return x.ExecDataSet("a_Email_GetRecordsPS", parameters);
		}
		#endregion

		#region private

		private EmailSet() : base("a_Email") {}

		#endregion
	}
}