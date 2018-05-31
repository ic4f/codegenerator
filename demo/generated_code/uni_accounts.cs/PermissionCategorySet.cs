using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class PermissionCategorySet : Core.DbRecord
	{
		#region GetRecords
		public static DataSet GetRecords(string SortExp)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@SortExp", SortExp)};

			PermissionCategorySet x = new PermissionCategorySet();
			return x.ExecDataSet("a_PermissionCategory_GetRecords", parameters);
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

			PermissionCategorySet x = new PermissionCategorySet();
			return x.ExecDataSet("a_PermissionCategory_GetRecordsP", parameters);
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

			PermissionCategorySet x = new PermissionCategorySet();
			return x.ExecDataSet("a_PermissionCategory_GetRecordsPS", parameters);
		}
		#endregion

		#region private

		private PermissionCategorySet() : base("a_PermissionCategory") {}

		#endregion
	}
}