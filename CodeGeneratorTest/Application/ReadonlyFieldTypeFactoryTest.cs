using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
    [TestFixture]
    public class ReadonlyFieldTypeFactoryTest : BaseTest
    {
        [Test]
        public void TestGetReadonlyTypeClassType()
        {
            Assert.AreEqual(a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType("created"), a.ReadonlyFieldType.Created);
            Assert.AreEqual(a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType("modified"), a.ReadonlyFieldType.Modified);
            Assert.AreEqual(a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType("timestamp"), a.ReadonlyFieldType.Timestamp);
            Assert.AreEqual(a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType(null), a.ReadonlyFieldType.Undefined);
            Assert.AreEqual(a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType(""), a.ReadonlyFieldType.Undefined);
        }

        [Test]
        [ExpectedException(typeof(g.UnknownReadonlyFieldTypeException))]
        public void TestUnknownReadonlyFieldType()
        {
            a.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType("wrong");
        }
    }
}