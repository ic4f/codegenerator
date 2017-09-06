using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
    [TestFixture]
    public class PrimaryKeyConstraintTest
    {
        [Test]
        public void TestProperties()
        {
            g.Sql.TableField f1 = makeField("name1");
            g.Sql.TableField f2 = makeField("name2");
            g.Sql.PrimaryKeyConstraint c1 = new g.Sql.PrimaryKeyConstraint(f1, "name");
            g.Sql.PrimaryKeyConstraint c2 = new g.Sql.PrimaryKeyConstraint(f1, f2, "name");

            Assert.AreSame(f1, c1.Field);
            Assert.AreSame(f1, c2.Field);
            Assert.AreSame(f2, c2.Field2);
            Assert.IsTrue(c1.IsUnary);
            Assert.IsFalse(c2.IsUnary);
            Assert.AreEqual("name", c1.Name);
            Assert.AreEqual("name", c2.Name);
            Assert.IsNull(c1.Field2);
        }

        [Test]
        public void TestEquals()
        {
            g.Sql.Table tbl1 = new g.Sql.Table("tablename1", null, false);
            g.Sql.Table tbl2 = new g.Sql.Table("tablename2", null, false);
            g.Sql.Table tbl3 = new g.Sql.Table("tablename1", null, false);

            g.Sql.TableField f1 = new g.Sql.TableField(tbl1, "fieldname1", "int", "int", -1, 20, 2, false);
            g.Sql.TableField f2 = new g.Sql.TableField(tbl1, "fieldname1", "int", "int", -1, 20, 2, false);
            g.Sql.TableField f3 = new g.Sql.TableField(tbl3, "fieldname1", "int", "int", -1, 20, 2, false);

            g.Sql.TableField f4 = new g.Sql.TableField(tbl1, "fieldname2", "int", "int", -1, 20, 2, false);
            g.Sql.TableField f5 = new g.Sql.TableField(tbl2, "fieldname1", "int", "int", -1, 20, 2, false);

            g.Sql.PrimaryKeyConstraint c1 = new g.Sql.PrimaryKeyConstraint(f1, null);
            g.Sql.PrimaryKeyConstraint c2 = new g.Sql.PrimaryKeyConstraint(f2, null);
            g.Sql.PrimaryKeyConstraint c3 = new g.Sql.PrimaryKeyConstraint(f3, null);

            g.Sql.PrimaryKeyConstraint c4 = new g.Sql.PrimaryKeyConstraint(f4, null);
            g.Sql.PrimaryKeyConstraint c5 = new g.Sql.PrimaryKeyConstraint(f5, null);

            g.Sql.PrimaryKeyConstraint c6 = new g.Sql.PrimaryKeyConstraint(f1, f2, null);
            g.Sql.PrimaryKeyConstraint c7 = new g.Sql.PrimaryKeyConstraint(f1, f2, null);
            g.Sql.PrimaryKeyConstraint c8 = new g.Sql.PrimaryKeyConstraint(f1, f4, null);
            g.Sql.UniqueConstraint uc = new g.Sql.UniqueConstraint(f1, null);

            //must be equal if same table name and same field name
            Assert.AreEqual(c1, c2);
            Assert.AreEqual(c1, c3);
            Assert.AreEqual(c2, c3);
            Assert.AreNotEqual(c1, c4);
            Assert.AreNotEqual(c1, c5);
            Assert.AreNotEqual(c2, c4);
            Assert.AreNotEqual(c2, c5);
            Assert.AreNotEqual(c3, c4);
            Assert.AreNotEqual(c3, c5);
            Assert.AreNotEqual(c1, uc);
            Assert.AreNotEqual(c2, uc);
            Assert.AreNotEqual(c3, uc);
            Assert.AreNotEqual(c4, uc);
            Assert.AreNotEqual(c5, uc);
            Assert.AreEqual(c6, c7);
            Assert.AreNotEqual(c6, c8);
            Assert.AreNotEqual(c7, c8);
        }

        [Test]
        [ExpectedException(typeof(g.ConstraintFormatException))]
        public void TestFieldIsNull()
        {
            g.Sql.PrimaryKeyConstraint c = new g.Sql.PrimaryKeyConstraint(null, null);
        }

        private g.Sql.TableField makeField(string name)
        {
            return new g.Sql.TableField(null, name, "decimal", "decimal(20,2)", -1, 20, 2, false);
        }
    }
}
