using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class SqlDatabaseLoaderTest : BaseTest
	{
		[Test]
		public void TestLoadDatabase()
		{
			g.Sql.Database db = dbLoader.Database;
			Assert.AreEqual(4, db.TablesHash.Count);
			Assert.IsTrue(db.TablesHash.Contains("test_Record1"));
			Assert.IsTrue(db.TablesHash.Contains("test_Record1Cat"));
			Assert.IsTrue(db.TablesHash.Contains("test_Record1Record2"));
			Assert.IsTrue(db.TablesHash.Contains("test_Record2"));
		}

		[Test]
		public void TestPrimaryKeyConstraints()
		{
			g.Sql.Database db = dbLoader.Database;

			g.Sql.Table t = db.GetTable("test_Record1");

			g.Sql.PrimaryKeyConstraint pk = t.PrimaryKey;
			Assert.AreEqual("Id", pk.Field.Name);
			Assert.IsTrue(pk.IsUnary);
			Assert.IsNull(pk.Field2);

			t = db.GetTable("test_Record1Cat");

			pk = t.PrimaryKey;
			Assert.AreEqual("Id", pk.Field.Name);
			Assert.IsTrue(pk.IsUnary);
			Assert.IsNull(pk.Field2);

			t = db.GetTable("test_Record1Record2");

			pk = t.PrimaryKey;
			Assert.AreEqual("Record1Id", pk.Field.Name);
			Assert.IsFalse(pk.IsUnary);
			Assert.AreEqual("Record2Id", pk.Field2.Name);

			t = db.GetTable("test_Record2");

			pk = t.PrimaryKey;
			Assert.AreEqual("Id", pk.Field.Name);
			Assert.IsTrue(pk.IsUnary);
			Assert.IsNull(pk.Field2);
		}

		[Test]
		public void TestForeignKeyConstraints()
		{
			g.Sql.Database db = dbLoader.Database;
			g.Sql.Table t = db.GetTable("test_Record1Record2");
			Hashtable fkeys = new Hashtable();
			foreach(g.Sql.Constraint c in t.Constraints)			
			{
				g.Sql.ForeignKeyConstraint temp = c as g.Sql.ForeignKeyConstraint;
				if (temp != null)
					fkeys.Add(temp.Field.Name, temp);
			}

			Assert.AreEqual(2, fkeys.Count);

			g.Sql.ForeignKeyConstraint fk = (g.Sql.ForeignKeyConstraint)fkeys["Record1Id"];			
			Assert.AreEqual(fk.Field.Name, "Record1Id");
			Assert.AreEqual(fk.RefTableName, "test_Record1");
			Assert.AreEqual(fk.RefFieldName, "Id");

			fk = (g.Sql.ForeignKeyConstraint)fkeys["Record2Id"];			
			Assert.AreEqual(fk.Field.Name, "Record2Id");
			Assert.AreEqual(fk.RefTableName, "test_Record2");
			Assert.AreEqual(fk.RefFieldName, "Id");
		}

		[Test]
		public void TestUniqueConstraints()
		{
			g.Sql.Database db = dbLoader.Database;
			g.Sql.Table t = db.GetTable("test_Record1");

         g.Sql.UniqueConstraint uq = null;
			foreach(g.Sql.Constraint c in t.Constraints)			
			{
				g.Sql.UniqueConstraint temp = c as g.Sql.UniqueConstraint;
				if (temp != null && c.Field.Name == "A4")
					uq = temp;
			}
			Assert.IsNotNull(uq);
		}

		[Test]
		public void TestGetTables()
		{
			DataTable tables = dbLoader.GetTables();			
			g.Sql.Table t1 = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);						
			g.Sql.Table t2 = new g.Sql.Table(tables.Rows[1][0].ToString(), null, false);
			g.Sql.Table t3 = new g.Sql.Table(tables.Rows[2][0].ToString(), null, false);
			g.Sql.Table t4 = new g.Sql.Table(tables.Rows[3][0].ToString(), null, false);

			Assert.AreEqual(4, tables.Rows.Count);
			Assert.AreEqual("test_Record1", t1.Name);
			Assert.AreEqual("test_Record1Cat", t2.Name);
			Assert.AreEqual("test_Record1Record2", t3.Name);
			Assert.AreEqual("test_Record2", t4.Name);
		}

		[Test]
		public void TestGetColumns()
		{
			DataTable tables = dbLoader.GetTables();
			g.Sql.Table t1 = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);
			g.Sql.Table t2 = new g.Sql.Table(tables.Rows[1][0].ToString(), null, false);
			g.Sql.Table t3 = new g.Sql.Table(tables.Rows[2][0].ToString(), null, false);
			g.Sql.Table t4 = new g.Sql.Table(tables.Rows[3][0].ToString(), null, false);

			DataTable columns1 = dbLoader.GetColumns(t1.Name);
			DataTable columns2 = dbLoader.GetColumns(t2.Name);
			DataTable columns3 = dbLoader.GetColumns(t3.Name);
			DataTable columns4 = dbLoader.GetColumns(t4.Name);

			Assert.AreEqual(26, columns1.Rows.Count);
			Assert.AreEqual(1, columns2.Rows.Count);
			Assert.AreEqual(2, columns3.Rows.Count);
			Assert.AreEqual(1, columns4.Rows.Count);
		}

		//this test tests whether the datarow is read correctly - i.e. NOT the correctness of the 
		//		sql.tablefield properties, but whether they are initialized corrrectlky based on the datarow data
		[Test]
		public void TestMakeField()
		{
			DataTable tables = dbLoader.GetTables();
			g.Sql.Table t1 = new g.Sql.Table(tables.Rows[0][0].ToString(), null, false);
			DataTable columns = dbLoader.GetColumns(t1.Name);

			g.Sql.TableField f = dbLoader.MakeField(columns.Rows[0], t1);
			Assert.AreEqual("Id", f.Name);
			Assert.IsTrue(f.IsIdentity);
			Assert.AreEqual("int", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[1], t1);
			Assert.AreEqual("Record1CatId", f.Name);
			Assert.AreEqual("int", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[2], t1);
			Assert.AreEqual("A1", f.Name);
			Assert.AreEqual("bigint", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[3], t1);
			Assert.AreEqual("A2", f.Name);
			Assert.AreEqual("binary", f.Datatype);
			Assert.AreEqual(8, f.Length);

			f = dbLoader.MakeField(columns.Rows[4], t1);
			Assert.AreEqual("A3", f.Name);
			Assert.AreEqual("bit", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[5], t1);
			Assert.AreEqual("A4", f.Name);
			Assert.AreEqual("char", f.Datatype);
			Assert.AreEqual(3, f.Length);

			f = dbLoader.MakeField(columns.Rows[6], t1);
			Assert.AreEqual("A5", f.Name);
			Assert.AreEqual("datetime", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[7], t1);
			Assert.AreEqual("A6", f.Name);
			Assert.AreEqual("decimal", f.Datatype);
			Assert.AreEqual(5, f.Precision);
			Assert.AreEqual(2, f.Scale);

			f = dbLoader.MakeField(columns.Rows[8], t1);
			Assert.AreEqual("A7", f.Name);
			Assert.AreEqual("float", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[9], t1);
			Assert.AreEqual("A8", f.Name);
			Assert.AreEqual("image", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[10], t1);
			Assert.AreEqual("A9", f.Name);
			Assert.AreEqual("int", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[11], t1);
			Assert.AreEqual("A10", f.Name);
			Assert.AreEqual("money", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[12], t1);
			Assert.AreEqual("A11", f.Name);
			Assert.AreEqual("nchar", f.Datatype);
			Assert.AreEqual(3, f.Length); 

			f = dbLoader.MakeField(columns.Rows[13], t1);
			Assert.AreEqual("A12", f.Name);
			Assert.AreEqual("ntext", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[14], t1);
			Assert.AreEqual("A13", f.Name);
			Assert.AreEqual("numeric", f.Datatype);
			Assert.AreEqual(4, f.Precision);
			Assert.AreEqual(3, f.Scale);

			f = dbLoader.MakeField(columns.Rows[15], t1);
			Assert.AreEqual("A14", f.Name);
			Assert.AreEqual("nvarchar", f.Datatype);
			Assert.AreEqual(3, f.Length);

			f = dbLoader.MakeField(columns.Rows[16], t1);
			Assert.AreEqual("A15", f.Name);
			Assert.AreEqual("real", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[17], t1);
			Assert.AreEqual("A16", f.Name);
			Assert.AreEqual("smalldatetime", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[18], t1);
			Assert.AreEqual("A17", f.Name);
			Assert.AreEqual("smallint", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[19], t1);
			Assert.AreEqual("A18", f.Name);
			Assert.AreEqual("smallmoney", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[20], t1);
			Assert.AreEqual("A19", f.Name);
			Assert.AreEqual("text", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[21], t1);
			Assert.AreEqual("A20", f.Name);
			Assert.AreEqual("timestamp", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[22], t1);
			Assert.AreEqual("A21", f.Name);
			Assert.AreEqual("tinyint", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[23], t1);
			Assert.AreEqual("A22", f.Name);
			Assert.AreEqual("uniqueidentifier", f.Datatype);

			f = dbLoader.MakeField(columns.Rows[24], t1);
			Assert.AreEqual("A23", f.Name);
			Assert.AreEqual("varbinary", f.Datatype);
			Assert.AreEqual(8, f.Length);

			f = dbLoader.MakeField(columns.Rows[25], t1);
			Assert.AreEqual("A24", f.Name);
			Assert.AreEqual("varchar", f.Datatype);
			Assert.AreEqual(3, f.Length);
		}


		[SetUp]
		public void SetUp()
		{
			string setupFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvSetup.txt";
			string teardownFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvTearDown.txt";
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
