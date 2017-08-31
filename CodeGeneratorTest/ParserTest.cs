using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class ParserTest
	{
		[SetUp]
		public void SetUp()
		{
			string schemaFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\parserTest1.xml";
			parser = new g.Parser(schemaFile);			
		}

		[Test]
		public void TestApplication()
		{
			t.ApplicationNode appNode = parser.ParseSchema();

			Assert.AreEqual(2, appNode.NamespaceNodesList.Count);
			Assert.AreEqual(2, appNode.NamespaceNodesHash.Count);
			Assert.IsTrue(appNode.NamespaceNodesHash.Contains("Namespace1"));
			Assert.IsTrue(appNode.NamespaceNodesHash.Contains("Namespace2"));			
		}

		[Test]
		public void TestNamespace1()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];

			Assert.AreEqual("Namespace1", nsNode.Name);
			Assert.AreEqual(2, nsNode.ClassNodesList.Count);
			Assert.AreEqual(2, nsNode.ClassNodesHash.Count);
			Assert.IsTrue(nsNode.ClassNodesHash.Contains("Class1"));
			Assert.IsTrue(nsNode.ClassNodesHash.Contains("Class2"));			
		}

		[Test]
		public void TestNamespace2()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace2"];

			Assert.AreEqual("Namespace2", nsNode.Name);
			Assert.AreEqual(2, nsNode.ClassNodesList.Count);
			Assert.AreEqual(2, nsNode.ClassNodesHash.Count);
			Assert.IsTrue(nsNode.ClassNodesHash.Contains("Class3"));		
			Assert.IsTrue(nsNode.ClassNodesHash.Contains("Class4"));	
		}

		[Test]
		public void TestClass1()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class1"];

			Assert.AreEqual("Class1", csNode.Name);
			Assert.AreEqual("record", csNode.Type);
			Assert.AreEqual("Table1", csNode.Table.Name);		
			Assert.AreEqual(0, csNode.AddSprocNodesHash.Count);
			Assert.AreEqual(0, csNode.AddSprocNodesList.Count);
		}

		[Test]
		public void TestClass2()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];

			Assert.AreEqual("Class2", csNode.Name);
			Assert.AreEqual("record", csNode.Type);
			Assert.AreEqual("Table2", csNode.Table.Name);		
			Assert.AreEqual(5, csNode.AddSprocNodesHash.Count);
			Assert.AreEqual(5, csNode.AddSprocNodesList.Count);
			Assert.IsTrue(csNode.AddSprocNodesHash.Contains("Sproc1"));
			Assert.IsTrue(csNode.AddSprocNodesHash.Contains("Sproc2"));
			Assert.IsTrue(csNode.AddSprocNodesHash.Contains("Sproc3"));
			Assert.IsTrue(csNode.AddSprocNodesHash.Contains("Sproc4"));
			Assert.IsTrue(csNode.AddSprocNodesHash.Contains("Sproc5"));
		}

		[Test]
		public void TestClass3()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace2"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class3"];

			Assert.AreEqual("Class3", csNode.Name);
			Assert.AreEqual("record", csNode.Type);
			Assert.AreEqual("Table3", csNode.Table.Name);		
			Assert.AreEqual(0, csNode.AddSprocNodesHash.Count);
			Assert.AreEqual(0, csNode.AddSprocNodesList.Count);
		}

		[Test]
		public void TestTable1()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class1"];
			t.TableNode tblNode = csNode.Table;

			Assert.AreEqual("Table1", tblNode.Name);
			Assert.IsTrue(tblNode.IsExternal);
			Assert.AreEqual(10, tblNode.FieldNodesHash.Count);
			Assert.AreEqual(10, tblNode.FieldNodesList.Count);
			Assert.AreEqual(0, tblNode.AddFieldNodesHash.Count);
			Assert.AreEqual(0, tblNode.AddFieldNodesList.Count);
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field1"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field2"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field3"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field4"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field5"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field6"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field7"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field8"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field9"));
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field10"));
		}

		[Test]
		public void TestTable2()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.TableNode tblNode = csNode.Table;

			Assert.AreEqual("Table2", tblNode.Name);
			Assert.IsFalse(tblNode.IsExternal);
			Assert.AreEqual(1, tblNode.FieldNodesHash.Count);
			Assert.AreEqual(1, tblNode.FieldNodesList.Count);
			Assert.AreEqual(4, tblNode.AddFieldNodesHash.Count);
			Assert.AreEqual(4, tblNode.AddFieldNodesList.Count);
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field1"));
			Assert.IsTrue(tblNode.AddFieldNodesHash.Contains("Field2"));
			Assert.IsTrue(tblNode.AddFieldNodesHash.Contains("Field3"));
			Assert.IsTrue(tblNode.AddFieldNodesHash.Contains("Field4"));
			Assert.IsTrue(tblNode.AddFieldNodesHash.Contains("Field5"));
		}

		[Test]
		public void TestTable3()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace2"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class3"];
			t.TableNode tblNode = csNode.Table;

			Assert.AreEqual("Table3", tblNode.Name);
			Assert.IsFalse(tblNode.IsExternal);
			Assert.AreEqual(1, tblNode.FieldNodesHash.Count);
			Assert.AreEqual(1, tblNode.FieldNodesList.Count);
			Assert.AreEqual(0, tblNode.AddFieldNodesHash.Count);
			Assert.AreEqual(0, tblNode.AddFieldNodesList.Count);
			Assert.IsTrue(tblNode.FieldNodesHash.Contains("Field1"));
		}

		[Test]
		public void TestTable1Fields()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class1"];
			t.TableNode tblNode = csNode.Table;
			t.FieldNode fldNode1 = (t.FieldNode)tblNode.FieldNodesHash["Field1"];
			t.FieldNode fldNode2 = (t.FieldNode)tblNode.FieldNodesHash["Field2"];
			t.FieldNode fldNode3 = (t.FieldNode)tblNode.FieldNodesHash["Field3"];
			t.FieldNode fldNode4 = (t.FieldNode)tblNode.FieldNodesHash["Field4"];
			t.FieldNode fldNode5 = (t.FieldNode)tblNode.FieldNodesHash["Field5"];
			t.FieldNode fldNode6 = (t.FieldNode)tblNode.FieldNodesHash["Field6"];
			t.FieldNode fldNode7 = (t.FieldNode)tblNode.FieldNodesHash["Field7"];
			t.FieldNode fldNode8 = (t.FieldNode)tblNode.FieldNodesHash["Field8"];
			t.FieldNode fldNode9 = (t.FieldNode)tblNode.FieldNodesHash["Field9"];
			t.FieldNode fldNode10 = (t.FieldNode)tblNode.FieldNodesHash["Field10"];

			runFieldTest(fldNode1, "Field1", "int", false, false, null, null, false, false, null, false, false, null);
			runFieldTest(fldNode2, "Field2", "int", true, false, null, null, false, false, null, false, false, null);
			runFieldTest(fldNode3, "Field3", "int", false, true, null, null, false, false, null, false, false, null);
			runFieldTest(fldNode4, "Field4", "int", false, false, "Table2", "Field1", false, false, null, false, false, null);
			runFieldTest(fldNode5, "Field5", "int", false, false, null, null, true, false, null, false, false, null);
			runFieldTest(fldNode6, "Field6", "varbinary(8)", false, false, null, null, false, true, null, false, false, null);
			runFieldTest(fldNode7, "Field7", "int", false, false, null, null, false, false, "field 7", false, false, null);
			runFieldTest(fldNode8, "Field8", "int", false, false, null, null, false, false, null, true, false, null);
			runFieldTest(fldNode9, "Field9", "int", false, false, null, null, false, false, null, false, true, null);
			runFieldTest(fldNode10, "Field10", "datetime", false, false, null, null, false, false, null, false, false, "created");
		}

		[Test]
		public void TestTable2Fields()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.TableNode tblNode = csNode.Table;
			t.FieldNode fldNode1 = (t.FieldNode)tblNode.FieldNodesHash["Field1"];

			runFieldTest(fldNode1, "Field1", "int", false, true, null, null, false, false, null, false, false, null);
		}

		[Test]
		public void TestTable3Fields()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace2"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class3"];
			t.TableNode tblNode = csNode.Table;
			t.FieldNode fldNode1 = (t.FieldNode)tblNode.FieldNodesHash["Field1"];

			runFieldTest(fldNode1, "Field1", "int", false, false, null, null, false, false, null, false, false, null);
		}

		[Test]
		public void TestTable2AddFields()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.TableNode tblNode = csNode.Table;
			t.AddFieldNode afldNode1 = (t.AddFieldNode)tblNode.AddFieldNodesHash["Field2"];
			t.AddFieldNode afldNode2 = (t.AddFieldNode)tblNode.AddFieldNodesHash["Field3"];
			t.AddFieldNode afldNode3 = (t.AddFieldNode)tblNode.AddFieldNodesHash["Field4"];
			t.AddFieldNode afldNode4 = (t.AddFieldNode)tblNode.AddFieldNodesHash["Field5"];

			runAdditionalFieldTest(afldNode1, "Field2", "int", "some sql 1", "field2 asc", null, false, false);
			runAdditionalFieldTest(afldNode2, "Field3", "int", "some sql 2", "field3 asc", "field 3", false, false);
			runAdditionalFieldTest(afldNode3, "Field4", "int", "some sql 3", "field4 asc", null, true, false);
			runAdditionalFieldTest(afldNode4, "Field5", "int", "some sql 4", "field5 asc", null, false, true);
		}

		[Test]
		public void TestClass2AdditionalSproc1()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.AddSprocNode sprocNode  = (t.AddSprocNode)csNode.AddSprocNodesHash["Sproc1"];

			Assert.AreEqual("Sproc1", sprocNode.Name);
			Assert.AreEqual("int", sprocNode.ReturnType);
			Assert.AreEqual(0, sprocNode.ParamNodesList.Count);
			Assert.AreEqual(0, sprocNode.ParamNodesHash.Count);
			Assert.AreEqual(0, sprocNode.ReturnFieldNodesHash.Count);
			Assert.AreEqual(0, sprocNode.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(0, sprocNode.AllReturnFieldsList.Count);
		}

		[Test]
		public void TestClass2AdditionalSproc2()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.AddSprocNode sprocNode  = (t.AddSprocNode)csNode.AddSprocNodesHash["Sproc2"];
			t.ParamNode paramNode1 = (t.ParamNode)sprocNode.ParamNodesHash["p1"];
			t.ParamNode paramNode2 = (t.ParamNode)sprocNode.ParamNodesHash["p2"];

			Assert.AreEqual("Sproc2", sprocNode.Name);
			Assert.AreEqual("void", sprocNode.ReturnType);
			Assert.AreEqual(2, sprocNode.ParamNodesList.Count);
			Assert.AreEqual(2, sprocNode.ParamNodesHash.Count);
			Assert.AreEqual(0, sprocNode.ReturnFieldNodesHash.Count);
			Assert.AreEqual(0, sprocNode.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(0, sprocNode.AllReturnFieldsList.Count);

			Assert.AreEqual("p1", paramNode1.Name);
			Assert.AreEqual("string", paramNode1.CsDatatype);
			Assert.IsFalse(paramNode1.IsEncrypted);

			Assert.AreEqual("p2", paramNode2.Name);
			Assert.AreEqual("string", paramNode2.CsDatatype);
			Assert.IsTrue(paramNode2.IsEncrypted);
		}

		[Test]
		public void TestClass2AdditionalSproc3()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.AddSprocNode sprocNode  = (t.AddSprocNode)csNode.AddSprocNodesHash["Sproc3"];
			t.SprocReturnFieldNode rfieldNode1 = (t.SprocReturnFieldNode)sprocNode.ReturnFieldNodesHash["Field1"];

			Assert.AreEqual("Sproc3", sprocNode.Name);
			Assert.AreEqual("generate", sprocNode.ReturnType);
			Assert.AreEqual(0, sprocNode.ParamNodesList.Count);
			Assert.AreEqual(0, sprocNode.ParamNodesHash.Count);
			Assert.AreEqual(1, sprocNode.ReturnFieldNodesHash.Count);
			Assert.AreEqual(0, sprocNode.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(1, sprocNode.AllReturnFieldsList.Count);

			Assert.AreEqual("Field1", rfieldNode1.Name);
		}

		[Test]
		public void TestClass2AdditionalSproc4()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.AddSprocNode sprocNode  = (t.AddSprocNode)csNode.AddSprocNodesHash["Sproc4"];
			t.SprocReturnFieldNode rfieldNode = (t.SprocReturnFieldNode)sprocNode.ReturnFieldNodesHash["Field1"];
			t.SprocCustomReturnFieldNode arfieldNode = (t.SprocCustomReturnFieldNode)sprocNode.CustomReturnFieldNodesHash["Addfield1"];

			Assert.AreEqual("Sproc4", sprocNode.Name);
			Assert.AreEqual("generate", sprocNode.ReturnType);
			Assert.AreEqual(0, sprocNode.ParamNodesList.Count);
			Assert.AreEqual(0, sprocNode.ParamNodesHash.Count);
			Assert.AreEqual(1, sprocNode.ReturnFieldNodesHash.Count);
			Assert.AreEqual(1, sprocNode.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(2, sprocNode.AllReturnFieldsList.Count);

			Assert.AreEqual("Field1", rfieldNode.Name);
			Assert.AreEqual("Addfield1", arfieldNode.Name);
			Assert.AreEqual("int", arfieldNode.CsDatatype);
			Assert.AreEqual("field1", arfieldNode.SortExpression);
			Assert.AreEqual(null, arfieldNode.Display);

			//order of return fields matters!	
			Assert.AreEqual(arfieldNode, sprocNode.AllReturnFieldsList[0]);
			Assert.AreEqual(rfieldNode, sprocNode.AllReturnFieldsList[1]);
		}

		[Test]
		public void TestClass2AdditionalSproc5()
		{
			t.ApplicationNode appNode = parser.ParseSchema();
			t.NamespaceNode nsNode = (t.NamespaceNode)appNode.NamespaceNodesHash["Namespace1"];
			t.ClassNode csNode = (t.ClassNode)nsNode.ClassNodesHash["Class2"];
			t.AddSprocNode sprocNode  = (t.AddSprocNode)csNode.AddSprocNodesHash["Sproc5"];
			t.SprocCustomReturnFieldNode arfieldNode1 = (t.SprocCustomReturnFieldNode)sprocNode.CustomReturnFieldNodesHash["Addfield1"];
			t.SprocCustomReturnFieldNode arfieldNode2 = (t.SprocCustomReturnFieldNode)sprocNode.CustomReturnFieldNodesHash["Addfield2"];

			Assert.AreEqual("Sproc5", sprocNode.Name);
			Assert.AreEqual("generate", sprocNode.ReturnType);
			Assert.AreEqual(0, sprocNode.ParamNodesList.Count);
			Assert.AreEqual(0, sprocNode.ParamNodesHash.Count);
			Assert.AreEqual(0, sprocNode.ReturnFieldNodesHash.Count);
			Assert.AreEqual(2, sprocNode.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(2, sprocNode.AllReturnFieldsList.Count);

			Assert.AreEqual("Addfield1", arfieldNode1.Name);
			Assert.AreEqual("string", arfieldNode1.CsDatatype);
			Assert.AreEqual("field1", arfieldNode1.SortExpression);
			Assert.AreEqual("field 1", arfieldNode1.Display);
			Assert.AreEqual("Addfield2", arfieldNode2.Name);
			Assert.AreEqual("string", arfieldNode2.CsDatatype);
			Assert.AreEqual("field2", arfieldNode2.SortExpression);
			Assert.AreEqual("field 2", arfieldNode2.Display);
		}

		private void runFieldTest(
			t.FieldNode fldNode,
			string name,
			string sqldatatype,
			bool identity,
			bool primarykey,
			string refTable,
			string refField,
			bool unique,
			bool encrypted,
			string display,
			bool excludefromtable,
			bool includewithparenttable,
			string readonlytype)
		{
			Assert.AreEqual(name, fldNode.Name);
			Assert.AreEqual(sqldatatype, fldNode.SqlDatatype);
			Assert.AreEqual(identity, fldNode.IsIdentity);
			Assert.AreEqual(primarykey, fldNode.IsPrimaryKey);
			Assert.AreEqual(refTable, fldNode.RefTable);
			Assert.AreEqual(refField, fldNode.RefField);
			Assert.AreEqual(unique, fldNode.IsUnique);
			Assert.AreEqual(encrypted, fldNode.IsEncrypted);
			Assert.AreEqual(display, fldNode.Display);
			Assert.AreEqual(excludefromtable, fldNode.ExcludeFromTable);
			Assert.AreEqual(includewithparenttable, fldNode.IncludeWithParentTable);
			Assert.AreEqual(readonlytype, fldNode.ReadonlyType);
		}

		private void runAdditionalFieldTest(
			t.AddFieldNode afldNode,
			string name,
			string sqldatatype,
			string sql,
			string sortexpression,
			string display,
			bool excludefromtable,
			bool includewithparenttable)
		{
			Assert.AreEqual(name, afldNode.Name);
			Assert.AreEqual(sqldatatype, afldNode.SqlDatatype);
			Assert.AreEqual(sql, afldNode.Sql);
			Assert.AreEqual(sortexpression, afldNode.SortExpression);
			Assert.AreEqual(display, afldNode.Display);
			Assert.AreEqual(excludefromtable, afldNode.ExcludeFromTable);
			Assert.AreEqual(includewithparenttable, afldNode.IncludeWithParentTable);
		}

		private g.Parser parser;
	}
}		

