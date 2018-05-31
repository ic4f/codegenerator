using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class MasterAccountAccountLink : Core.DataClass
	{
		#region constructor

		public MasterAccountAccountLink() : base() {}

		#endregion

		#region Create 
		public int Create(int MasterAccountId, int LinkedAccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@MasterAccountId", MasterAccountId), 
				new SqlParameter("@LinkedAccountId", LinkedAccountId)};

			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_Create", parameters);
		}
		#endregion

		#region CreateAllByMasterAccount 
		public int CreateAllByMasterAccount(int MasterAccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@MasterAccountId", MasterAccountId)};
			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_CreateAllByMasterAccount", parameters);
		}
		#endregion

		#region CreateAllByAccount 
		public int CreateAllByAccount(int LinkedAccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@LinkedAccountId", LinkedAccountId)};
			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_CreateAllByAccount", parameters);
		}
		#endregion

		#region UpdateByMasterAccount
		public void UpdateByMasterAccount(int MasterAccountId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(MasterAccountId, Convert.ToInt32(toDelete[i]));
			for (int i=0; i<toAdd.Count; i++)
				Create(MasterAccountId, Convert.ToInt32(toAdd[i]));
		}
		#endregion

		#region UpdateByAccount
		public void UpdateByAccount(int LinkedAccountId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), LinkedAccountId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), LinkedAccountId);
		}
		#endregion

		#region Delete 
		public int Delete(int MasterAccountId, int LinkedAccountId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@MasterAccountId", MasterAccountId), 
				new SqlParameter("@LinkedAccountId", LinkedAccountId)};

			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByMasterAccount 
		public int DeleteAllByMasterAccount(int MasterAccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@MasterAccountId", MasterAccountId)};
			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_DeleteAllByMasterAccount", parameters);
		}
		#endregion

		#region DeleteAllByAccount 
		public int DeleteAllByAccount(int LinkedAccountId)
		{
			SqlParameter[] parameters = {new SqlParameter("@LinkedAccountId", LinkedAccountId)};
			MasterAccountAccountLink x = new MasterAccountAccountLink();
			return (int)x.ExecNonQuery("a_MasterAccountAccountLink_DeleteAllByAccount", parameters);
		}
		#endregion

	}
}