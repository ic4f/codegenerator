using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UserAccountLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int UserId, string AccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@AccountId", AccountId)};

			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_Create", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public static int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_CreateAllByUser", parameters);
		}
		#endregion

		#region CreateAllByAccount 
		public static int CreateAllByAccount(int AccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccountId", AccountId)};
			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_CreateAllByAccount", parameters);
		}
		#endregion

		#region UpdateByUser
		public static void UpdateByUser(int UserId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(string id in toDelete)
				Delete(UserId, id);
			foreach(string id in toAdd)
				Create(UserId, id);
		}
		#endregion

		#region UpdateByAccount
		public static void UpdateByAccount(string AccountId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(id, AccountId);
			foreach(int id in toAdd)
				Create(id, AccountId);
		}
		#endregion

		#region Delete 
		public static int Delete(int UserId, string AccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@AccountId", AccountId)};

			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public static int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region DeleteAllByAccount 
		public static int DeleteAllByAccount(int AccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccountId", AccountId)};
			UserAccountLink x = new UserAccountLink();
			return (int)x.ExecNonQuery("a_UserAccountLink_DeleteAllByAccount", parameters);
		}
		#endregion

		#region private

		private UserAccountLink() : base("a_UserAccountLink") {}

		#endregion
	}
}