using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
    [TestFixture]
    public class TableNodeTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            Assert.AreEqual("name", n.Name);
            Assert.IsFalse(n.IsExternal);
        }

        [Test]
        public void TestFields()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            t.FieldNode f1 = new t.FieldNode("a", "int", false, false, "", "", false, false, "", false, false, true, true, "");
            t.FieldNode f2 = new t.FieldNode("b", "int", false, false, "", "", false, false, "", false, false, true, true, "");
            n.AddField(f1);
            n.AddField(f2);
            Assert.AreEqual(2, n.FieldNodesHash.Count);
            Assert.AreEqual(2, n.FieldNodesList.Count);
            Assert.AreSame(f1, (t.FieldNode)n.FieldNodesHash["a"]);
            Assert.AreSame(f2, (t.FieldNode)n.FieldNodesHash["b"]);
            Assert.AreSame(f1, (t.FieldNode)n.FieldNodesList[0]);
            Assert.AreSame(f2, (t.FieldNode)n.FieldNodesList[1]);
        }

        [Test]
        public void TestAdditionalFields()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            t.AddFieldNode f1 = new t.AddFieldNode("a", "int", "", "", "", false, false, true, true);
            t.AddFieldNode f2 = new t.AddFieldNode("b", "int", "", "", "", false, false, true, true);
            n.AddAdditionalField(f1);
            n.AddAdditionalField(f2);
            Assert.AreEqual(2, n.AddFieldNodesHash.Count);
            Assert.AreEqual(2, n.AddFieldNodesList.Count);
            Assert.AreSame(f1, (t.AddFieldNode)n.AddFieldNodesHash["a"]);
            Assert.AreSame(f2, (t.AddFieldNode)n.AddFieldNodesHash["b"]);
            Assert.AreSame(f1, (t.AddFieldNode)n.AddFieldNodesList[0]);
            Assert.AreSame(f2, (t.AddFieldNode)n.AddFieldNodesList[1]);
        }

        [Test]
        [ExpectedException(typeof(g.DuplicateFieldException))]
        public void TestDuplicateField()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            n.AddField(new t.FieldNode("a", "int", false, false, "", "", false, false, "", false, false, true, true, ""));
            n.AddField(new t.FieldNode("a", "int", false, false, "", "", false, false, "", false, false, true, true, ""));
        }

        [Test]
        [ExpectedException(typeof(g.DuplicateAdditionalFieldException))]
        public void TestDuplicateAdditionalField()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            n.AddAdditionalField(new t.AddFieldNode("a", "int", "", "", "", false, false, true, true));
            n.AddAdditionalField(new t.AddFieldNode("a", "int", "", "", "", false, false, true, true));
        }

        [Test]
        [ExpectedException(typeof(g.DuplicateFieldAdditionalFieldException))]
        public void TestDuplicateFieldOrAdditionalField()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            n.AddField(new t.FieldNode("a", "int", false, false, "", "", false, false, "", false, false, true, true, ""));
            n.AddAdditionalField(new t.AddFieldNode("a", "int", "", "", "", false, false, true, true));
            n.Validate();
        }

        [Test]
        [ExpectedException(typeof(g.MultipleIdentityException))]
        public void TestMultipleIdentity()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            n.AddField(new t.FieldNode("a", "int", true, false, "", "", false, false, "", false, false, true, true, ""));
            n.AddField(new t.FieldNode("b", "int", true, false, "", "", false, false, "", false, false, true, true, ""));
            n.Validate();
        }

        [Test]
        [ExpectedException(typeof(g.ExceededTableRowCapacityException))]
        public void TestExceededTableRowCapacity()
        {
            t.TableNode n = new CodeGenerator.ParseTree.TableNode("name", false);
            n.AddField(new t.FieldNode("a", "varchar(7960)", false, false, "", "", false, false, "", false, false, true, true, ""));
            n.AddField(new t.FieldNode("b", "varchar(1)", false, false, "", "", false, false, "", false, false, true, true, ""));
            n.Validate();
        }
    }
}
