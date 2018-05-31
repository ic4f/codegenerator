using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class User
	{
		#region constructor
		public User(int recordId)
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

		#region string Login
		public string Login
		{
			get { return login; }
			set { login = value; }
		}
		#endregion

		#region string Password
		public string Password
		{
			get { return (new Core.EncryptionTool()).Decrypt((byte[])password); }
			set { password = (new Core.EncryptionTool()).Encrypt(value); }
		}
		#endregion

		#region string FirstName
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}
		#endregion

		#region string LastName
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}
		#endregion

		#region string CampusCode
		public string CampusCode
		{
			get { return campusCode; }
			set { campusCode = value; }
		}
		#endregion

		#region string UniId
		public string UniId
		{
			get { return uniId; }
			set { uniId = value; }
		}
		#endregion

		#region bool ConAgree
		public bool ConAgree
		{
			get { return conAgree; }
			set { conAgree = value; }
		}
		#endregion

		#region bool OnlineAccess
		public bool OnlineAccess
		{
			get { return onlineAccess; }
			set { onlineAccess = value; }
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

		#region string FullName
		public string FullName
		{
			get { return fullName; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			ArrayList p = new ArrayList();
			p.Add(new Param("Id", id));
			p.Add(new Param("Login", login));
			p.Add(new Param("Password", password));
			p.Add(new Param("FirstName", firstName));
			p.Add(new Param("LastName", lastName));
			p.Add(new Param("CampusCode", campusCode));
			p.Add(new Param("UniId", uniId));
			p.Add(new Param("ConAgree", conAgree));
			p.Add(new Param("OnlineAccess", onlineAccess));
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
			login = (string)p[1];
			password = (byte[])p[2];
			firstName = (string)p[3];
			lastName = (string)p[4];
			campusCode = (string)p[5];
			uniId = (string)p[6];
			conAgree = (bool)p[7];
			onlineAccess = (bool)p[8];
			created = (DateTime)p[9];
			modified = (DateTime)p[10];
			modifiedBy = (string)p[11];
			fullName = Convert.ToString(p[12]);
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_User") {}
		}

		private DbTool tool;
		private int id;
		private string login;
		private byte[] password;
		private string firstName;
		private string lastName;
		private string campusCode;
		private string uniId;
		private bool conAgree;
		private bool onlineAccess;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string fullName;

		#endregion
	}
}