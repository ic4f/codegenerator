using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class LineupView : Core.DataClass
	{
		#region constructor
		public LineupView(int recordId) : base()
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

		#region int LineupId
		public int LineupId
		{
			get { return lineupId; }
			set { lineupId = value; }
		}
		#endregion

		#region string WitnessFirstName
		public string WitnessFirstName
		{
			get { return witnessFirstName; }
			set { witnessFirstName = value; }
		}
		#endregion

		#region string WitnessLastName
		public string WitnessLastName
		{
			get { return witnessLastName; }
			set { witnessLastName = value; }
		}
		#endregion

		#region string Relevance
		public string Relevance
		{
			get { return relevance; }
			set { relevance = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
		}
		#endregion

		#region string CreatedBy
		public string CreatedBy
		{
			get { return createdBy; }
			set { createdBy = value; }
		}
		#endregion

		#region bool IsCompleted
		public bool IsCompleted
		{
			get { return isCompleted; }
			set { isCompleted = value; }
		}
		#endregion

		#region private

		private void loadRecord(int recordId)
		{
			SqlCommand command = new SqlCommand("a_LineupView_GetRecord", Connection);
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
				lineupId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				witnessFirstName = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				witnessLastName = reader.GetString(3);

			if (reader[4] != System.DBNull.Value)
				relevance = reader.GetString(4);

			if (reader[5] != System.DBNull.Value)
				created = reader.GetDateTime(5);

			if (reader[6] != System.DBNull.Value)
				createdBy = reader.GetString(6);

			if (reader[7] != System.DBNull.Value)
				isCompleted = reader.GetBoolean(7);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int lineupId;
		private string witnessFirstName;
		private string witnessLastName;
		private string relevance;
		private DateTime created;
		private string createdBy;
		private bool isCompleted;

		#endregion
	}
}