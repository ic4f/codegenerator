using System;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Represents a Primary Key constraint in a SQL Server db table
    /// </summary>
    public class PrimaryKeyConstraint : Constraint
    {
        public PrimaryKeyConstraint(TableField field, string name) : this(field, null, name) { }

        public PrimaryKeyConstraint(TableField field1, TableField field2, string name) : base(field1, name)
        {
            this.field2 = field2;
        }

        public TableField Field2 { get { return field2; } }

        public bool IsUnary { get { return field2 == null; } }

        public override bool Equals(object obj)
        {
            PrimaryKeyConstraint pc = obj as PrimaryKeyConstraint;
            if (pc == null)
                return false;
            else if (pc.IsUnary)
            {
                if (!this.IsUnary)
                    return false;
                else
                    return pc.Field.Table.Name.Equals(Field.Table.Name) && pc.Field.Name.Equals(Field.Name);
            }
            else
            {
                if (this.IsUnary)
                    return false;
                else
                    return pc.Field.Table.Name.Equals(Field.Table.Name) && pc.Field.Name.Equals(Field.Name) &&
                         pc.Field2.Name.Equals(field2.Name);
            }
        }

        public override int GetHashCode()
        {
            if (field2 == null)
                return Field.Table.Name.GetHashCode() + Field.Name.GetHashCode();
            else
                return Field.Table.Name.GetHashCode() + Field.Name.GetHashCode() + field2.Name.GetHashCode();
        }

        public override string ToString()
        {
            if (IsUnary)
                return "PrimaryKeyConstraint (table = " + Field.Table.Name + " field = " + Field.Name + ")";
            else
                return "PrimaryKeyConstraint (table = " + Field.Table.Name + " field1 = " + Field.Name + ", field2 = " + Field2.Name + ")";
        }

        private TableField field2;
    }
}
