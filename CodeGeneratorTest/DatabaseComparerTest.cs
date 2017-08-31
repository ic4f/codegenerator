using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class DatabaseComparerTest : BaseTest
	{
		[Test]
		public void TestTables()
		{
			ArrayList addTables1 = comparer1.AddTables;
			ArrayList deleteTables1 = comparer1.DeleteTables;	
			ArrayList retainTables1 = comparer1.RetainTables;
			Assert.AreEqual(0, addTables1.Count);
			Assert.AreEqual(0, deleteTables1.Count);
			Assert.AreEqual(4, retainTables1.Count);

			ArrayList addTables2 = comparer2.AddTables;
			ArrayList deleteTables2 = comparer2.DeleteTables;		
			ArrayList retainTables2 = comparer2.RetainTables;
			Assert.AreEqual(1, addTables2.Count);
			Assert.AreEqual(0, deleteTables2.Count);		
			Assert.AreEqual(4, retainTables2.Count);
			Assert.AreEqual("test_Record3", ((g.Sql.Table)addTables2[0]).Name);

			ArrayList addTables3 = comparer3.AddTables;
			ArrayList deleteTables3 = comparer3.DeleteTables;	
			ArrayList retainTables3 = comparer3.RetainTables;
			Assert.AreEqual(0, addTables3.Count);
			Assert.AreEqual(1, deleteTables3.Count);
			Assert.AreEqual(3, retainTables3.Count);
			Assert.AreEqual("test_Record1Record2", ((g.Sql.Table)deleteTables3[0]).Name);

			ArrayList addTables4 = comparer4.AddTables;
			ArrayList deleteTables4 = comparer4.DeleteTables;
			ArrayList retainTables4 = comparer4.RetainTables;
			Assert.AreEqual(1, addTables4.Count);
			Assert.AreEqual(1, deleteTables4.Count);
			Assert.AreEqual(3, retainTables4.Count);
			Assert.AreEqual("test_Record3", ((g.Sql.Table)addTables4[0]).Name);
			Assert.AreEqual("test_Record1Record2", ((g.Sql.Table)deleteTables4[0]).Name);
		}

		[Test]
		public void TestConstraints()
		{
			ArrayList addCons1 = comparer1.AddConstraints;
			ArrayList deleteCons1 = comparer1.DeleteConstraints;
			ArrayList updateCons1 = comparer1.UpdateConstraints;
			Assert.AreEqual(0, addCons1.Count);
			Assert.AreEqual(0, deleteCons1.Count);
			Assert.AreEqual(0, updateCons1.Count);

			ArrayList addCons2 = comparer2.AddConstraints;
			ArrayList deleteCons2 = comparer2.DeleteConstraints;
			ArrayList updateCons2 = comparer2.UpdateConstraints;
			Assert.AreEqual(1, addCons2.Count);
			Assert.AreEqual(2, deleteCons2.Count);
			Assert.AreEqual(1, updateCons2.Count);

			ArrayList addCons3 = comparer3.AddConstraints;
			ArrayList deleteCons3 = comparer3.DeleteConstraints;
			ArrayList updateCons3 = comparer3.UpdateConstraints;
			Assert.AreEqual(0, addCons3.Count);
			Assert.AreEqual(3, deleteCons3.Count);
			Assert.AreEqual(0, updateCons3.Count);

			ArrayList addCons4 = comparer4.AddConstraints;
			ArrayList deleteCons4 = comparer4.DeleteConstraints;
			ArrayList updateCons4 = comparer4.UpdateConstraints;
			Assert.AreEqual(1, addCons4.Count);
			Assert.AreEqual(3, deleteCons4.Count);
			Assert.AreEqual(0, updateCons4.Count);
		}

		[Test]
		public void TestFields()
		{
			ArrayList tables = comparer4.RetainTables;
			string t1 = ((g.Sql.Table)tables[0]).Name;
			string t2 = ((g.Sql.Table)tables[1]).Name;
			string t3 = ((g.Sql.Table)tables[2]).Name;

			ArrayList t1AddFields = comparer4.GetAddFields(t1);
			ArrayList t1DeleteFields = comparer4.GetDeleteFields(t1);
			ArrayList t1UpdateFields = comparer4.GetUpdateFields(t1);

			ArrayList t2AddFields = comparer4.GetAddFields(t2);
			ArrayList t2DeleteFields = comparer4.GetDeleteFields(t2);
			ArrayList t2UpdateFields = comparer4.GetUpdateFields(t2);

			ArrayList t3AddFields = comparer4.GetAddFields(t3);
			ArrayList t3DeleteFields = comparer4.GetDeleteFields(t3);
			ArrayList t3UpdateFields = comparer4.GetUpdateFields(t3);

			Assert.AreEqual(2, t1AddFields.Count);
			Assert.AreEqual(0, t2AddFields.Count);
			Assert.AreEqual(1, t3AddFields.Count);

			Assert.AreEqual(2, t1DeleteFields.Count);
			Assert.AreEqual(0, t2DeleteFields.Count);
			Assert.AreEqual(0, t3DeleteFields.Count);

			Assert.AreEqual(8, t1UpdateFields.Count);
			Assert.AreEqual(0, t2UpdateFields.Count);
			Assert.AreEqual(0, t3UpdateFields.Count);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatibleLength()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest5.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatiblePrecision()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest6.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatibleScale()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest7.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatibleIdentity()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest8.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatibleTypes1()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest9.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[Test]
		[ExpectedException(typeof(g.FieldCompatibilityException))]
		public void TestIncompatibleTypes2()
		{
			g.Parser parser =  new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest10.xml");
			t.ApplicationNode appNode = parser.ParseSchema();
			new g.SchemaValidator(appNode).Validate();
			g.Sql.Database schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
			g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
		}

		[SetUp]
		public void SetUp()
		{
			string setupFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvSetup.txt";
			string teardownFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvTearDown.txt";
			envBuilder = new Sql.DbEnvironmentBuilder(Connection, setupFile, teardownFile);
			envBuilder.Setup();

			g.SqlDatabaseLoader dbLoader = new CodeGenerator.SqlDatabaseLoader(Connection, true);

			string schemaFile1 = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest1.xml";
			string schemaFile2 = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest2.xml";
			string schemaFile3 = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest3.xml";
			string schemaFile4 = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest4.xml";

			g.Parser parser1 =  new g.Parser(schemaFile1);	
			g.Parser parser2 =  new g.Parser(schemaFile2);
			g.Parser parser3 =  new g.Parser(schemaFile3);
			g.Parser parser4 =  new g.Parser(schemaFile4);		

			t.ApplicationNode appNode1 = parser1.ParseSchema();
			t.ApplicationNode appNode2 = parser2.ParseSchema();
			t.ApplicationNode appNode3 = parser3.ParseSchema();
			t.ApplicationNode appNode4 = parser4.ParseSchema();

			new g.SchemaValidator(appNode1).Validate();
			new g.SchemaValidator(appNode2).Validate();
			new g.SchemaValidator(appNode3).Validate();
			new g.SchemaValidator(appNode4).Validate();

			g.Sql.Database schemaDatabase1 = new g.SchemaDatabaseLoader(appNode1).Database;
			g.Sql.Database schemaDatabase2 = new g.SchemaDatabaseLoader(appNode2).Database;
			g.Sql.Database schemaDatabase3 = new g.SchemaDatabaseLoader(appNode3).Database;
			g.Sql.Database schemaDatabase4 = new g.SchemaDatabaseLoader(appNode4).Database;

			sqlDatabase = dbLoader.Database;

			comparer1 = new g.DatabaseComparer(sqlDatabase, schemaDatabase1);
			comparer2 = new g.DatabaseComparer(sqlDatabase, schemaDatabase2);
			comparer3 = new g.DatabaseComparer(sqlDatabase, schemaDatabase3);
			comparer4 = new g.DatabaseComparer(sqlDatabase, schemaDatabase4);
		}

		[TearDown]
		public void TearDown() { envBuilder.TearDown(); }

		private Sql.DbEnvironmentBuilder envBuilder;
		private g.DatabaseComparer comparer1;	//same tables
		private g.DatabaseComparer comparer2;	//+1 table			
		private g.DatabaseComparer comparer3;	//-1 table
		private g.DatabaseComparer comparer4;	//+1-1 table  //change fields only here!
		private g.Sql.Database sqlDatabase;
	}
}
