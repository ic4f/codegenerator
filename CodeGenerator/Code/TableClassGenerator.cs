using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code
{
	public abstract class TableClassGenerator : ClassGenerator
	{
		public TableClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace) 
		{
			className = c.Name + "Table";
		} 

		public override string GetCode()
		{
			StringBuilder sb = new StringBuilder();
			MakeHeader(sb);			
			MakeMainPart(sb);
			MakeFooter(sb);
			return sb.ToString();
		}

		public override string GetFileName()
		{
			return className + Helper.FileExtension;
		}

		protected string ClassName { get { return className; } }

		protected abstract void MakeHeader(StringBuilder sb);

		protected abstract void MakeMainPart(StringBuilder sb);

		protected abstract void MakeFooter(StringBuilder sb);

		private string className;
	}
}
