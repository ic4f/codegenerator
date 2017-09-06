using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class ApplicationObjectTest : BaseTest
    {
        [Test]
        public void TestNamespaces()
        {
            a.ApplicationObject x = new CodeGenerator.Application.ApplicationObject();
            a.NamespaceObject n1 = new a.NamespaceObject("a");
            a.NamespaceObject n2 = new a.NamespaceObject("b");
            x.AddNamespace(n1);
            x.AddNamespace(n2);
            Assert.AreEqual(2, x.NamespacesHash.Count);
            Assert.AreEqual(2, x.NamespacesList.Count);
            Assert.AreSame(n1, (a.NamespaceObject)x.NamespacesHash["a"]);
            Assert.AreSame(n2, (a.NamespaceObject)x.NamespacesHash["b"]);
            Assert.AreSame(n1, (a.NamespaceObject)x.NamespacesList[0]);
            Assert.AreSame(n2, (a.NamespaceObject)x.NamespacesList[1]);
        }

        [Test]
        public void TestGetClassByTableName()
        {
            a.ApplicationObject app = new CodeGenerator.Application.ApplicationObject();
            a.NamespaceObject n1 = new a.NamespaceObject("a");
            a.NamespaceObject n2 = new a.NamespaceObject("b");
            app.AddNamespace(n1);
            app.AddNamespace(n2);
            a.ClassObject c1 = new a.ClassObject(n1, "class1", a.ClassType.Record, false, "tbl1", "tbl");
            a.ClassObject c2 = new a.ClassObject(n2, "class2", a.ClassType.Record, false, "tbl2", "tbl");
            n1.AddClass(c1);
            n2.AddClass(c2);
            Assert.AreSame(c2, app.GetClassByTableName("tbl2"));
        }
    }
}