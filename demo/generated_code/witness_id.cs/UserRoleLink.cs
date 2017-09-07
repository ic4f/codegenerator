using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class UserRoleLink : Core.DataClass
	{
		#region constructor

		public UserRoleLink() : base() {}

		#endregion

		#region Create 
		public int Create(int UserId, int RoleId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@RoleId", RoleId)};

			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_Create", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_CreateAllByUser", parameters);
		}
		#endregion

		#region CreateAllByRole 
		public int CreateAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_CreateAllByRole", parameters);
		}
		#endregion

		#region UpdateByUser
		public void UpdateByUser(int UserId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(UserId, Convert.ToInt32(toDelete[i]));
			for (int i=0; i<toAdd.Count; i++)
				Create(UserId, Convert.ToInt32(toAdd[i]));
		}
		#endregion

		#region UpdateByRole
		public void UpdateByRole(int RoleId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), RoleId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), RoleId);
		}
		#endregion

		#region Delete 
		public int Delete(int UserId, int RoleId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@RoleId", RoleId)};

			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region DeleteAllByRole 
		public int DeleteAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			UserRoleLink x = new UserRoleLink();
			return (int)x.ExecNonQuery("a_UserRoleLink_DeleteAllByRole", parameters);
		}
		#endregion

	}
}