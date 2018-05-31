using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UsrGroupUserLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int UsrGroupId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId), 
				new SqlParameter("@UserId", UserId)};

			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_Create", parameters);
		}
		#endregion

		#region CreateAllByUsrGroup 
		public static int CreateAllByUsrGroup(int UsrGroupId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UsrGroupId", UsrGroupId)};
			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_CreateAllByUsrGroup", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public static int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_CreateAllByUser", parameters);
		}
		#endregion

		#region UpdateByUsrGroup
		public static void UpdateByUsrGroup(int UsrGroupId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(UsrGroupId, id);
			foreach(int id in toAdd)
				Create(UsrGroupId, id);
		}
		#endregion

		#region UpdateByUser
		public static void UpdateByUser(int UserId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(id, UserId);
			foreach(int id in toAdd)
				Create(id, UserId);
		}
		#endregion

		#region Delete 
		public static int Delete(int UsrGroupId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UsrGroupId", UsrGroupId), 
				new SqlParameter("@UserId", UserId)};

			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByUsrGroup 
		public static int DeleteAllByUsrGroup(int UsrGroupId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UsrGroupId", UsrGroupId)};
			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_DeleteAllByUsrGroup", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public static int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UsrGroupUserLink x = new UsrGroupUserLink();
			return (int)x.ExecNonQuery("a_UsrGroupUserLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region private

		private UsrGroupUserLink() : base("a_UsrGroupUserLink") {}

		#endregion
	}
}