using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
	[TestFixture]
	public class SprocReturnFieldNodeTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			t.SprocReturnFieldNode n = new CodeGenerator.ParseTree.SprocReturnFieldNode("a");
			Assert.AreEqual("a", n.Name);
		}
	}
}
