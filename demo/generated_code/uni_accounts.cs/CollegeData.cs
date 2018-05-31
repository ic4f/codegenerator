using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class CollegeData : Core.DataClass
	{
		#region constructor 
		public CollegeData() : base() {}
		#endregion

		#region Create
		public int Create(
			string Name)
		{
			SqlCommand command = new SqlCommand("a_College_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

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
			SqlCommand command = new SqlCommand("a_College_Delete", Connection);
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
		public CollegeList GetList()
		{
			SqlCommand command = new SqlCommand("a_College_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public CollegeTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_College_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public CollegeTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_College_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public CollegeTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_College_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region private

		private CollegeTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				CollegeRow r = new CollegeRow();
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
			return new CollegeTable(rows, totalCount);
		}

		private CollegeList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 2)
				includeSelect = true;

			while (reader.Read())
			{
				CollegeListRow r = new CollegeListRow();
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
			return new CollegeList(rows, totalCount);
		}

		#endregion
	}
}