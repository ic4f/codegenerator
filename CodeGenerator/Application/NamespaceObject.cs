using System;
using System.Collections;

namespace CodeGenerator.Application
{
	public class NamespaceObject
	{
		public NamespaceObject(string name)
		{
			classesList = new ArrayList();
			classesHash = new Hashtable();
			linkClassesList = new ArrayList();
			linkClassesHash = new Hashtable();
			nonLinkClassesList = new ArrayList();
			nonLinkClassesHash = new Hashtable();
			this.name = name;
		}

		public void AddClass(ClassObject c)
		{	
			classesList.Add(c);
			classesHash.Add(c.Name, c);

			if (c.Type == ClassType.Link)
			{
				linkClassesList.Add(c);
				linkClassesHash.Add(c.Name, c);
			}
			else
			{
				nonLinkClassesList.Add(c);
				nonLinkClassesHash.Add(c.Name, c);
			}
		}

		public string Name { get { return name; } }

		public ArrayList ClassesList { get { return classesList; } }

		public Hashtable ClassesHash { get { return classesHash; } }

		public ArrayList LinkClassesList { get { return linkClassesList; } }

		public Hashtable LinkClassesHash { get { return linkClassesHash; } }
	
		public ArrayList NonLinkClassesList { get { return nonLinkClassesList; } }

		public Hashtable NonLinkClassesHash { get { return nonLinkClassesHash; } }

		private ArrayList classesList;
		private Hashtable classesHash;
		private ArrayList linkClassesList;
		private Hashtable linkClassesHash;
		private ArrayList nonLinkClassesList;
		private Hashtable nonLinkClassesHash;
		private string name;
	}
}
