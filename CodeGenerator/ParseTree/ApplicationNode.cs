using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class ApplicationNode : IParseTreeNode
	{
		public ApplicationNode()
		{
			namespaceNodesList = new ArrayList();
			namespaceNodesHash = new Hashtable();
		}

		public void Validate()
		{
			foreach (NamespaceNode node in namespaceNodesList)
				node.Validate();
		}

		public void AddNamespace(NamespaceNode node)
		{
			namespaceNodesList.Add(node);
			try 
			{
				namespaceNodesHash.Add(node.Name, node);
			}
			catch (Exception e)
			{
				throw new DuplicateNamespaceException("Duplicate namespace error; " + e.Message);
			}			
		}

		public ArrayList NamespaceNodesList { get { return namespaceNodesList; } }
		public Hashtable NamespaceNodesHash { get { return namespaceNodesHash; } }

		private ArrayList namespaceNodesList;
		private Hashtable namespaceNodesHash;
	}
}
