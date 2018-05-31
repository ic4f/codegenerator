using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class MasterAccount : Core.DataClass
	{
		#region constructor
		public MasterAccount(int recordId) : base()
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
			set { accountId = value; }
		}
		#endregion

		#region string Account_AccountType
		public string Account_AccountType
		{
			get { return account_AccountType; }
		}
		#endregion

		#region string Account_FullAccountNumber
		public string Account_FullAccountNumber
		{
			get { return account_FullAccountNumber; }
		}
		#endregion

		#region string Account_AccountDescription
		public string Account_AccountDescription
		{
			get { return account_AccountDescription; }
		}
		#endregion

		#region string Account_AcctStat
		public string Account_AcctStat
		{
			get { return account_AcctStat; }
		}
		#endregion

		#region string Account_AdminUnit
		public string Account_AdminUnit
		{
			get { return account_AdminUnit; }
		}
		#endregion

		#region DateTime Account_Modified
		public DateTime Account_Modified
		{
			get { return account_Modified; }
		}
		#endregion

		#region string Account_ModifiedBy
		public string Account_ModifiedBy
		{
			get { return account_ModifiedBy; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (AccountId > 0)
				command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			else
				command.Parameters.Add(new SqlParameter("@AccountId", System.DBNull.Value));

			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();

			if (result >= 0)
				loadRecord(id); //some values might have been changed by the db code
			return result;
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecord", Connection);
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
				account_AccountType = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				account_FullAccountNumber = reader.GetString(3);

			if (reader[4] != System.DBNull.Value)
				account_AccountDescription = reader.GetString(4);

			if (reader[5] != System.DBNull.Value)
				account_AcctStat = reader.GetString(5);

			if (reader[6] != System.DBNull.Value)
				account_AdminUnit = reader.GetString(6);

			if (reader[7] != System.DBNull.Value)
				account_Modified = reader.GetDateTime(7);

			if (reader[8] != System.DBNull.Value)
				account_ModifiedBy = reader.GetString(8);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int accountId;
		private string account_AccountType;
		private string account_FullAccountNumber;
		private string account_AccountDescription;
		private string account_AcctStat;
		private string account_AdminUnit;
		private DateTime account_Modified;
		private string account_ModifiedBy;

		#endregion
	}
}