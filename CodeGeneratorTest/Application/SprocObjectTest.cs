using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
	[TestFixture]
	public class SprocObjectTest : BaseTest
	{
		[Test]
		public void TestProperties()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
			ns.AddClass(c);
			a.SprocObject s = new a.SprocObject(c, "sproc", "int");
			c.AddSproc(s);

			Assert.AreEqual("sproc", s.Name);
			Assert.AreEqual("int", s.ReturnType);
			Assert.IsFalse(s.IsCustomReturnType);
			Assert.AreSame(c, s.Class);
		}

		[Test]
		public void TestParams()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
			ns.AddClass(c);
			a.SprocObject s = new a.SprocObject(c, "sproc", "int");
			c.AddSproc(s);
			a.SprocParamObject p1 = new a.SprocParamObject(s, "p1", "int", false);
			a.SprocParamObject p2 = new a.SprocParamObject(s, "p2", "int", false);
			s.AddParam(p1);
			s.AddParam(p2);

			Assert.AreEqual(2, s.ParamsHash.Count);
			Assert.AreEqual(2, s.ParamsList.Count);
			Assert.AreSame(p1, (a.SprocParamObject)s.ParamsHash["p1"]);
			Assert.AreSame(p2, (a.SprocParamObject)s.ParamsHash["p2"]);
			Assert.AreSame(p1, (a.SprocParamObject)s.ParamsList[0]);
			Assert.AreSame(p2, (a.SprocParamObject)s.ParamsList[1]);				
		}

		[Test]
		public void TestReturnFields()
		{
			a.NamespaceObject ns = new a.NamespaceObject("a");
			a.ClassObject c = new a.ClassObject(ns, "a", a.ClassType.Record, false, "t", "t");
			ns.AddClass(c);
			a.SprocObject s = new a.SprocObject(c, "sproc", "int");
			c.AddSproc(s);
			a.SprocReturnFieldObject f1 = new a.SprocReturnFieldObject(s, "a1", "int", "b", "c");
			a.SprocReturnFieldObject f2 = new a.SprocReturnFieldObject(s, "a2", "int", "b", "c");
			s.AddReturnField(f1);
			s.AddReturnField(f2);

			Assert.AreEqual(2, s.ReturnFieldsHash.Count);
			Assert.AreEqual(2, s.ReturnFieldsList.Count);
			Assert.AreSame(f1, (a.SprocReturnFieldObject)s.ReturnFieldsHash["a1"]);
			Assert.AreSame(f2, (a.SprocReturnFieldObject)s.ReturnFieldsHash["a2"]);
			Assert.AreSame(f1, (a.SprocReturnFieldObject)s.ReturnFieldsList[0]);
			Assert.AreSame(f2, (a.SprocReturnFieldObject)s.ReturnFieldsList[1]);	
		}
	}
}