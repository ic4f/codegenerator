using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccountSummaryData : Core.DataClass
	{
		#region constructor 
		public AccountSummaryData() : base() {}
		#endregion

		#region GetList
		public AccountSummaryList GetList()
		{
			SqlCommand command = new SqlCommand("a_AccountSummary_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public AccountSummaryTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_AccountSummary_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public AccountSummaryTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_AccountSummary_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region private

		private AccountSummaryTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 9)
				includeSelect = true;

			while (reader.Read())
			{
				AccountSummaryRow r = new AccountSummaryRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.StartDate = reader.GetDateTime(2);

				if (reader[3] != System.DBNull.Value)
					r.EndDate = reader.GetDateTime(3);

				if (reader[4] != System.DBNull.Value)
					r.BeginningBalance = reader.GetDecimal(4);

				if (reader[5] != System.DBNull.Value)
					r.EndingBalance = reader.GetDecimal(5);

				if (reader[6] != System.DBNull.Value)
					r.TotalRevenues = reader.GetDecimal(6);

				if (reader[7] != System.DBNull.Value)
					r.TotalExpences = reader.GetDecimal(7);

				if (reader[8] != System.DBNull.Value)
					r.CreatedDate = reader.GetDateTime(8);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(9))
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
			return new AccountSummaryTable(rows, totalCount);
		}

		private AccountSummaryList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				AccountSummaryListRow r = new AccountSummaryListRow();
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
			return new AccountSummaryList(rows, totalCount);
		}

		#endregion
	}
}