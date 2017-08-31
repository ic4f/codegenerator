using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest.ParseTree
{
	[TestFixture]
	public class AddSprocNodeTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");

			Assert.AreEqual("name", n.Name);
			Assert.AreEqual("int", n.ReturnType);
		}

		[Test]
		public void TestParams()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			t.ParamNode p1 = new CodeGenerator.ParseTree.ParamNode("name1", "int", false);
			t.ParamNode p2 = new CodeGenerator.ParseTree.ParamNode("name2", "int", false);
			n.AddParam(p1);
			n.AddParam(p2);
			Assert.AreEqual(2, n.ParamNodesHash.Count);
			Assert.AreEqual(2, n.ParamNodesList.Count);
			Assert.AreSame(p1, (t.ParamNode)n.ParamNodesHash["name1"]);
			Assert.AreSame(p2, (t.ParamNode)n.ParamNodesHash["name2"]);
			Assert.AreSame(p1, (t.ParamNode)n.ParamNodesList[0]);
			Assert.AreSame(p2, (t.ParamNode)n.ParamNodesList[1]);
		}

		[Test]
		public void TestReturnFields()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			t.SprocReturnFieldNode f1 = new CodeGenerator.ParseTree.SprocReturnFieldNode("name1");
			t.SprocReturnFieldNode f2 = new CodeGenerator.ParseTree.SprocReturnFieldNode("name2");
			t.SprocCustomReturnFieldNode cf1 = new CodeGenerator.ParseTree.SprocCustomReturnFieldNode(
				"name3", "int", "a", "a");
			t.SprocCustomReturnFieldNode cf2 = new CodeGenerator.ParseTree.SprocCustomReturnFieldNode(
				"name4", "int", "a", "a");
			n.AddReturnField(f1);
			n.AddReturnField(f2);
			n.AddCustomReturnField(cf1);
			n.AddCustomReturnField(cf2);

			Assert.AreEqual(2, n.ReturnFieldNodesHash.Count);
			Assert.AreEqual(2, n.CustomReturnFieldNodesHash.Count);
			Assert.AreEqual(4, n.AllReturnFieldsList.Count);
			Assert.AreSame(f1, (t.SprocReturnFieldNode)n.ReturnFieldNodesHash["name1"]);
			Assert.AreSame(f2, (t.SprocReturnFieldNode)n.ReturnFieldNodesHash["name2"]);
			Assert.AreSame(cf1, (t.SprocCustomReturnFieldNode)n.CustomReturnFieldNodesHash["name3"]);
			Assert.AreSame(cf2, (t.SprocCustomReturnFieldNode)n.CustomReturnFieldNodesHash["name4"]);
			Assert.AreSame(f1, (t.SprocReturnFieldNode)n.AllReturnFieldsList[0]);
			Assert.AreSame(f2, (t.SprocReturnFieldNode)n.AllReturnFieldsList[1]);
			Assert.AreSame(cf1, (t.SprocCustomReturnFieldNode)n.AllReturnFieldsList[2]);
			Assert.AreSame(cf2, (t.SprocCustomReturnFieldNode)n.AllReturnFieldsList[3]);
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateReturnFieldException))]
		public void TestDuplicateReturnField()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			n.AddReturnField(new t.SprocReturnFieldNode("a"));
			n.AddReturnField(new t.SprocReturnFieldNode("a"));
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateCustomReturnFieldException))]
		public void TestDuplicateCustomReturnField()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			n.AddCustomReturnField(new t.SprocCustomReturnFieldNode("a", "int", "a", "a"));
			n.AddCustomReturnField(new t.SprocCustomReturnFieldNode("a", "int", "a", "a"));
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateReturnFieldCustomReturnFieldException))]
		public void TestDuplicateReturnOrCustomReturnField()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "generate");
			n.AddReturnField(new t.SprocReturnFieldNode("a"));
			n.AddCustomReturnField(new t.SprocCustomReturnFieldNode("a", "int", "a", "a"));
			n.Validate();
		}
		
		[Test]
		[ExpectedException(typeof(g.DuplicateParamException))]
		public void TestDuplicateParam()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			n.AddParam(new t.ParamNode("a", "int", false));
			n.AddParam(new t.ParamNode("a", "int", false));
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocReturnTypeException))]
		public void TestInvalidSprocReturnType()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "wrong");
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestNoReturnFieldsWithGenerateReturnType()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "generate");
			n.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestReturnFieldsWithNotGenerateReturnType()
		{
			t.AddSprocNode n = new CodeGenerator.ParseTree.AddSprocNode("name", "int");
			n.AddReturnField(new t.SprocReturnFieldNode("a"));
			n.Validate();
		}
	}
}