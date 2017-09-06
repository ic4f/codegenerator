using System;
using System.Collections;
using CodeGenerator;

namespace ConsoleApp
{
    class MainApp
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Select application:");
            Console.WriteLine("1 - [app 1 name goes here]");
            Console.WriteLine("2 - [app 2 name goes here]");
            //more choices 
            Console.WriteLine("0 - EXIT");

            while (true)
            {
                try
                {
                    int app = Convert.ToInt32(Console.ReadLine());
                    switch (app)
                    {
                        case 1: runAppName1(); break;
                        //more cases
                        case 0: Console.WriteLine("Bye!"); break;
                    }
                    break;
                }
                catch (Exception) { Console.WriteLine("Invalid input"); }
            }
        }

        private static void runAppName1() //1
        {
            string connection = System.Configuration.ConfigurationSettings.AppSettings["[name of connection]"];
            string schemaPath = @"[path to app schema file]";
            string outputPath = @"[path to output directory]";
            Main m = new Main(connection, schemaPath, "[app namespace]", outputPath, true);
            m.Run();
        }

    }
}
