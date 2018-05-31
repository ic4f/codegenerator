using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Email
	{
		#region constructor
		public Email(int recordId)
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

		#region string Subject
		public string Subject
		{
			get { return subject; }
			set { subject = value; }
		}
		#endregion

		#region string Message
		public string Message
		{
			get { return message; }
			set { message = value; }
		}
		#endregion

		#region DateTime Sent
		public DateTime Sent
		{
			get { return sent; }
			set { sent = value; }
		}
		#endregion

		#region string SentBy
		public string SentBy
		{
			get { return sentBy; }
			set { sentBy = value; }
		}
		#endregion

		#region private

		private void Load(ArrayList p)
		{
			id = (int)p[0];
			subject = (string)p[1];
			message = (string)p[2];
			sent = (DateTime)p[3];
			sentBy = (string)p[4];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_Email") {}
		}

		private DbTool tool;
		private int id;
		private string subject;
		private string message;
		private DateTime sent;
		private string sentBy;

		#endregion
	}
}