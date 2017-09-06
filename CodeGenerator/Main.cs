using System;
using System.Text;
using System.Collections;
using System.IO;
using t = CodeGenerator.ParseTree;
using a = CodeGenerator.Application;

namespace CodeGenerator
{
    public class Main
    {
        public Main(string connection, string parentNamespace, string outputDir, bool isCs) :
            this(connection, null, parentNamespace, outputDir, isCs)
        { }

        public Main(
            string connection, string schemaPath, string parentNamespace, string outputDir, bool isCs)
        {
            this.schemaPath = schemaPath;
            dbTool = new DatabaseTool(connection);
            this.parentNamespace = parentNamespace;
            this.outputDir = outputDir;
            this.isCs = isCs;
        }

        public void Run()
        {
            try
            {
                if (schemaPath == null)
                    schemaPath = getSchemaFile();

                Parser schParser = new Parser(schemaPath);
                t.ApplicationNode appRootNode = schParser.ParseSchema();

                SchemaValidator schValidator = new SchemaValidator(appRootNode);
                schValidator.Validate();

                SchemaDatabaseLoader schLoader = new SchemaDatabaseLoader(appRootNode);
                Sql.Database schemaDatabase = schLoader.Database;

                SqlDatabaseLoader sqlLoader = new SqlDatabaseLoader(dbTool.Connection, false);
                Sql.Database sqlDatabase = sqlLoader.Database;

                DatabaseComparer comparer = new DatabaseComparer(sqlDatabase, schemaDatabase);
                DatabaseUpdatesHelper helper = new DatabaseUpdatesHelper(comparer);

                ApplicationLoader appLoader = new ApplicationLoader(appRootNode);

                Console.WriteLine();
                Console.WriteLine("The following changes will be made to the database:");
                Console.WriteLine(helper.GetTableUpdatesSummary());
                Console.WriteLine(helper.GetConstraintUpdatesSummary());
                Console.WriteLine(helper.GetFieldUpdatesSummary());
                getUserConfirmation();
                Console.WriteLine("UPDATING DATABASE...");
                UpdateTablesConstraints(helper);
                generateSprocs(appLoader.Application);
                Console.WriteLine("DATABASE UPDATES COMPLETE\nGENERATING CODE...");
                generateCode(appLoader.Application);
                Console.WriteLine("CODE GENERATION COMPLETE!");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Process aborted due to the following problem:");
                Console.Error.WriteLine(e.Message + "\n" + e.ToString());
            }
        }

        public void UpdateTablesConstraints(DatabaseUpdatesHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(helper.GetDropConstraintsScript());   //drop ALL constraints
            sb.Append(helper.GetDropTablesScript());            //drop tables (DeleteTables collection)
            sb.Append(helper.GetCreateTablesScript());      //create tables (AddTables collection)
            sb.Append(helper.GetAlterTablesScript(true));   //alter table fields WITHOUT null edits
            sb.Append(helper.GetCreateConstraintsScript()); //create ALL constraints
            sb.Append(helper.GetAlterTablesScript(false));  //alter table fields with null edits
            dbTool.executeSql(sb.ToString());
        }

        private void generateSprocs(a.ApplicationObject application)
        {
            foreach (a.NamespaceObject n in application.NamespacesList)
            {
                foreach (a.ClassObject c in n.NonLinkClassesList)
                    new RecordSprocGenerator(c, dbTool).CreateProcedures();
                foreach (a.ClassObject c in n.LinkClassesList)
                    new LinkSprocGenerator(c, dbTool).CreateProcedures();
            }
        }

        private void generateCode(a.ApplicationObject application)
        {
            Code.ClassGenerator g, g1, g2, g3, g4, g5;
            foreach (a.NamespaceObject n in application.NamespacesList)
                foreach (a.ClassObject c in n.ClassesList)
                    if (c.Type == a.ClassType.Link)
                    {
                        g = new Code.Cs.LinkClassGenerator(c, parentNamespace);
                        writeFile(c.Namespace.Name, g.GetFileName(), g.GetCode());
                    }
                    else
                    {
                        g1 = new Code.Cs.InstanceClassGenerator(c, parentNamespace);
                        g2 = new Code.Cs.TableClassGenerator(c, parentNamespace);
                        g3 = new Code.Cs.ListClassGenerator(c, parentNamespace);
                        g4 = new Code.Cs.DataClassGenerator(c, parentNamespace);

                        writeFile(c.Namespace.Name, g1.GetFileName(), g1.GetCode());
                        writeFile(c.Namespace.Name, g2.GetFileName(), g2.GetCode());
                        writeFile(c.Namespace.Name, g3.GetFileName(), g3.GetCode());
                        writeFile(c.Namespace.Name, g4.GetFileName(), g4.GetCode());

                        foreach (a.SprocObject sproc in c.CustomReturnTypeSprocsList)
                        {
                            g5 = new Code.Cs.CustomTableClassGenerator(c, parentNamespace, sproc);
                            writeFile(c.Namespace.Name, g5.GetFileName(), g5.GetCode());
                        }
                    }
        }

        private void writeFile(string subdir, string fileName, string code)
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            if (!Directory.Exists(outputDir + "\\" + subdir))
                Directory.CreateDirectory(outputDir + "\\" + subdir);

            StreamWriter writer = File.CreateText(outputDir + "\\" + subdir + "\\" + fileName);
            writer.Write(code);
            writer.Flush();
            writer.Close();
        }

        private string getSchemaFile()
        {
            string path;
            while (true)
            {
                Console.WriteLine("Please provide the schema file path or type 'N' to abort");
                path = Console.ReadLine();
                if (path != "")
                    if (path == "N" || path == "n")
                        System.Environment.Exit(0);
                if (File.Exists(path))
                    return path;
                else
                    Console.WriteLine("File not found");
            }
        }

        private void getUserConfirmation()
        {
            string input;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("To proceed with these updates type 'yes', to abort type 'N'");
                input = Console.ReadLine();
                if (input != "")
                    if (string.Compare(input, "N", true) == 0)
                    {
                        Console.WriteLine("UPDATE ABORTED");
                        System.Environment.Exit(0);
                    }
                    else if (string.Compare(input, "yes", true) == 0)
                        return;
            }
        }

        private DatabaseTool dbTool;
        private string schemaPath;
        private string parentNamespace;
        private string outputDir;
        private bool isCs;
    }
}
