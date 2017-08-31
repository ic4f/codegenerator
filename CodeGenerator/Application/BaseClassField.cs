using System;

namespace CodeGenerator.Application
{
	public abstract class BaseClassField
	{
		public BaseClassField(
			ClassObject parent,
			string name,
			string displ,
			bool excludefromtable,
			bool includewithparenttable,
			bool includeinlist,
			bool isdefaultsort,
			Sql.DatatypeInstance datatype)
		{
			this.parent = parent;
			this.name = name;
			this.display = displ;
			this.excludefromtable = excludefromtable;
			this.includewithparenttable = includewithparenttable;
			this.includeinlist = includeinlist;
			this.isdefaultsort = isdefaultsort;
			this.datatype = datatype;

			if (display == null || display == "")
				display = name;

			SqlHelper sh = new SqlHelper();
			sqlName = sh.GetSqlName(name);			
		}

		public ClassObject ParentClass { get { return parent; } }

		public string Name { get { return name; } }

		public string SqlName { get { return sqlName; } }

		public Sql.DatatypeInstance Datatype { get { return datatype; } }

		public string Display { get { return display; } }

		public bool ExcludeFromTable { get { return excludefromtable; } }

		public bool IncludeWithParentTable { get { return includewithparenttable; } }

		public virtual string SortExpression { get { return parent.TableSqlName + "." + SqlName; } }

		public override bool Equals(object obj)
		{
			BaseClassField f = obj as BaseClassField;
			if (f != null)			
				return f.Name == Name;			
			else
				return false;
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public abstract string Sql { get; }

		public bool IncludeInList { get { return includeinlist; } }

		public bool IsDefaultSort { get { return isdefaultsort; } }

		private ClassObject parent;
		private string name;
		private string sqlName;
		private Sql.DatatypeInstance datatype;
		private string display;
		private bool excludefromtable;
		private bool includewithparenttable;
		private bool includeinlist;
		private bool isdefaultsort;
	}
}