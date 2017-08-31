using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class DatatypeLoaderTest
	{

		#region test load datatypes from schema

		[Test]
		public void TestSchemaValidDatatype1()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("int");
			Assert.AreEqual("int", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(4, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("int", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSchemaValidDatatype2()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("char(10)");
			Assert.AreEqual("char", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("char(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSchemaValidDatatype3()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(20)");
			Assert.AreEqual("decimal", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(17, dl.Datatype.Length);
			Assert.AreEqual(20, dl.Datatype.Precision);
			Assert.AreEqual((new g.Sql.Decimal().DefaultScale), dl.Datatype.Scale);
			Assert.AreEqual("decimal(20)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSchemaValidDatatype4()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(20,5)");
			Assert.AreEqual("decimal", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(17, dl.Datatype.Length);
			Assert.AreEqual(20, dl.Datatype.Precision);
			Assert.AreEqual(5, dl.Datatype.Scale);
			Assert.AreEqual("decimal(20,5)", dl.Datatype.FullSqlName);
		}
		#endregion

		#region test exceptions (loading from schema)

		[Test]
		[ExpectedException(typeof(g.UnknownSqlDatatypeException))]
		public void TestUnknownDatatype()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("nosuchdatatype");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestWrongFormat1()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(1,2,3)");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestWrongFormat2()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(1");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestWrongFormat3()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(a");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestWrongDataFormat1()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("int(1)");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestWrongDataFormat2()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("char(2,1)");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestIllegalLength()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("char(8001)");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestIllegalPrecision()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(39)");
		}

		[Test]
		[ExpectedException(typeof(g.SqlDatatypeFormatException))]
		public void TestIllegalScale()
		{	
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal(10,11)");
		}
		#endregion

		#region test load datatypes from database data

		[Test]
		public void TestSqlBigint()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("bigint", 42, 42, 42);
			Assert.AreEqual("bigint", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(8, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("bigint", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlBinary()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("binary", 10, 42, 42);
			Assert.AreEqual("binary", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("binary(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlBit()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("bit", 42, 42, 42);
			Assert.AreEqual("bit", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(1, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("bit", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlChar()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("char", 10, 42, 42);
			Assert.AreEqual("char", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("char(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlDatetime()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("datetime", 42, 42, 42);
			Assert.AreEqual("datetime", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(8, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("datetime", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlDecimal()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("decimal", 42, 20, 10);
			Assert.AreEqual("decimal", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(17, dl.Datatype.Length);
			Assert.AreEqual(20, dl.Datatype.Precision);
			Assert.AreEqual(10, dl.Datatype.Scale);
			Assert.AreEqual("decimal(20,10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlFloat()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("float", 42, 42, 42);
			Assert.AreEqual("float", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(8, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("float", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlImage()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("image", 42, 42, 42);
			Assert.AreEqual("image", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(16, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("image", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlInt()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("int", 42, 42, 42);
			Assert.AreEqual("int", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(4, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("int", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlMoney()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("money", 42, 42, 42);
			Assert.AreEqual("money", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(8, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("money", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlNchar()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("nchar", 10, 42, 42);
			Assert.AreEqual("nchar", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("nchar(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlNtext()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("ntext", 42, 42, 42);
			Assert.AreEqual("ntext", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(16, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("ntext", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlNumeric()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("numeric", 42, 20, 10);
			Assert.AreEqual("numeric", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(17, dl.Datatype.Length);
			Assert.AreEqual(20, dl.Datatype.Precision);
			Assert.AreEqual(10, dl.Datatype.Scale);
			Assert.AreEqual("numeric(20,10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlNvarchar()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("nvarchar", 10, 42, 42);
			Assert.AreEqual("nvarchar", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("nvarchar(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlReal()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("real", 42, 42, 42);
			Assert.AreEqual("real", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(4, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("real", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlSmalldatetime()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("smalldatetime", 42, 42, 42);
			Assert.AreEqual("smalldatetime", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(4, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("smalldatetime", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlSmallint()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("smallint", 42, 42, 42);
			Assert.AreEqual("smallint", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(2, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("smallint", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlSmallmoney()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("smallmoney", 42, 42, 42);
			Assert.AreEqual("smallmoney", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(4, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("smallmoney", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlText()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("text", 42, 42, 42);
			Assert.AreEqual("text", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(16, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("text", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlTimestamp()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("timestamp", 42, 42, 42);
			Assert.AreEqual("timestamp", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(8, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("timestamp", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlTinyint()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("tinyint", 42, 42, 42);
			Assert.AreEqual("tinyint", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(1, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("tinyint", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlUniqueidentifier()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("uniqueidentifier", 42, 42, 42);
			Assert.AreEqual("uniqueidentifier", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(16, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("uniqueidentifier", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlVarbinary()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("varbinary", 10, 42, 42);
			Assert.AreEqual("varbinary", dl.Datatype.Name);
			Assert.IsFalse(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("varbinary(10)", dl.Datatype.FullSqlName);
		}

		[Test]
		public void TestSqlVarchar()
		{
			g.DatatypeLoader dl = new g.DatatypeLoader("varchar", 10, 42, 42);			
			Assert.AreEqual("varchar", dl.Datatype.Name);
			Assert.IsTrue(dl.Datatype.IsTextType);
			Assert.AreEqual(10, dl.Datatype.Length);
			Assert.AreEqual(-1, dl.Datatype.Precision);
			Assert.AreEqual(-1, dl.Datatype.Scale);
			Assert.AreEqual("varchar(10)", dl.Datatype.FullSqlName);
		}
		#endregion 
	}
}
