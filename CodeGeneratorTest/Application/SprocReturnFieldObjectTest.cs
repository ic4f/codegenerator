using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class SprocReturnFieldObjectTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            a.NamespaceObject ns = new a.NamespaceObject("a");
            a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
            ns.AddClass(c);
            a.SprocObject s = new a.SprocObject(c, "sproc", "int");
            c.AddSproc(s);
            a.SprocReturnFieldObject f = new a.SprocReturnFieldObject(s, "a", "int", "b", "c");
            s.AddReturnField(f);

            Assert.AreEqual("a", f.Name);
            Assert.AreEqual("int", f.CsDatatype);
            Assert.AreEqual("b", f.SortExpression);
            Assert.AreEqual("c", f.Display);
            Assert.AreSame(s, f.Sproc);
        }
    }
}