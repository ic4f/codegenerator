using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest
{
    [TestFixture]
    public class SqlDatabaseLoaderTest2 : BaseTest
    {
        [Test]
        public void TestLoadDatabase()
        {
            g.Sql.Database db = dbLoader.Database;
            Assert.AreEqual(1, db.TablesHash.Count);
            Assert.IsTrue(db.TablesHash.Contains("test_Record2"));
        }

        [Test]
        public void TestPrimaryKeyConstraints()
        {
            g.Sql.Database db = dbLoader.Database;
            g.Sql.Table t = db.GetTable("test_Record2");
            Assert.IsNull(t.PrimaryKey);
        }

        [Test]
        public void TestForeignKeyConstraints()
        {
            g.Sql.Database db = dbLoader.Database;
            g.Sql.Table t = db.GetTable("test_Record2");
            Hashtable fkeys = new Hashtable();
            foreach (g.Sql.Constraint c in t.Constraints)
            {
                g.Sql.ForeignKeyConstraint temp = c as g.Sql.ForeignKeyConstraint;
                if (temp != null)
                    fkeys.Add(temp.Field.Name, temp);
            }
            Assert.AreEqual(0, fkeys.Count);
        }

        [Test]
        public void TestUniqueConstraints()
        {
            g.Sql.Database db = dbLoader.Database;
            g.Sql.Table t = db.GetTable("test_Record2");

            g.Sql.UniqueConstraint uq = null;
            foreach (g.Sql.Constraint c in t.Constraints)
            {
                g.Sql.UniqueConstraint temp = c as g.Sql.UniqueConstraint;
                if (temp != null && c.Field.Name == "a4")
                    uq = temp;
            }
            Assert.IsNull(uq);
        }

        [Test]
        public void TestGetTables()
        {
            DataTable tables = dbLoader.GetTables();
            g.Sql.Table t = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);
            Assert.AreEqual(1, tables.Rows.Count);
            Assert.AreEqual("test_Record2", t.Name);
        }

        [Test]
        public void TestGetColumns()
        {
            DataTable tables = dbLoader.GetTables();
            g.Sql.Table t = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);
            DataTable columns = dbLoader.GetColumns(t.Name);
            Assert.AreEqual(1, columns.Rows.Count);
        }

        //this test tests whether the dataRow is read correctly - i.e. NOT the correctness of the 
        //		sql.tablefield properties, but whether they are initialized corrrectly based on the dataRow data
        [Test]
        public void TestMakeField()
        {
            DataTable tables = dbLoader.GetTables();
            g.Sql.Table t = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);
            DataTable columns = dbLoader.GetColumns(t.Name);

            g.Sql.TableField f = dbLoader.MakeField(columns.Rows[0], t);
            Assert.AreEqual("Id", f.Name);
            Assert.IsTrue(f.IsIdentity);
            Assert.AreEqual("int", f.Datatype);
        }


        [SetUp]
        public void SetUp()
        {
            string setupFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvSetup2.txt";
            string teardownFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvTearDown2.txt";
            envBuilder = new Sql.DbEnvironmentBuilder(Connection, setupFile, teardownFile);
            envBuilder.Setup();

            dbLoader = new CodeGenerator.SqlDatabaseLoader(Connection, true);
        }

        [TearDown]
        public void TearDown() { envBuilder.TearDown(); }

        private Sql.DbEnvironmentBuilder envBuilder;
        private g.SqlDatabaseLoader dbLoader;
    }
}
