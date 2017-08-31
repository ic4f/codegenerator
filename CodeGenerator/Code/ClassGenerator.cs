using System;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code
{
	public abstract class ClassGenerator
	{
		public ClassGenerator(ClassObject c, string parentNamespace)
		{
			this.c = c;
			this.parentNamespace = parentNamespace;
			helper = MakeHelper();
			datatypeHelper = new DatatypeHelper();
		}	

		public int PublicMethodCount
		{
			get { return pmcount; }
			set { pmcount = value; }
		}

		protected abstract BaseHelper MakeHelper();

		public abstract string GetCode();

		protected BaseHelper Helper { get { return helper; } }

		protected DatatypeHelper DatatypeHelper { get { return datatypeHelper; } }

		protected ClassObject Class { get { return c; } }

		protected string ParentNamespace { get { return parentNamespace; } }

		public virtual string GetFileName()
		{
			return Class.Name + helper.FileExtension;
		}

		protected string Tabs(int tabs)
		{
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<tabs; i++)
				sb.Append("\t");
			return sb.ToString();
		}

		protected void RemoveLastComma(StringBuilder sb) 
		{ 
			string temp = sb.ToString();
			int comma = temp.LastIndexOf(",");
			if (comma > -1)
			{
				temp = temp.Substring(0, comma); 
				sb.Remove(0, sb.Length);
				sb.Append(temp);
			}
		}

		private int pmcount;
		private ClassObject c;
		private string parentNamespace;
		private BaseHelper helper;
		private DatatypeHelper datatypeHelper;
	}
}
