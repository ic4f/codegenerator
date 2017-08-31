using System;
using System.Collections;

namespace CodeGenerator.Sql
{
	/// <summary>
	///  Represents a table in a SQL Server db
	/// </summary>
	public class Table
	{
		public Table(string name, Database parent, bool isExternal)
		{
			this.name = name;
			this.parent = parent;	
			this.isExternal = isExternal;
			fieldsList = new ArrayList();
			fieldsHash = new Hashtable();
			constraints = new ArrayList();
			pkey = null;		

			SqlHelper sh = new SqlHelper();
			sqlName = sh.GetSqlName(name);
		}

		public Database Database { get { return parent; } }

		public string Name { get { return name; } }

		public string SqlName { get { return sqlName; } }

		public bool IsExternal { get { return isExternal; } }

		public Hashtable FieldsHash { get { return fieldsHash; } }

		public ArrayList FieldsList { get { return fieldsList; } }

		public ArrayList Constraints { get { return constraints; } }

		public ArrayList UniqueConstraints 
		{		
			get 
			{
				if (uniqueConstraints == null)
				{
					uniqueConstraints = new ArrayList();				
					foreach(Constraint c in constraints)
						if (c is UniqueConstraint)
							uniqueConstraints.Add(c);
				}				
				return uniqueConstraints;
			}
		}

		public ArrayList ForeignKeyConstraints 
		{		
			get 
			{
				if (fkeyConstraints == null)
				{
					fkeyConstraints = new ArrayList();				
					foreach(Constraint c in constraints)
						if (c is ForeignKeyConstraint)
							fkeyConstraints.Add(c);
				}				
				return fkeyConstraints;
			}
		}

		public PrimaryKeyConstraint PrimaryKey { get { return pkey; } } 

		public void AddField(TableField f)
		{		
			fieldsList.Add(f);
			fieldsHash.Add(f.Name, f);					
		}

		public TableField GetField(string name)
		{
			TableField f = (TableField)fieldsHash[name];
			if (f == null)
				throw new TableException("Field " + name + " not found");
			return f;
		}

		public void AddConstraint(Constraint c)
		{
			if (constraints.Contains(c))
				throw new DuplicateConstraintException("Table " + name + " already contains a constraint " + c.ToString());

			if (!fieldsHash.Contains(c.Field.Name))
				throw new TableException("Table " + name + " does not contain the constraint field in " + c.ToString());

			constraints.Add(c);

			if (c is PrimaryKeyConstraint)
				setPrimaryKey((PrimaryKeyConstraint)c);
		}

		private void setPrimaryKey(PrimaryKeyConstraint c)
		{
			if (pkey == null)
				pkey = c;
			else if (pkey != null & !pkey.IsUnary)
				throw new DuplicateConstraintException("Table " + name + " already has a primary key on 2 fields - can't alter");
			else if (pkey != null & pkey.IsUnary)
			{
				PrimaryKeyConstraint newPkey = new PrimaryKeyConstraint(pkey.Field, c.Field, pkey.Name);				
				constraints.Remove(c); 
				constraints.Remove(pkey); 
				constraints.Add(newPkey);					
				pkey = newPkey;
			}
		}

		private Database parent;
		private string name;		
		private string sqlName;
		private bool isExternal;
		private ArrayList fieldsList;
		private Hashtable fieldsHash;
		private ArrayList constraints;
		private ArrayList uniqueConstraints;
		private ArrayList fkeyConstraints;
		private PrimaryKeyConstraint pkey;
	}
}
