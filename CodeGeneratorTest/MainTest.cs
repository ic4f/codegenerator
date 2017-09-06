using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
    [TestFixture]
    public class MainTest : BaseTest
    {
        /* 
		* 1. load default db
		* 2. make lots of updates to the db
		* 3. load and check the new values			
		*/
        [Test]
        public void TestUpdateTablesConstraints()
        {
            updateDatabase();

            g.SqlDatabaseLoader sqlLoader = new g.SqlDatabaseLoader(Connection, true);
            g.Sql.Database newDb = sqlLoader.Database;

            runTestTables(newDb);

            g.Sql.Table tblRecord1 = (g.Sql.Table)newDb.TablesHash["test_Record1"];
            g.Sql.Table tblRecord2 = (g.Sql.Table)newDb.TablesHash["test_Record2"];
            g.Sql.Table tblRecord3 = (g.Sql.Table)newDb.TablesHash["test_Record3"];
            g.Sql.Table tblRecord1Cat = (g.Sql.Table)newDb.TablesHash["test_Record1Cat"];

            runTestRecord1Constraints(tblRecord1);
            runTestRecord2Constraints(tblRecord2);
            runTestRecord3Constraints(tblRecord3);
            runTestRecord1CatConstraints(tblRecord1Cat);

            runTestRecord1Fields(tblRecord1);
            runTestRecord2Fields(tblRecord2);
            runTestRecord3Fields(tblRecord3);
            runTestRecord1CatFields(tblRecord1Cat);
        }

        private void runTestTables(g.Sql.Database db)
        {
            Assert.AreEqual(4, db.TablesHash.Count);
            Assert.IsTrue(db.TablesHash.Contains("test_Record1"));
            Assert.IsTrue(db.TablesHash.Contains("test_Record2"));
            Assert.IsTrue(db.TablesHash.Contains("test_Record1Cat"));
            Assert.IsTrue(db.TablesHash.Contains("test_Record3"));
        }

        private void runTestRecord1Constraints(g.Sql.Table t)
        {
            Assert.AreEqual(3, t.Constraints.Count);
        }

        private void runTestRecord2Constraints(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.Constraints.Count);
        }

        private void runTestRecord3Constraints(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.Constraints.Count);
        }

        private void runTestRecord1CatConstraints(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.Constraints.Count);
        }

        private void runTestRecord1Fields(g.Sql.Table t)
        {
            Assert.AreEqual(10, t.FieldsHash.Count);

            g.Sql.TableField f1 = (g.Sql.TableField)t.FieldsHash["Id"];
            Assert.AreEqual("Id", f1.Name);
            Assert.AreEqual("int", f1.SqlDatatype);

            g.Sql.TableField f2 = (g.Sql.TableField)t.FieldsHash["Record1CatId"];
            Assert.AreEqual("Record1CatId", f2.Name);
            Assert.AreEqual("int", f2.SqlDatatype);

            g.Sql.TableField f3 = (g.Sql.TableField)t.FieldsHash["A4"];
            Assert.AreEqual("A4", f3.Name);
            Assert.AreEqual("char(3)", f3.SqlDatatype);

            g.Sql.TableField f4 = (g.Sql.TableField)t.FieldsHash["A5"];
            Assert.AreEqual("A5", f4.Name);
            Assert.AreEqual("datetime", f4.SqlDatatype);

            g.Sql.TableField f5 = (g.Sql.TableField)t.FieldsHash["A6"];
            Assert.AreEqual("A6", f5.Name);
            Assert.AreEqual("decimal(5,4)", f5.SqlDatatype);

            g.Sql.TableField f6 = (g.Sql.TableField)t.FieldsHash["A7"];
            Assert.AreEqual("A7", f6.Name);
            Assert.AreEqual("float", f6.SqlDatatype);

            g.Sql.TableField f7 = (g.Sql.TableField)t.FieldsHash["A9"];
            Assert.AreEqual("A9", f7.Name);
            Assert.AreEqual("int", f7.SqlDatatype);

            g.Sql.TableField f8 = (g.Sql.TableField)t.FieldsHash["A11"];
            Assert.AreEqual("A11", f8.Name);
            Assert.AreEqual("nchar(300)", f8.SqlDatatype);

            g.Sql.TableField f9 = (g.Sql.TableField)t.FieldsHash["Newa20"];
            Assert.AreEqual("Newa20", f9.Name);
            Assert.AreEqual("timestamp", f9.SqlDatatype);

            g.Sql.TableField f10 = (g.Sql.TableField)t.FieldsHash["Newcol"];
            Assert.AreEqual("Newcol", f10.Name);
            Assert.AreEqual("ntext", f10.SqlDatatype);
        }

        private void runTestRecord2Fields(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.FieldsHash.Count);
            g.Sql.TableField f1 = (g.Sql.TableField)t.FieldsHash["Id"];
            Assert.AreEqual("Id", f1.Name);
        }

        private void runTestRecord3Fields(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.FieldsHash.Count);
            g.Sql.TableField f1 = (g.Sql.TableField)t.FieldsHash["Id"];
            Assert.AreEqual("Id", f1.Name);
        }

        private void runTestRecord1CatFields(g.Sql.Table t)
        {
            Assert.AreEqual(1, t.FieldsHash.Count);
            g.Sql.TableField f1 = (g.Sql.TableField)t.FieldsHash["Id"];
            Assert.AreEqual("Id", f1.Name);
        }

        private void updateDatabase()
        {
            string schemaFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\mainTest.xml";

            g.DatabaseTool dbTool = new g.DatabaseTool(Connection);

            t.ApplicationNode appRootNode = new g.Parser(schemaFile).ParseSchema();
            new g.SchemaValidator(appRootNode).Validate();

            g.SchemaDatabaseLoader schLoader = new g.SchemaDatabaseLoader(appRootNode);
            g.Sql.Database schemaDatabase = schLoader.Database;

            g.SqlDatabaseLoader sqlLoader = new g.SqlDatabaseLoader(dbTool.Connection, false);
            g.Sql.Database sqlDatabase = sqlLoader.Database;

            g.DatabaseComparer comparer = new g.DatabaseComparer(sqlDatabase, schemaDatabase);
            g.DatabaseUpdatesHelper helper = new g.DatabaseUpdatesHelper(comparer);

            g.Main main = new g.Main(Connection, schemaFile, "Adv.Test", "Output", true);
            main.UpdateTablesConstraints(helper);
        }

        [SetUp]
        public void SetUp()
        {
            string setupFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvSetup.txt";
            string teardownFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvTearDown.txt";
            envBuilder = new Sql.DbEnvironmentBuilder(Connection, setupFile, teardownFile);
            envBuilder.Setup();
        }

        [TearDown]
        public void TearDown() { envBuilder.TearDown(); }

        private Sql.DbEnvironmentBuilder envBuilder;
    }
}
