using System;

namespace CodeGenerator.Sql
{
	public abstract class Constraint
	{
		public Constraint(TableField field, string name)
		{
			if (field == null)
				throw new ConstraintFormatException("Constraint field cannot be null");

			this.field = field;
			this.name = name;
		}

		public string Name { get { return name; } }

		public virtual bool IsIdenticalTo(Constraint c)
		{
			return Equals(c);
		}

		public TableField Field { get { return field; } }

		private TableField field;
		private string name;
	}
}
