using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class TransactionTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField AccountNumberField 
		public static c.IDataField AccountNumberField { get { return new AccountNumberFieldClass(); } }
		#endregion

		#region static IDataField DonorPayeeField 
		public static c.IDataField DonorPayeeField { get { return new DonorPayeeFieldClass(); } }
		#endregion

		#region static IDataField TransDescriptionField 
		public static c.IDataField TransDescriptionField { get { return new TransDescriptionFieldClass(); } }
		#endregion

		#region static IDataField TransDateField 
		public static c.IDataField TransDateField { get { return new TransDateFieldClass(); } }
		#endregion

		#region static IDataField TransAmountField 
		public static c.IDataField TransAmountField { get { return new TransAmountFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public TransactionTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 7; } }
		#endregion

		#region TransactionRow this[int row]
		public TransactionRow this[int row] { get { return (TransactionRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "[Transaction].Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class AccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "AccountNumber"; } }
			public string SortExpression { get { return "[Transaction].AccountNumber"; } }
			public string Display { get { return "Account Number"; } }
		}

		private class DonorPayeeFieldClass : c.IDataField
		{
			public string DataField { get { return "DonorPayee"; } }
			public string SortExpression { get { return "[Transaction].DonorPayee"; } }
			public string Display { get { return "Donor/Payee"; } }
		}

		private class TransDescriptionFieldClass : c.IDataField
		{
			public string DataField { get { return "TransDescription"; } }
			public string SortExpression { get { return "[Transaction].TransDescription"; } }
			public string Display { get { return "Description"; } }
		}

		private class TransDateFieldClass : c.IDataField
		{
			public string DataField { get { return "TransDate"; } }
			public string SortExpression { get { return "[Transaction].TransDate"; } }
			public string Display { get { return "Date"; } }
		}

		private class TransAmountFieldClass : c.IDataField
		{
			public string DataField { get { return "TransAmount"; } }
			public string SortExpression { get { return "[Transaction].TransAmount"; } }
			public string Display { get { return "Amount"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class TransactionRow : c.IDataRow
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

		#region string DonorPayee
		public string DonorPayee
		{
			get { return donorPayee; }
			set { donorPayee = value; }
		}
		#endregion

		#region string TransDescription
		public string TransDescription
		{
			get { return transDescription; }
			set { transDescription = value; }
		}
		#endregion

		#region DateTime TransDate
		public DateTime TransDate
		{
			get { return transDate; }
			set { transDate = value; }
		}
		#endregion

		#region decimal TransAmount
		public decimal TransAmount
		{
			get { return transAmount; }
			set { transAmount = value; }
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
		private string donorPayee;
		private string transDescription;
		private DateTime transDate;
		private decimal transAmount;
		private bool selected;
		#endregion
	}
}