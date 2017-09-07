using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Param = Wid.Core.Parameter;

namespace Wid.Data
{
	public class LineupText : Core.DataClass
	{
		#region constructor
		public LineupText(int recordId) : base()
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

		#region string Name
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region string Content
		public string Content
		{
			get { return content; }
			set { content = value; }
		}
		#endregion

		#region int Rank
		public int Rank
		{
			get { return rank; }
			set { rank = value; }
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
			SqlCommand command = new SqlCommand("a_LineupText_Update", Connection);
			command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Id", Id));
			if (Name != null)
				command.Parameters.Add(new SqlParameter("@Name", Name));
			else
				command.Parameters.Add(new SqlParameter("@Name", System.DBNull.Value));

			if (Content != null)
				command.Parameters.Add(new SqlParameter("@Content", Content));
			else
				command.Parameters.Add(new SqlParameter("@Content", System.DBNull.Value));

				command.Parameters.Add(new SqlParameter("@Rank", Rank));
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
			SqlCommand command = new SqlCommand("a_LineupText_GetRecord", Connection);
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
				name = reader.GetString(1);

			if (reader[2] != System.DBNull.Value)
				content = reader.GetString(2);

			if (reader[3] != System.DBNull.Value)
				rank = reader.GetInt32(3);

			if (reader[4] != System.DBNull.Value)
				modified = reader.GetDateTime(4);

			if (reader[5] != System.DBNull.Value)
				modifiedBy = reader.GetString(5);

			reader.Close();
			Connection.Close();
		}
		private int id;
		private string name;
		private string content;
		private int rank;
		private DateTime modified;
		private string modifiedBy;

		#endregion
	}
}