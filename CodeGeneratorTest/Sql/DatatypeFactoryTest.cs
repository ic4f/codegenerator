using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class DatatypeFactoryTest
	{
		[Test]
		public void TestMakeDatatype()
		{
			g.Sql.BaseDatatype dt = g.Sql.DatatypeFactory.MakeDatatype("bigint");
			Assert.AreEqual(dt.Name, "bigint");

			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("bigint") is g.Sql.Bigint);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("binary") is g.Sql.Binary);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("bit") is g.Sql.Bit);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("char") is g.Sql.Char);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("datetime") is g.Sql.Datetime);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("decimal") is g.Sql.Decimal);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("float") is g.Sql.Float);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("image") is g.Sql.Image);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("int") is g.Sql.Int);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("money") is g.Sql.Money);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("nchar") is g.Sql.Nchar);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("ntext") is g.Sql.Ntext);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("numeric") is g.Sql.Numeric);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("nvarchar") is g.Sql.Nvarchar);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("real") is g.Sql.Real);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("smalldatetime") is g.Sql.Smalldatetime);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("smallint") is g.Sql.Smallint);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("smallmoney") is g.Sql.Smallmoney);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("text") is g.Sql.Text);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("timestamp") is g.Sql.Timestamp);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("tinyint") is g.Sql.Tinyint);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("uniqueidentifier") is g.Sql.Uniqueidentifier);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("varbinary") is g.Sql.Varbinary);
			Assert.IsTrue(g.Sql.DatatypeFactory.MakeDatatype("varchar") is g.Sql.Varchar);			
			Assert.IsFalse(g.Sql.DatatypeFactory.MakeDatatype("varchar") is g.Sql.Varbinary);
		}

		[Test]
		[ExpectedException(typeof(g.UnknownSqlDatatypeException))]
		public void TestUnknownDatatype()
		{
			g.Sql.DatatypeFactory.MakeDatatype("no such datatype");
		}
	}
}
