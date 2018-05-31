using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class Accounts_PayoutTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountNumberField 
		public static c.IDataField AccountNumberField { get { return new AccountNumberFieldClass(); } }
		#endregion

		#region static IDataField PayoutField 
		public static c.IDataField PayoutField { get { return new PayoutFieldClass(); } }
		#endregion

		#region static IDataField PayoutDateField 
		public static c.IDataField PayoutDateField { get { return new PayoutDateFieldClass(); } }
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
		public Accounts_PayoutTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 7; } }
		#endregion

		#region Accounts_PayoutRow this[int row]
		public Accounts_PayoutRow this[int row] { get { return (Accounts_PayoutRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Accounts_Payout.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountNumber"; } }
			public string SortExpression { get { return "Accounts_Payout.AccountNumber"; } }
			public string Display { get { return "AccountNumber"; } }
		}

		private class PayoutFieldClass : c.IDataField
		{
			public string DataField { get { return "Payout"; } }
			public string SortExpression { get { return "Accounts_Payout.Payout"; } }
			public string Display { get { return "Payout"; } }
		}

		private class PayoutDateFieldClass : c.IDataField
		{
			public string DataField { get { return "PayoutDate"; } }
			public string SortExpression { get { return "Accounts_Payout.PayoutDate"; } }
			public string Display { get { return "PayoutDate"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "Accounts_Payout.Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "Accounts_Payout.ModifiedBy"; } }
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

	public class Accounts_PayoutRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string AccountNumber
		public string AccountNumber
		{
			get { return accountNumber; }
			set { accountNumber = value; }
		}
		#endregion

		#region decimal Payout
		public decimal Payout
		{
			get { return payout; }
			set { payout = value; }
		}
		#endregion

		#region DateTime PayoutDate
		public DateTime PayoutDate
		{
			get { return payoutDate; }
			set { payoutDate = value; }
		}
		#endregion

		#region string Modified
		public string Modified
		{
			get { return modified; }
			set { modified = value; }
		}
		#endregion

		#region int ModifiedBy
		public int ModifiedBy
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
		private string accountNumber;
		private decimal payout;
		private DateTime payoutDate;
		private string modified;
		private int modifiedBy;
		private bool selected;
		#endregion
	}
}