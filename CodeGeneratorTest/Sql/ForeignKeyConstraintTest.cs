using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class ForeignKeyConstraintTest
	{
		[Test]
		public void TestProperties()
		{
			g.Sql.TableField f = makeField("name");
			g.Sql.ForeignKeyConstraint c = new g.Sql.ForeignKeyConstraint(f, "rt", "rf", "name");
			
			Assert.AreSame(f, c.Field);
			Assert.AreEqual("rt", c.RefTableName);
			Assert.AreEqual("rf", c.RefFieldName);			
			Assert.AreEqual("name", c.Name);
		}

		[Test]
		public void TestEqualsAndIdentical()
		{
			g.Sql.Table tbl1 = new g.Sql.Table("tablename1", null, false);
			g.Sql.Table tbl2 = new g.Sql.Table("tablename2", null, false);
			g.Sql.Table tbl3 = new g.Sql.Table("tablename1", null, false);

			g.Sql.TableField f1 = new g.Sql.TableField(tbl1, "fieldname1", "int", "int", -1, 20, 2, false);
			g.Sql.TableField f2 = new g.Sql.TableField(tbl1, "fieldname1", "int", "int", -1, 20, 2, false);
			g.Sql.TableField f3 = new g.Sql.TableField(tbl3, "fieldname1", "int", "int", -1, 20, 2, false);

			g.Sql.TableField f4 = new g.Sql.TableField(tbl1, "fieldname2", "int", "int", -1, 20, 2, false);
			g.Sql.TableField f5 = new g.Sql.TableField(tbl2, "fieldname1", "int", "int", -1, 20, 2, false);

			g.Sql.ForeignKeyConstraint c1 = new g.Sql.ForeignKeyConstraint(f1, "rt", "rf", null);
			g.Sql.ForeignKeyConstraint c2 = new g.Sql.ForeignKeyConstraint(f2, "rt", "rf", null);
			g.Sql.ForeignKeyConstraint c3 = new g.Sql.ForeignKeyConstraint(f3, "rt", "rf1", null);

			g.Sql.ForeignKeyConstraint c4 = new g.Sql.ForeignKeyConstraint(f4, "rt", "rf", null);
			g.Sql.ForeignKeyConstraint c5 = new g.Sql.ForeignKeyConstraint(f5, "rt", "rf", null);
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

			Assert.IsTrue(c1.IsIdenticalTo(c2));
			Assert.IsFalse(c1.IsIdenticalTo(c3));
			Assert.IsFalse(c2.IsIdenticalTo(c3));
			Assert.IsFalse(c1.IsIdenticalTo(c4));
			Assert.IsFalse(c1.IsIdenticalTo(c5));
			Assert.IsFalse(c2.IsIdenticalTo(c4));
			Assert.IsFalse(c2.IsIdenticalTo(c5));
			Assert.IsFalse(c3.IsIdenticalTo(c4));
			Assert.IsFalse(c3.IsIdenticalTo(c5));
			Assert.IsFalse(c1.IsIdenticalTo(uc));
			Assert.IsFalse(c2.IsIdenticalTo(uc));
			Assert.IsFalse(c3.IsIdenticalTo(uc));
			Assert.IsFalse(c4.IsIdenticalTo(uc));
			Assert.IsFalse(c5.IsIdenticalTo(uc));
		}

		[Test]
		[ExpectedException(typeof(g.ConstraintFormatException))]
		public void TestFieldIsNull()
		{	
			g.Sql.ForeignKeyConstraint c = new g.Sql.ForeignKeyConstraint(null, "rt", "rf", null);
		}

		[Test]
		[ExpectedException(typeof(g.ConstraintFormatException))]
		public void TestRefTableIsNull()
		{			
			g.Sql.TableField f = makeField("name");
			g.Sql.ForeignKeyConstraint c = new g.Sql.ForeignKeyConstraint(f, null, "rf", null);
		}

		[Test]
		[ExpectedException(typeof(g.ConstraintFormatException))]
		public void TestRefFieldIsNull()
		{
			g.Sql.TableField f = makeField("name");
			g.Sql.ForeignKeyConstraint c = new g.Sql.ForeignKeyConstraint(f, "rt", null, null);
		}
		
		private g.Sql.TableField makeField(string name)
		{
			return new g.Sql.TableField(null, name, "decimal", "decimal(20,2)", -1, 20, 2, false);
		} 
	}
}