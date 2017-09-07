using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class UserCaseLink : Core.DataClass
	{
		#region constructor

		public UserCaseLink() : base() {}

		#endregion

		#region Create 
		public int Create(int UserId, int CaseId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@CaseId", CaseId)};

			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_Create", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_CreateAllByUser", parameters);
		}
		#endregion

		#region CreateAllByCase 
		public int CreateAllByCase(int CaseId)
		{
			SqlParameter[] parameters = {new SqlParameter("@CaseId", CaseId)};
			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_CreateAllByCase", parameters);
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

		#region UpdateByCase
		public void UpdateByCase(int CaseId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), CaseId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), CaseId);
		}
		#endregion

		#region Delete 
		public int Delete(int UserId, int CaseId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@UserId", UserId), 
				new SqlParameter("@CaseId", CaseId)};

			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_DeleteAllByUser", parameters);
		}
		#endregion

		#region DeleteAllByCase 
		public int DeleteAllByCase(int CaseId)
		{
			SqlParameter[] parameters = {new SqlParameter("@CaseId", CaseId)};
			UserCaseLink x = new UserCaseLink();
			return (int)x.ExecNonQuery("a_UserCaseLink_DeleteAllByCase", parameters);
		}
		#endregion

	}
}