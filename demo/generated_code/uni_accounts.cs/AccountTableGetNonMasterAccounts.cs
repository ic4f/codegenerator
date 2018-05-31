using System;
using System.Collections;
using c = Far.Core;

namespace Far.Data
{
	public class AccountTableGetNonMasterAccounts : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField FullAccountNumberField 
		public static c.IDataField FullAccountNumberField { get { return new FullAccountNumberFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public AccountTableGetNonMasterAccounts(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 2; } }
		#endregion

		#region AccountRowGetNonMasterAccounts this[int row]
		public AccountRowGetNonMasterAccounts this[int row] { get { return (AccountRowGetNonMasterAccounts)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "Account.Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class FullAccountNumberFieldClass : c.IDataField
		{
			public string DataField { get { return "FullAccountNumber"; } }
			public string SortExpression { get { return "Account.FullAccountNumber"; } }
			public string Display { get { return "Full Account Number"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class AccountRowGetNonMasterAccounts : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string FullAccountNumber
		public string FullAccountNumber
		{
			get { return fullAccountNumber; }
			set { fullAccountNumber = value; }
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
		private string fullAccountNumber;
		private bool selected;
		#endregion
	}
}