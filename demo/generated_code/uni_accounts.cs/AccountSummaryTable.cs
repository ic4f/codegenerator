using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccountSummaryTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountIdField 
		public static c.IDataField AccountIdField { get { return new AccountIdFieldClass(); } }
		#endregion

		#region static IDataField StartDateField 
		public static c.IDataField StartDateField { get { return new StartDateFieldClass(); } }
		#endregion

		#region static IDataField EndDateField 
		public static c.IDataField EndDateField { get { return new EndDateFieldClass(); } }
		#endregion

		#region static IDataField BeginningBalanceField 
		public static c.IDataField BeginningBalanceField { get { return new BeginningBalanceFieldClass(); } }
		#endregion

		#region static IDataField EndingBalanceField 
		public static c.IDataField EndingBalanceField { get { return new EndingBalanceFieldClass(); } }
		#endregion

		#region static IDataField TotalRevenuesField 
		public static c.IDataField TotalRevenuesField { get { return new TotalRevenuesFieldClass(); } }
		#endregion

		#region static IDataField TotalExpencesField 
		public static c.IDataField TotalExpencesField { get { return new TotalExpencesFieldClass(); } }
		#endregion

		#region static IDataField CreatedDateField 
		public static c.IDataField CreatedDateField { get { return new CreatedDateFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccountSummaryTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 10; } }
		#endregion

		#region AccountSummaryRow this[int row]
		public AccountSummaryRow this[int row] { get { return (AccountSummaryRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "AccountSummary.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountIdFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountId"; } }
			public string SortExpression { get { return "AccountSummary.AccountId"; } }
			public string Display { get { return "AccountId"; } }
		}

		private class StartDateFieldClass : c.IDataField
		{
			public string DataField { get { return "StartDate"; } }
			public string SortExpression { get { return "AccountSummary.StartDate"; } }
			public string Display { get { return "Start Date"; } }
		}

		private class EndDateFieldClass : c.IDataField
		{
			public string DataField { get { return "EndDate"; } }
			public string SortExpression { get { return "AccountSummary.EndDate"; } }
			public string Display { get { return "End Date"; } }
		}

		private class BeginningBalanceFieldClass : c.IDataField
		{
			public string DataField { get { return "BeginningBalance"; } }
			public string SortExpression { get { return "AccountSummary.BeginningBalance"; } }
			public string Display { get { return "Beginning Balance"; } }
		}

		private class EndingBalanceFieldClass : c.IDataField
		{
			public string DataField { get { return "EndingBalance"; } }
			public string SortExpression { get { return "AccountSummary.EndingBalance"; } }
			public string Display { get { return "Donor/Payee"; } }
		}

		private class TotalRevenuesFieldClass : c.IDataField
		{
			public string DataField { get { return "TotalRevenues"; } }
			public string SortExpression { get { return "AccountSummary.TotalRevenues"; } }
			public string Display { get { return "Total Revenues"; } }
		}

		private class TotalExpencesFieldClass : c.IDataField
		{
			public string DataField { get { return "TotalExpences"; } }
			public string SortExpression { get { return "AccountSummary.TotalExpences"; } }
			public string Display { get { return "Total Expences"; } }
		}

		private class CreatedDateFieldClass : c.IDataField
		{
			public string DataField { get { return "CreatedDate"; } }
			public string SortExpression { get { return "AccountSummary.CreatedDate"; } }
			public string Display { get { return "Extracted"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class AccountSummaryRow : c.IDataRow
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

		#region DateTime StartDate
		public DateTime StartDate
		{
			get { return startDate; }
			set { startDate = value; }
		}
		#endregion

		#region DateTime EndDate
		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}
		#endregion

		#region decimal BeginningBalance
		public decimal BeginningBalance
		{
			get { return beginningBalance; }
			set { beginningBalance = value; }
		}
		#endregion

		#region decimal EndingBalance
		public decimal EndingBalance
		{
			get { return endingBalance; }
			set { endingBalance = value; }
		}
		#endregion

		#region decimal TotalRevenues
		public decimal TotalRevenues
		{
			get { return totalRevenues; }
			set { totalRevenues = value; }
		}
		#endregion

		#region decimal TotalExpences
		public decimal TotalExpences
		{
			get { return totalExpences; }
			set { totalExpences = value; }
		}
		#endregion

		#region DateTime CreatedDate
		public DateTime CreatedDate
		{
			get { return createdDate; }
			set { createdDate = value; }
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
		private DateTime startDate;
		private DateTime endDate;
		private decimal beginningBalance;
		private decimal endingBalance;
		private decimal totalRevenues;
		private decimal totalExpences;
		private DateTime createdDate;
		private bool selected;
		#endregion
	}
}