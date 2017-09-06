using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
    [TestFixture]
    public class DatabaseTest
    {
        [Test]
        public void TestAddGetTables()
        {
            g.Sql.Database db = new g.Sql.Database();

            g.Sql.Table t1 = new g.Sql.Table("Name1", db, false);
            g.Sql.Table t2 = new g.Sql.Table("Name2", db, false);

            Assert.AreEqual(0, db.TablesHash.Count);

            db.AddTable(t1);
            db.AddTable(t2);
            Assert.AreEqual(2, db.TablesHash.Count);

            g.Sql.Table t1a = db.GetTable("Name1");
            g.Sql.Table t2a = db.GetTable("Name2");

            Assert.AreSame(t1, t1a);
            Assert.AreSame(t2, t2a);
        }

        [Test]
        [ExpectedException(typeof(g.TableNotFoundException))]
        public void TestTableNotFound()
        {
            g.Sql.Database db = new g.Sql.Database();
            g.Sql.Table t = db.GetTable("Name");
        }
    }
}
