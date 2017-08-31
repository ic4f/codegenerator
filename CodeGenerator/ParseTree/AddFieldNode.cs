using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class AddFieldNode : BaseFieldNode
	{
		public AddFieldNode(
			string name,
			string sqldatatype,
			string sql,
			string sortexpression,
			string display,
			bool excludefromtable,
			bool includewithparenttable,
			bool includeInList,
			bool isDefaultSort) : base(
				name, sqldatatype, display, excludefromtable, includewithparenttable, includeInList, isDefaultSort)
		{
			this.sql = sql;
			this.sortexpression = sortexpression;
		}

		public string Sql { get { return sql; } }

		public string SortExpression { get { return sortexpression; } }

		private string sql;
		private string sortexpression;
	}
}