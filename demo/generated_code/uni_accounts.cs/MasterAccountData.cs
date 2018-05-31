using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class MasterAccountData : Core.DataClass
	{
		#region constructor 
		public MasterAccountData() : base() {}
		#endregion

		#region Create
		public int Create(
			int AccountId)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_Create", Connection);
			command.CommandType = CommandType.StoredProcedure;

			if (AccountId > 0)
				command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			else
				command.Parameters.Add(new SqlParameter("@AccountId", System.DBNull.Value));

			Connection.Open();
			int result = Convert.ToInt32(command.ExecuteScalar());
			Connection.Close();
			return result;
		}
		#endregion

		#region Delete
		public int Delete(int Id, bool DeleteDependents)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_Delete", Connection);
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
		public MasterAccountList GetList()
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public MasterAccountTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public MasterAccountTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public MasterAccountTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountField
		public MasterAccountTable GetRecordsByAccountField(int AccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountField", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountFieldP
		public MasterAccountTable GetRecordsByAccountFieldP(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountFieldP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountFieldPS
		public MasterAccountTable GetRecordsByAccountFieldPS(
			int AccountId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountFieldPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLink
		public MasterAccountTable GetRecordsByAccountLink(int LinkedAccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkP
		public MasterAccountTable GetRecordsByAccountLinkP(
			int LinkedAccountId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccountLinkPS
		public MasterAccountTable GetRecordsByAccountLinkPS(
			int LinkedAccountId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetRecordsByAccountLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetAccountLinks
		public MasterAccountList GetAccountLinks(int LinkedAccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetAccountLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksP
		public MasterAccountList GetAccountLinksP(int LinkedAccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetAccountLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccountLinksPS
		public MasterAccountList GetAccountLinksPS(			int LinkedAccountId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_MasterAccount_GetAccountLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@LinkedAccountId", LinkedAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetIdByAccount
		public int GetIdByAccount(
			int AccountId)
		{
			SqlCommand command = new SqlCommand("MasterAccount_GetIdByAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccountId", AccountId));
			Connection.Open();
			object result = command.ExecuteScalar();
			Connection.Close();
			return Convert.ToInt32(result);
		}
		#endregion

		#region GetWithQueryP
		public MasterAccountTableGetWithQueryP GetWithQueryP(
			string Query,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("MasterAccount_GetWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadMasterAccountTableGetWithQueryP(command);
		}
		#endregion

		#region private

		private MasterAccountTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 9)
				includeSelect = true;

			while (reader.Read())
			{
				MasterAccountRow r = new MasterAccountRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Account_AccountType = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.Account_FullAccountNumber = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Account_AccountDescription = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.Account_AcctStat = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.Account_AdminUnit = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.Account_Modified = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.Account_ModifiedBy = reader.GetString(8);

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
			return new MasterAccountTable(rows, totalCount);
		}

		private MasterAccountList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 1)
				includeSelect = true;

			while (reader.Read())
			{
				MasterAccountListRow r = new MasterAccountListRow();
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
			return new MasterAccountList(rows, totalCount);
		}

		private MasterAccountTableGetWithQueryP loadMasterAccountTableGetWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				MasterAccountRowGetWithQueryP r = new MasterAccountRowGetWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountId = reader.GetInt32(1);

				if (reader[2] != System.DBNull.Value)
					r.Account_AccountType = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.Account_FullAccountNumber = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Account_AccountDescription = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.Account_AcctStat = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.Account_AdminUnit = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.Account_Modified = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.Account_ModifiedBy = reader.GetString(8);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new MasterAccountTableGetWithQueryP(rows, totalCount);
		}

		#endregion
	}
}