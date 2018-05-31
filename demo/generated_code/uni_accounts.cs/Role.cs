using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Role
	{
		#region constructor
		public Role(int recordId)
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
			set { description = value; }
		}
		#endregion

		#region short Rank
		public short Rank
		{
			get { return rank; }
			set { rank = value; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			ArrayList p = new ArrayList();
			p.Add(new Param("Id", id));
			p.Add(new Param("Description", description));
			p.Add(new Param("Rank", rank));

			int result = tool.UpdateRecord(id, p);
			if (result >= 0)
				Load(tool.LoadRecord(id)); //must reload: some values might have been changed

			return result;
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
			public DbTool() : base("a_Role") {}
		}

		private DbTool tool;
		private int id;
		private string description;
		private short rank;

		#endregion
	}
}