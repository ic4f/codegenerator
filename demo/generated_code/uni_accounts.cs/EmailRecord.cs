using System;
using Far.Core;

namespace Far.Data
{
	public class EmailRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region Subject 
		public class Subject : IRecordField
		{
			public string DataField { get { return "Subject"; } }
			public string SortExpression { get { return "Subject"; } }
			public string Display { get { return "Subject"; } }
		}
		#endregion

		#region Message 
		public class Message : IRecordField
		{
			public string DataField { get { return "Message"; } }
			public string SortExpression { get { return "Message"; } }
			public string Display { get { return "Message"; } }
		}
		#endregion

		#region Sent 
		public class Sent : IRecordField
		{
			public string DataField { get { return "Sent"; } }
			public string SortExpression { get { return "Sent"; } }
			public string Display { get { return "Sent"; } }
		}
		#endregion

		#region SentBy 
		public class SentBy : IRecordField
		{
			public string DataField { get { return "SentBy"; } }
			public string SortExpression { get { return "SentBy"; } }
			public string Display { get { return "Sent By"; } }
		}
		#endregion

	}
}