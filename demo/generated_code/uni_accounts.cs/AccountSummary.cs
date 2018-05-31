using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class AccountSummary : Core.DataClass
	{
		#region constructor
		public AccountSummary(int recordId) : base()
		{
			loadRecord(recordId);
		}
		#endregion

		#region int Id
		public int Id
		{
			get { return id; }
		}
		#endregion

		#region int AccountId
		public int AccountId
		{
			get { return accountId; }
		}
		#endregion

		#region DateTime StartDate
		public DateTime StartDate
		{
			get { return startDate; }
		}
		#endregion

		#region DateTime EndDate
		public DateTime EndDate
		{
			get { return endDate; }
		}
		#endregion

		#region decimal BeginningBalance
		public decimal BeginningBalance
		{
			get { return beginningBalance; }
		}
		#endregion

		#region decimal EndingBalance
		public decimal EndingBalance
		{
			get { return endingBalance; }
		}
		#endregion

		#region decimal TotalRevenues
		public decimal TotalRevenues
		{
			get { return totalRevenues; }
		}
		#endregion

		#region decimal TotalExpences
		public decimal TotalExpences
		{
			get { return totalExpences; }
		}
		#endregion

		#region DateTime CreatedDate
		public DateTime CreatedDate
		{
			get { return createdDate; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_AccountSummary_GetRecord", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", recordId));

			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			if (!reader.HasRows)
				throw new Core.AppException("Record with id = " + recordId + " not found.");
			reader.Read();

			if (reader[0] != System.DBNull.Value)
				id = reader.GetInt32(0);

			if (reader[1] != System.DBNull.Value)
				accountId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				startDate = reader.GetDateTime(2);

			if (reader[3] != System.DBNull.Value)
				endDate = reader.GetDateTime(3);

			if (reader[4] != System.DBNull.Value)
				beginningBalance = reader.GetDecimal(4);

			if (reader[5] != System.DBNull.Value)
				endingBalance = reader.GetDecimal(5);

			if (reader[6] != System.DBNull.Value)
				totalRevenues = reader.GetDecimal(6);

			if (reader[7] != System.DBNull.Value)
				totalExpences = reader.GetDecimal(7);

			if (reader[8] != System.DBNull.Value)
				createdDate = reader.GetDateTime(8);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int accountId;
		private DateTime startDate;
		private DateTime endDate;
		private decimal beginningBalance;
		private decimal endingBalance;
		private decimal totalRevenues;
		private decimal totalExpences;
		private DateTime createdDate;

		#endregion
	}
}