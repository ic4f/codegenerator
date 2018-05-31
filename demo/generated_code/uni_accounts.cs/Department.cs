using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Department
	{
		#region constructor
		public Department(int recordId)
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

		#region int CollegeId
		public int CollegeId
		{
			get { return collegeId; }
			set { collegeId = value; }
		}
		#endregion

		#region string Name
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion

		#region string College_Name
		public string College_Name
		{
			get { return college_Name; }
		}
		#endregion

		#region int Update()
		public int Update()
		{
			ArrayList p = new ArrayList();
			p.Add(new Param("Id", id));
			p.Add(new Param("CollegeId", collegeId));
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
			collegeId = (int)p[1];
			name = (string)p[2];
			college_Name = (string)p[3];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_Department") {}
		}

		private DbTool tool;
		private int id;
		private int collegeId;
		private string name;
		private string college_Name;

		#endregion
	}
}