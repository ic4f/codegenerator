using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class PayoutsTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountField 
		public static c.IDataField AccountField { get { return new AccountFieldClass(); } }
		#endregion

		#region static IDataField PayoutField 
		public static c.IDataField PayoutField { get { return new PayoutFieldClass(); } }
		#endregion

		#region static IDataField DateField 
		public static c.IDataField DateField { get { return new DateFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public PayoutsTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 5; } }
		#endregion

		#region PayoutsRow this[int row]
		public PayoutsRow this[int row] { get { return (PayoutsRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Payouts.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountFieldClass : c.IDataField
		{
			public string DataField { get { return "Account"; } }
			public string SortExpression { get { return "Payouts.Account"; } }
			public string Display { get { return "Account"; } }
		}

		private class PayoutFieldClass : c.IDataField
		{
			public string DataField { get { return "Payout"; } }
			public string SortExpression { get { return "Payouts.Payout"; } }
			public string Display { get { return "Payout"; } }
		}

		private class DateFieldClass : c.IDataField
		{
			public string DataField { get { return "Date"; } }
			public string SortExpression { get { return "Payouts.[Date]"; } }
			public string Display { get { return "Date"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class PayoutsRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string Account
		public string Account
		{
			get { return account; }
			set { account = value; }
		}
		#endregion

		#region decimal Payout
		public decimal Payout
		{
			get { return payout; }
			set { payout = value; }
		}
		#endregion

		#region DateTime Date
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
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
		private string account;
		private decimal payout;
		private DateTime date;
		private bool selected;
		#endregion
	}
}