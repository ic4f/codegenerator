using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class Permission : Core.DataClass
	{
		#region constructor
		public Permission(int recordId) : base()
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

		#region int CategoryId
		public int CategoryId
		{
			get { return categoryId; }
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
			SqlCommand command = new SqlCommand("a_Permission_GetRecord", Connection);
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
				categoryId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				description = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				rank = reader.GetInt16(3);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int categoryId;
		private string description;
		private short rank;

		#endregion
	}
}