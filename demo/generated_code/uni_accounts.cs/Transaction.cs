using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Transaction : Core.DataClass
	{
		#region constructor
		public Transaction(int recordId) : base()
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

		#region string DonorPayee
		public string DonorPayee
		{
			get { return donorPayee; }
		}
		#endregion

		#region string TransDescription
		public string TransDescription
		{
			get { return transDescription; }
		}
		#endregion

		#region DateTime TransDate
		public DateTime TransDate
		{
			get { return transDate; }
		}
		#endregion

		#region decimal TransAmount
		public decimal TransAmount
		{
			get { return transAmount; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_Transaction_GetRecord", Connection);
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
				donorPayee = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				transDescription = reader.GetString(3);

			if (reader[4] != System.DBNull.Value)
				transDate = reader.GetDateTime(4);

			if (reader[5] != System.DBNull.Value)
				transAmount = reader.GetDecimal(5);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string accountNumber;
		private string donorPayee;
		private string transDescription;
		private DateTime transDate;
		private decimal transAmount;

		#endregion
	}
}