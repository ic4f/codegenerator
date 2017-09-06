using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
    [TestFixture]
    public class ClassNodeTest : BaseTest
    {
        [Test]
        public void TestProperties()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "record");
            Assert.AreEqual("a", n.Name);
            Assert.AreEqual("record", n.Type);

            t.TableNode tn = new CodeGenerator.ParseTree.TableNode("name", true);
            n.Table = tn;
            Assert.AreSame(tn, n.Table);
        }

        [Test]
        public void TestSprocs()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "record");
            t.AddSprocNode s1 = new CodeGenerator.ParseTree.AddSprocNode("a", "int");
            t.AddSprocNode s2 = new CodeGenerator.ParseTree.AddSprocNode("b", "int");
            n.AddAdditionalSproc(s1);
            n.AddAdditionalSproc(s2);
            Assert.AreEqual(2, n.AddSprocNodesHash.Count);
            Assert.AreEqual(2, n.AddSprocNodesList.Count);
            Assert.AreSame(s1, (t.AddSprocNode)n.AddSprocNodesHash["a"]);
            Assert.AreSame(s2, (t.AddSprocNode)n.AddSprocNodesHash["b"]);
            Assert.AreSame(s1, (t.AddSprocNode)n.AddSprocNodesList[0]);
            Assert.AreSame(s2, (t.AddSprocNode)n.AddSprocNodesList[1]);
        }

        [Test]
        [ExpectedException(typeof(g.DuplicateSprocException))]
        public void TestDuplicateSproc()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "record");
            n.AddAdditionalSproc(new t.AddSprocNode("a", "int"));
            n.AddAdditionalSproc(new t.AddSprocNode("a", "int"));
        }

        [Test]
        [ExpectedException(typeof(g.MultiplePrimaryKeysException))]
        public void Test3PrimaryKeysLinkTable()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "link");
            t.TableNode tn = new CodeGenerator.ParseTree.TableNode("name", true);
            tn.AddField(new t.FieldNode("a", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            tn.AddField(new t.FieldNode("b", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            tn.AddField(new t.FieldNode("c", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            n.Table = tn;
            n.Validate();
        }

        [Test]
        [ExpectedException(typeof(g.MultiplePrimaryKeysException))]
        public void Test2PrimaryKeysRecordTable()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "record");
            t.TableNode tn = new CodeGenerator.ParseTree.TableNode("name", true);
            tn.AddField(new t.FieldNode("a", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            tn.AddField(new t.FieldNode("b", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            n.Table = tn;
            n.Validate();
        }

        [Test]
        [ExpectedException(typeof(g.InvalidReturnFieldException))]
        public void TestInvalidReturnField()
        {
            t.ClassNode n = new CodeGenerator.ParseTree.ClassNode("a", "record");
            t.TableNode tn = new CodeGenerator.ParseTree.TableNode("name", true);
            n.Table = tn;
            tn.AddField(new t.FieldNode("a", "int", false, true, "", "", false, false, "", false, false, true, true, ""));
            t.AddSprocNode sn = new CodeGenerator.ParseTree.AddSprocNode("sproc", "int");
            sn.AddReturnField(new t.SprocReturnFieldNode("wrong"));
            n.AddAdditionalSproc(sn);
            n.Validate();
        }
    }
}
