using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class RolePermissionLink : Core.DataClass
	{
		#region constructor

		public RolePermissionLink() : base() {}

		#endregion

		#region Create 
		public int Create(int RoleId, int PermissionId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId), 
				new SqlParameter("@PermissionId", PermissionId)};

			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_Create", parameters);
		}
		#endregion

		#region CreateAllByRole 
		public int CreateAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_CreateAllByRole", parameters);
		}
		#endregion

		#region CreateAllByPermission 
		public int CreateAllByPermission(int PermissionId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PermissionId", PermissionId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_CreateAllByPermission", parameters);
		}
		#endregion

		#region UpdateByRole
		public void UpdateByRole(int RoleId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(RoleId, Convert.ToInt32(toDelete[i]));
			for (int i=0; i<toAdd.Count; i++)
				Create(RoleId, Convert.ToInt32(toAdd[i]));
		}
		#endregion

		#region UpdateByPermission
		public void UpdateByPermission(int PermissionId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), PermissionId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), PermissionId);
		}
		#endregion

		#region Delete 
		public int Delete(int RoleId, int PermissionId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@RoleId", RoleId), 
				new SqlParameter("@PermissionId", PermissionId)};

			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByRole 
		public int DeleteAllByRole(int RoleId)
		{
			SqlParameter[] parameters = {new SqlParameter("@RoleId", RoleId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_DeleteAllByRole", parameters);
		}
		#endregion

		#region DeleteAllByPermission 
		public int DeleteAllByPermission(int PermissionId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PermissionId", PermissionId)};
			RolePermissionLink x = new RolePermissionLink();
			return (int)x.ExecNonQuery("a_RolePermissionLink_DeleteAllByPermission", parameters);
		}
		#endregion

	}
}