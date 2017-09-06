using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class SprocParamObjectTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            a.NamespaceObject ns = new a.NamespaceObject("a");
            a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
            ns.AddClass(c);
            a.SprocObject s = new a.SprocObject(c, "sproc", "int");
            c.AddSproc(s);
            a.SprocParamObject p = new a.SprocParamObject(s, "p1", "int", false);
            s.AddParam(p);

            Assert.AreEqual("p1", p.Name);
            Assert.AreEqual("int", p.CsDatatype);
            Assert.IsFalse(p.IsEncrypted);
            Assert.AreSame(s, p.Sproc);
        }
    }
}