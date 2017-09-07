using System;
using System.Collections;
using c = Wid.Core;

namespace Wid.Data
{
	public class UserTable : c.AbstractDataTable
	{
		#region static IDataField IdField 
		public static c.IDataField IdField { get { return new IdFieldClass(); } }
		#endregion

		#region static IDataField LoginField 
		public static c.IDataField LoginField { get { return new LoginFieldClass(); } }
		#endregion

		#region static IDataField PasswordField 
		public static c.IDataField PasswordField { get { return new PasswordFieldClass(); } }
		#endregion

		#region static IDataField FirstNameField 
		public static c.IDataField FirstNameField { get { return new FirstNameFieldClass(); } }
		#endregion

		#region static IDataField LastNameField 
		public static c.IDataField LastNameField { get { return new LastNameFieldClass(); } }
		#endregion

		#region static IDataField CreatedField 
		public static c.IDataField CreatedField { get { return new CreatedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedField 
		public static c.IDataField ModifiedField { get { return new ModifiedFieldClass(); } }
		#endregion

		#region static IDataField ModifiedByField 
		public static c.IDataField ModifiedByField { get { return new ModifiedByFieldClass(); } }
		#endregion

		#region static IDataField FullNameField 
		public static c.IDataField FullNameField { get { return new FullNameFieldClass(); } }
		#endregion

		#region static IDataField SelectedField 
		public static c.IDataField SelectedField { get { return new SelectedRecord(); } }
		#endregion

		#region constructor 
		public UserTable(ArrayList rows, int totalCount) : base(rows, totalCount) {}
		#endregion

		#region int FieldCount
		public override int FieldCount { get { return 10; } }
		#endregion

		#region UserRow this[int row]
		public UserRow this[int row] { get { return (UserRow)Rows[row]; } }
		#endregion

		#region private
		private class IdFieldClass : c.IDataField
		{
			public string DataField { get { return "Id"; } }
			public string SortExpression { get { return "[User].Id"; } }
			public string Display { get { return "Id"; } }
		}

		private class LoginFieldClass : c.IDataField
		{
			public string DataField { get { return "Login"; } }
			public string SortExpression { get { return "[User].Login"; } }
			public string Display { get { return "Login"; } }
		}

		private class PasswordFieldClass : c.IDataField
		{
			public string DataField { get { return "Password"; } }
			public string SortExpression { get { return "[User].Password"; } }
			public string Display { get { return "Password"; } }
		}

		private class FirstNameFieldClass : c.IDataField
		{
			public string DataField { get { return "FirstName"; } }
			public string SortExpression { get { return "[User].FirstName"; } }
			public string Display { get { return "FirstName"; } }
		}

		private class LastNameFieldClass : c.IDataField
		{
			public string DataField { get { return "LastName"; } }
			public string SortExpression { get { return "[User].LastName"; } }
			public string Display { get { return "LastName"; } }
		}

		private class CreatedFieldClass : c.IDataField
		{
			public string DataField { get { return "Created"; } }
			public string SortExpression { get { return "[User].Created"; } }
			public string Display { get { return "Created"; } }
		}

		private class ModifiedFieldClass : c.IDataField
		{
			public string DataField { get { return "Modified"; } }
			public string SortExpression { get { return "[User].Modified"; } }
			public string Display { get { return "Modified"; } }
		}

		private class ModifiedByFieldClass : c.IDataField
		{
			public string DataField { get { return "ModifiedBy"; } }
			public string SortExpression { get { return "[User].ModifiedBy"; } }
			public string Display { get { return "ModifiedBy"; } }
		}

		private class FullNameFieldClass : c.IDataField
		{
			public string DataField { get { return "FullName"; } }
			public string SortExpression { get { return "LastName"; } }
			public string Display { get { return "Full Name"; } }
		}

		private class SelectedRecord : c.IDataField
		{
			public string DataField { get { return "Selected"; } }
			public string SortExpression { get { return "Selected"; } }
			public string Display { get { return "Selected"; } }
		}
		#endregion
	}

	public class UserRow : c.IDataRow
	{
		#region int Id
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		#endregion

		#region string Login
		public string Login
		{
			get { return login; }
			set { login = value; }
		}
		#endregion

		#region byte[] Password
		public byte[] Password
		{
			get { return password; }
			set { password = value; }
		}
		#endregion

		#region string FirstName
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}
		#endregion

		#region string LastName
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}
		#endregion

		#region DateTime Created
		public DateTime Created
		{
			get { return created; }
			set { created = value; }
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

		#region string FullName
		public string FullName
		{
			get { return fullName; }
			set { fullName = value; }
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
		private string login;
		private byte[] password;
		private string firstName;
		private string lastName;
		private DateTime created;
		private DateTime modified;
		private string modifiedBy;
		private string fullName;
		private bool selected;
		#endregion
	}
}