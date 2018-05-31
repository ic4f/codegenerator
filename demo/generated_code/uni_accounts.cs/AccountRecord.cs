using System;
using Far.Core;

namespace Far.Data
{
	public class AccountRecord
	{
		#region Id 
		public class Id : IRecordField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Id"; } }
			public string Display { get { return "Id"; } }
		}
		#endregion

		#region AccountType 
		public class AccountType : IRecordField
		{
			public string DataField { get { return "AccountType"; } }
			public string SortExpression { get { return "AccountType"; } }
			public string Display { get { return "Account Type"; } }
		}
		#endregion

		#region AccountNumber 
		public class AccountNumber : IRecordField
		{
			public string DataField { get { return "AccountNumber"; } }
			public string SortExpression { get { return "AccountNumber"; } }
			public string Display { get { return "Account Number"; } }
		}
		#endregion

		#region FullAccountNumber 
		public class FullAccountNumber : IRecordField
		{
			public string DataField { get { return "FullAccountNumber"; } }
			public string SortExpression { get { return "FullAccountNumber"; } }
			public string Display { get { return "Full Account Number"; } }
		}
		#endregion

		#region AccountDescription 
		public class AccountDescription : IRecordField
		{
			public string DataField { get { return "AccountDescription"; } }
			public string SortExpression { get { return "AccountDescription"; } }
			public string Display { get { return "Account Description"; } }
		}
		#endregion

		#region AcctStat 
		public class AcctStat : IRecordField
		{
			public string DataField { get { return "AcctStat"; } }
			public string SortExpression { get { return "AcctStat"; } }
			public string Display { get { return "Account Status"; } }
		}
		#endregion

		#region AcctStatDesc 
		public class AcctStatDesc : IRecordField
		{
			public string DataField { get { return "AcctStatDesc"; } }
			public string SortExpression { get { return "AcctStatDesc"; } }
			public string Display { get { return "Account Status Description"; } }
		}
		#endregion

		#region AdminUnit 
		public class AdminUnit : IRecordField
		{
			public string DataField { get { return "AdminUnit"; } }
			public string SortExpression { get { return "AdminUnit"; } }
			public string Display { get { return "Administrative Unit"; } }
		}
		#endregion

		#region AdminUnitDesc 
		public class AdminUnitDesc : IRecordField
		{
			public string DataField { get { return "AdminUnitDesc"; } }
			public string SortExpression { get { return "AdminUnitDesc"; } }
			public string Display { get { return "Administrative Unit Description"; } }
		}
		#endregion

		#region AdminUnitStat 
		public class AdminUnitStat : IRecordField
		{
			public string DataField { get { return "AdminUnitStat"; } }
			public string SortExpression { get { return "AdminUnitStat"; } }
			public string Display { get { return "Administrative Unit Status"; } }
		}
		#endregion

		#region RespPerson1 
		public class RespPerson1 : IRecordField
		{
			public string DataField { get { return "RespPerson1"; } }
			public string SortExpression { get { return "RespPerson1"; } }
			public string Display { get { return "Responsible Person 1"; } }
		}
		#endregion

		#region RespPerson1CC 
		public class RespPerson1CC : IRecordField
		{
			public string DataField { get { return "RespPerson1CC"; } }
			public string SortExpression { get { return "RespPerson1CC"; } }
			public string Display { get { return "Responsible Person 1 Campus Code"; } }
		}
		#endregion

		#region RespPerson2 
		public class RespPerson2 : IRecordField
		{
			public string DataField { get { return "RespPerson2"; } }
			public string SortExpression { get { return "RespPerson2"; } }
			public string Display { get { return "Responsible Person 2"; } }
		}
		#endregion

		#region RespPerson2CC 
		public class RespPerson2CC : IRecordField
		{
			public string DataField { get { return "RespPerson2CC"; } }
			public string SortExpression { get { return "RespPerson2CC"; } }
			public string Display { get { return "Responsible Person 2 Campus Code"; } }
		}
		#endregion

		#region RespPerson3 
		public class RespPerson3 : IRecordField
		{
			public string DataField { get { return "RespPerson3"; } }
			public string SortExpression { get { return "RespPerson3"; } }
			public string Display { get { return "Responsible Person 3"; } }
		}
		#endregion

		#region RespPerson3CC 
		public class RespPerson3CC : IRecordField
		{
			public string DataField { get { return "RespPerson3CC"; } }
			public string SortExpression { get { return "RespPerson3CC"; } }
			public string Display { get { return "Responsible Person 3 Campus Code"; } }
		}
		#endregion

		#region CopyHolder1 
		public class CopyHolder1 : IRecordField
		{
			public string DataField { get { return "CopyHolder1"; } }
			public string SortExpression { get { return "CopyHolder1"; } }
			public string Display { get { return "Copy Holder 1"; } }
		}
		#endregion

		#region CopyHolder1CC 
		public class CopyHolder1CC : IRecordField
		{
			public string DataField { get { return "CopyHolder1CC"; } }
			public string SortExpression { get { return "CopyHolder1CC"; } }
			public string Display { get { return "Copy Holder 1 Campus Code"; } }
		}
		#endregion

		#region CopyHolder2 
		public class CopyHolder2 : IRecordField
		{
			public string DataField { get { return "CopyHolder2"; } }
			public string SortExpression { get { return "CopyHolder2"; } }
			public string Display { get { return "Copy Holder 2"; } }
		}
		#endregion

		#region CopyHolder2CC 
		public class CopyHolder2CC : IRecordField
		{
			public string DataField { get { return "CopyHolder2CC"; } }
			public string SortExpression { get { return "CopyHolder2CC"; } }
			public string Display { get { return "Copy Holder 2 Campus Code"; } }
		}
		#endregion

		#region Guidelines 
		public class Guidelines : IRecordField
		{
			public string DataField { get { return "Guidelines"; } }
			public string SortExpression { get { return "Guidelines"; } }
			public string Display { get { return "Guidelines"; } }
		}
		#endregion

		#region Modified 
		public class Modified : IRecordField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Modified"; } }
			public string Display { get { return "Modified"; } }
		}
		#endregion

		#region ModifiedBy 
		public class ModifiedBy : IRecordField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "ModifiedBy"; } }
			public string Display { get { return "Modified By"; } }
		}
		#endregion

	}
}