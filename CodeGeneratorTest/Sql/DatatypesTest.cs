using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class DatatypesTest
	{
		[Test]
		public void TestBigint()
		{
			g.Sql.Bigint dt = new g.Sql.Bigint();
			Assert.AreEqual("bigint", dt.Name);
			Assert.AreEqual(8, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestBinary()
		{
			g.Sql.Binary dt = new g.Sql.Binary();
			Assert.AreEqual("binary", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(8000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestBit()
		{
			g.Sql.Bit dt = new g.Sql.Bit();
			Assert.AreEqual("bit", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestChar()
		{
			g.Sql.Char dt = new g.Sql.Char();
			Assert.AreEqual("char", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(8000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestDatetime()
		{
			g.Sql.Datetime dt = new g.Sql.Datetime();
			Assert.AreEqual("datetime", dt.Name);
			Assert.AreEqual(8, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestDecimal()
		{
			g.Sql.Decimal dt = new g.Sql.Decimal();
			Assert.AreEqual("decimal", dt.Name);
			Assert.AreEqual(17, dt.DefaultFieldLength);
			Assert.AreEqual(18, dt.DefaultPrecision);
			Assert.AreEqual(0, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(38, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsTrue(dt.SetPrecisionScale);
		}

		[Test]
		public void TestFloat()
		{
			g.Sql.Float dt = new g.Sql.Float();
			Assert.AreEqual("float", dt.Name);
			Assert.AreEqual(8, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestImage()
		{
			g.Sql.Image dt = new g.Sql.Image();
			Assert.AreEqual("image", dt.Name);
			Assert.AreEqual(16, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestInt()
		{
			g.Sql.Int dt = new g.Sql.Int();
			Assert.AreEqual("int", dt.Name);
			Assert.AreEqual(4, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestMoney()
		{
			g.Sql.Money dt = new g.Sql.Money();
			Assert.AreEqual("money", dt.Name);
			Assert.AreEqual(8, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestNchar()
		{
			g.Sql.Nchar dt = new g.Sql.Nchar();
			Assert.AreEqual("nchar", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(4000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestNtext()
		{
			g.Sql.Ntext dt = new g.Sql.Ntext();
			Assert.AreEqual("ntext", dt.Name);
			Assert.AreEqual(16, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestNumeric()
		{
			g.Sql.Numeric dt = new g.Sql.Numeric();
			Assert.AreEqual("numeric", dt.Name);
			Assert.AreEqual(17, dt.DefaultFieldLength);
			Assert.AreEqual(18, dt.DefaultPrecision);
			Assert.AreEqual(0, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(38, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsTrue(dt.SetPrecisionScale);
		}

		[Test]
		public void TestNvarchar()
		{
			g.Sql.Nvarchar dt = new g.Sql.Nvarchar();
			Assert.AreEqual("nvarchar", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(4000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestReal()
		{
			g.Sql.Real dt = new g.Sql.Real();
			Assert.AreEqual("real", dt.Name);
			Assert.AreEqual(4, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestSmalldatetime()
		{
			g.Sql.Smalldatetime dt = new g.Sql.Smalldatetime();
			Assert.AreEqual("smalldatetime", dt.Name);
			Assert.AreEqual(4, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestSmallint()
		{
			g.Sql.Smallint dt = new g.Sql.Smallint();
			Assert.AreEqual("smallint", dt.Name);
			Assert.AreEqual(2, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestSmallmoney()
		{
			g.Sql.Smallmoney dt = new g.Sql.Smallmoney();
			Assert.AreEqual("smallmoney", dt.Name);
			Assert.AreEqual(4, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestText()
		{
			g.Sql.Text dt = new g.Sql.Text();
			Assert.AreEqual("text", dt.Name);
			Assert.AreEqual(16, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestTimestamp()
		{
			g.Sql.Timestamp dt = new g.Sql.Timestamp();
			Assert.AreEqual("timestamp", dt.Name);
			Assert.AreEqual(8, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestTinyint()
		{
			g.Sql.Tinyint dt = new g.Sql.Tinyint();
			Assert.AreEqual("tinyint", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestUniqueidentifier()
		{
			g.Sql.Uniqueidentifier dt = new g.Sql.Uniqueidentifier();
			Assert.AreEqual("uniqueidentifier", dt.Name);
			Assert.AreEqual(16, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(-1, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsFalse(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestVarbinary()
		{
			g.Sql.Varbinary dt = new g.Sql.Varbinary();
			Assert.AreEqual("varbinary", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(8000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}

		[Test]
		public void TestVarchar()
		{
			g.Sql.Varchar dt = new g.Sql.Varchar();
			Assert.AreEqual("varchar", dt.Name);
			Assert.AreEqual(1, dt.DefaultFieldLength);
			Assert.AreEqual(-1, dt.DefaultPrecision);
			Assert.AreEqual(-1, dt.DefaultScale);
			Assert.AreEqual(8000, dt.MaxCharLength);
			Assert.AreEqual(-1, dt.MaxPrecision);
			Assert.IsTrue(dt.SetCharLength);
			Assert.IsFalse(dt.SetPrecisionScale);
		}
	}
}
