using System;
using System.Collections;
using System.Data;
using NUnit.Framework;
using g = CodeGenerator;
using t = CodeGenerator.ParseTree;

namespace CodeGeneratorTest
{
    [TestFixture]
    public class SchemaDatabaseLoaderTest2 : BaseTest
    {
        [Test]
        public void TestDatabaseEquivalence()
        {
            g.Sql.Database db1 = dbLoader.Database;
            Assert.AreEqual(4, db1.TablesHash.Count);
            Assert.IsTrue(db1.TablesHash.Contains("test_Record1"));
            Assert.IsTrue(db1.TablesHash.Contains("test_Record1Cat"));
            Assert.IsTrue(db1.TablesHash.Contains("test_Record1Record2"));
            Assert.IsTrue(db1.TablesHash.Contains("test_Record2"));

            g.Sql.Database db2 = schemaDatabase;
            Assert.AreEqual(4, db2.TablesHash.Count);
            Assert.IsTrue(db2.TablesHash.Contains("test_Record1"));
            Assert.IsTrue(db2.TablesHash.Contains("test_Record1Cat"));
            Assert.IsTrue(db2.TablesHash.Contains("test_Record1Record2"));
            Assert.IsTrue(db2.TablesHash.Contains("test_Record2"));
        }

        [Test]
        public void TestTablesEquivalence()
        {
            g.Sql.Database db1 = dbLoader.Database;
            g.Sql.Table t11 = db1.GetTable("test_Record1");
            g.Sql.Table t12 = db1.GetTable("test_Record1Cat");
            g.Sql.Table t13 = db1.GetTable("test_Record1Record2");
            g.Sql.Table t14 = db1.GetTable("test_Record2");

            g.Sql.Database db2 = schemaDatabase;
            g.Sql.Table t21 = db2.GetTable("test_Record1");
            g.Sql.Table t22 = db2.GetTable("test_Record1Cat");
            g.Sql.Table t23 = db2.GetTable("test_Record1Record2");
            g.Sql.Table t24 = db2.GetTable("test_Record2");

            Assert.AreEqual(t11.Name, t21.Name);
            Assert.AreEqual(t11.PrimaryKey.Field.Name, t21.PrimaryKey.Field.Name);

            Assert.AreEqual(t12.Name, t22.Name);
            Assert.AreEqual(t12.PrimaryKey.Field.Name, t22.PrimaryKey.Field.Name);

            Assert.AreEqual(t13.Name, t23.Name);
            Assert.AreEqual(t13.PrimaryKey.Field.Name, t23.PrimaryKey.Field.Name);
            Assert.AreEqual(t13.PrimaryKey.Field2.Name, t23.PrimaryKey.Field2.Name);

            Assert.AreEqual(t14.Name, t24.Name);
            Assert.AreEqual(t14.PrimaryKey.Field.Name, t24.PrimaryKey.Field.Name);
        }

        [Test]
        public void TestConstraintsEquivalence()
        {
            g.Sql.Database db1 = dbLoader.Database;
            g.Sql.Table t11 = db1.GetTable("test_Record1");
            g.Sql.Table t12 = db1.GetTable("test_Record1Cat");
            g.Sql.Table t13 = db1.GetTable("test_Record1Record2");
            g.Sql.Table t14 = db1.GetTable("test_Record2");

            g.Sql.Database db2 = schemaDatabase;
            g.Sql.Table t21 = db2.GetTable("test_Record1");
            g.Sql.Table t22 = db2.GetTable("test_Record1Cat");
            g.Sql.Table t23 = db2.GetTable("test_Record1Record2");
            g.Sql.Table t24 = db2.GetTable("test_Record2");

            //check constraints
            runConstraintsCheck(t11.Constraints, t21.Constraints);
            runConstraintsCheck(t12.Constraints, t22.Constraints);
            runConstraintsCheck(t13.Constraints, t23.Constraints);
            runConstraintsCheck(t14.Constraints, t24.Constraints);
        }

        [Test]
        public void TestFieldsEquivalence()
        {
            g.Sql.Database db1 = dbLoader.Database;
            g.Sql.Table t11 = db1.GetTable("test_Record1");
            g.Sql.Table t12 = db1.GetTable("test_Record1Cat");
            g.Sql.Table t13 = db1.GetTable("test_Record1Record2");
            g.Sql.Table t14 = db1.GetTable("test_Record2");

            g.Sql.Database db2 = schemaDatabase;
            g.Sql.Table t21 = db2.GetTable("test_Record1");
            g.Sql.Table t22 = db2.GetTable("test_Record1Cat");
            g.Sql.Table t23 = db2.GetTable("test_Record1Record2");
            g.Sql.Table t24 = db2.GetTable("test_Record2");

            runFieldsCheck(t11.FieldsHash, t21.FieldsHash);
            runFieldsCheck(t12.FieldsHash, t22.FieldsHash);
            runFieldsCheck(t13.FieldsHash, t23.FieldsHash);
            runFieldsCheck(t14.FieldsHash, t24.FieldsHash);
        }

        private void runFieldsCheck(Hashtable sqlFields, Hashtable schFields)
        {
            Assert.AreEqual(sqlFields.Count, schFields.Count);
            foreach (g.Sql.TableField sqlF in sqlFields.Values)
            {
                g.Sql.TableField schF = (g.Sql.TableField)schFields[sqlF.Name];
                Assert.AreEqual(sqlF.Datatype, schF.Datatype);
                Assert.AreEqual(sqlF.IsIdentity, schF.IsIdentity);
                Assert.AreEqual(sqlF.Length, schF.Length);
                Assert.AreEqual(sqlF.Name, schF.Name);
                Assert.AreEqual(sqlF.Precision, schF.Precision);
                Assert.AreEqual(sqlF.Scale, schF.Scale);
            }
        }

        private void runConstraintsCheck(ArrayList sqlCons, ArrayList schCons)
        {
            Assert.AreEqual(sqlCons.Count, schCons.Count);

            Hashtable uniquesSql = new Hashtable();
            Hashtable uniquesSch = new Hashtable();
            Hashtable defaultsSql = new Hashtable();
            Hashtable defaultsSch = new Hashtable();
            Hashtable pkeysSql = new Hashtable();
            Hashtable pkeysSch = new Hashtable();
            Hashtable fkeysSql = new Hashtable();
            Hashtable fkeysSch = new Hashtable();

            foreach (g.Sql.Constraint c in sqlCons)
                if (c is g.Sql.UniqueConstraint)
                    uniquesSql.Add(c.Field.Name, c);
                else if (c is g.Sql.PrimaryKeyConstraint)
                    pkeysSql.Add(c.Field.Name, c);
                else if (c is g.Sql.ForeignKeyConstraint)
                    fkeysSql.Add(c.Field.Name, c);

            foreach (g.Sql.Constraint c in schCons)
                if (c is g.Sql.UniqueConstraint)
                    uniquesSch.Add(c.Field.Name, c);
                else if (c is g.Sql.PrimaryKeyConstraint)
                    pkeysSch.Add(c.Field.Name, c);
                else if (c is g.Sql.ForeignKeyConstraint)
                    fkeysSch.Add(c.Field.Name, c);

            Assert.AreEqual(uniquesSql.Count, uniquesSch.Count);
            Assert.AreEqual(defaultsSql.Count, defaultsSch.Count);
            Assert.AreEqual(pkeysSql.Count, pkeysSch.Count);
            Assert.AreEqual(fkeysSql.Count, fkeysSch.Count);

            foreach (g.Sql.UniqueConstraint cSql in uniquesSql.Values)
            {
                g.Sql.UniqueConstraint cSch = (g.Sql.UniqueConstraint)uniquesSch[cSql.Field.Name];
                Assert.AreEqual(cSql.Field.Name, cSch.Field.Name);
            }
            foreach (g.Sql.PrimaryKeyConstraint cSql in pkeysSql.Values)
            {
                g.Sql.PrimaryKeyConstraint cSch = (g.Sql.PrimaryKeyConstraint)pkeysSch[cSql.Field.Name];
                Assert.AreEqual(cSql.Field.Name, cSch.Field.Name);
                Assert.AreEqual(cSql.IsUnary, cSch.IsUnary);
                if (!cSql.IsUnary)
                    Assert.AreEqual(cSql.Field2.Name, cSch.Field2.Name);
            }
            foreach (g.Sql.ForeignKeyConstraint cSql in fkeysSql.Values)
            {
                g.Sql.ForeignKeyConstraint cSch = (g.Sql.ForeignKeyConstraint)fkeysSch[cSql.Field.Name];
                Assert.AreEqual(cSql.Field.Name, cSch.Field.Name);
                Assert.AreEqual(cSql.RefFieldName, cSch.RefFieldName);
                Assert.AreEqual(cSql.RefTableName, cSch.RefTableName);
            }
        }

        [SetUp]
        public void SetUp()
        {
            string setupFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvSetup.txt";
            string teardownFile = @"C:\development\vs\CodeGenerator\CodeGeneratorTest\mics\dbTestEnvTearDown.txt";
            envBuilder = new Sql.DbEnvironmentBuilder(Connection, setupFile, teardownFile);
            envBuilder.Setup();

            dbLoader = new CodeGenerator.SqlDatabaseLoader(Connection, true);

            g.Parser parser = new g.Parser(@"C:\development\vs\CodeGenerator\CodeGeneratorTest\xml\databaseComparerTest1.xml");
            t.ApplicationNode appNode = parser.ParseSchema();
            new g.SchemaValidator(appNode).Validate();
            schemaDatabase = new g.SchemaDatabaseLoader(appNode).Database;
        }

        [TearDown]
        public void TearDown() { envBuilder.TearDown(); }

        private Sql.DbEnvironmentBuilder envBuilder;
        private g.SqlDatabaseLoader dbLoader;
        private g.Sql.Database schemaDatabase;
    }
}
