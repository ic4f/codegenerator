using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class AddSprocNode : IParseTreeNode
	{
		public AddSprocNode(string name, string returnType) 
		{
			this.name = name;
			this.returnType = returnType;
			paramNodesList = new ArrayList();
			paramNodesHash = new Hashtable();
			returnFieldNodesHash = new Hashtable();
			customReturnFieldNodesHash = new Hashtable();
			allReturnFieldsList = new ArrayList();
			validateReturnType();
		}

		public void Validate()
		{
			if (returnType == DatatypeHelper.GenerateReturnType && 
				(returnFieldNodesHash.Count == 0 && customReturnFieldNodesHash.Count == 0))
				throw new InvalidSprocFormatException("A sproc with 'generate' return type must have return fields or addReturn fiels");

			if (returnType != DatatypeHelper.GenerateReturnType && 
				(returnFieldNodesHash.Count > 0 || customReturnFieldNodesHash.Count > 0))
				throw new InvalidSprocFormatException("A sproc with a built-in return type cannot have return fields or addReturn fiels");

			checkDuplicateReturnFields();
		}
		
		public string Name { get { return name; } }

		public string ReturnType { get { return returnType; } }

		public void AddParam(ParamNode node)
		{
			paramNodesList.Add(node);
			try 
			{
				paramNodesHash.Add(node.Name, node);
			}
			catch (Exception e)
			{
				throw new DuplicateParamException("Duplicate param error; " + e.Message);		
			}			
		}

		public void AddReturnField(SprocReturnFieldNode node)
		{
			try 
			{
				returnFieldNodesHash.Add(node.Name, node);
				allReturnFieldsList.Add(node);
			}
			catch (Exception e)
			{
				throw new  DuplicateReturnFieldException("Duplicate returnField error; " + e.Message);			
			}				
		}

		public void AddCustomReturnField(SprocCustomReturnFieldNode node)
		{
			try 
			{
				customReturnFieldNodesHash.Add(node.Name, node);
				allReturnFieldsList.Add(node);
			}
			catch (Exception e)
			{
				throw new  DuplicateCustomReturnFieldException("Duplicate additionalReturnField error; " + e.Message);			
			}	
		}

		public Hashtable ReturnFieldNodesHash { get { return returnFieldNodesHash; } }

		public Hashtable CustomReturnFieldNodesHash { get { return customReturnFieldNodesHash; } }

		public ArrayList AllReturnFieldsList { get { return allReturnFieldsList; } }

		public ArrayList ParamNodesList { get { return paramNodesList; } }
		public Hashtable ParamNodesHash { get { return paramNodesHash; } }

		private void validateReturnType()
		{
			DatatypeHelper dtHelper = new DatatypeHelper();
			if (!dtHelper.IsValidSprocReturnType(returnType))
				throw new InvalidSprocReturnTypeException("Datatype cannot be a sproc return type: " + returnType);
		}

		private void checkDuplicateReturnFields()
		{
			foreach (SprocReturnFieldNode rfieldNode in returnFieldNodesHash.Values)
				findDuplicateReturnField(rfieldNode);
			foreach (SprocCustomReturnFieldNode arfieldNode in customReturnFieldNodesHash.Values)
				findDuplicateReturnField(arfieldNode);
		}

		private void findDuplicateReturnField(ISprocReturnFieldNode node)
		{
			foreach (SprocReturnFieldNode rfieldNode in returnFieldNodesHash.Values)			
				if (rfieldNode != node && rfieldNode.Name == node.Name)
					throw new DuplicateReturnFieldCustomReturnFieldException("Duplicate return fields: " + rfieldNode.Name);			
			foreach (SprocCustomReturnFieldNode arfieldNode in customReturnFieldNodesHash.Values)
				if (arfieldNode != node && arfieldNode.Name == node.Name)
					throw new DuplicateReturnFieldCustomReturnFieldException("Duplicate return fields: " + arfieldNode.Name);
		}

		private string name;
		private string returnType;
		private ArrayList paramNodesList;
		private Hashtable paramNodesHash;
		private Hashtable returnFieldNodesHash;
		private Hashtable customReturnFieldNodesHash;
		private ArrayList allReturnFieldsList;
	}
}
