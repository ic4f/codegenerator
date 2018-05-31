using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Far.Data
{
	public class EmailUserLink : Core.DataClass
	{
		#region constructor

		public EmailUserLink() : base() {}

		#endregion

		#region Create 
		public int Create(int EmailId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@EmailId", EmailId), 
				new SqlParameter("@UserId", UserId)};

			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_Create", parameters);
		}
		#endregion

		#region CreateAllByEmail 
		public int CreateAllByEmail(int EmailId)
		{
			SqlParameter[] parameters = {new SqlParameter("@EmailId", EmailId)};
			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_CreateAllByEmail", parameters);
		}
		#endregion

		#region CreateAllByUser 
		public int CreateAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_CreateAllByUser", parameters);
		}
		#endregion

		#region UpdateByEmail
		public void UpdateByEmail(int EmailId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(EmailId, Convert.ToInt32(toDelete[i]));
			for (int i=0; i<toAdd.Count; i++)
				Create(EmailId, Convert.ToInt32(toAdd[i]));
		}
		#endregion

		#region UpdateByUser
		public void UpdateByUser(int UserId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), UserId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), UserId);
		}
		#endregion

		#region Delete 
		public int Delete(int EmailId, int UserId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@EmailId", EmailId), 
				new SqlParameter("@UserId", UserId)};

			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByEmail 
		public int DeleteAllByEmail(int EmailId)
		{
			SqlParameter[] parameters = {new SqlParameter("@EmailId", EmailId)};
			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_DeleteAllByEmail", parameters);
		}
		#endregion

		#region DeleteAllByUser 
		public int DeleteAllByUser(int UserId)
		{
			SqlParameter[] parameters = {new SqlParameter("@UserId", UserId)};
			EmailUserLink x = new EmailUserLink();
			return (int)x.ExecNonQuery("a_EmailUserLink_DeleteAllByUser", parameters);
		}
		#endregion

	}
}