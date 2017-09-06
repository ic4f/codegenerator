using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
    [TestFixture]
    public class FieldNodeTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            t.FieldNode n = new CodeGenerator.ParseTree.FieldNode(
                "name", "varchar(50)", true, true, "table", "field", false, false, "my name", true, false, true, true, null);

            Assert.AreEqual("name", n.Name);
            Assert.AreEqual("varchar(50)", n.SqlDatatype);
            Assert.IsTrue(n.IsIdentity);
            Assert.IsTrue(n.IsPrimaryKey);
            Assert.AreEqual("table", n.RefTable);
            Assert.AreEqual("field", n.RefField);
            Assert.IsFalse(n.IsUnique);
            Assert.IsFalse(n.IsEncrypted);
            Assert.AreEqual("my name", n.Display);
            Assert.IsTrue(n.ExcludeFromTable);
            Assert.IsFalse(n.IncludeWithParentTable);
            Assert.IsNull(n.ReadonlyType);
            Assert.IsTrue(n.IncludeInList);
            Assert.IsTrue(n.IsDefaultSort);
        }

        [Test]
        [ExpectedException(typeof(g.InvalidEncryptionFormatException))]
        public void TestInvalidEncryptionFormat()
        {
            t.FieldNode n = new CodeGenerator.ParseTree.FieldNode(
                "name", "varbinary(7)", true, true, "table", "field", false, true, "my node", true, false, true, true, null);
            n.Validate();
        }
    }
}