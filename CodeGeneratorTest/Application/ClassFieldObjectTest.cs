using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class ClassFieldObjectTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            a.NamespaceObject ns = new a.NamespaceObject("a");
            a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
            ns.AddClass(c);
            g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
            a.ClassFieldObject f = new a.ClassFieldObject(
                c, "User", true, true, "b", "c", true, false, "", true, true, true, true, a.ReadonlyFieldType.Created, di);

            Assert.AreSame(c, f.ParentClass);
            Assert.AreEqual("User", f.Name);
            Assert.AreEqual("[User]", f.SqlName);
            Assert.AreSame(di, f.Datatype);
            Assert.AreEqual("User", f.Display);
            Assert.IsTrue(f.IncludeWithParentTable);
            Assert.IsTrue(f.ExcludeFromTable);
            Assert.AreEqual("[User]", f.SortExpression);
            Assert.AreEqual("t.[User] AS t_User", f.Sql);
            Assert.IsTrue(f.IsIdentity);
            Assert.IsTrue(f.IsPrimaryKey);
            Assert.IsTrue(f.IsForeignKey);
            Assert.IsNull(f.ReferencedField);
            Assert.IsNull(f.ReferencedClass);
            Assert.IsTrue(f.IsUnique);
            Assert.IsFalse(f.IsEncrypted);
            Assert.IsTrue(f.IsCreatedType);
            Assert.IsFalse(f.IsModifiedType);
            Assert.IsFalse(f.IsTimestampType);
            Assert.IsTrue(f.IncludeInList);
            Assert.IsTrue(f.IsDefaultSort);
        }

        [Test]
        public void TestLoadReference()
        {
            a.ApplicationObject app = new CodeGenerator.Application.ApplicationObject();
            a.NamespaceObject ns = new a.NamespaceObject("a");
            app.AddNamespace(ns);
            a.ClassObject c1 = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t1", "t1");
            ns.AddClass(c1);
            a.ClassObject c2 = new a.ClassObject(ns, "b", a.ClassType.Record, false, "t2", "t2");
            ns.AddClass(c2);

            g.Sql.DatatypeInstance di = new CodeGenerator.DatatypeLoader("int").Datatype;
            a.ClassFieldObject f1 = new a.ClassFieldObject(
                c1, "class1field1", true, true, null, null, false, false, "d", true, true, true, true, a.ReadonlyFieldType.Undefined, di);
            c1.AddField(f1);
            a.ClassFieldObject f2 = new a.ClassFieldObject(
                c1, "class2field1", true, true, "t1", "class1field1", false, false, "d", true, true, true, true, a.ReadonlyFieldType.Undefined, di);
            c2.AddField(f2);

            f2.LoadReference(app);
            Assert.AreSame(c1, f2.ReferencedClass);
            Assert.AreSame(f1, f2.ReferencedField);
        }
    }
}