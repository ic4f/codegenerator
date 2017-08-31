using System;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator
{
	public abstract class SprocGenerator
	{
		public static int MinimumErrorCode = -16;

		public static string GetName(ClassObject classObj, string command) 
		{ 
			return "a_" + classObj.TableName + "_" + command;
		}

		public static string GetCustomName(ClassObject classObj, string command) 
		{ 
			return classObj.TableName + "_" + command;
		}

		public SprocGenerator(ClassObject classObj, DatabaseTool dbTool)
		{
			this.classObj = classObj;
			this.dbTool = dbTool;
		}

		protected void CreateProcedure(string command, string parameters, string statements)
		{
			string name = "dbo." + GetName(classObj, command);
			StringBuilder sb1 = new StringBuilder();
			sb1.AppendFormat("\nIF OBJECT_ID('{0}') IS NOT NULL \n", name);
			sb1.AppendFormat("  DROP PROC {0}\n", name);		
			dbTool.executeSql(sb1.ToString());

			StringBuilder sb2 = new StringBuilder();
			sb2.AppendFormat("CREATE PROCEDURE {0}\n", name);
			sb2.Append(parameters);							
			sb2.Append("\nAS\n");			
			sb2.Append(statements);	
			dbTool.executeSql(sb2.ToString());
		}

		protected ClassObject Class { get { return classObj; } }

		protected string MakeFieldParam(ClassFieldObject f)
		{
			return "  @" + f.Name + " " + f.Datatype.FullSqlName;
		}

		private ClassObject classObj;
		private DatabaseTool dbTool;
	}
}
