using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class Case : Core.DataClass
	{
		#region constructor
		public Case(int recordId) : base()
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

		#region string Number
		public string Number
		{
			get { return number; }
			set { number = value; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
			set { description = value; }
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

		#region int Update()
		public int Update()
		{
			SqlCommand command = new SqlCommand("a_Case_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (Number != null)
				command.Parameters.Add(new SqlParameter("@Number", Number));
			else
				command.Parameters.Add(new SqlParameter("@Number", System.DBNull.Value));

			if (Description != null)
				command.Parameters.Add(new SqlParameter("@Description", Description));
			else
				command.Parameters.Add(new SqlParameter("@Description", System.DBNull.Value));

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
			SqlCommand command = new SqlCommand("a_Case_GetRecord", Connection);
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
				number = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				description = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				created = reader.GetDateTime(3);

			if (reader[4] != System.DBNull.Value)
				modified = reader.GetDateTime(4);

			if (reader[5] != System.DBNull.Value)
				modifiedBy = reader.GetString(5);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string number;
		private string description;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;

		#endregion
	}
}