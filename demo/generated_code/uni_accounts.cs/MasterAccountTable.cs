using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class MasterAccountTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountIdField 
		public static c.IDataField AccountIdField { get { return new AccountIdFieldClass(); } }
		#endregion

		#region static IDataField Account_AccountTypeField 
		public static c.IDataField Account_AccountTypeField { get { return new Account_AccountTypeFieldClass(); } }
		#endregion

		#region static IDataField Account_FullAccountNumberField 
		public static c.IDataField Account_FullAccountNumberField { get { return new Account_FullAccountNumberFieldClass(); } }
		#endregion

		#region static IDataField Account_AccountDescriptionField 
		public static c.IDataField Account_AccountDescriptionField { get { return new Account_AccountDescriptionFieldClass(); } }
		#endregion

		#region static IDataField Account_AcctStatField 
		public static c.IDataField Account_AcctStatField { get { return new Account_AcctStatFieldClass(); } }
		#endregion

		#region static IDataField Account_AdminUnitField 
		public static c.IDataField Account_AdminUnitField { get { return new Account_AdminUnitFieldClass(); } }
		#endregion

		#region static IDataField Account_ModifiedField 
		public static c.IDataField Account_ModifiedField { get { return new Account_ModifiedFieldClass(); } }
		#endregion

		#region static IDataField Account_ModifiedByField 
		public static c.IDataField Account_ModifiedByField { get { return new Account_ModifiedByFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public MasterAccountTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 10; } }
		#endregion

		#region MasterAccountRow this[int row]
		public MasterAccountRow this[int row] { get { return (MasterAccountRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "MasterAccount.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountIdFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountId"; } }
			public string SortExpression { get { return "MasterAccount.AccountId"; } }
			public string Display { get { return "AccountId"; } }
		}

		private class Account_AccountTypeFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_AccountType"; } }
			public string SortExpression { get { return "Account.AccountType"; } }
			public string Display { get { return "Account Account Type"; } }
		}

		private class Account_FullAccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_FullAccountNumber"; } }
			public string SortExpression { get { return "Account.FullAccountNumber"; } }
			public string Display { get { return "Account Full Account Number"; } }
		}

		private class Account_AccountDescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_AccountDescription"; } }
			public string SortExpression { get { return "Account.AccountDescription"; } }
			public string Display { get { return "Account Account Description"; } }
		}

		private class Account_AcctStatFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_AcctStat"; } }
			public string SortExpression { get { return "Account.AcctStat"; } }
			public string Display { get { return "Account Account Status"; } }
		}

		private class Account_AdminUnitFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_AdminUnit"; } }
			public string SortExpression { get { return "Account.AdminUnit"; } }
			public string Display { get { return "Account Administrative Unit"; } }
		}

		private class Account_ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_Modified"; } }
			public string SortExpression { get { return "Account.Modified"; } }
			public string Display { get { return "Account Modified"; } }
		}

		private class Account_ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "Account_ModifiedBy"; } }
			public string SortExpression { get { return "Account.ModifiedBy"; } }
			public string Display { get { return "Account Modified By"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class MasterAccountRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region int AccountId
		public int AccountId
		{
			get { return accountId; }
			set { accountId = value; }
		}
		#endregion

		#region string Account_AccountType
		public string Account_AccountType
		{
			get { return account_AccountType; }
			set { account_AccountType = value; }
		}
		#endregion

		#region string Account_FullAccountNumber
		public string Account_FullAccountNumber
		{
			get { return account_FullAccountNumber; }
			set { account_FullAccountNumber = value; }
		}
		#endregion

		#region string Account_AccountDescription
		public string Account_AccountDescription
		{
			get { return account_AccountDescription; }
			set { account_AccountDescription = value; }
		}
		#endregion

		#region string Account_AcctStat
		public string Account_AcctStat
		{
			get { return account_AcctStat; }
			set { account_AcctStat = value; }
		}
		#endregion

		#region string Account_AdminUnit
		public string Account_AdminUnit
		{
			get { return account_AdminUnit; }
			set { account_AdminUnit = value; }
		}
		#endregion

		#region DateTime Account_Modified
		public DateTime Account_Modified
		{
			get { return account_Modified; }
			set { account_Modified = value; }
		}
		#endregion

		#region string Account_ModifiedBy
		public string Account_ModifiedBy
		{
			get { return account_ModifiedBy; }
			set { account_ModifiedBy = value; }
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
		private int accountId;
		private string account_AccountType;
		private string account_FullAccountNumber;
		private string account_AccountDescription;
		private string account_AcctStat;
		private string account_AdminUnit;
		private DateTime account_Modified;
		private string account_ModifiedBy;
		private bool selected;
		#endregion
	}
}