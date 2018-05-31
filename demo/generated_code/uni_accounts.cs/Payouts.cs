using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Payouts : Core.DataClass
	{
		#region constructor
		public Payouts(int recordId) : base()
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

		#region string Account
		public string Account
		{
			get { return account; }
		}
		#endregion

		#region decimal Payout
		public decimal Payout
		{
			get { return payout; }
		}
		#endregion

		#region DateTime Date
		public DateTime Date
		{
			get { return date; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_Payouts_GetRecord", Connection);
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
				account = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				payout = reader.GetDecimal(2);

			if (reader[3] != System.DBNull.Value)
				date = reader.GetDateTime(3);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string account;
		private decimal payout;
		private DateTime date;

		#endregion
	}
}