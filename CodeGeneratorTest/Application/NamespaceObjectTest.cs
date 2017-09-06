using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class NamespaceObjectTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            a.NamespaceObject ns = new a.NamespaceObject("a");
            Assert.AreEqual("a", ns.Name);
        }

        [Test]
        public void TestClasses()
        {
            a.NamespaceObject n = new a.NamespaceObject("a");
            a.ClassObject c1 = new a.ClassObject(n, "class1", a.ClassType.Link, false, "tbl1", "tbl");
            a.ClassObject c2 = new a.ClassObject(n, "class2", a.ClassType.Final, false, "tbl1", "tbl");
            a.ClassObject c3 = new a.ClassObject(n, "class3", a.ClassType.Record, false, "tbl1", "tbl");
            a.ClassObject c4 = new a.ClassObject(n, "class4", a.ClassType.Readonly, false, "tbl1", "tbl");
            n.AddClass(c1);
            n.AddClass(c2);
            n.AddClass(c3);
            n.AddClass(c4);

            Assert.AreEqual(4, n.ClassesHash.Count);
            Assert.AreEqual(4, n.ClassesList.Count);
            Assert.AreEqual(1, n.LinkClassesHash.Count);
            Assert.AreEqual(1, n.LinkClassesList.Count);
            Assert.AreEqual(3, n.NonLinkClassesHash.Count);
            Assert.AreEqual(3, n.NonLinkClassesList.Count);

            Assert.AreSame(c1, (a.ClassObject)n.ClassesHash["class1"]);
            Assert.AreSame(c2, (a.ClassObject)n.ClassesHash["class2"]);
            Assert.AreSame(c3, (a.ClassObject)n.ClassesHash["class3"]);
            Assert.AreSame(c4, (a.ClassObject)n.ClassesHash["class4"]);
            Assert.AreSame(c1, (a.ClassObject)n.ClassesList[0]);
            Assert.AreSame(c2, (a.ClassObject)n.ClassesList[1]);
            Assert.AreSame(c3, (a.ClassObject)n.ClassesList[2]);
            Assert.AreSame(c4, (a.ClassObject)n.ClassesList[3]);

            Assert.AreSame(c1, (a.ClassObject)n.LinkClassesHash["class1"]);
            Assert.AreSame(c1, (a.ClassObject)n.LinkClassesList[0]);

            Assert.AreSame(c2, (a.ClassObject)n.NonLinkClassesHash["class2"]);
            Assert.AreSame(c3, (a.ClassObject)n.NonLinkClassesHash["class3"]);
            Assert.AreSame(c4, (a.ClassObject)n.NonLinkClassesHash["class4"]);
            Assert.AreSame(c2, (a.ClassObject)n.NonLinkClassesList[0]);
            Assert.AreSame(c3, (a.ClassObject)n.NonLinkClassesList[1]);
            Assert.AreSame(c4, (a.ClassObject)n.NonLinkClassesList[2]);
        }
    }
}