using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class PermissionCategory
	{
		#region constructor
		public PermissionCategory(int recordId)
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

		private void Load(ArrayList p)
		{
			id = (int)p[0];
			description = (string)p[1];
			rank = (short)p[2];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_PermissionCategory") {}
		}

		private DbTool tool;
		private int id;
		private string description;
		private short rank;

		#endregion
	}
}