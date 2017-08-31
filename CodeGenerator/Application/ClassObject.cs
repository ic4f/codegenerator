using System;
using System.Collections;

namespace CodeGenerator.Application
{
	public class ClassObject
	{
		public ClassObject(
			NamespaceObject parent, 
			string name, 
			ClassType type, 
			bool isExternal,
			string tableName,
			string tableSqlName)
		{
			this.parent = parent;
			this.name = name;
			this.type = type;
			this.isExternal = isExternal;
			this.tableName = tableName;
			this.tableSqlName = tableSqlName;
			pkeyField1 = null;
			pkeyField2 = null;
			defaultSortField = null;

			sprocsList = new ArrayList();
			sprocsHash = new Hashtable();

			customSprocsList = new ArrayList();
			customSprocsHash = new Hashtable();

			fieldsList = new ArrayList();
			fieldsHash = new Hashtable();

			addFieldsList = new ArrayList();
			addFieldsHash = new Hashtable();

			includeWithParentIDataFieldList = new ArrayList();
			includeWithParentIDataFieldHash = new Hashtable();

			includeWithListList = new ArrayList();
			includeWithListHash = new Hashtable();

			uniqueFieldList = new ArrayList();
			uniqueFieldHash = new Hashtable();

			foreignKeyFieldList = new ArrayList();
			foreignKeyFieldHash = new Hashtable();

			referringClassesHash = new Hashtable();
			referringLinkClassesHash = new Hashtable();
			referringNonLinkClassesHash = new Hashtable();
		}

		public NamespaceObject Namespace { get { return parent; } }

		public string Name { get { return name; } }

		public ClassType Type { get { return type; } }

		public bool IsExternal { get { return isExternal; } }

		public string TableName { get { return tableName; } }

		public string TableSqlName { get { return tableSqlName; } }

		public ArrayList StandardReturnTypeSprocsList { get { return sprocsList; } }
		public Hashtable StandardReturnTypeSprocsHash { get { return sprocsHash; } }

		public ArrayList CustomReturnTypeSprocsList { get { return customSprocsList; } }
		public Hashtable CustomReturnTypeSprocsHash { get { return customSprocsHash; } }

		public ArrayList FieldsList { get { return fieldsList; } }
		public Hashtable FieldsHash { get { return fieldsHash; } }

		public ArrayList AddFieldsList { get { return addFieldsList; } }
		public Hashtable AddFieldsHash { get { return addFieldsHash; } }

		public ArrayList IncludeWithParentIDataFieldList { get { return includeWithParentIDataFieldList; } }
		public Hashtable IncludeWithParentIDataFieldHash { get { return includeWithParentIDataFieldHash; } }

		public ArrayList IncludeWithListList
		{ 
			get 
			{ 
				if (includeWithListList.Count == 0)
					includeWithListList.Add(pkeyField1);
				return includeWithListList; 
			} 
		}

		public Hashtable IncludeWithListHash 
		{ 
			get 
			{ 
				if (includeWithListHash.Count == 0)
					includeWithListHash.Add(pkeyField1.Name, pkeyField1);
				return includeWithListHash; 
			} 
		}

		public ArrayList UniqueFieldList { get { return uniqueFieldList; } }
		public Hashtable UniqueFieldHash { get { return uniqueFieldHash; } }

		public ArrayList ForeignKeyFieldList { get { return foreignKeyFieldList; } }
		public Hashtable ForeignKeyFieldHash { get { return foreignKeyFieldHash; } }

		public ArrayList InstanceIDataFieldList 
		{ 
			get 
			{
				if (instanceIDataFieldList == null)
					loadFieldCollections();
				return instanceIDataFieldList; 
			}
		}
	
		public ArrayList SetIDataFieldList
		{ 
			get 
			{
				if (setIDataFieldList == null)
					loadFieldCollections();
				return setIDataFieldList; 
			}
		}

		public ClassFieldObject PrimaryKeyField1 { get { return pkeyField1; } }

		public ClassFieldObject PrimaryKeyField2 { get { return pkeyField2; } }

		public BaseClassField DefaultSortField 
		{ 
			get 
			{
				if (defaultSortField == null)
					defaultSortField = (BaseClassField)fieldsList[0];
				return defaultSortField; 
			} 
		}

		public void AddSproc(SprocObject sp)
		{
			if (sp.IsCustomReturnType)
			{
				customSprocsList.Add(sp);
				customSprocsHash.Add(sp.Name, sp);
			}
			else
			{
				sprocsList.Add(sp);
				sprocsHash.Add(sp.Name, sp);
			}
		}

		public void AddField(ClassFieldObject f)
		{
			fieldsList.Add(f);
			fieldsHash.Add(f.Name, f);

			if (f.IsUnique)
			{
				uniqueFieldList.Add(f);
				uniqueFieldHash.Add(f.Name, f);
			}

			if (f.IsForeignKey)
			{
				foreignKeyFieldList.Add(f);
				foreignKeyFieldHash.Add(f.Name, f);
			}

			if (f.IsPrimaryKey)
			{
				if (pkeyField1 == null)
					pkeyField1 = f;
				else if (pkeyField2 == null)
					pkeyField2 = f;
			}

			processIncludeWithParent(f);
			processIncludeWithList(f);
			processDefaultSort(f);
		}

		public void AddAdditionalField(ClassAddFieldObject f)
		{
			addFieldsList.Add(f);
			addFieldsHash.Add(f.Name, f);

			processIncludeWithParent(f);
			processIncludeWithList(f);
			processDefaultSort(f);
		}

		public Hashtable ReferringClassesHash { get { return referringClassesHash; } }

		public Hashtable ReferringLinkClassesHash { get { return referringLinkClassesHash; } }

		public Hashtable ReferringNonLinkClassesHash { get { return referringNonLinkClassesHash; } }

		public void AddReferringClass(ClassObject c)
		{
			referringClassesHash.Add(c.Name, c);

			if (c.Type == ClassType.Link)
				referringLinkClassesHash.Add(c.Name, c);
			else
				referringNonLinkClassesHash.Add(c.Name, c);
		}

		public bool HasTextField
		{
			get
			{
				foreach(BaseClassField f in setIDataFieldList)
					if (f.Datatype.IsTextType)
						return true;
//				foreach(ClassAddFieldObject f in addFieldsList)
//					if (f.Datatype.IsTextType)
//						return true;
				return false;
			}
		}

		public ClassFieldObject GetForeignKeyFieldForClass(ClassObject refClass)
		{
			foreach(ClassFieldObject f in fieldsList)
				if (f.IsForeignKey && f.ReferencedClass == refClass)
					return f;
			return null;
		}

		private void loadFieldCollections()
		{
			instanceIDataFieldList = new ArrayList();
			foreach(ClassFieldObject f in fieldsList)
				instanceIDataFieldList.Add(f);
								
			foreach(ClassAddFieldObject f in addFieldsList)
				instanceIDataFieldList.Add(f);
			
			setIDataFieldList = new ArrayList();
			foreach(ClassFieldObject f in fieldsList)			
				if (!f.ExcludeFromTable)
					setIDataFieldList.Add(f);
									
			foreach(ClassAddFieldObject f in addFieldsList)
				if (!f.ExcludeFromTable)
					setIDataFieldList.Add(f);
				
			addChildFields();
		}

		private void addChildFields()
		{
			foreach (ClassFieldObject fkey in foreignKeyFieldList)				
				if (fkey.ReferencedClass.Type != ClassType.Link)
				{
					foreach(BaseClassField f in fkey.ReferencedClass.IncludeWithParentIDataFieldList)
						if (f != fkey.ReferencedField)	
						{
							instanceIDataFieldList.Add(f);
							setIDataFieldList.Add(f);
						}
				}
		}

		private void processDefaultSort(BaseClassField f)
		{
			if (f.IsDefaultSort && defaultSortField == null)
				defaultSortField = f;
			else if (f.IsDefaultSort && defaultSortField != null) 
				throw new DuplicateDefaultSortFieldException(
					"class " + Name + " cannot have multiple default sort fields");
		}

		private void processIncludeWithParent(BaseClassField f)
		{
			if (f.IncludeWithParentTable)
			{
				includeWithParentIDataFieldList.Add(f);
				includeWithParentIDataFieldHash.Add(f.Name, f);
			}
		}
		
		private void processIncludeWithList(BaseClassField f)
		{
			if (f.IncludeInList)
			{
				includeWithListList.Add(f);
				includeWithListHash.Add(f.Name, f);
			}
		}

		private NamespaceObject parent;
		private string name;
		private ClassType type;
		private bool isExternal;
		private string tableName;
		private string tableSqlName;
		private ClassFieldObject pkeyField1;
		private ClassFieldObject pkeyField2;
		private BaseClassField defaultSortField;

		private ArrayList sprocsList;
		private Hashtable sprocsHash;

		private ArrayList customSprocsList;
		private Hashtable customSprocsHash;
		
		private ArrayList fieldsList;
		private Hashtable fieldsHash;
		
		private ArrayList addFieldsList;
		private Hashtable addFieldsHash;

		private ArrayList instanceIDataFieldList;
		
		private ArrayList setIDataFieldList;

		private ArrayList includeWithParentIDataFieldList;
		private Hashtable includeWithParentIDataFieldHash;

		private ArrayList includeWithListList;
		private Hashtable includeWithListHash;

		private ArrayList uniqueFieldList;
		private Hashtable uniqueFieldHash;
		
		private ArrayList foreignKeyFieldList;
		private Hashtable foreignKeyFieldHash;

		private Hashtable referringClassesHash;
		private Hashtable referringLinkClassesHash;
		private Hashtable referringNonLinkClassesHash;
	}
}
