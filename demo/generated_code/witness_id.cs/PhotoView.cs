using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class PhotoView : Core.DataClass
	{
		#region constructor
		public PhotoView(int recordId) : base()
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

		#region int LineupViewId
		public int LineupViewId
		{
			get { return lineupViewId; }
			set { lineupViewId = value; }
		}
		#endregion

		#region int PhotoId
		public int PhotoId
		{
			get { return photoId; }
			set { photoId = value; }
		}
		#endregion

		#region string Result
		public string Result
		{
			get { return result; }
			set { result = value; }
		}
		#endregion

		#region string Certainty
		public string Certainty
		{
			get { return certainty; }
			set { certainty = value; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_PhotoView_GetRecord", Connection);
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
				lineupViewId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				photoId = reader.GetInt32(2);

			if (reader[3] != System.DBNull.Value)
				result = reader.GetString(3);

			if (reader[4] != System.DBNull.Value)
				certainty = reader.GetString(4);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int lineupViewId;
		private int photoId;
		private string result;
		private string certainty;

		#endregion
	}
}