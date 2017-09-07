using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class PermissionCategory : Core.DataClass
	{
		#region constructor
		public PermissionCategory(int recordId) : base()
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

		#region string Description
		public string Description
		{
			get { return description; }
		}
		#endregion

		#region short Rank
		public short Rank
		{
			get { return rank; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_PermissionCategory_GetRecord", Connection);
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
				description = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				rank = reader.GetInt16(2);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string description;
		private short rank;

		#endregion
	}
}