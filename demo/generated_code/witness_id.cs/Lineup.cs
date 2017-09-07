using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class Lineup : Core.DataClass
	{
		#region constructor
		public Lineup(int recordId) : base()
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

		#region int SuspectId
		public int SuspectId
		{
			get { return suspectId; }
			set { suspectId = value; }
		}
		#endregion

		#region int SuspectPhotoPosition
		public int SuspectPhotoPosition
		{
			get { return suspectPhotoPosition; }
			set { suspectPhotoPosition = value; }
		}
		#endregion

		#region int CaseId
		public int CaseId
		{
			get { return caseId; }
			set { caseId = value; }
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

		#region string Case_Number
		public string Case_Number
		{
			get { return case_Number; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			SqlCommand command = new SqlCommand("a_Lineup_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (SuspectId > 0)
				command.Parameters.Add(new SqlParameter("@SuspectId", SuspectId));
			else
				command.Parameters.Add(new SqlParameter("@SuspectId", System.DBNull.Value));

				command.Parameters.Add(new SqlParameter("@SuspectPhotoPosition", SuspectPhotoPosition));
			if (CaseId > 0)
				command.Parameters.Add(new SqlParameter("@CaseId", CaseId));
			else
				command.Parameters.Add(new SqlParameter("@CaseId", System.DBNull.Value));

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
			SqlCommand command = new SqlCommand("a_Lineup_GetRecord", Connection);
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
				suspectId = reader.GetInt32(1);

			if (reader[2] != System.DBNull.Value)
				suspectPhotoPosition = reader.GetInt32(2);

			if (reader[3] != System.DBNull.Value)
				caseId = reader.GetInt32(3);

			if (reader[4] != System.DBNull.Value)
				description = reader.GetString(4);

			if (reader[5] != System.DBNull.Value)
				created = reader.GetDateTime(5);

			if (reader[6] != System.DBNull.Value)
				modified = reader.GetDateTime(6);

			if (reader[7] != System.DBNull.Value)
				modifiedBy = reader.GetString(7);

			if (reader[8] != System.DBNull.Value)
				case_Number = reader.GetString(8);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private int suspectId;
		private int suspectPhotoPosition;
		private int caseId;
		private string description;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string case_Number;

		#endregion
	}
}