using System;

namespace CodeGenerator.Sql
{
	/// <summary>
	/// Represents a Unique constraint in a SQL Server db table
	/// </summary>
	public class UniqueConstraint : Constraint
	{
		public UniqueConstraint(TableField field, string name) : base(field, name) {}

		public override bool Equals(object obj)
		{
			UniqueConstraint c = obj as UniqueConstraint;
			if (c == null)
				return false;
			else
				return c.Field.Table.Name.Equals(Field.Table.Name) && c.Field.Name.Equals(Field.Name);
		}

		public override int GetHashCode()
		{
			return Field.Table.Name.GetHashCode() + Field.Name.GetHashCode();  //table name + field name
		}

		public override string ToString()
		{
			return "UniqueConstraint (table = " + Field.Table.Name + " field = " + Field.Name + ")";
		}
	}
}