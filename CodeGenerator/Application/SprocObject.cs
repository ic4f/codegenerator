using System;
using System.Collections;

namespace CodeGenerator.Application
{
	public class SprocObject
	{
		public SprocObject(ClassObject parent, string name, string returnType)
		{
			this.parent = parent;
			this.name = name;			
			paramsList = new ArrayList();
			paramsHash = new Hashtable();
			returnFieldsList = new ArrayList();
			returnFieldsHash = new Hashtable();

			if (returnType == DatatypeHelper.GenerateReturnType)
			{
				isCustomReturnType = true;
				this.returnType = null;
			}
			else
			{
				isCustomReturnType = false;
				this.returnType = returnType;
			}
		}

		public ClassObject Class { get { return parent; } }

		public string Name { get { return name; } }

		public bool IsCustomReturnType { get { return isCustomReturnType; } }

		public string ReturnType { get { return returnType; } }

		public ArrayList ParamsList { get { return paramsList; } }
		public Hashtable ParamsHash { get { return paramsHash; } }

		public ArrayList ReturnFieldsList { get { return returnFieldsList; } }
		public Hashtable ReturnFieldsHash { get { return returnFieldsHash; } }

		public void AddParam(SprocParamObject p)
		{
			paramsList.Add(p);
			paramsHash.Add(p.Name, p);
		}

		public void AddReturnField(SprocReturnFieldObject f)
		{
			returnFieldsList.Add(f);
			returnFieldsHash.Add(f.Name, f);
		}

		private ClassObject parent;
		private string name;
		private string returnType;
		private bool isCustomReturnType;

		private ArrayList paramsList;
		private Hashtable paramsHash;

		private ArrayList returnFieldsList;
		private Hashtable returnFieldsHash;
	}
}
