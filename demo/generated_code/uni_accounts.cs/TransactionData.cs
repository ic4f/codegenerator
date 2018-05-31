using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class TransactionData : Core.DataClass
	{
		#region constructor 
		public TransactionData() : base() {}
		#endregion

		#region GetList
		public TransactionList GetList()
		{
			SqlCommand command = new SqlCommand("a_Transaction_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public TransactionTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Transaction_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public TransactionTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Transaction_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public TransactionTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Transaction_GetRecordsPS", Connection);
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

		private TransactionTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 6)
				includeSelect = true;

			while (reader.Read())
			{
				TransactionRow r = new TransactionRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.DonorPayee = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.TransDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.TransDate = reader.GetDateTime(4);

				if (reader[5] != System.DBNull.Value)
					r.TransAmount = reader.GetDecimal(5);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(6))
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
			return new TransactionTable(rows, totalCount);
		}

		private TransactionList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				TransactionListRow r = new TransactionListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(1))
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
			return new TransactionList(rows, totalCount);
		}

		#endregion
	}
}