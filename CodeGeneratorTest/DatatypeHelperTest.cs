using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;

namespace CodeGeneratorTest
{
    [TestFixture]
    public class DatatypeHelperTest
    {
        [Test]
        public void TestIsConvertibleToSqlType()
        {
            Assert.IsTrue(csHelper.IsSqlConvertible("bool"));
            Assert.IsTrue(csHelper.IsSqlConvertible("byte"));
            Assert.IsTrue(csHelper.IsSqlConvertible("sbyte"));
            Assert.IsTrue(csHelper.IsSqlConvertible("char"));
            Assert.IsTrue(csHelper.IsSqlConvertible("decimal"));
            Assert.IsTrue(csHelper.IsSqlConvertible("double"));
            Assert.IsTrue(csHelper.IsSqlConvertible("float"));
            Assert.IsTrue(csHelper.IsSqlConvertible("int"));
            Assert.IsTrue(csHelper.IsSqlConvertible("uint"));
            Assert.IsTrue(csHelper.IsSqlConvertible("long"));
            Assert.IsTrue(csHelper.IsSqlConvertible("ulong"));
            Assert.IsTrue(csHelper.IsSqlConvertible("short"));
            Assert.IsTrue(csHelper.IsSqlConvertible("ushort"));
            Assert.IsTrue(csHelper.IsSqlConvertible("string"));
            Assert.IsTrue(csHelper.IsSqlConvertible("DateTime"));
        }

        [Test]
        public void TestIsValidSprocReturnType()
        {
            Assert.IsTrue(csHelper.IsValidSprocReturnType("generate"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("DataSet"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("ArrayList"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("void"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("bool"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("byte"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("sbyte"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("char"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("decimal"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("double"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("float"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("int"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("uint"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("long"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("ulong"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("short"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("ushort"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("string"));
            Assert.IsTrue(csHelper.IsValidSprocReturnType("DateTime"));
        }

        [SetUp]
        public void SetUp()
        {
            csHelper = new g.DatatypeHelper();
        }

        private g.DatatypeHelper csHelper;
    }
}