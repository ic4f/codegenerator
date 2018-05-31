using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccountTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountTypeField 
		public static c.IDataField AccountTypeField { get { return new AccountTypeFieldClass(); } }
		#endregion

		#region static IDataField AccountNumberField 
		public static c.IDataField AccountNumberField { get { return new AccountNumberFieldClass(); } }
		#endregion

		#region static IDataField FullAccountNumberField 
		public static c.IDataField FullAccountNumberField { get { return new FullAccountNumberFieldClass(); } }
		#endregion

		#region static IDataField AccountDescriptionField 
		public static c.IDataField AccountDescriptionField { get { return new AccountDescriptionFieldClass(); } }
		#endregion

		#region static IDataField AcctStatField 
		public static c.IDataField AcctStatField { get { return new AcctStatFieldClass(); } }
		#endregion

		#region static IDataField AdminUnitField 
		public static c.IDataField AdminUnitField { get { return new AdminUnitFieldClass(); } }
		#endregion

		#region static IDataField ModifiedField 
		public static c.IDataField ModifiedField { get { return new ModifiedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedByField 
		public static c.IDataField ModifiedByField { get { return new ModifiedByFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccountTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 10; } }
		#endregion

		#region AccountRow this[int row]
		public AccountRow this[int row] { get { return (AccountRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Account.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountTypeFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountType"; } }
			public string SortExpression { get { return "Account.AccountType"; } }
			public string Display { get { return "Account Type"; } }
		}

		private class AccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountNumber"; } }
			public string SortExpression { get { return "Account.AccountNumber"; } }
			public string Display { get { return "Account Number"; } }
		}

		private class FullAccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "FullAccountNumber"; } }
			public string SortExpression { get { return "Account.FullAccountNumber"; } }
			public string Display { get { return "Full Account Number"; } }
		}

		private class AccountDescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountDescription"; } }
			public string SortExpression { get { return "Account.AccountDescription"; } }
			public string Display { get { return "Account Description"; } }
		}

		private class AcctStatFieldClass : c.IDataField
		{
			public string DataField { get { return "AcctStat"; } }
			public string SortExpression { get { return "Account.AcctStat"; } }
			public string Display { get { return "Account Status"; } }
		}

		private class AdminUnitFieldClass : c.IDataField
		{
			public string DataField { get { return "AdminUnit"; } }
			public string SortExpression { get { return "Account.AdminUnit"; } }
			public string Display { get { return "Administrative Unit"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Account.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "Account.ModifiedBy"; } }
			public string Display { get { return "Modified By"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class AccountRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string AccountType
		public string AccountType
		{
			get { return accountType; }
			set { accountType = value; }
		}
		#endregion

		#region string AccountNumber
		public string AccountNumber
		{
			get { return accountNumber; }
			set { accountNumber = value; }
		}
		#endregion

		#region string FullAccountNumber
		public string FullAccountNumber
		{
			get { return fullAccountNumber; }
			set { fullAccountNumber = value; }
		}
		#endregion

		#region string AccountDescription
		public string AccountDescription
		{
			get { return accountDescription; }
			set { accountDescription = value; }
		}
		#endregion

		#region string AcctStat
		public string AcctStat
		{
			get { return acctStat; }
			set { acctStat = value; }
		}
		#endregion

		#region string AdminUnit
		public string AdminUnit
		{
			get { return adminUnit; }
			set { adminUnit = value; }
		}
		#endregion

		#region DateTime Modified
		public DateTime Modified
		{
			get { return modified; }
			set { modified = value; }
		}
		#endregion

		#region string ModifiedBy
		public string ModifiedBy
		{
			get { return modifiedBy; }
			set { modifiedBy = value; }
		}
		#endregion

		#region bool Selected
		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}
		#endregion

		#region private
		private int id;
		private string accountType;
		private string accountNumber;
		private string fullAccountNumber;
		private string accountDescription;
		private string acctStat;
		private string adminUnit;
		private DateTime modified;
		private string modifiedBy;
		private bool selected;
		#endregion
	}
}