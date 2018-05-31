using System;
using System.Collections;
using Param = Far.Core.Parameter;

namespace Far.Data
{
	public class Account
	{
		#region constructor
		public Account(int recordId)
		{
			tool = new DbTool();
			Load(tool.LoadRecord(recordId));
		}
		#endregion

		#region string Id
		public string Id
		{
			get { return id; }
		}
		#endregion

		#region string AccountType
		public string AccountType
		{
			get { return accountType; }
		}
		#endregion

		#region string AccountNumber
		public string AccountNumber
		{
			get { return accountNumber; }
		}
		#endregion

		#region string FullAccountNumber
		public string FullAccountNumber
		{
			get { return fullAccountNumber; }
		}
		#endregion

		#region string AccountDescription
		public string AccountDescription
		{
			get { return accountDescription; }
		}
		#endregion

		#region string AcctStat
		public string AcctStat
		{
			get { return acctStat; }
		}
		#endregion

		#region string AcctStatDesc
		public string AcctStatDesc
		{
			get { return acctStatDesc; }
		}
		#endregion

		#region string AdminUnit
		public string AdminUnit
		{
			get { return adminUnit; }
		}
		#endregion

		#region string AdminUnitDesc
		public string AdminUnitDesc
		{
			get { return adminUnitDesc; }
		}
		#endregion

		#region string AdminUnitStat
		public string AdminUnitStat
		{
			get { return adminUnitStat; }
		}
		#endregion

		#region string RespPerson1
		public string RespPerson1
		{
			get { return respPerson1; }
		}
		#endregion

		#region string RespPerson1CC
		public string RespPerson1CC
		{
			get { return respPerson1CC; }
		}
		#endregion

		#region string RespPerson2
		public string RespPerson2
		{
			get { return respPerson2; }
		}
		#endregion

		#region string RespPerson2CC
		public string RespPerson2CC
		{
			get { return respPerson2CC; }
		}
		#endregion

		#region string RespPerson3
		public string RespPerson3
		{
			get { return respPerson3; }
		}
		#endregion

		#region string RespPerson3CC
		public string RespPerson3CC
		{
			get { return respPerson3CC; }
		}
		#endregion

		#region string CopyHolder1
		public string CopyHolder1
		{
			get { return copyHolder1; }
		}
		#endregion

		#region string CopyHolder1CC
		public string CopyHolder1CC
		{
			get { return copyHolder1CC; }
		}
		#endregion

		#region string CopyHolder2
		public string CopyHolder2
		{
			get { return copyHolder2; }
		}
		#endregion

		#region string CopyHolder2CC
		public string CopyHolder2CC
		{
			get { return copyHolder2CC; }
		}
		#endregion

		#region string Guidelines
		public string Guidelines
		{
			get { return guidelines; }
		}
		#endregion

		#region string Modified
		public string Modified
		{
			get { return modified; }
		}
		#endregion

		#region string ModifiedBy
		public string ModifiedBy
		{
			get { return modifiedBy; }
		}
		#endregion

		#region private

		private void Load(ArrayList p)
		{
			id = (string)p[0];
			accountType = (string)p[1];
			accountNumber = (string)p[2];
			fullAccountNumber = (string)p[3];
			accountDescription = (string)p[4];
			acctStat = (string)p[5];
			acctStatDesc = (string)p[6];
			adminUnit = (string)p[7];
			adminUnitDesc = (string)p[8];
			adminUnitStat = (string)p[9];
			respPerson1 = (string)p[10];
			respPerson1CC = (string)p[11];
			respPerson2 = (string)p[12];
			respPerson2CC = (string)p[13];
			respPerson3 = (string)p[14];
			respPerson3CC = (string)p[15];
			copyHolder1 = (string)p[16];
			copyHolder1CC = (string)p[17];
			copyHolder2 = (string)p[18];
			copyHolder2CC = (string)p[19];
			guidelines = (string)p[20];
			modified = (string)p[21];
			modifiedBy = (string)p[22];
		}

		private class DbTool : Core.DbRecord
		{
			public DbTool() : base("vw_Accounts") {}
		}

		private DbTool tool;
		private string id;
		private string accountType;
		private string accountNumber;
		private string fullAccountNumber;
		private string accountDescription;
		private string acctStat;
		private string acctStatDesc;
		private string adminUnit;
		private string adminUnitDesc;
		private string adminUnitStat;
		private string respPerson1;
		private string respPerson1CC;
		private string respPerson2;
		private string respPerson2CC;
		private string respPerson3;
		private string respPerson3CC;
		private string copyHolder1;
		private string copyHolder1CC;
		private string copyHolder2;
		private string copyHolder2CC;
		private string guidelines;
		private string modified;
		private string modifiedBy;

		#endregion
	}
}