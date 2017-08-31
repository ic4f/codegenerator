using System;
using System.Collections;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
	[TestFixture]
	public class SchemaValidatorTest
	{
		[Test]
		[ExpectedException(typeof(g.DuplicateNamespaceException))]
		public void TestDuplicateNamespaceNames()
		{
			sv = getValidator("schemaValidatorTest1.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateClassException))]
		public void TestDuplicateClassNames()
		{
			sv = getValidator("schemaValidatorTest2.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateTableException))]
		public void TestDuplicateTableNames()
		{
			sv = getValidator("schemaValidatorTest3.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateTableException))]
		public void TestDuplicateTableNamesDiffNamespaces()
		{
			sv = getValidator("schemaValidatorTest7.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateFieldException))]
		public void TestDuplicateFieldNames()
		{
			sv = getValidator("schemaValidatorTest4.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateAdditionalFieldException))]
		public void TestDuplicateAdditionalFieldNames()
		{
			sv = getValidator("schemaValidatorTest5.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateFieldAdditionalFieldException))]
		public void TestDuplicateFieldAndAdditionalFieldNames()
		{
			sv = getValidator("schemaValidatorTest6.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateSprocException))]
		public void TestDuplicateSprocNames()
		{
			sv = getValidator("schemaValidatorTest9.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateParamException))]
		public void TestDuplicateParamNames()
		{
			sv = getValidator("schemaValidatorTest8.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateReturnFieldException))]
		public void TestDuplicateReturnFieldNames()
		{
			sv = getValidator("schemaValidatorTest10.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateCustomReturnFieldException))]
		public void TestDuplicateCustomReturnFieldNames()
		{
			sv = getValidator("schemaValidatorTest11.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.DuplicateReturnFieldCustomReturnFieldException))]
		public void TestDuplicateReturnFieldAndCustomReturnFieldNames()
		{
			sv = getValidator("schemaValidatorTest12.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.UnknownClassTypeException))]
		public void TestInvalidClassType()
		{
			sv = getValidator("schemaValidatorTest13.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.MultipleIdentityException))]
		public void TestMultipleIdentityFields()
		{
			sv = getValidator("schemaValidatorTest14.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidEncryptionFormatException))]
		public void TestInvalidEncryptionDatatype()
		{
			sv = getValidator("schemaValidatorTest15.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidEncryptionFormatException))]
		public void TestInvalidEncryptionDatatypeLength()
		{
			sv = getValidator("schemaValidatorTest16.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.UnknownReadonlyFieldTypeException))]
		public void TestInvalidReadonlyFieldType()
		{
			sv = getValidator("schemaValidatorTest17.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.ExceededTableRowCapacityException))]
		public void TestExceededTableRowCapacity()
		{
			sv = getValidator("schemaValidatorTest18.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.MultiplePrimaryKeysException))]
		public void TestMultiplePrimaryKeysRecordTable()
		{
			sv = getValidator("schemaValidatorTest19.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.MultiplePrimaryKeysException))]
		public void TestMultiplePrimaryKeysLinkTable()
		{
			sv = getValidator("schemaValidatorTest20.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidReturnFieldException))]
		public void TestInvalidReturnFields()
		{
			sv = getValidator("schemaValidatorTest21.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestInvalidSprocFormatVoidReturn()
		{
			sv = getValidator("schemaValidatorTest22.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestInvalidSprocFormatScalarReturn1()
		{
			sv = getValidator("schemaValidatorTest23.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestInvalidSprocFormatScalarReturn2()
		{
			sv = getValidator("schemaValidatorTest24.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.InvalidSprocFormatException))]
		public void TestInvalidSprocFormatScalarReturn3()
		{
			sv = getValidator("schemaValidatorTest25.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.ForeignKeyInvalidRefTableException))]
		public void TestForeignKeyInvalidTable()
		{
			sv = getValidator("schemaValidatorTest26.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.ForeignKeyInvalidRefFieldException))]
		public void TestForeignKeyInvalidField()
		{
			sv = getValidator("schemaValidatorTest27.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.ForeignKeyRefFieldMissingConstraintException))]
		public void TestForeignKeyNoPkeyUniqueConstraint()
		{
			sv = getValidator("schemaValidatorTest28.xml");
			sv.Validate();
		}

		[Test]
		[ExpectedException(typeof(g.ForeignKeyInvalidRefFieldDatatypeException))]
		public void TestForeignKeyInvalidDatatype()
		{
			sv = getValidator("schemaValidatorTest29.xml");
			sv.Validate();
		}

		private g.SchemaValidator getValidator(string testFile)
		{
			string schemaFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\" + testFile;
			g.Parser parser = new g.Parser(schemaFile);			
			return new g.SchemaValidator(parser.ParseSchema());
		}

		private g.SchemaValidator sv;
	}
}
