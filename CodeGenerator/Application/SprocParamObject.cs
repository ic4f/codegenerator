using System;

namespace CodeGenerator.Application
{
	public class SprocParamObject
	{
		public SprocParamObject(SprocObject parent, string name, string csDatatype, bool isEncrypted)
		{
			this.parent = parent;
			this.name = name;
			this.csDatatype = csDatatype;
			this.isEncrypted = isEncrypted;
		}

		public SprocObject Sproc { get { return parent; } }

		public string Name { get { return name; } }

		public string CsDatatype { get { return csDatatype; } }

		public bool IsEncrypted { get { return isEncrypted; } }

		public override bool Equals(object obj)
		{
			SprocParamObject p = obj as SprocParamObject;
			if (p != null)			
				return p.name == name;			
			else
				return false;
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString() + "(" + name + ", " + csDatatype + ", " + isEncrypted + ")";
		}

		private SprocObject parent;
		private string name;
		private string csDatatype;
		private bool isEncrypted;
	}
}
