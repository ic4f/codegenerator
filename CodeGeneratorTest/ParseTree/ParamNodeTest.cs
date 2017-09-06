using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
    [TestFixture]
    public class ParamNodeTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            t.ParamNode n = new CodeGenerator.ParseTree.ParamNode("name", "int", true);
            Assert.AreEqual("name", n.Name);
            Assert.AreEqual("int", n.CsDatatype);
            Assert.IsTrue(n.IsEncrypted);
        }

        [Test]
        [ExpectedException(typeof(g.UnknownCsDatatypeException))]
        public void TestNotValidatCsDatatype()
        {
            t.ParamNode n = new CodeGenerator.ParseTree.ParamNode("name", "wrong", true);
            n.Validate();
        }
    }
}
