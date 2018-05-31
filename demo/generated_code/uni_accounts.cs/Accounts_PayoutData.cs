using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class Accounts_PayoutData : Core.DataClass
	{
		#region constructor 
		public Accounts_PayoutData() : base() {}
		#endregion

		#region GetList
		public Accounts_PayoutList GetList()
		{
			SqlCommand command = new SqlCommand("a_Accounts_Payout_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public Accounts_PayoutTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Accounts_Payout_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public Accounts_PayoutTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Accounts_Payout_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public Accounts_PayoutTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Accounts_Payout_GetRecordsPS", Connection);
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

		private Accounts_PayoutTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 6)
				includeSelect = true;

			while (reader.Read())
			{
				Accounts_PayoutRow r = new Accounts_PayoutRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.Payout = reader.GetDecimal(2);

				if (reader[3] != System.DBNull.Value)
					r.PayoutDate = reader.GetDateTime(3);

				if (reader[4] != System.DBNull.Value)
					r.Modified = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.ModifiedBy = reader.GetInt32(5);

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
			return new Accounts_PayoutTable(rows, totalCount);
		}

		private Accounts_PayoutList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				Accounts_PayoutListRow r = new Accounts_PayoutListRow();
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
			return new Accounts_PayoutList(rows, totalCount);
		}

		#endregion
	}
}