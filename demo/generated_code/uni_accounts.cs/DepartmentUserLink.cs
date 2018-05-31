using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class DepartmentUserLink : Core.DbRecord
	{
		#region Create 
		public static int Create(int DepartmentId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId), 
				new SqlParameter("@UserId", UserId)};

			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_Create", parameters);
		}
		#endregion

		#region CreateAllByDepartment 
		public static int CreateAllByDepartment(int DepartmentId)
		{
			SqlParameter[] parameters = {new SqlParameter("@DepartmentId", DepartmentId)};
			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_CreateAllByDepartment", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public static int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_CreateAllByUser", parameters);
		}
		#endregion

		#region UpdateByDepartment
		public static void UpdateByDepartment(int DepartmentId, ArrayList toDelete, ArrayList toAdd)
		{
			foreach(int id in toDelete)
				Delete(DepartmentId, id);
			foreach(int id in toAdd)
				Create(DepartmentId, id);
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
		public static int Delete(int DepartmentId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@DepartmentId", DepartmentId), 
				new SqlParameter("@UserId", UserId)};

			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByDepartment 
		public static int DeleteAllByDepartment(int DepartmentId)
		{
			SqlParameter[] parameters = {new SqlParameter("@DepartmentId", DepartmentId)};
			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_DeleteAllByDepartment", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public static int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			DepartmentUserLink x = new DepartmentUserLink();
			return (int)x.ExecNonQuery("a_DepartmentUserLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region private

		private DepartmentUserLink() : base("a_DepartmentUserLink") {}

		#endregion
	}
}