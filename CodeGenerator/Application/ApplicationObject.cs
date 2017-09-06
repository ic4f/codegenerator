using System;
using System.Collections;

namespace CodeGenerator.Application
{
    public class ApplicationObject
    {
        public ApplicationObject()
        {
            namespacesList = new ArrayList();
            namespacesHash = new Hashtable();
        }

        public ArrayList NamespacesList { get { return namespacesList; } }

        public Hashtable NamespacesHash { get { return namespacesHash; } }

        public void AddNamespace(NamespaceObject ns)
        {
            namespacesList.Add(ns);
            namespacesHash.Add(ns.Name, ns);
        }

        public ClassObject GetClassByTableName(string name)
        {
            foreach (NamespaceObject ns in namespacesList)
                foreach (ClassObject cs in ns.ClassesList)
                    if (cs.TableName == name)
                        return cs;
            return null;
        }

        private ArrayList namespacesList;
        private Hashtable namespacesHash;
    }
}
