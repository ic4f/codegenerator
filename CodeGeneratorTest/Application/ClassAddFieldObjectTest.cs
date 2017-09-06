using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class ClassAddFieldObjectTest : BaseTest
    {
        [Test]
        public void TestGetReadonlyTypeClassType()
        {
            a.NamespaceObject ns = new a.NamespaceObject("a");
            a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
            ns.AddClass(c);
            g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
            a.ClassAddFieldObject f = new a.ClassAddFieldObject(c, "a", "b", "c", "d", true, true, true, true, di);

            Assert.AreEqual("a", f.Name);
            Assert.AreEqual("b", f.Sql);
            Assert.AreEqual("c", f.SortExpression);
            Assert.AreEqual("d", f.Display);
            Assert.IsTrue(f.IncludeWithParentTable);
            Assert.IsTrue(f.ExcludeFromTable);
            Assert.AreSame(di, f.Datatype);
            Assert.AreSame(c, f.ParentClass);
            Assert.AreEqual("a", f.SqlName);
            Assert.IsTrue(f.IncludeInList);
            Assert.IsTrue(f.IsDefaultSort);
        }
    }
}