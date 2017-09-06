using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
    public class NamespaceNode : IParseTreeNode
    {
        public NamespaceNode(string name)
        {
            this.name = name;
            classNodesList = new ArrayList();
            classNodesHash = new Hashtable();
        }

        public void Validate()
        {
            foreach (ClassNode node in classNodesList)
                node.Validate();
        }

        public string Name { get { return name; } }

        public void AddClass(ClassNode node)
        {
            classNodesList.Add(node);
            try
            {
                classNodesHash.Add(node.Name, node);
            }
            catch (Exception e)
            {
                throw new DuplicateClassException("Duplicate class error; " + e.Message);
            }
        }

        public ArrayList ClassNodesList { get { return classNodesList; } }
        public Hashtable ClassNodesHash { get { return classNodesHash; } }

        private string name;
        private ArrayList classNodesList;
        private Hashtable classNodesHash;
    }
}
