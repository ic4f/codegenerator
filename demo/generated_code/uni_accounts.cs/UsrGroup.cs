using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class UsrGroup
	{
		#region constructor
		public UsrGroup(int recordId)
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

		#region string Name
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			ArrayList p = new ArrayList();
			p.Add(new Param("Id", id));
			p.Add(new Param("Name", name));

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
			name = (string)p[1];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_UsrGroup") {}
		}

		private DbTool tool;
		private int id;
		private string name;

		#endregion
	}
}