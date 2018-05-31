using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class UserRoleLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int UserId, int RoleId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@RoleId", RoleId)};

			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_Create", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public static int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_CreateAllByUser", parameters);
		}
		#endregion

		#region CreateAllByRole 
		public static int CreateAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_CreateAllByRole", parameters);
		}
		#endregion

		#region UpdateByUser
		public static void UpdateByUser(int UserId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(UserId, id);
			foreach(int id in toAdd)
				Create(UserId, id);
		}
		#endregion

		#region UpdateByRole
		public static void UpdateByRole(int RoleId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(id, RoleId);
			foreach(int id in toAdd)
				Create(id, RoleId);
		}
		#endregion

		#region Delete 
		public static int Delete(int UserId, int RoleId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@RoleId", RoleId)};

			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public static int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region DeleteAllByRole 
		public static int DeleteAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_DeleteAllByRole", parameters);
		}
		#endregion

		#region private

		private UserRoleLink() : base("a_UserRoleLink") {}

		#endregion
	}
}