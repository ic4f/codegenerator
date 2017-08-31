using System;

namespace CodeGenerator.Application
{
	public class ClassAddFieldObject : BaseClassField
	{
		public ClassAddFieldObject(
			ClassObject parent,
			string name,
			string sql,
			string sortexpression,
			string display,
			bool excludefromtable,
			bool includewithparenttable,
			bool includeinlist,
			bool isdefaultsort,
			Sql.DatatypeInstance datatype) : base(
				parent, name, display, excludefromtable, includewithparenttable, includeinlist, isdefaultsort, datatype)
	{
		this.sql = sql;
		this.sortexpression = sortexpression;
	}

	public override string Sql { get { return sql; } }

	public override string SortExpression { get { return sortexpression; } }

	private string sql;
	private string sortexpression;
	}
}

