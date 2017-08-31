using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
	[TestFixture]
	public class AddFieldNodeTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			t.AddFieldNode n = new CodeGenerator.ParseTree.AddFieldNode(
				"name", "int", "select something", "name asc", "my name", true, false, true, true);

			Assert.AreEqual("name", n.Name);
			Assert.AreEqual("int", n.SqlDatatype);
			Assert.AreEqual("select something", n.Sql);
			Assert.AreEqual("name asc", n.SortExpression);
			Assert.AreEqual("my name", n.Display);
			Assert.IsTrue(n.ExcludeFromTable);
			Assert.IsFalse(n.IncludeWithParentTable);
		}

		[Test]
		[ExpectedException(typeof(g.UnknownSqlDatatypeException))]
		public void TestInvalidDatatype()
		{
			t.AddFieldNode n = new CodeGenerator.ParseTree.AddFieldNode(
				"name", "wrong", "select something", "name asc", "my name", true, false, true, true);
			n.Validate();
		}
	}
}