using System;

namespace CodeGenerator.Sql
{
	/// <summary>
	/// Represents a Foreign Key constraint in a SQL Server db table
	/// 
	/// Note: equals and gethashcode operate ONLY on the field value, because by "equals" we imply
	/// "this type of constraint on this field"	 
	/// </summary>
	public class ForeignKeyConstraint : Constraint
	{
		public ForeignKeyConstraint(TableField field, string refTableName, string refFieldName, string name) :
			base(field, name) 
		{
			if (refTableName == null)
				throw new ConstraintFormatException("Constraint referenced table cannot be null");
			if (refFieldName == null)
				throw new ConstraintFormatException("Constraint referenced field cannot be null");

			this.refTableName = refTableName;
			this.refFieldName = refFieldName;
		}

		public string RefTableName { get { return refTableName;  } }

		public string RefFieldName { get { return refFieldName;  } }

		public string RefTableSqlName { get { return "[" + refTableName + "]";  } }

		public string RefFieldSqlName { get { return "[" + refFieldName + "]";  } }

		public override bool IsIdenticalTo(Constraint c)
		{
			ForeignKeyConstraint fc = c as ForeignKeyConstraint;
			if (fc == null)
				return false;
			else			
				return Equals(fc) && refTableName == fc.RefTableName && refFieldName == fc.RefFieldName;			
		}

		public override bool Equals(object obj)
		{
			ForeignKeyConstraint c = obj as ForeignKeyConstraint;
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
			return "ForeignKeyConstraint (table = " + Field.Table.Name + " field = " + Field.Name + ", refTable = " + refTableName + 
				", refField = " + refFieldName + ")";
		}

		private string refTableName;
		private string refFieldName;
	}
}