using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class DepartmentData : Core.DataClass
	{
		#region constructor 
		public DepartmentData() : base() {}
		#endregion

		#region Create
		public int Create(
			int CollegeId,
			string Name)
		{
			SqlCommand command = new SqlCommand("a_Department_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (CollegeId > 0)
				command.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
			else
				command.Parameters.Add(new SqlParameter("@CollegeId", System.DBNull.Value));

			command.Parameters.Add(new SqlParameter("@Name", Name));
			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id, bool DeleteDependents)
		{
			SqlCommand command = new SqlCommand("a_Department_Delete", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", Id));
			command.Parameters.Add(new SqlParameter("@DeleteDependents", DeleteDependents));
			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();
			return result;
		}
		#endregion

		#region GetList
		public DepartmentList GetList()
		{
			SqlCommand command = new SqlCommand("a_Department_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public DepartmentTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public DepartmentTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public DepartmentTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCollegeField
		public DepartmentTable GetRecordsByCollegeField(int CollegeId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByCollegeField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCollegeFieldP
		public DepartmentTable GetRecordsByCollegeFieldP(
			int CollegeId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByCollegeFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByCollegeFieldPS
		public DepartmentTable GetRecordsByCollegeFieldPS(
			int CollegeId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByCollegeFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLink
		public DepartmentTable GetRecordsByUserLink(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByUserLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkP
		public DepartmentTable GetRecordsByUserLinkP(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByUserLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkPS
		public DepartmentTable GetRecordsByUserLinkPS(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Department_GetRecordsByUserLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetUserLinks
		public DepartmentList GetUserLinks(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Department_GetUserLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksP
		public DepartmentList GetUserLinksP(int UserId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Department_GetUserLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksPS
		public DepartmentList GetUserLinksPS(			int UserId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Department_GetUserLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region private

		private DepartmentTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 4)
				includeSelect = true;

			while (reader.Read())
			{
				DepartmentRow r = new DepartmentRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.CollegeId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Name = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.College_Name = reader.GetString(3);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(4))
					r.Selected = true;

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new DepartmentTable(rows, totalCount);
		}

		private DepartmentList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				DepartmentListRow r = new DepartmentListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.Name = reader.GetString(1);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(2))
					r.Selected = true;

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new DepartmentList(rows, totalCount);
		}

		#endregion
	}
}