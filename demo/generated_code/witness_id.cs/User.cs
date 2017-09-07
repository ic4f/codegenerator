using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class User : Core.DataClass
	{
		#region constructor
		public User(int recordId) : base()
		{
			loadRecord(recordId);
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
			SqlCommand command = new SqlCommand("a_User_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (Login != null)
				command.Parameters.Add(new SqlParameter("@Login", Login));
			else
				command.Parameters.Add(new SqlParameter("@Login", System.DBNull.Value));

			if (Password != null)
				command.Parameters.Add(new SqlParameter("@Password", (new Core.EncryptionTool()).Encrypt(Password)));
			else
				command.Parameters.Add(new SqlParameter("@Password", System.DBNull.Value));

			if (FirstName != null)
				command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
			else
				command.Parameters.Add(new SqlParameter("@FirstName", System.DBNull.Value));

			if (LastName != null)
				command.Parameters.Add(new SqlParameter("@LastName", LastName));
			else
				command.Parameters.Add(new SqlParameter("@LastName", System.DBNull.Value));

			if (ModifiedBy != null)
				command.Parameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
			else
				command.Parameters.Add(new SqlParameter("@ModifiedBy", System.DBNull.Value));

			Connection.Open();
			int result = command.ExecuteNonQuery();
			Connection.Close();

			if (result >= 0)
				loadRecord(id); //some values might have been changed by the db code
			return result;
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_User_GetRecord", Connection);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add(new SqlParameter("@Id", recordId));

			Connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			if (!reader.HasRows)
				throw new Core.AppException("Record with id = " + recordId + " not found.");
			reader.Read();

			if (reader[0] != System.DBNull.Value)
				id = reader.GetInt32(0);

			if (reader[1] != System.DBNull.Value)
				login = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				password = (byte[])reader.GetValue(2);

			if (reader[3] != System.DBNull.Value)
				firstName = reader.GetString(3);

			if (reader[4] != System.DBNull.Value)
				lastName = reader.GetString(4);

			if (reader[5] != System.DBNull.Value)
				created = reader.GetDateTime(5);

			if (reader[6] != System.DBNull.Value)
				modified = reader.GetDateTime(6);

			if (reader[7] != System.DBNull.Value)
				modifiedBy = reader.GetString(7);

			if (reader[8] != System.DBNull.Value)
				fullName = reader.GetString(8);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string login;
		private byte[] password;
		private string firstName;
		private string lastName;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string fullName;

		#endregion
	}
}