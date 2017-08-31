using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
	[TestFixture]
	public class ApplicationNodeTest : BaseTest
	{
		[Test]
		public void TestNamespaces()
		{
			t.ApplicationNode n = new CodeGenerator.ParseTree.ApplicationNode();
			t.NamespaceNode ns1 = new t.NamespaceNode("name1");
			t.NamespaceNode ns2 = new t.NamespaceNode("name2");
			n.AddNamespace(ns1);
			n.AddNamespace(ns2);
			Assert.AreEqual(2, n.NamespaceNodesHash.Count);
			Assert.AreEqual(2, n.NamespaceNodesList.Count);
			Assert.AreSame(ns1, (t.NamespaceNode)n.NamespaceNodesHash["name1"]);
			Assert.AreSame(ns2, (t.NamespaceNode)n.NamespaceNodesHash["name2"]);
			Assert.AreSame(ns1, (t.NamespaceNode)n.NamespaceNodesList[0]);
			Assert.AreSame(ns2, (t.NamespaceNode)n.NamespaceNodesList[1]);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateNamespaceException))]
		public void TestDuplicateNamespace()
		{
			t.ApplicationNode n = new CodeGenerator.ParseTree.ApplicationNode();
			n.AddNamespace(new t.NamespaceNode("a"));
			n.AddNamespace(new t.NamespaceNode("a"));
		}
	}
}
