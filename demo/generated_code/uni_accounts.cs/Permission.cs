using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Permission
	{
		#region constructor
		public Permission(int recordId)
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

		#region int CategoryId
		public int CategoryId
		{
			get { return categoryId; }
		}
		#endregion

		#region string Description
		public string Description
		{
			get { return description; }
		}
		#endregion

		#region string Code
		public string Code
		{
			get { return code; }
		}
		#endregion

		#region short Rank
		public short Rank
		{
			get { return rank; }
		}
		#endregion

		#region string PermissionCategory_Description
		public string PermissionCategory_Description
		{
			get { return permissionCategory_Description; }
		}
		#endregion

		#region short PermissionCategory_Rank
		public short PermissionCategory_Rank
		{
			get { return permissionCategory_Rank; }
		}
		#endregion

		#region private

		private void Load(ArrayList p)
		{
			id = (int)p[0];
			categoryId = (int)p[1];
			description = (string)p[2];
			code = (string)p[3];
			rank = (short)p[4];
			permissionCategory_Description = (string)p[5];
			permissionCategory_Rank = (short)p[6];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("a_Permission") {}
		}

		private DbTool tool;
		private int id;
		private int categoryId;
		private string description;
		private string code;
		private short rank;
		private string permissionCategory_Description;
		private short permissionCategory_Rank;

		#endregion
	}
}