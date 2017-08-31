using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using g = CodeGenerator;

namespace CodeGeneratorTest.Sql
{
	/// <summary>
	/// Creates a database environment for testing
	/// </summary>
	public class DbEnvironmentBuilder
	{
		public DbEnvironmentBuilder(string connection, string setupFile, string teardownFile) 
		{
			this.connection = connection;
			this.setupFile = setupFile;
			this.teardownFile = teardownFile;
		}

		public void Setup() { execute(setupFile);	}

		public void TearDown() { execute(teardownFile);	}

		private void execute(string file)
		{			
			StringBuilder sb = new StringBuilder();
			try 
			{				
				using (StreamReader sr = new StreamReader(file)) 
				{
					string line;					
					while ((line = sr.ReadLine()) != null)
						sb.AppendFormat("{0}\n", line);
				}
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
	
			SqlConnection sqlConnection = new SqlConnection(connection);
			SqlCommand command = new SqlCommand(sb.ToString(), sqlConnection);
			command.CommandType = CommandType.Text;
			sqlConnection.Open();
			command.ExecuteNonQuery();
			sqlConnection.Close();		
		}

		private string connection;
		private string setupFile;
		private string teardownFile;
	}
}

