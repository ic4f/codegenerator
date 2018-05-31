using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class RolePermissionLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int RoleId, int PermissionId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId), 
				new SqlParameter("@PermissionId", PermissionId)};

			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_Create", parameters);
		}
		#endregion

		#region CreateAllByRole 
		public static int CreateAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_CreateAllByRole", parameters);
		}
		#endregion

		#region CreateAllByPermission 
		public static int CreateAllByPermission(int PermissionId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PermissionId", PermissionId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_CreateAllByPermission", parameters);
		}
		#endregion

		#region UpdateByRole
		public static void UpdateByRole(int RoleId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(RoleId, id);
			foreach(int id in toAdd)
				Create(RoleId, id);
		}
		#endregion

		#region UpdateByPermission
		public static void UpdateByPermission(int PermissionId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(id, PermissionId);
			foreach(int id in toAdd)
				Create(id, PermissionId);
		}
		#endregion

		#region Delete 
		public static int Delete(int RoleId, int PermissionId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId), 
				new SqlParameter("@PermissionId", PermissionId)};

			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByRole 
		public static int DeleteAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_DeleteAllByRole", parameters);
		}
		#endregion

		#region DeleteAllByPermission 
		public static int DeleteAllByPermission(int PermissionId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PermissionId", PermissionId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_DeleteAllByPermission", parameters);
		}
		#endregion

		#region private

		private RolePermissionLink() : base("a_RolePermissionLink") {}

		#endregion
	}
}