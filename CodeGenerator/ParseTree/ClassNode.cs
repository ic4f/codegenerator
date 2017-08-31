using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class ClassNode : IParseTreeNode
	{
		public ClassNode(string name, string type) 
		{
			this.name = name;
			this.type = type;
			addSprocNodesList = new ArrayList();
			addSprocNodesHash = new Hashtable();
		}

		public void Validate()
		{
			Application.ClassTypeFactory.GetClassType(type);
			table.Validate();
			checkPrimaryKeys();
			checkReturnFields();
			foreach (AddSprocNode node in addSprocNodesList)
				node.Validate();
		}

		public string Name { get { return name; } }

		public string Type { get { return type; } }
		
		public TableNode Table
		{ 
			get { return table; } 
			set { table = value; }
		}

		public void AddAdditionalSproc(AddSprocNode node)
		{
			addSprocNodesList.Add(node);
			try 
			{
				addSprocNodesHash.Add(node.Name, node);
			}
			catch (Exception e)
			{
				throw new DuplicateSprocException("Duplicate additionalSproc error; " + e.Message);			
			}			
		}

		public ArrayList AddSprocNodesList { get { return addSprocNodesList; } }
		public Hashtable AddSprocNodesHash { get { return addSprocNodesHash; } }

		private void checkPrimaryKeys()
		{			
			int pkeys = 0;
			foreach (FieldNode node in Table.FieldNodesList)			
				if (node.IsPrimaryKey)
					pkeys++;

			Application.ClassType cType = Application.ClassTypeFactory.GetClassType(type);
			if ( (pkeys > 2) || (pkeys > 1 && cType != Application.ClassType.Link) )
				throw new MultiplePrimaryKeysException("Too many primary keys in table " + Table.Name);
		}

		private void checkReturnFields()
		{		
			//each return field must correspond to a table field name,
			//or must be a customReturnField node.
			foreach (AddSprocNode sproc in addSprocNodesList)			
				foreach(SprocReturnFieldNode rField in sproc.ReturnFieldNodesHash.Values)
					checkReturnFieldName(rField.Name);			
		}

		private void checkReturnFieldName(string name)
		{
			bool found = false;
			foreach (FieldNode field in Table.FieldNodesList)
				if (field.Name == name)
					found = true;

			if (!found)
				throw new InvalidReturnFieldException(name + " is an invalid return field");
		}

		private string name;
		private string type;
		private TableNode table;
		private ArrayList addSprocNodesList;
		private Hashtable addSprocNodesHash;
	}
}
