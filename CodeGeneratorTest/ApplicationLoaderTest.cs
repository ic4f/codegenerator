using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class ApplicationLoaderTest : BaseTest
	{
		[SetUp]
		public void SetUp()
		{
			string schemaFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\applicationLoaderTest.xml";
			g.Parser parser = new g.Parser(schemaFile);		
			t.ApplicationNode appNode = parser.ParseSchema();
			new CodeGenerator.SchemaValidator(appNode).Validate();
			app = new g.ApplicationLoader(appNode).Application;		
		}

		[Test]
		public void TestNamespaces()
		{
			Assert.AreEqual(1, app.NamespacesList.Count);			
		}

		[Test]
		public void TestClasses()
		{
			a.NamespaceObject n = (a.NamespaceObject)app.NamespacesList[0];
			Assert.AreEqual(4, n.ClassesList.Count);			
			Assert.AreEqual(1, n.LinkClassesList.Count);
			Assert.AreEqual(3, n.NonLinkClassesList.Count);			
		}

		[Test]
		public void TestClass1()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual("Class1", c.Name);
			Assert.AreEqual(a.ClassType.Record, c.Type);
			Assert.IsFalse(c.IsExternal);
			Assert.AreEqual("Class1Table", c.TableName);
			Assert.AreEqual("Class1Table", c.TableSqlName);
			Assert.AreEqual(4, c.FieldsList.Count);
			Assert.AreEqual(1, c.AddFieldsList.Count);			
			Assert.AreEqual(0, c.IncludeWithParentIDataFieldList.Count);
			Assert.AreEqual(0, c.UniqueFieldList.Count);
			Assert.AreEqual(0, c.StandardReturnTypeSprocsList.Count);
			Assert.AreEqual(0, c.CustomReturnTypeSprocsList.Count);
		}

		[Test]
		public void TestClass1PrimaryKeys()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual("Id", c.PrimaryKeyField1.Name);
			Assert.IsNull(c.PrimaryKeyField2);
		}

		[Test]
		public void TestClass1ForeignKeys()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual(2, c.ForeignKeyFieldList.Count);
			a.ClassFieldObject fkey1 = (a.ClassFieldObject)c.ForeignKeyFieldList[0];
			a.ClassFieldObject fkey2 = (a.ClassFieldObject)c.ForeignKeyFieldList[1];
			Assert.AreEqual("Class2Id", fkey1.Name);
			Assert.AreEqual("Class3Id", fkey2.Name);
			Assert.AreSame(getClass("Class2"), fkey1.ReferencedClass);
			Assert.AreSame(getClass("Class3"), fkey2.ReferencedClass);
			Assert.AreSame(getClass("Class2").FieldsHash[fkey1.ReferencedField.Name], fkey1.ReferencedField);
			Assert.AreSame(getClass("Class3").FieldsHash[fkey2.ReferencedField.Name], fkey2.ReferencedField);
		}

		[Test]
		public void TestClass1InstanceFields()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual(7, c.InstanceIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.InstanceIDataFieldList[0]).Name);
			Assert.AreEqual("Class2Id", ((a.BaseClassField)c.InstanceIDataFieldList[1]).Name);
			Assert.AreEqual("Class3Id", ((a.BaseClassField)c.InstanceIDataFieldList[2]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.InstanceIDataFieldList[3]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.InstanceIDataFieldList[4]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.InstanceIDataFieldList[5]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.InstanceIDataFieldList[6]).Name);	
		}

		[Test]
		public void TestClass1SetFields()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual(6, c.SetIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.SetIDataFieldList[0]).Name);
			Assert.AreEqual("Class2Id", ((a.BaseClassField)c.SetIDataFieldList[1]).Name);
			Assert.AreEqual("Class3Id", ((a.BaseClassField)c.SetIDataFieldList[2]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.SetIDataFieldList[3]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.SetIDataFieldList[4]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.SetIDataFieldList[5]).Name);	
		}

		[Test]
		public void TestClass1ReferringClasses()
		{
			a.ClassObject c = getClass("Class1");
			Assert.AreEqual(0, c.ReferringClassesHash.Count);
			Assert.AreEqual(0, c.ReferringLinkClassesHash.Count);
			Assert.AreEqual(0, c.ReferringNonLinkClassesHash.Count);
		}

		[Test]
		public void TestClass1GetForeignKeyFieldForClass()
		{
			a.ClassObject c = getClass("Class1");
			a.ClassFieldObject fkey1 = (a.ClassFieldObject)c.ForeignKeyFieldList[0];
			a.ClassFieldObject fkey2 = (a.ClassFieldObject)c.ForeignKeyFieldList[1];

			Assert.AreEqual(fkey1, c.GetForeignKeyFieldForClass(getClass("Class2")));
			Assert.AreEqual(fkey2, c.GetForeignKeyFieldForClass(getClass("Class3")));
		}

		[Test]
		public void TestClass2()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual("Class2", c.Name);
			Assert.AreEqual(a.ClassType.Record, c.Type);
			Assert.IsFalse(c.IsExternal);
			Assert.AreEqual("Class2Table", c.TableName);
			Assert.AreEqual("Class2Table", c.TableSqlName);
			Assert.AreEqual(3, c.FieldsList.Count);
			Assert.AreEqual(0, c.AddFieldsList.Count);			
			Assert.AreEqual(1, c.IncludeWithParentIDataFieldList.Count);
			Assert.AreEqual(0, c.UniqueFieldList.Count);
			Assert.AreEqual(0, c.StandardReturnTypeSprocsList.Count);
			Assert.AreEqual(0, c.CustomReturnTypeSprocsList.Count);
		}

		[Test]
		public void TestClass2PrimaryKeys()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual("Id", c.PrimaryKeyField1.Name);
			Assert.IsNull(c.PrimaryKeyField2);
		}

		[Test]
		public void TestClass2ForeignKeys()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual(0, c.ForeignKeyFieldList.Count);
		}

		[Test]
		public void TestClass2InstanceFields()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual(3, c.InstanceIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.InstanceIDataFieldList[0]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.InstanceIDataFieldList[1]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.InstanceIDataFieldList[2]).Name);
		}

		[Test]
		public void TestClass2SetFields()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual(3, c.SetIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.SetIDataFieldList[0]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.SetIDataFieldList[1]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.SetIDataFieldList[2]).Name);
		}

		[Test]
		public void TestClass2ReferringClasses()
		{
			a.ClassObject c = getClass("Class2");
			Assert.AreEqual(2, c.ReferringClassesHash.Count);
			Assert.AreEqual(1, c.ReferringLinkClassesHash.Count);
			Assert.AreEqual(1, c.ReferringNonLinkClassesHash.Count);
		}

		[Test]
		public void TestClass3()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual("Class3", c.Name);
			Assert.AreEqual(a.ClassType.Record, c.Type);
			Assert.IsFalse(c.IsExternal);
			Assert.AreEqual("Class3Table", c.TableName);
			Assert.AreEqual("Class3Table", c.TableSqlName);
			Assert.AreEqual(3, c.FieldsList.Count);
			Assert.AreEqual(0, c.AddFieldsList.Count);			
			Assert.AreEqual(1, c.IncludeWithParentIDataFieldList.Count);
			Assert.AreEqual(0, c.UniqueFieldList.Count);
			Assert.AreEqual(0, c.StandardReturnTypeSprocsList.Count);
			Assert.AreEqual(0, c.CustomReturnTypeSprocsList.Count);
		}

		[Test]
		public void TestClass3PrimaryKeys()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual("Id", c.PrimaryKeyField1.Name);
			Assert.IsNull(c.PrimaryKeyField2);
		}

		[Test]
		public void TestClass3ForeignKeys()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual(0, c.ForeignKeyFieldList.Count);
		}

		[Test]
		public void TestClass3InstanceFields()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual(3, c.InstanceIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.InstanceIDataFieldList[0]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.InstanceIDataFieldList[1]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.InstanceIDataFieldList[2]).Name);
		}

		[Test]
		public void TestClass3SetFields()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual(3, c.SetIDataFieldList.Count);
			Assert.AreEqual("Id", ((a.BaseClassField)c.SetIDataFieldList[0]).Name);
			Assert.AreEqual("A", ((a.BaseClassField)c.SetIDataFieldList[1]).Name);
			Assert.AreEqual("B", ((a.BaseClassField)c.SetIDataFieldList[2]).Name);
		}

		[Test]
		public void TestClass3ReferringClasses()
		{
			a.ClassObject c = getClass("Class3");
			Assert.AreEqual(2, c.ReferringClassesHash.Count);
			Assert.AreEqual(1, c.ReferringLinkClassesHash.Count);
			Assert.AreEqual(1, c.ReferringNonLinkClassesHash.Count);
		}

		[Test]
		public void TestClass2Class3Link()
		{
			a.ClassObject c = getClass("Class2Class3Link");
			Assert.AreEqual("Class2Class3Link", c.Name);
			Assert.AreEqual(a.ClassType.Link, c.Type);
			Assert.IsFalse(c.IsExternal);
			Assert.AreEqual("Class2Class3LinkTable", c.TableName);
			Assert.AreEqual("Class2Class3LinkTable", c.TableSqlName);
			Assert.AreEqual(2, c.FieldsList.Count);
			Assert.AreEqual(0, c.AddFieldsList.Count);			
			Assert.AreEqual(0, c.IncludeWithParentIDataFieldList.Count);
			Assert.AreEqual(0, c.UniqueFieldList.Count);
			Assert.AreEqual(0, c.StandardReturnTypeSprocsList.Count);
			Assert.AreEqual(0, c.CustomReturnTypeSprocsList.Count);
		}

		[Test]
		public void TestClass2Class3LinkPrimaryKeys()
		{
			a.ClassObject c = getClass("Class2Class3Link");
			Assert.AreEqual("Class2Id", c.PrimaryKeyField1.Name);
			Assert.AreEqual("Class3Id", c.PrimaryKeyField2.Name);
		}

		[Test]
		public void TestClass2Class3LinkForeignKeys()
		{
			a.ClassObject c = getClass("Class2Class3Link");
			Assert.AreEqual(2, c.ForeignKeyFieldList.Count);
			a.ClassFieldObject fkey1 = (a.ClassFieldObject)c.ForeignKeyFieldList[0];
			a.ClassFieldObject fkey2 = (a.ClassFieldObject)c.ForeignKeyFieldList[1];
			Assert.AreEqual("Class2Id", fkey1.Name);
			Assert.AreEqual("Class3Id", fkey2.Name);
			Assert.AreSame(getClass("Class2"), fkey1.ReferencedClass);
			Assert.AreSame(getClass("Class3"), fkey2.ReferencedClass);
			Assert.AreSame(getClass("Class2").FieldsHash[fkey1.ReferencedField.Name], fkey1.ReferencedField);
			Assert.AreSame(getClass("Class3").FieldsHash[fkey2.ReferencedField.Name], fkey2.ReferencedField);
		}

		private a.ClassObject getClass(string name)
		{
			a.NamespaceObject n = (a.NamespaceObject)app.NamespacesList[0];
			return (a.ClassObject)n.ClassesHash[name];
		}

		private a.ApplicationObject app;
	}
}
				