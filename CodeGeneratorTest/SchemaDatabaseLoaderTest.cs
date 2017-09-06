using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
    [TestFixture]
    public class SchemaDatabaseLoaderTest
    {
        [SetUp]
        public void SetUp()
        {
            string schemaFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\parserTest1.xml";
            g.Parser parser = new g.Parser(schemaFile);
            t.ApplicationNode appNode = parser.ParseSchema();
            new CodeGenerator.SchemaValidator(appNode).Validate();
            database = new g.SchemaDatabaseLoader(appNode).Database;
        }

        [Test]
        public void TestDatabase()
        {
            Assert.AreEqual(4, database.TablesHash.Count);
            Assert.AreEqual(4, database.TablesList.Count);
            Assert.AreEqual(3, database.InternalTablesHash.Count);
            Assert.AreEqual(3, database.InternalTablesList.Count);
            Assert.AreEqual("Table1", database.GetTable("Table1").Name);
            Assert.AreEqual("Table2", database.GetTable("Table2").Name);
            Assert.AreEqual("Table3", database.GetTable("Table3").Name);
            Assert.AreEqual("Table1Table2Link", database.GetTable("Table1Table2Link").Name);
        }

        [Test]
        public void TestTable1()
        {
            g.Sql.Table table = database.GetTable("Table1");
            Assert.AreEqual("Table1", table.Name);
            Assert.AreEqual("Table1", table.SqlName);
            Assert.AreEqual(database, table.Database);
            Assert.IsTrue(table.IsExternal);
            Assert.AreEqual(10, table.FieldsHash.Count);
            Assert.AreEqual(10, table.FieldsList.Count);

            //test constraints
        }

        [Test]
        public void TestTable2()
        {
            g.Sql.Table table = database.GetTable("Table2");
            Assert.AreEqual("Table2", table.Name);
            Assert.AreEqual("Table2", table.SqlName);
            Assert.AreEqual(database, table.Database);
            Assert.IsFalse(table.IsExternal);
            Assert.AreEqual(1, table.FieldsHash.Count);
            Assert.AreEqual(1, table.FieldsList.Count);

            //test constraints
        }

        [Test]
        public void TestTable3()
        {
            g.Sql.Table table = database.GetTable("Table3");
            Assert.AreEqual("Table3", table.Name);
            Assert.AreEqual("Table3", table.SqlName);
            Assert.AreEqual(database, table.Database);
            Assert.IsFalse(table.IsExternal);
            Assert.AreEqual(1, table.FieldsHash.Count);
            Assert.AreEqual(1, table.FieldsList.Count);

            //test constraints
        }

        [Test]
        public void TestTable1Fields()
        {
            g.Sql.Table table = database.GetTable("Table1");
            g.Sql.TableField field1 = (g.Sql.TableField)table.FieldsHash["Field1"];
            g.Sql.TableField field2 = (g.Sql.TableField)table.FieldsHash["Field2"];
            g.Sql.TableField field3 = (g.Sql.TableField)table.FieldsHash["Field3"];
            g.Sql.TableField field4 = (g.Sql.TableField)table.FieldsHash["Field4"];
            g.Sql.TableField field5 = (g.Sql.TableField)table.FieldsHash["Field5"];
            g.Sql.TableField field6 = (g.Sql.TableField)table.FieldsHash["Field6"];
            g.Sql.TableField field7 = (g.Sql.TableField)table.FieldsHash["Field7"];
            g.Sql.TableField field8 = (g.Sql.TableField)table.FieldsHash["Field8"];
            g.Sql.TableField field9 = (g.Sql.TableField)table.FieldsHash["Field9"];
            g.Sql.TableField field10 = (g.Sql.TableField)table.FieldsHash["Field10"];
            runFieldTest(field1, table, "Field1", "Field1", "int", false);
            runFieldTest(field2, table, "Field2", "Field2", "int", true);
            runFieldTest(field3, table, "Field3", "Field3", "int", false);
            runFieldTest(field4, table, "Field4", "Field4", "int", false);
            runFieldTest(field5, table, "Field5", "Field5", "int", false);
            runFieldTest(field6, table, "Field6", "Field6", "varbinary(8)", false);
            runFieldTest(field7, table, "Field7", "Field7", "int", false);
            runFieldTest(field8, table, "Field8", "Field8", "int", false);
            runFieldTest(field9, table, "Field9", "Field9", "int", false);
            runFieldTest(field10, table, "Field10", "Field10", "datetime", false);
        }

        [Test]
        public void TestTable2Fields()
        {
            g.Sql.Table table = database.GetTable("Table2");
            g.Sql.TableField field1 = (g.Sql.TableField)table.FieldsHash["Field1"];
            runFieldTest(field1, table, "Field1", "Field1", "int", false);
        }

        [Test]
        public void TestTable3Fields()
        {
            g.Sql.Table table = database.GetTable("Table3");
            g.Sql.TableField field1 = (g.Sql.TableField)table.FieldsHash["Field1"];
            runFieldTest(field1, table, "Field1", "Field1", "int", false);
        }

        [Test]
        public void TestConstraintsTable1()
        {
            g.Sql.Table table = database.GetTable("Table1");
            Assert.AreEqual(1, table.ForeignKeyConstraints.Count);
            Assert.AreEqual(1, table.UniqueConstraints.Count);

            g.Sql.PrimaryKeyConstraint pkey = table.PrimaryKey;
            Assert.AreEqual("Field3", pkey.Field.Name);
            Assert.IsNull(pkey.Field2);
            Assert.IsTrue(pkey.IsUnary);

            g.Sql.ForeignKeyConstraint fkey = (g.Sql.ForeignKeyConstraint)table.ForeignKeyConstraints[0];
            Assert.AreEqual("Field4", fkey.Field.Name);
            Assert.AreEqual("Field1", fkey.RefFieldName);
            Assert.AreEqual("Table2", fkey.RefTableName);

            g.Sql.UniqueConstraint uc = (g.Sql.UniqueConstraint)table.UniqueConstraints[0];
            Assert.AreEqual("Field5", uc.Field.Name);
        }

        [Test]
        public void TestConstraintsTable2()
        {
            g.Sql.Table table = database.GetTable("Table2");
            Assert.AreEqual(0, table.ForeignKeyConstraints.Count);
            Assert.AreEqual(0, table.UniqueConstraints.Count);

            g.Sql.PrimaryKeyConstraint pkey = table.PrimaryKey;
            Assert.AreEqual("Field1", pkey.Field.Name);
            Assert.IsNull(pkey.Field2);
            Assert.IsTrue(pkey.IsUnary);
        }

        [Test]
        public void TestConstraintsTable3()
        {
            g.Sql.Table table = database.GetTable("Table3");
            Assert.AreEqual(0, table.ForeignKeyConstraints.Count);
            Assert.AreEqual(0, table.UniqueConstraints.Count);
            Assert.IsNull(table.PrimaryKey);
        }

        [Test]
        public void TestConstraintsTable4()
        {
            g.Sql.Table table = database.GetTable("Table1Table2Link");
            Assert.AreEqual(2, table.ForeignKeyConstraints.Count);
            Assert.AreEqual(0, table.UniqueConstraints.Count);

            g.Sql.PrimaryKeyConstraint pkey = table.PrimaryKey;
            Assert.AreEqual("Field1", pkey.Field.Name);
            Assert.AreEqual("Field2", pkey.Field2.Name);
            Assert.IsFalse(pkey.IsUnary);

            g.Sql.ForeignKeyConstraint fkey1 = (g.Sql.ForeignKeyConstraint)table.ForeignKeyConstraints[0];
            Assert.AreEqual("Field1", fkey1.Field.Name);
            Assert.AreEqual("Field3", fkey1.RefFieldName);
            Assert.AreEqual("Table1", fkey1.RefTableName);

            g.Sql.ForeignKeyConstraint fkey2 = (g.Sql.ForeignKeyConstraint)table.ForeignKeyConstraints[1];
            Assert.AreEqual("Field2", fkey2.Field.Name);
            Assert.AreEqual("Field1", fkey2.RefFieldName);
            Assert.AreEqual("Table2", fkey2.RefTableName);
        }

        private void runFieldTest(
            g.Sql.TableField f,
            g.Sql.Table parent,
            string name,
            string sqlName,
            string sqlDatatype,
            bool isIdentity)
        {
            Assert.AreEqual(parent, f.Table);
            Assert.AreEqual(name, f.Name);
            Assert.AreEqual(sqlName, f.SqlName);
            Assert.AreEqual(sqlDatatype, f.SqlDatatype);
            Assert.AreEqual(isIdentity, f.IsIdentity);
        }

        private g.Sql.Database database;
    }
}