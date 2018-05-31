using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class CollegeSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			string Name)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@Name", Name)};

			CollegeSet x = new CollegeSet();
			return Convert.ToInt32(x.ExecScalar("a_College_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			CollegeSet x = new CollegeSet();
			return x.ExecNonQuery("a_College_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			CollegeSet x = new CollegeSet();
			return x.ExecDataSet("a_College_GetRecords", parameters);
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

			CollegeSet x = new CollegeSet();
			return x.ExecDataSet("a_College_GetRecordsP", parameters);
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

			CollegeSet x = new CollegeSet();
			return x.ExecDataSet("a_College_GetRecordsPS", parameters);
		}
		#endregion

		#region private

		private CollegeSet() : base("a_College") {}

		#endregion
	}
}