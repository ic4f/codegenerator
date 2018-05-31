using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Accounts_Payout : Core.DataClass
	{
		#region constructor
		public Accounts_Payout(int recordId) : base()
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

		#region string AccountNumber
		public string AccountNumber
		{
			get { return accountNumber; }
		}
		#endregion

		#region decimal Payout
		public decimal Payout
		{
			get { return payout; }
		}
		#endregion

		#region DateTime PayoutDate
		public DateTime PayoutDate
		{
			get { return payoutDate; }
		}
		#endregion

		#region string Modified
		public string Modified
		{
			get { return modified; }
		}
		#endregion

		#region int ModifiedBy
		public int ModifiedBy
		{
			get { return modifiedBy; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_Accounts_Payout_GetRecord", Connection);
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
				accountNumber = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				payout = reader.GetDecimal(2);

			if (reader[3] != System.DBNull.Value)
				payoutDate = reader.GetDateTime(3);

			if (reader[4] != System.DBNull.Value)
				modified = reader.GetString(4);

			if (reader[5] != System.DBNull.Value)
				modifiedBy = reader.GetInt32(5);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string accountNumber;
		private decimal payout;
		private DateTime payoutDate;
		private string modified;
		private int modifiedBy;

		#endregion
	}
}