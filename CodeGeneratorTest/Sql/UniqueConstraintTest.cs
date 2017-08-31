using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class UniqueConstraintTest
	{
		[Test]
		public void TestProperties()
		{
			g.Sql.TableField f = makeField("name");
			g.Sql.UniqueConstraint c = new g.Sql.UniqueConstraint(f, null);
			Assert.AreSame(f, c.Field);
		}

		[Test]
		public void TestEquals()
		{
			g.Sql.Table tbl1 = new g.Sql.Table("tablename1", null, false);
			g.Sql.Table tbl2 = new g.Sql.Table("tablename2", null, false);
			g.Sql.Table tbl3 = new g.Sql.Table("tablename1", null, false);

			g.Sql.TableField f1 = new g.Sql.TableField(tbl1, "fieldname1", "decimal", "decimal(20,2)", -1, 20, 2, false);
			g.Sql.TableField f2 = new g.Sql.TableField(tbl1, "fieldname1", "decimal", "decimal(20,2)", -1, 20, 2, false);
			g.Sql.TableField f3 = new g.Sql.TableField(tbl3, "fieldname1", "decimal", "decimal(20,2)", -1, 20, 2, false);

			g.Sql.TableField f4 = new g.Sql.TableField(tbl1, "fieldname2", "decimal", "decimal(20,2)", -1, 20, 2, false);
			g.Sql.TableField f5 = new g.Sql.TableField(tbl2, "fieldname1", "decimal", "decimal(20,2)", -1, 20, 2, false);

			g.Sql.UniqueConstraint c1 = new g.Sql.UniqueConstraint(f1, null);
			g.Sql.UniqueConstraint c2 = new g.Sql.UniqueConstraint(f2, null);
			g.Sql.UniqueConstraint c3 = new g.Sql.UniqueConstraint(f3, null);

			g.Sql.UniqueConstraint c4 = new g.Sql.UniqueConstraint(f4, null);
			g.Sql.UniqueConstraint c5 = new g.Sql.UniqueConstraint(f5, null);
			g.Sql.PrimaryKeyConstraint dc = new g.Sql.PrimaryKeyConstraint(f1, null);

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
			Assert.AreNotEqual(c1, dc);
			Assert.AreNotEqual(c2, dc);
			Assert.AreNotEqual(c3, dc);
			Assert.AreNotEqual(c4, dc);
			Assert.AreNotEqual(c5, dc);
		}

		[Test]
		[ExpectedException(typeof(g.ConstraintFormatException))]
		public void TestFieldIsNull()
		{
			g.Sql.UniqueConstraint c = new g.Sql.UniqueConstraint(null, null);
		}

		private g.Sql.TableField makeField(string name)
		{
			return new g.Sql.TableField(null, name, "decimal", "decimal(20,2)", -1, 20, 2, false);
		}
	}
}
