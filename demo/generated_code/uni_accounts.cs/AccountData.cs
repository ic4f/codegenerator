using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccountData : Core.DataClass
	{
		#region constructor 
		public AccountData() : base() {}
		#endregion

		#region GetList
		public AccountList GetList()
		{
			SqlCommand command = new SqlCommand("a_Account_GetList", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return LoadList(command);
		}
		#endregion

		#region GetRecords
		public AccountTable GetRecords(string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecords", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsP
		public AccountTable GetRecordsP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsPS
		public AccountTable GetRecordsPS(
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByMasterAccountLink
		public AccountTable GetRecordsByMasterAccountLink(int MasterAccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByMasterAccountLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByMasterAccountLinkP
		public AccountTable GetRecordsByMasterAccountLinkP(
			int MasterAccountId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByMasterAccountLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByMasterAccountLinkPS
		public AccountTable GetRecordsByMasterAccountLinkPS(
			int MasterAccountId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByMasterAccountLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLink
		public AccountTable GetRecordsByUserLink(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByUserLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkP
		public AccountTable GetRecordsByUserLinkP(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByUserLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByUserLinkPS
		public AccountTable GetRecordsByUserLinkPS(
			int UserId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByUserLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccCategoryLink
		public AccountTable GetRecordsByAccCategoryLink(int AccCategoryId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByAccCategoryLink", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccCategoryLinkP
		public AccountTable GetRecordsByAccCategoryLinkP(
			int AccCategoryId
,			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByAccCategoryLinkP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadTable(command);
		}
		#endregion

		#region GetRecordsByAccCategoryLinkPS
		public AccountTable GetRecordsByAccCategoryLinkPS(
			int AccCategoryId
,			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetRecordsByAccCategoryLinkPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadTable(command);
		}
		#endregion

		#region GetMasterAccountLinks
		public AccountList GetMasterAccountLinks(int MasterAccountId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksP
		public AccountList GetMasterAccountLinksP(int MasterAccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksPS
		public AccountList GetMasterAccountLinksPS(			int MasterAccountId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksByUser
		public AccountList GetMasterAccountLinksByUser(int MasterAccountId, int UserId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksByUser", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksByUserP
		public AccountList GetMasterAccountLinksByUserP(int MasterAccountId, int UserId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksByUserP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksByAccCategory
		public AccountList GetMasterAccountLinksByAccCategory(int MasterAccountId, int AccCategoryId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksByAccCategory", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetMasterAccountLinksByAccCategoryP
		public AccountList GetMasterAccountLinksByAccCategoryP(int MasterAccountId, int AccCategoryId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetMasterAccountLinksByAccCategoryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinks
		public AccountList GetUserLinks(int UserId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksP
		public AccountList GetUserLinksP(int UserId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksPS
		public AccountList GetUserLinksPS(			int UserId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksByMasterAccount
		public AccountList GetUserLinksByMasterAccount(int UserId, int MasterAccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksByMasterAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksByMasterAccountP
		public AccountList GetUserLinksByMasterAccountP(int UserId, int MasterAccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksByMasterAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksByAccCategory
		public AccountList GetUserLinksByAccCategory(int UserId, int AccCategoryId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksByAccCategory", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetUserLinksByAccCategoryP
		public AccountList GetUserLinksByAccCategoryP(int UserId, int AccCategoryId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetUserLinksByAccCategoryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinks
		public AccountList GetAccCategoryLinks(int AccCategoryId, string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinks", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksP
		public AccountList GetAccCategoryLinksP(int AccCategoryId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksPS
		public AccountList GetAccCategoryLinksPS(			int AccCategoryId,
			string SortExp,
			int PageSize,
			int PageNum,
			string SearchField,
			string SearchKeyword)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksPS", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			command.Parameters.Add(new SqlParameter("@SearchField", SearchField));
			command.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksByMasterAccount
		public AccountList GetAccCategoryLinksByMasterAccount(int AccCategoryId, int MasterAccountId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksByMasterAccount", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksByMasterAccountP
		public AccountList GetAccCategoryLinksByMasterAccountP(int AccCategoryId, int MasterAccountId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksByMasterAccountP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksByUser
		public AccountList GetAccCategoryLinksByUser(int AccCategoryId, int UserId, 
 string SortExp)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksByUser", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksByUserP
		public AccountList GetAccCategoryLinksByUserP(int AccCategoryId, int UserId, string SortExp, int PageSize, int PageNum)
		{
			SqlCommand command = new SqlCommand("a_Account_GetAccCategoryLinksByUserP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return LoadList(command);
		}
		#endregion

		#region GetAccCategoryLinksWithQueryP
		public AccountTableGetAccCategoryLinksWithQueryP GetAccCategoryLinksWithQueryP(
			int UserId,
			string Query,
			int AccCategoryId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetAccCategoryLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetAccCategoryLinksWithQueryP(command);
		}
		#endregion

		#region GetAccCategoryLinksWithQuery
		public AccountTableGetAccCategoryLinksWithQuery GetAccCategoryLinksWithQuery(
			int UserId,
			string Query,
			int AccCategoryId)
		{
			SqlCommand command = new SqlCommand("Account_GetAccCategoryLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@AccCategoryId", AccCategoryId));
			return loadAccountTableGetAccCategoryLinksWithQuery(command);
		}
		#endregion

		#region GetUserLinksWithQueryP
		public AccountTableGetUserLinksWithQueryP GetUserLinksWithQueryP(
			string Query,
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksWithQueryP(command);
		}
		#endregion

		#region GetUserLinksWithQuery
		public AccountTableGetUserLinksWithQuery GetUserLinksWithQuery(
			string Query,
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksWithQuery(command);
		}
		#endregion

		#region GetMasterAccountLinksWithQueryP
		public AccountTableGetMasterAccountLinksWithQueryP GetMasterAccountLinksWithQueryP(
			string Query,
			int MasterAccountId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetMasterAccountLinksWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@MasterAccountId", MasterAccountId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetMasterAccountLinksWithQueryP(command);
		}
		#endregion

		#region GetWithQueryP
		public AccountTableGetWithQueryP GetWithQueryP(
			string Query,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetWithQueryP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetWithQueryP(command);
		}
		#endregion

		#region GetWithQuery
		public AccountTableGetWithQuery GetWithQuery(
			string Query)
		{
			SqlCommand command = new SqlCommand("Account_GetWithQuery", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Query", Query));
			return loadAccountTableGetWithQuery(command);
		}
		#endregion

		#region GetNonMasterAccounts
		public AccountTableGetNonMasterAccounts GetNonMasterAccounts()
		{
			SqlCommand command = new SqlCommand("Account_GetNonMasterAccounts", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetNonMasterAccounts(command);
		}
		#endregion

		#region GetAdminUnits
		public AccountTableGetAdminUnits GetAdminUnits()
		{
			SqlCommand command = new SqlCommand("Account_GetAdminUnits", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetAdminUnits(command);
		}
		#endregion

		#region GetByActiveStatus
		public AccountTableGetByActiveStatus GetByActiveStatus()
		{
			SqlCommand command = new SqlCommand("Account_GetByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetByActiveStatus(command);
		}
		#endregion

		#region GetByActiveStatusP
		public AccountTableGetByActiveStatusP GetByActiveStatusP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetByActiveStatusP(command);
		}
		#endregion

		#region GetAlumni
		public AccountTableGetAlumni GetAlumni()
		{
			SqlCommand command = new SqlCommand("Account_GetAlumni", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetAlumni(command);
		}
		#endregion

		#region GetAlumniP
		public AccountTableGetAlumniP GetAlumniP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetAlumniP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetAlumniP(command);
		}
		#endregion

		#region GetAlumniByActiveStatus
		public AccountTableGetAlumniByActiveStatus GetAlumniByActiveStatus()
		{
			SqlCommand command = new SqlCommand("Account_GetAlumniByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetAlumniByActiveStatus(command);
		}
		#endregion

		#region GetAlumniByActiveStatusP
		public AccountTableGetAlumniByActiveStatusP GetAlumniByActiveStatusP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetAlumniByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetAlumniByActiveStatusP(command);
		}
		#endregion

		#region GetFoundation
		public AccountTableGetFoundation GetFoundation()
		{
			SqlCommand command = new SqlCommand("Account_GetFoundation", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetFoundation(command);
		}
		#endregion

		#region GetFoundationP
		public AccountTableGetFoundationP GetFoundationP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetFoundationP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetFoundationP(command);
		}
		#endregion

		#region GetFoundationByActiveStatus
		public AccountTableGetFoundationByActiveStatus GetFoundationByActiveStatus()
		{
			SqlCommand command = new SqlCommand("Account_GetFoundationByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			return loadAccountTableGetFoundationByActiveStatus(command);
		}
		#endregion

		#region GetFoundationByActiveStatusP
		public AccountTableGetFoundationByActiveStatusP GetFoundationByActiveStatusP(
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetFoundationByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetFoundationByActiveStatusP(command);
		}
		#endregion

		#region GetByAdminUnit
		public AccountTableGetByAdminUnit GetByAdminUnit(
			string AdminUnit)
		{
			SqlCommand command = new SqlCommand("Account_GetByAdminUnit", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AdminUnit", AdminUnit));
			return loadAccountTableGetByAdminUnit(command);
		}
		#endregion

		#region GetByAdminUnitP
		public AccountTableGetByAdminUnitP GetByAdminUnitP(
			string AdminUnit,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetByAdminUnitP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@AdminUnit", AdminUnit));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetByAdminUnitP(command);
		}
		#endregion

		#region GetUserLinksByActiveStatus
		public AccountTableGetUserLinksByActiveStatus GetUserLinksByActiveStatus(
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksByActiveStatus(command);
		}
		#endregion

		#region GetUserLinksByActiveStatusP
		public AccountTableGetUserLinksByActiveStatusP GetUserLinksByActiveStatusP(
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksByActiveStatusP(command);
		}
		#endregion

		#region GetUserLinksAlumni
		public AccountTableGetUserLinksAlumni GetUserLinksAlumni(
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksAlumni", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksAlumni(command);
		}
		#endregion

		#region GetUserLinksAlumniP
		public AccountTableGetUserLinksAlumniP GetUserLinksAlumniP(
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksAlumniP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksAlumniP(command);
		}
		#endregion

		#region GetUserLinksAlumniByActiveStatus
		public AccountTableGetUserLinksAlumniByActiveStatus GetUserLinksAlumniByActiveStatus(
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksAlumniByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksAlumniByActiveStatus(command);
		}
		#endregion

		#region GetUserLinksAlumniByActiveStatusP
		public AccountTableGetUserLinksAlumniByActiveStatusP GetUserLinksAlumniByActiveStatusP(
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksAlumniByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksAlumniByActiveStatusP(command);
		}
		#endregion

		#region GetUserLinksFoundation
		public AccountTableGetUserLinksFoundation GetUserLinksFoundation(
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksFoundation", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksFoundation(command);
		}
		#endregion

		#region GetUserLinksFoundationP
		public AccountTableGetUserLinksFoundationP GetUserLinksFoundationP(
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksFoundationP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksFoundationP(command);
		}
		#endregion

		#region GetUserLinksFoundationByActiveStatus
		public AccountTableGetUserLinksFoundationByActiveStatus GetUserLinksFoundationByActiveStatus(
			int UserId)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksFoundationByActiveStatus", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			return loadAccountTableGetUserLinksFoundationByActiveStatus(command);
		}
		#endregion

		#region GetUserLinksFoundationByActiveStatusP
		public AccountTableGetUserLinksFoundationByActiveStatusP GetUserLinksFoundationByActiveStatusP(
			int UserId,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksFoundationByActiveStatusP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksFoundationByActiveStatusP(command);
		}
		#endregion

		#region GetUserLinksByAdminUnit
		public AccountTableGetUserLinksByAdminUnit GetUserLinksByAdminUnit(
			int UserId,
			string AdminUnit)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksByAdminUnit", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@AdminUnit", AdminUnit));
			return loadAccountTableGetUserLinksByAdminUnit(command);
		}
		#endregion

		#region GetUserLinksByAdminUnitP
		public AccountTableGetUserLinksByAdminUnitP GetUserLinksByAdminUnitP(
			int UserId,
			string AdminUnit,
			string SortExp,
			int PageSize,
			int PageNum)
		{
			SqlCommand command = new SqlCommand("Account_GetUserLinksByAdminUnitP", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@UserId", UserId));
			command.Parameters.Add(new SqlParameter("@AdminUnit", AdminUnit));
			command.Parameters.Add(new SqlParameter("@SortExp", SortExp));
			command.Parameters.Add(new SqlParameter("@PageSize", PageSize));
			command.Parameters.Add(new SqlParameter("@PageNum", PageNum));
			return loadAccountTableGetUserLinksByAdminUnitP(command);
		}
		#endregion

		#region private

		private AccountTable LoadTable(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 9)
				includeSelect = true;

			while (reader.Read())
			{
				AccountRow r = new AccountRow();
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountType = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.AcctStat = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.AdminUnit = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(8);

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
			return new AccountTable(rows, totalCount);
		}

		private AccountList LoadList(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			bool includeSelect = false;
			if (reader.FieldCount > 4)
				includeSelect = true;

			while (reader.Read())
			{
				AccountListRow r = new AccountListRow();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				r.Selected = false;
				if (includeSelect && reader.GetBoolean(4))
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
			return new AccountList(rows, totalCount);
		}

		private AccountTableGetAccCategoryLinksWithQueryP loadAccountTableGetAccCategoryLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAccCategoryLinksWithQueryP r = new AccountRowGetAccCategoryLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAccCategoryLinksWithQueryP(rows, totalCount);
		}

		private AccountTableGetAccCategoryLinksWithQuery loadAccountTableGetAccCategoryLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAccCategoryLinksWithQuery r = new AccountRowGetAccCategoryLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAccCategoryLinksWithQuery(rows, totalCount);
		}

		private AccountTableGetUserLinksWithQueryP loadAccountTableGetUserLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksWithQueryP r = new AccountRowGetUserLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksWithQueryP(rows, totalCount);
		}

		private AccountTableGetUserLinksWithQuery loadAccountTableGetUserLinksWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksWithQuery r = new AccountRowGetUserLinksWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksWithQuery(rows, totalCount);
		}

		private AccountTableGetMasterAccountLinksWithQueryP loadAccountTableGetMasterAccountLinksWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetMasterAccountLinksWithQueryP r = new AccountRowGetMasterAccountLinksWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetMasterAccountLinksWithQueryP(rows, totalCount);
		}

		private AccountTableGetWithQueryP loadAccountTableGetWithQueryP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetWithQueryP r = new AccountRowGetWithQueryP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountType = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.AcctStat = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.AdminUnit = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(8);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetWithQueryP(rows, totalCount);
		}

		private AccountTableGetWithQuery loadAccountTableGetWithQuery(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetWithQuery r = new AccountRowGetWithQuery();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountType = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(4);

				if (reader[5] != System.DBNull.Value)
					r.AcctStat = reader.GetString(5);

				if (reader[6] != System.DBNull.Value)
					r.AdminUnit = reader.GetString(6);

				if (reader[7] != System.DBNull.Value)
					r.Modified = reader.GetDateTime(7);

				if (reader[8] != System.DBNull.Value)
					r.ModifiedBy = reader.GetString(8);

				if (reader[9] != System.DBNull.Value)
					r.NumberAndDescription = reader.GetString(9);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetWithQuery(rows, totalCount);
		}

		private AccountTableGetNonMasterAccounts loadAccountTableGetNonMasterAccounts(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetNonMasterAccounts r = new AccountRowGetNonMasterAccounts();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(1);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetNonMasterAccounts(rows, totalCount);
		}

		private AccountTableGetAdminUnits loadAccountTableGetAdminUnits(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAdminUnits r = new AccountRowGetAdminUnits();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.AdminUnit = reader.GetString(0);

				if (reader[1] != System.DBNull.Value)
					r.Description = reader.GetString(1);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAdminUnits(rows, totalCount);
		}

		private AccountTableGetByActiveStatus loadAccountTableGetByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetByActiveStatus r = new AccountRowGetByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetByActiveStatus(rows, totalCount);
		}

		private AccountTableGetByActiveStatusP loadAccountTableGetByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetByActiveStatusP r = new AccountRowGetByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetAlumni loadAccountTableGetAlumni(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAlumni r = new AccountRowGetAlumni();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAlumni(rows, totalCount);
		}

		private AccountTableGetAlumniP loadAccountTableGetAlumniP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAlumniP r = new AccountRowGetAlumniP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAlumniP(rows, totalCount);
		}

		private AccountTableGetAlumniByActiveStatus loadAccountTableGetAlumniByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAlumniByActiveStatus r = new AccountRowGetAlumniByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAlumniByActiveStatus(rows, totalCount);
		}

		private AccountTableGetAlumniByActiveStatusP loadAccountTableGetAlumniByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetAlumniByActiveStatusP r = new AccountRowGetAlumniByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetAlumniByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetFoundation loadAccountTableGetFoundation(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetFoundation r = new AccountRowGetFoundation();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetFoundation(rows, totalCount);
		}

		private AccountTableGetFoundationP loadAccountTableGetFoundationP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetFoundationP r = new AccountRowGetFoundationP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetFoundationP(rows, totalCount);
		}

		private AccountTableGetFoundationByActiveStatus loadAccountTableGetFoundationByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetFoundationByActiveStatus r = new AccountRowGetFoundationByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetFoundationByActiveStatus(rows, totalCount);
		}

		private AccountTableGetFoundationByActiveStatusP loadAccountTableGetFoundationByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetFoundationByActiveStatusP r = new AccountRowGetFoundationByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetFoundationByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetByAdminUnit loadAccountTableGetByAdminUnit(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetByAdminUnit r = new AccountRowGetByAdminUnit();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetByAdminUnit(rows, totalCount);
		}

		private AccountTableGetByAdminUnitP loadAccountTableGetByAdminUnitP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetByAdminUnitP r = new AccountRowGetByAdminUnitP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetByAdminUnitP(rows, totalCount);
		}

		private AccountTableGetUserLinksByActiveStatus loadAccountTableGetUserLinksByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksByActiveStatus r = new AccountRowGetUserLinksByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksByActiveStatus(rows, totalCount);
		}

		private AccountTableGetUserLinksByActiveStatusP loadAccountTableGetUserLinksByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksByActiveStatusP r = new AccountRowGetUserLinksByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetUserLinksAlumni loadAccountTableGetUserLinksAlumni(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksAlumni r = new AccountRowGetUserLinksAlumni();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksAlumni(rows, totalCount);
		}

		private AccountTableGetUserLinksAlumniP loadAccountTableGetUserLinksAlumniP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksAlumniP r = new AccountRowGetUserLinksAlumniP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksAlumniP(rows, totalCount);
		}

		private AccountTableGetUserLinksAlumniByActiveStatus loadAccountTableGetUserLinksAlumniByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksAlumniByActiveStatus r = new AccountRowGetUserLinksAlumniByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksAlumniByActiveStatus(rows, totalCount);
		}

		private AccountTableGetUserLinksAlumniByActiveStatusP loadAccountTableGetUserLinksAlumniByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksAlumniByActiveStatusP r = new AccountRowGetUserLinksAlumniByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksAlumniByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetUserLinksFoundation loadAccountTableGetUserLinksFoundation(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksFoundation r = new AccountRowGetUserLinksFoundation();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksFoundation(rows, totalCount);
		}

		private AccountTableGetUserLinksFoundationP loadAccountTableGetUserLinksFoundationP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksFoundationP r = new AccountRowGetUserLinksFoundationP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksFoundationP(rows, totalCount);
		}

		private AccountTableGetUserLinksFoundationByActiveStatus loadAccountTableGetUserLinksFoundationByActiveStatus(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksFoundationByActiveStatus r = new AccountRowGetUserLinksFoundationByActiveStatus();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksFoundationByActiveStatus(rows, totalCount);
		}

		private AccountTableGetUserLinksFoundationByActiveStatusP loadAccountTableGetUserLinksFoundationByActiveStatusP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksFoundationByActiveStatusP r = new AccountRowGetUserLinksFoundationByActiveStatusP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksFoundationByActiveStatusP(rows, totalCount);
		}

		private AccountTableGetUserLinksByAdminUnit loadAccountTableGetUserLinksByAdminUnit(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksByAdminUnit r = new AccountRowGetUserLinksByAdminUnit();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksByAdminUnit(rows, totalCount);
		}

		private AccountTableGetUserLinksByAdminUnitP loadAccountTableGetUserLinksByAdminUnitP(SqlCommand command)
		{
			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			ArrayList rows = new ArrayList();

			while (reader.Read())
			{
				AccountRowGetUserLinksByAdminUnitP r = new AccountRowGetUserLinksByAdminUnitP();
				r.Selected = false;
				rows.Add(r);

				if (reader[0] != System.DBNull.Value)
					r.Id = reader.GetInt32(0);

				if (reader[1] != System.DBNull.Value)
					r.AccountNumber = reader.GetString(1);

				if (reader[2] != System.DBNull.Value)
					r.FullAccountNumber = reader.GetString(2);

				if (reader[3] != System.DBNull.Value)
					r.AccountDescription = reader.GetString(3);

				if (reader[4] != System.DBNull.Value)
					r.Selected = reader.GetBoolean(4);

			}
			int totalCount = rows.Count;
			if (reader.NextResult())
			{
				reader.Read();
				totalCount = reader.GetInt32(0);
			}
			reader.Close();
			Connection.Close();
			return new AccountTableGetUserLinksByAdminUnitP(rows, totalCount);
		}

		#endregion
	}
}