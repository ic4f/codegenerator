using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class TableTest
	{
		[Test]
		public void TestProperties()
		{
			g.Sql.Database db = new g.Sql.Database();
			g.Sql.Table t = new g.Sql.Table("Name", db, false);
			Assert.AreSame(db, t.Database);
			Assert.AreEqual("Name", t.Name);
			Assert.AreEqual("Name", t.SqlName);
		}

		[Test]
		public void TestReservedName()
		{
			g.Sql.Database db = new g.Sql.Database();
			g.Sql.Table t = new g.Sql.Table("user", db, false);
			Assert.AreSame(db, t.Database);
			Assert.AreEqual("user", t.Name);
			Assert.AreEqual("[user]", t.SqlName);
		}

		[Test]
		public void TestAlterPrimaryKey()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "int", false);
			g.Sql.TableField f2 = makeField("name2", "int", false);
			t.AddField(f1);
			t.AddField(f2);
			g.Sql.PrimaryKeyConstraint c1 = new g.Sql.PrimaryKeyConstraint(f1, null);		
			Assert.IsNull(t.PrimaryKey);
			t.AddConstraint(c1);
			Assert.IsNotNull(t.PrimaryKey);
			g.Sql.PrimaryKeyConstraint c2 = new g.Sql.PrimaryKeyConstraint(f2, null);		
			t.AddConstraint(c2);
			Assert.AreEqual("name1", t.PrimaryKey.Field.Name);
			Assert.AreEqual("name2", t.PrimaryKey.Field2.Name);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateConstraintException))]
		public void TestDuplicateUniqueConstraints()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "int", true);
			t.AddField(f1);
			g.Sql.UniqueConstraint c1 = new g.Sql.UniqueConstraint(f1, null);
			g.Sql.UniqueConstraint c2 = new g.Sql.UniqueConstraint(f1, null);
			t.AddConstraint(c1);
			t.AddConstraint(c2);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateConstraintException))]
		public void TestDuplicateForeignKeyConstraints()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "int", true);
			t.AddField(f1);
			g.Sql.ForeignKeyConstraint c1 = new g.Sql.ForeignKeyConstraint(f1, "table1", "field1", null);
			g.Sql.ForeignKeyConstraint c2 = new g.Sql.ForeignKeyConstraint(f1, "table2", "field2", null);
			t.AddConstraint(c1);
			t.AddConstraint(c2);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateConstraintException))]
		public void TestDuplicatePrimaryKeyConstraints1()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "int", true);
			t.AddField(f1);
			g.Sql.PrimaryKeyConstraint c1 = new g.Sql.PrimaryKeyConstraint(f1, null);
			g.Sql.PrimaryKeyConstraint c2 = new g.Sql.PrimaryKeyConstraint(f1, null);
			t.AddConstraint(c1);
			t.AddConstraint(c2);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateConstraintException))]
		public void TestDuplicatePrimaryKeyConstraints2()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "int", true);
			g.Sql.TableField f2 = makeField("name2", "int", false);
			t.AddField(f1);
			t.AddField(f2);
			g.Sql.PrimaryKeyConstraint c1 = new g.Sql.PrimaryKeyConstraint(f1, f2, null);
			g.Sql.PrimaryKeyConstraint c2 = new g.Sql.PrimaryKeyConstraint(f1, f2, null);
			t.AddConstraint(c1);
			t.AddConstraint(c2);
		}

		[Test]
		public void TestFields()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			Assert.AreEqual(0, t.FieldsHash.Count);

			g.Sql.TableField f1 = makeField("name1", "varchar", false);
			g.Sql.TableField f2 = makeField("name2", "char", false);
			t.AddField(f1);
			t.AddField(f2);

			Assert.AreEqual(2, t.FieldsHash.Count);
			Assert.IsTrue(t.FieldsHash.Contains(f1.Name));
			Assert.IsTrue(t.FieldsHash.Contains(f2.Name));
			Assert.IsFalse(t.FieldsHash.Contains("xyz"));
			Assert.AreSame(f1, t.GetField(f1.Name));
			Assert.AreSame(f2, t.GetField(f2.Name));
			Assert.AreNotSame(f2, t.GetField(f1.Name));
		}

		[Test]
		public void TestPrimaryKey()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f = makeField("name2", "char", false);
			t.AddField(f);
			g.Sql.PrimaryKeyConstraint c = new g.Sql.PrimaryKeyConstraint(f, null);
			t.AddConstraint(c);
			Assert.AreSame(c, t.PrimaryKey);
		}

		[Test]
		public void TestConstraints()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "char", false);
			g.Sql.TableField f2 = makeField("name2", "char", false);
			t.AddField(f1);
			t.AddField(f2);
			g.Sql.UniqueConstraint c1 = new g.Sql.UniqueConstraint(f1, null);
			g.Sql.UniqueConstraint c2 = new g.Sql.UniqueConstraint(f2, null);

			//create identical constraints to check if they are present in the table
			g.Sql.UniqueConstraint c1a = new g.Sql.UniqueConstraint(f1, null);
			g.Sql.UniqueConstraint c2a = new g.Sql.UniqueConstraint(f2, null);
			
			Assert.AreEqual(0, t.Constraints.Count);
			Assert.IsFalse(t.Constraints.Contains(c1a));
			Assert.IsFalse(t.Constraints.Contains(c2a));

			t.AddConstraint(c1);
			Assert.AreEqual(1, t.Constraints.Count);
			Assert.IsTrue(t.Constraints.Contains(c1a));
			Assert.IsFalse(t.Constraints.Contains(c2a));

			t.AddConstraint(c2);
			Assert.AreEqual(2, t.Constraints.Count);
			Assert.IsTrue(t.Constraints.Contains(c1a));
			Assert.IsTrue(t.Constraints.Contains(c2a));		
			
		}

		[Test]
		public void TestDifferentConstraints()
		{
			g.Sql.Table t = new g.Sql.Table("Name", null, false);
			g.Sql.TableField f1 = makeField("name1", "char", false);
			g.Sql.TableField f2 = makeField("name2", "char", false);
			t.AddField(f1);
			t.AddField(f2);

			g.Sql.UniqueConstraint uc1 = new g.Sql.UniqueConstraint(f1, null);
			g.Sql.UniqueConstraint uc2 = new g.Sql.UniqueConstraint(f2, null);
			g.Sql.PrimaryKeyConstraint pc1 = new g.Sql.PrimaryKeyConstraint(f1, "c");
			t.AddConstraint(uc1);
			t.AddConstraint(uc2);			
			t.AddConstraint(pc1);

			Assert.AreEqual(2, t.UniqueConstraints.Count);
			Assert.AreEqual(0, t.ForeignKeyConstraints.Count);
		}

		private g.Sql.TableField makeField(string name, string dt, bool isId)
		{
			g.Sql.Table tbl = new g.Sql.Table("a", null, false);
			return new g.Sql.TableField(tbl, name, dt, dt, -1, 20, 2, isId);
		}

		private g.Sql.TableField makeField(string name, string dt, int length, bool isId)
		{
			g.Sql.Table tbl = new g.Sql.Table("a", null, false);
			return new g.Sql.TableField(tbl, name, dt, dt, length, 20, 2, isId);
		}
	}
}
