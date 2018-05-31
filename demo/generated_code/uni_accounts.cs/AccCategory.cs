using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class AccCategory
	{
		#region constructor
		public AccCategory(int recordId)
		{
			tool = new DbTool();
			Load(tool.LoadRecord(recordId));
		}
		#endregion

		#region int Id
		public int Id
		{
			get { return id; }
		}
		#endregion

		#region int UserId
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion

		#region short Rank
		public short Rank
		{
			get { return rank; }
			set { rank = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
		}
		#endregion

		#region DateTime Modified
		public DateTime Modified
		{
			get { return modified; }
		}
		#endregion

		#region string ModifiedBy
		public string ModifiedBy
		{
			get { return modifiedBy; }
			set { modifiedBy = value; }
		}
		#endregion

		#region string User_Login
		public string User_Login
		{
			get { return user_Login; }
		}
		#endregion

		#region string User_Password
		public string User_Password
		{
			get { return (new Core.EncryptionTool()).Decrypt((byte[])user_Password); }
		}
		#endregion

		#region string User_FirstName
		public string User_FirstName
		{
			get { return user_FirstName; }
		}
		#endregion

		#region string User_LastName
		public string User_LastName
		{
			get { return user_LastName; }
		}
		#endregion

		#region string User_CampusCode
		public string User_CampusCode
		{
			get { return user_CampusCode; }
		}
		#endregion

		#region string User_UniId
		public string User_UniId
		{
			get { return user_UniId; }
		}
		#endregion

		#region bool User_ConAgree
		public bool User_ConAgree
		{
			get { return user_ConAgree; }
		}
		#endregion

		#region bool User_OnlineAccess
		public bool User_OnlineAccess
		{
			get { return user_OnlineAccess; }
		}
		#endregion

		#region DateTime User_Created
		public DateTime User_Created
		{
			get { return user_Created; }
		}
		#endregion

		#region DateTime User_Modified
		public DateTime User_Modified
		{
			get { return user_Modified; }
		}
		#endregion

		#region string User_ModifiedBy
		public string User_ModifiedBy
		{
			get { return user_ModifiedBy; }
		}
		#endregion

		#region string User_FullName
		public string User_FullName
		{
			get { return user_FullName; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			ArrayList p = new ArrayList();
			p.Add(new Param("Id", id));
			p.Add(new Param("UserId", userId));
			p.Add(new Param("Description", description));
			p.Add(new Param("Rank", rank));
			p.Add(new Param("ModifiedBy", modifiedBy));

			int result = tool.UpdateRecord(id, p);
			if (result >= 0)
				Load(tool.LoadRecord(id)); //must reload: some values might have been changed

			return result;
		}
		#endregion

		#region private

		private void Load(ArrayList p)
		{
			id = (int)p[0];
			userId = (int)p[1];
			description = (string)p[2];
			rank = (short)p[3];
			created = (DateTime)p[4];
			modified = (DateTime)p[5];
			modifiedBy = (string)p[6];
			user_Login = (string)p[7];
			user_Password = (byte[])p[8];
			user_FirstName = (string)p[9];
			user_LastName = (string)p[10];
			user_CampusCode = (string)p[11];
			user_UniId = (string)p[12];
			user_ConAgree = (bool)p[13];
			user_OnlineAccess = (bool)p[14];
			user_Created = (DateTime)p[15];
			user_Modified = (DateTime)p[16];
			user_ModifiedBy = (string)p[17];
			user_FullName = Convert.ToString(p[18]);
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_AccCategory") {}
		}

		private DbTool tool;
		private int id;
		private int userId;
		private string description;
		private short rank;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string user_Login;
		private byte[] user_Password;
		private string user_FirstName;
		private string user_LastName;
		private string user_CampusCode;
		private string user_UniId;
		private bool user_ConAgree;
		private bool user_OnlineAccess;
		private DateTime user_Created;
		private DateTime user_Modified;
		private string user_ModifiedBy;
		private string user_FullName;

		#endregion
	}
}