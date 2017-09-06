using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
    [TestFixture]
    public class NamespaceNodeTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            t.NamespaceNode n = new t.NamespaceNode("name1");
            Assert.AreEqual("name1", n.Name);
        }

        [Test]
        public void TestClasses()
        {
            t.NamespaceNode n = new t.NamespaceNode("name1");
            t.ClassNode c1 = new CodeGenerator.ParseTree.ClassNode("name1", "record");
            t.ClassNode c2 = new CodeGenerator.ParseTree.ClassNode("name2", "record");
            n.AddClass(c1);
            n.AddClass(c2);
            Assert.AreEqual(2, n.ClassNodesHash.Count);
            Assert.AreEqual(2, n.ClassNodesList.Count);
            Assert.AreSame(c1, (t.ClassNode)n.ClassNodesHash["name1"]);
            Assert.AreSame(c2, (t.ClassNode)n.ClassNodesHash["name2"]);
            Assert.AreSame(c1, (t.ClassNode)n.ClassNodesList[0]);
            Assert.AreSame(c2, (t.ClassNode)n.ClassNodesList[1]);
        }

        [Test]
        [ExpectedException(typeof(g.DuplicateClassException))]
        public void TestDuplicateClasses()
        {
            t.NamespaceNode n = new t.NamespaceNode("name1");
            n.AddClass(new t.ClassNode("a", "record"));
            n.AddClass(new t.ClassNode("a", "record"));
        }
    }
}
