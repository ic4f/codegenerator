using System;
using System.Data;
using System.Data.SqlClient;

namespace CodeGenerator
{
    public class DatabaseTool
    {
        public DatabaseTool(string connection)
        {
            this.connection = connection;
        }

        public string Connection { get { return connection; } }

        public void executeSql(string sqlText)
        {
            //Console.WriteLine(sqlText);
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand command = new SqlCommand(sqlText, sqlConnection);
            command.CommandType = CommandType.Text;
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private string connection;
    }
}
