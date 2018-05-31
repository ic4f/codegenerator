using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class AccCategoryAccountLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int AccCategoryId, string AccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId), 
				new SqlParameter("@AccountId", AccountId)};

			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_Create", parameters);
		}
		#endregion

		#region CreateAllByAccCategory 
		public static int CreateAllByAccCategory(int AccCategoryId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccCategoryId", AccCategoryId)};
			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_CreateAllByAccCategory", parameters);
		}
		#endregion

		#region CreateAllByAccount 
		public static int CreateAllByAccount(int AccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccountId", AccountId)};
			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_CreateAllByAccount", parameters);
		}
		#endregion

		#region UpdateByAccCategory
		public static void UpdateByAccCategory(int AccCategoryId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(string id in toDelete)
				Delete(AccCategoryId, id);
			foreach(string id in toAdd)
				Create(AccCategoryId, id);
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
		public static int Delete(int AccCategoryId, string AccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@AccCategoryId", AccCategoryId), 
				new SqlParameter("@AccountId", AccountId)};

			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByAccCategory 
		public static int DeleteAllByAccCategory(int AccCategoryId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccCategoryId", AccCategoryId)};
			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_DeleteAllByAccCategory", parameters);
		}
		#endregion

		#region DeleteAllByAccount 
		public static int DeleteAllByAccount(int AccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@AccountId", AccountId)};
			AccCategoryAccountLink x = new AccCategoryAccountLink();
			return (int)x.ExecNonQuery("a_AccCategoryAccountLink_DeleteAllByAccount", parameters);
		}
		#endregion

		#region private

		private AccCategoryAccountLink() : base("a_AccCategoryAccountLink") {}

		#endregion
	}
}