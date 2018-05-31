using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class DepartmentSet : Core.DbRecord
	{
		#region Create
		public static int Create(
			int CollegeId,
			string Name)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CollegeId", CollegeId),
				new SqlParameter("@Name", Name)};

			DepartmentSet x = new DepartmentSet();
			return Convert.ToInt32(x.ExecScalar("a_Department_Create", parameters));
		}
		#endregion

		#region Delete
		public static int Delete(int Id)
		{
			SqlParameter[] parameters = { new SqlParameter("@Id", Id)};
			DepartmentSet x = new DepartmentSet();
			return x.ExecNonQuery("a_Department_Delete", parameters);
		}
		#endregion

		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecords", parameters);
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

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsP", parameters);
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

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsPS", parameters);
		}
		#endregion

		#region GetRecordsByCollege
		public static DataSet GetRecordsByCollege(int CollegeId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CollegeId", CollegeId),
				new SqlParameter("@SortExp", SortExp)};
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByCollege", parameters);
		}
		#endregion

		#region GetRecordsByCollegeP
		public static DataSet GetRecordsByCollegeP(
			int CollegeId,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CollegeId", CollegeId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum)};
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByCollegeP", parameters);
		}
		#endregion

		#region GetRecordsByCollegePS
		public static DataSet GetRecordsByCollegePS(
			int CollegeId,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@CollegeId", CollegeId),
				new SqlParameter("@SortExp", SortExp),
				new SqlParameter("@PageSize", PageSize),
				new SqlParameter("@PageNum", PageNum),
				new SqlParameter("@SearchField", SearchField),
				new SqlParameter("@SearchKeyword", SearchKeyword)};

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByCollegePS", parameters);
		}
		#endregion

		#region GetRecordsByUser
		public static DataSet GetRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByUser", parameters);
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
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByUserP", parameters);
		}
		#endregion

		#region GetAllRecordsByUser
		public static DataSet GetAllRecordsByUser(int UserId, string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId),
				new SqlParameter("@SortExp", SortExp)};
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetAllRecordsByUser", parameters);
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
			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetAllRecordsByUserP", parameters);
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

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetRecordsByUserPS", parameters);
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

			DepartmentSet x = new DepartmentSet();
			return x.ExecDataSet("a_Department_GetAllRecordsByUserPS", parameters);
		}
		#endregion

		#region private

		private DepartmentSet() : base("a_Department") {}

		#endregion
	}
}