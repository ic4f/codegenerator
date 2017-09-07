using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Wid.Data
{
	public class LineupPhotoLink : Core.DataClass
	{
		#region constructor

		public LineupPhotoLink() : base() {}

		#endregion

		#region Create 
		public int Create(int LineupId, int PhotoId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@LineupId", LineupId), 
				new SqlParameter("@PhotoId", PhotoId)};

			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_Create", parameters);
		}
		#endregion

		#region CreateAllByLineup 
		public int CreateAllByLineup(int LineupId)
		{
			SqlParameter[] parameters = {new SqlParameter("@LineupId", LineupId)};
			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_CreateAllByLineup", parameters);
		}
		#endregion

		#region CreateAllByPhoto 
		public int CreateAllByPhoto(int PhotoId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PhotoId", PhotoId)};
			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_CreateAllByPhoto", parameters);
		}
		#endregion

		#region UpdateByLineup
		public void UpdateByLineup(int LineupId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(LineupId, Convert.ToInt32(toDelete[i]));
			for (int i=0; i<toAdd.Count; i++)
				Create(LineupId, Convert.ToInt32(toAdd[i]));
		}
		#endregion

		#region UpdateByPhoto
		public void UpdateByPhoto(int PhotoId, ArrayList toDelete, ArrayList toAdd)
		{
			for (int i=0; i<toDelete.Count; i++)
				Delete(Convert.ToInt32(toDelete[i]), PhotoId);
			for (int i=0; i<toAdd.Count; i++)
				Create(Convert.ToInt32(toAdd[i]), PhotoId);
		}
		#endregion

		#region Delete 
		public int Delete(int LineupId, int PhotoId)
		{
			SqlParameter[] parameters = { 
				new SqlParameter("@LineupId", LineupId), 
				new SqlParameter("@PhotoId", PhotoId)};

			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_Delete", parameters);
		}
		#endregion

		#region DeleteAllByLineup 
		public int DeleteAllByLineup(int LineupId)
		{
			SqlParameter[] parameters = {new SqlParameter("@LineupId", LineupId)};
			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_DeleteAllByLineup", parameters);
		}
		#endregion

		#region DeleteAllByPhoto 
		public int DeleteAllByPhoto(int PhotoId)
		{
			SqlParameter[] parameters = {new SqlParameter("@PhotoId", PhotoId)};
			LineupPhotoLink x = new LineupPhotoLink();
			return (int)x.ExecNonQuery("a_LineupPhotoLink_DeleteAllByPhoto", parameters);
		}
		#endregion

	}
}