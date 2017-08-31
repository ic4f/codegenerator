using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
	[TestFixture]
	public class ClassObjectTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c);

			Assert.AreSame(ns, c.Namespace);
			Assert.AreEqual("a", c.Name);
			Assert.AreEqual(a.ClassType.Record, c.Type);
			Assert.IsFalse(c.IsExternal);
			Assert.AreEqual("tbl", c.TableName);
			Assert.AreEqual("[tbl]", c.TableSqlName);				
		}

		[Test]
		public void TestFields()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c);
			g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
			a.ClassFieldObject f1 = new a.ClassFieldObject(
				c, "a", false, false, "a", "c", true, false, "d", true, true, true, true, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f2 = new a.ClassFieldObject(
				c, "b", false, false, null, null, false, false, "d", true, true, false, false, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f3 = new a.ClassFieldObject(
				c, "c", false, false, "b", "c", true, false, "d", true, true, true, false, a.ReadonlyFieldType.Created, di);
			c.AddField(f1);
			c.AddField(f2);
			c.AddField(f3);

			//test fields
			Assert.AreEqual(3, c.FieldsHash.Count);
			Assert.AreEqual(3, c.FieldsList.Count);
			Assert.AreSame(f1, c.FieldsHash["a"]);
			Assert.AreSame(f2, c.FieldsHash["b"]);
			Assert.AreSame(f3, c.FieldsHash["c"]);
			Assert.AreSame(f1, c.FieldsList[0]);
			Assert.AreSame(f2, c.FieldsList[1]);
			Assert.AreSame(f3, c.FieldsList[2]);

			//test unique fields
			Assert.AreEqual(2, c.UniqueFieldHash.Count);
			Assert.AreEqual(2, c.UniqueFieldList.Count);
			Assert.AreSame(f1, c.UniqueFieldHash["a"]);
			Assert.AreSame(f3, c.UniqueFieldHash["c"]);
			Assert.AreSame(f1, c.UniqueFieldList[0]);
			Assert.AreSame(f3, c.UniqueFieldList[1]);

			//test fkey fields
			Assert.AreEqual(2, c.ForeignKeyFieldHash.Count);
			Assert.AreEqual(2, c.ForeignKeyFieldList.Count);
			Assert.AreSame(f1, c.ForeignKeyFieldHash["a"]);
			Assert.AreSame(f3, c.ForeignKeyFieldHash["c"]);
			Assert.AreSame(f1, c.ForeignKeyFieldList[0]);
			Assert.AreSame(f3, c.ForeignKeyFieldList[1]);

			//test default sort field
			Assert.AreSame(f1, c.FieldsList[0]);

			//test include in list
			Assert.AreEqual(2, c.IncludeWithListList.Count);
			Assert.AreEqual(2, c.IncludeWithListHash.Count);
			Assert.AreSame(f1, c.IncludeWithListHash["a"]);
			Assert.AreSame(f3, c.IncludeWithListHash["c"]);
			Assert.AreSame(f1, c.IncludeWithListList[0]);
			Assert.AreSame(f3, c.IncludeWithListList[1]);
		}

		public void TestAddFields()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c);
			g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
			a.ClassAddFieldObject f1 = new a.ClassAddFieldObject(c, "a", "b", "c", "d", false, false, true, true, di);
			a.ClassAddFieldObject f2 = new a.ClassAddFieldObject(c, "a", "b", "c", "d", false, false, true, false, di);
			a.ClassAddFieldObject f3 = new a.ClassAddFieldObject(c, "a", "b", "c", "d", false, false, true, false, di);
			c.AddAdditionalField(f1);
			c.AddAdditionalField(f2);
			c.AddAdditionalField(f3);

			Assert.AreEqual(3, c.AddFieldsHash.Count);
			Assert.AreEqual(3, c.AddFieldsList.Count);
			Assert.AreSame(f1, c.AddFieldsHash["a"]);
			Assert.AreSame(f2, c.AddFieldsHash["b"]);
			Assert.AreSame(f3, c.AddFieldsHash["c"]);
			Assert.AreSame(f1, c.AddFieldsList[0]);
			Assert.AreSame(f2, c.AddFieldsList[1]);
			Assert.AreSame(f3, c.AddFieldsList[2]);
		}

		[Test]
		public void TestSprocs()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c);

			a.SprocObject s1 = new CodeGenerator.Application.SprocObject(c, "a", "int");
			a.SprocObject s2 = new CodeGenerator.Application.SprocObject(c, "b", "generate");
			a.SprocObject s3 = new CodeGenerator.Application.SprocObject(c, "c", "int");
			c.AddSproc(s1);
			c.AddSproc(s2);
			c.AddSproc(s3);

			Assert.AreEqual(2, c.StandardReturnTypeSprocsHash.Count);
			Assert.AreEqual(2, c.StandardReturnTypeSprocsList.Count);
			Assert.AreEqual(1, c.CustomReturnTypeSprocsHash.Count);
			Assert.AreEqual(1, c.CustomReturnTypeSprocsList.Count);
			Assert.AreSame(s1, (a.SprocObject)c.StandardReturnTypeSprocsHash["a"]);
			Assert.AreSame(s3, (a.SprocObject)c.StandardReturnTypeSprocsHash["c"]);
			Assert.AreSame(s1, (a.SprocObject)c.StandardReturnTypeSprocsList[0]);
			Assert.AreSame(s3, (a.SprocObject)c.StandardReturnTypeSprocsList[1]);
			Assert.AreSame(s2, (a.SprocObject)c.CustomReturnTypeSprocsHash["b"]);
			Assert.AreSame(s2, (a.SprocObject)c.CustomReturnTypeSprocsList[0]);
		}

		[Test]
		public void TestPrimaryKeys()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c1 = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c1);
			g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
			a.ClassFieldObject f1 = new a.ClassFieldObject(
				c1, "a", true, true, "a", "c", true, false, "d", true, true, true, true, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f2 = new a.ClassFieldObject(
				c1, "b", false, false, null, null, false, false, "d", true, true, true, false, a.ReadonlyFieldType.Created, di);
			c1.AddField(f1);
			c1.AddField(f2);

			a.ClassObject c2 = new a.ClassObject(ns, "b", a.ClassType.Link, false, "tbl", "[tbl]");
			ns.AddClass(c2);
			a.ClassFieldObject f3 = new a.ClassFieldObject(
				c2, "a", true, true, "a", "c", true, false, "d", true, true, true, true, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f4 = new a.ClassFieldObject(
				c2, "b", true, true, null, null, false, false, "d", true, true, true, false, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f5 = new a.ClassFieldObject(
				c2, "c", false, false, null, null, false, false, "d", true, true, true, false, a.ReadonlyFieldType.Created, di);
			c2.AddField(f3);
			c2.AddField(f4);
			c2.AddField(f5);

			Assert.AreSame(f1, c1.PrimaryKeyField1);
			Assert.IsNull(c1.PrimaryKeyField2);
			Assert.AreSame(f3, c2.PrimaryKeyField1);
			Assert.AreSame(f4, c2.PrimaryKeyField2);
		}

		public void TestIncludeWithParentFields()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "tbl", "[tbl]");
			ns.AddClass(c);
			g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
			a.ClassAddFieldObject f1 = new a.ClassAddFieldObject(c, "a", "b", "c", "d", false, true, true, false, di);
			a.ClassAddFieldObject f2 = new a.ClassAddFieldObject(c, "a", "b", "c", "d", false, false, true, false, di);
			a.ClassFieldObject f3 = new a.ClassFieldObject(
				c, "a", false, false, "a", "c", true, false, "d", false, true, true, true, a.ReadonlyFieldType.Created, di);
			a.ClassFieldObject f4 = new a.ClassFieldObject(
				c, "b", false, false, null, null, false, false, "d", false, false, true, true, a.ReadonlyFieldType.Created, di);

			c.AddAdditionalField(f1);
			c.AddAdditionalField(f2);
			c.AddField(f3);
			c.AddField(f4);

			Assert.AreEqual(2, c.IncludeWithParentIDataFieldHash.Count);
			Assert.AreEqual(2, c.IncludeWithParentIDataFieldList.Count);
			Assert.AreSame(f1, c.IncludeWithParentIDataFieldHash["a"]);
			Assert.AreSame(f3, c.IncludeWithParentIDataFieldHash["b"]);
			Assert.AreSame(f1, c.IncludeWithParentIDataFieldList[0]);
			Assert.AreSame(f3, c.IncludeWithParentIDataFieldList[1]);
		}


//The following relationships are tested in ApplicationLoaderTest:
//		public ArrayList InstanceIDataFieldList 
//		public Hashtable InstanceIDataFieldHash
//		public ArrayList SetIDataFieldList
//		public Hashtable SetIDataFieldHash
//		public Hashtable ReferringClassesHash { get { return referringClassesHash; } }
//		public Hashtable ReferringLinkClassesHash { get { return referringLinkClassesHash; } }
//		public Hashtable ReferringNonLinkClassesHash { get { return referringNonLinkClassesHash; } }
//		public ClassFieldObject GetForeignKeyFieldForClass(ClassObject refClass)

	}
}