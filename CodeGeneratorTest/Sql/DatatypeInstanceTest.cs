using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	[TestFixture]
	public class DatatypeInstanceTest
	{
		[Test]
		public void TestProperties()
		{
			g.Sql.DatatypeInstance di = new CodeGenerator.Sql.DatatypeInstance(
				"varchar", "varchar(50)", 50, 1, 2, true);

			Assert.AreEqual("varchar", di.Name);
			Assert.AreEqual("varchar(50)", di.FullSqlName);
			Assert.AreEqual(50, di.Length);
			Assert.AreEqual(1, di.Precision);
			Assert.AreEqual(2, di.Scale);
			Assert.IsTrue(di.IsTextType);
		}
	}
}