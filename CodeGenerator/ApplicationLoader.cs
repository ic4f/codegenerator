using System;
using System.Collections;
using t = CodeGenerator.ParseTree;
using a = CodeGenerator.Application;

namespace CodeGenerator
{
	public class ApplicationLoader
	{
		public ApplicationLoader(t.ApplicationNode root)
		{
			application = new a.ApplicationObject();
			loadApplication(root);
			loadDependencies();
		}
		
		public a.ApplicationObject Application { get { return application; } }

		private void loadApplication(t.ApplicationNode root)
		{
			foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
				loadNamespace(nsNode);		
		}

		private void loadNamespace(t.NamespaceNode nsNode)
		{
			a.NamespaceObject ns = new a.NamespaceObject(nsNode.Name);
			application.AddNamespace(ns);

			foreach (t.ClassNode csNode in nsNode.ClassNodesList)
				loadClass(ns, csNode);
		}

		private void loadClass(a.NamespaceObject parent, t.ClassNode csNode)
		{
			string name = csNode.Name;
			a.ClassType type = a.ClassTypeFactory.GetClassType(csNode.Type);			
			bool isExternal = csNode.Table.IsExternal;
			string tableName = csNode.Table.Name;
			string tableSqlName = new SqlHelper().GetSqlName(tableName);
			a.ClassObject cs = new a.ClassObject(parent, name, type, isExternal, tableName, tableSqlName);
			parent.AddClass(cs);

			foreach (t.FieldNode fNode in csNode.Table.FieldNodesList)
				loadField(cs, fNode);

			foreach (t.AddFieldNode afNode in csNode.Table.AddFieldNodesList)
				loadAdditionalField(cs, afNode);

			foreach (t.AddSprocNode spNode in csNode.AddSprocNodesList)
				loadSproc(cs, spNode);
		}

		private void loadField(a.ClassObject parent, t.FieldNode fNode)
		{
			string name = fNode.Name;
			string sqldatatype = fNode.SqlDatatype;
			bool identity = fNode.IsIdentity;
			bool primarykey = fNode.IsPrimaryKey;
			string refTable = fNode.RefTable;
			string refField = fNode.RefField;
			bool unique = fNode.IsUnique;
			bool encrypted = fNode.IsEncrypted;
			string display = fNode.Display;
			bool excludefromtable = fNode.ExcludeFromTable;
			bool includewithparenttable = fNode.IncludeWithParentTable;
			bool includeinlist = fNode.IncludeInList;
			bool isdefaultsort = fNode.IsDefaultSort;

			a.ReadonlyFieldType	readonlytype = a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType(fNode.ReadonlyType);
      
			Sql.DatatypeInstance di = new DatatypeLoader(sqldatatype).Datatype;

			a.ClassFieldObject f = new a.ClassFieldObject(
				parent, name, identity, primarykey, refTable, refField, unique, encrypted, display,
				excludefromtable, includewithparenttable, includeinlist, isdefaultsort, readonlytype, di);
			
			parent.AddField(f);
		}

		private void loadAdditionalField(a.ClassObject parent, t.AddFieldNode fNode)
		{
			string name = fNode.Name;
			string sqldatatype = fNode.SqlDatatype;		
			string display = fNode.Display;
			bool excludefromtable = fNode.ExcludeFromTable;
			bool includewithparenttable = fNode.IncludeWithParentTable;
			string sql = fNode.Sql;
			string sortexpression = fNode.SortExpression;
			bool includeinlist = fNode.IncludeInList;
			bool isdefaultsort = fNode.IsDefaultSort;

			Sql.DatatypeInstance di = new DatatypeLoader(sqldatatype).Datatype;

			a.ClassAddFieldObject af = new a.ClassAddFieldObject(
				parent, name, sql, sortexpression, display, excludefromtable, includewithparenttable, includeinlist, isdefaultsort, di);
			parent.AddAdditionalField(af);
		}

		private void loadSproc(a.ClassObject parent, t.AddSprocNode spNode)
		{
			a.SprocObject sp = new a.SprocObject(parent, spNode.Name, spNode.ReturnType);
			parent.AddSproc(sp);

			foreach (t.ParamNode pNode in spNode.ParamNodesList)
				loadSprocParam(sp, pNode);

			foreach (t.ISprocReturnFieldNode rfNode in spNode.AllReturnFieldsList)
				loadSprocReturnField(sp, rfNode);
		}

		private void loadSprocParam(a.SprocObject parent, t.ParamNode pNode)
		{
			a.SprocParamObject sp = new a.SprocParamObject(parent, pNode.Name, pNode.CsDatatype, pNode.IsEncrypted);
			parent.AddParam(sp);
		}

		private void loadSprocReturnField(a.SprocObject parent, t.ISprocReturnFieldNode node)
		{			
			string name;
			string csDatatype;
			string sortExpression;
			string display;

			t.SprocReturnFieldNode fNode = node as t.SprocReturnFieldNode;
			if (fNode != null)
			{
				name = fNode.Name;
				a.ClassFieldObject classField = (a.ClassFieldObject)parent.Class.FieldsHash[name];
				sortExpression = classField.SortExpression;
				display = classField.Display;				
				csDatatype = new DatatypeHelper().GetCsDatatype(classField.Datatype.Name);
			}
			else
			{
				t.SprocCustomReturnFieldNode cfNode = node as t.SprocCustomReturnFieldNode;
				name = cfNode.Name;
				csDatatype = cfNode.CsDatatype;
				sortExpression = cfNode.SortExpression;
				display = cfNode.Display;								
			}
			a.SprocReturnFieldObject rField = 
				new a.SprocReturnFieldObject(parent, name, csDatatype, sortExpression, display);
			parent.AddReturnField(rField);
		}

		private void loadDependencies()
		{
			foreach (a.NamespaceObject ns in application.NamespacesList)
				foreach (a.ClassObject cs in ns.ClassesList)
					foreach (a.ClassFieldObject f in cs.FieldsList)
						f.LoadReference(application);
		}

		private a.ApplicationObject application;
	}
}
