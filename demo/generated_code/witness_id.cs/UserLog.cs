using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class UserLog : Core.DataClass
	{
		#region constructor
		public UserLog(int recordId) : base()
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

		#region int UserId
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
		}
		#endregion

		#region string User_FullName
		public string User_FullName
		{
			get { return user_FullName; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_UserLog_GetRecord", Connection);
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
				userId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				created = reader.GetDateTime(2);

			if (reader[3] != System.DBNull.Value)
				user_FullName = reader.GetString(3);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int userId;
		private DateTime created;
		private string user_FullName;

		#endregion
	}
}