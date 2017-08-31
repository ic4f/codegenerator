using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class SprocReturnFieldNode : ISprocReturnFieldNode
	{
		public SprocReturnFieldNode(string name)
		{
			this.name = name;
		}

		public void Validate() {} //nothing to validate

		public string Name { get { return name; } }

		private string name;
	}
}