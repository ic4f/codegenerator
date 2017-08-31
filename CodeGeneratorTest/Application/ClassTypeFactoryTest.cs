using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using a = CodeGenerator.Application;

namespace CodeGeneratorTest.Application
{
	[TestFixture]
	public class ClassTypeFactoryTest : BaseTest
	{
		[Test]
		public void TestGetReadonlyTypeClassType()
		{
			Assert.AreEqual(a.ClassTypeFactory.GetClassType("record"), a.ClassType.Record);
			Assert.AreEqual(a.ClassTypeFactory.GetClassType("link"), a.ClassType.Link);
			Assert.AreEqual(a.ClassTypeFactory.GetClassType("readonly"), a.ClassType.Readonly);
			Assert.AreEqual(a.ClassTypeFactory.GetClassType("final"), a.ClassType.Final);
		}

		[Test]
		[ExpectedException(typeof(g.UnknownClassTypeException))]
		public void TestUnknownClassType()
		{
			a.ClassTypeFactory.GetClassType("wrong");
		}
	}
}