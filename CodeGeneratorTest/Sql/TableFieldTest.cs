using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class TableFieldTest
	{		
		[Test]
		public void TestProperties()
		{
			g.Sql.Table tbl = new g.Sql.Table("name", null, false);
			g.Sql.TableField f1 = new g.Sql.TableField(tbl, "name", "decimal", "decimal(20,2)", -1, 20, 2, false);

			Assert.AreSame(tbl, f1.Table);
			Assert.AreEqual("name", f1.Name);
			Assert.AreEqual("name", f1.SqlName);
			Assert.AreEqual("decimal", f1.Datatype);
			Assert.AreEqual(20, f1.Precision);
			Assert.AreEqual(2, f1.Scale);
			Assert.AreEqual(-1, f1.Length);
			Assert.AreEqual(false, f1.IsIdentity);
		}

		[Test]
		public void TestReservedName()
		{
			g.Sql.Table tbl = new g.Sql.Table("user", null, false);
			g.Sql.TableField f1 = new g.Sql.TableField(tbl, "user", "decimal", "decimal(20,2)", -1, 20, 2, false);

			Assert.AreSame(tbl, f1.Table);
			Assert.AreEqual("user", f1.Name);
			Assert.AreEqual("[user]", f1.SqlName);
			Assert.AreEqual("decimal", f1.Datatype);
			Assert.AreEqual(20, f1.Precision);
			Assert.AreEqual(2, f1.Scale);
			Assert.AreEqual(-1, f1.Length);
			Assert.AreEqual(false, f1.IsIdentity);
		}
	}
}

