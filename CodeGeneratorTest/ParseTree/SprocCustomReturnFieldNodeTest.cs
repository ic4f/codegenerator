using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
	[TestFixture]
	public class SprocCustomReturnFieldNodeTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			t.SprocCustomReturnFieldNode n = new CodeGenerator.ParseTree.SprocCustomReturnFieldNode(
				"a", "int", "b", "c");
			Assert.AreEqual("a", n.Name);
			Assert.AreEqual("int", n.CsDatatype);
			Assert.AreEqual("b", n.SortExpression);
			Assert.AreEqual("c", n.Display);			
		}

		[Test]
		[ExpectedException(typeof(g.UnknownCsDatatypeException))]
		public void TestNotValidatCsDatatype()
		{
			t.SprocCustomReturnFieldNode n = new CodeGenerator.ParseTree.SprocCustomReturnFieldNode(
				"a", "wrong", "b", "c");
			n.Validate();
		}
	}
}
