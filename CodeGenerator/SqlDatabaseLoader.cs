using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CodeGenerator
{
	/// <summary>
	/// Loads a database object with schema info retrieved from a SQL Server db
	/// </summary>
	public class SqlDatabaseLoader
	{
		public SqlDatabaseLoader(string connString, bool isSafe)
		{
			this.connString = connString;
			database = new Sql.Database();

			if ( isSafe && (connString.IndexOf("Test") < 0) && (connString.IndexOf("test") < 0) && 
				(connString.IndexOf("Development") < 0) && (connString.IndexOf("development") < 0) )
				throw new Exception("you cannot run the generator on a database which name does not include the words 'test' or 'development'");

			LoadDatabase();
		}

		public Sql.Database Database { get { return database; } }

		private void LoadDatabase()
		{
			DataTable tables = GetTables();
			foreach(DataRow drTable in tables.Rows)
			{
				Sql.Table table = new Sql.Table(drTable[0].ToString(), database, false);
				database.AddTable(table);
				
				Hashtable fields = new Hashtable();

				DataTable columns = GetColumns(table.Name);
				foreach(DataRow drColumn in columns.Rows)				
				{
					Sql.TableField f = MakeField(drColumn, table);
					fields.Add(f.Name, f);
					table.AddField(f);
				}

				DataTable cons = GetConstraints(table.Name);
				if (cons != null)
				{
					int i = 0;				
					while (i < cons.Rows.Count)
					{
						Sql.Constraint c = null;
						DataRow dr = cons.Rows[i];
						string typeStr = dr["constraint_type"].ToString();
						if (typeStr.IndexOf("PRIMARY") > -1)
							c = MakePrimaryKeyConstraint(dr, fields);
						else if (typeStr.IndexOf("FOREIGN") > -1) 
							c = MakeForeignKeyConstraint(dr, cons.Rows[++i], fields);
						else if (typeStr.IndexOf("UNIQUE") > -1) 
							c = MakeUniqueConstraint(dr, fields);

						if (c	!= null)
							table.AddConstraint(c);
						i++;
					}
				}
			}
		}

		public DataTable GetTables()
		{ 
			return GetDataSet(sqlGetTables()).Tables[0];
		}

		public DataTable GetColumns(string tableName)
		{
			return GetDataSet(sqlGetColumnsByTable(tableName)).Tables[0];
		}

		public DataTable GetConstraints(string tableName)
		{
			DataSet constraints = GetDataSet(sqlGetConstraints(tableName));			
			if (constraints.Tables.Count < 2) //no constraints!
				return null;
			return constraints.Tables[1];
		}

		public Sql.PrimaryKeyConstraint MakePrimaryKeyConstraint(DataRow dr, Hashtable fields)
		{
			string keysStr = dr["constraint_keys"].ToString();
			string name = dr["constraint_name"].ToString();
			string[] arrColumns = keysStr.Split(',');
			
			if (arrColumns.Length == 1)
			{
				Sql.TableField f = (Sql.TableField)fields[arrColumns[0]];
				return new Sql.PrimaryKeyConstraint(f, name);
			}
			else
			{	
				Sql.TableField f1 = (Sql.TableField)fields[arrColumns[0].Trim()];
				Sql.TableField f2 = (Sql.TableField)fields[arrColumns[1].Trim()];
				return new Sql.PrimaryKeyConstraint(f1, f2, name);
			}
		}

		public Sql.ForeignKeyConstraint MakeForeignKeyConstraint(DataRow dr1, DataRow dr2, Hashtable fields)
		{
			string name = dr1["constraint_name"].ToString();
			string keysStr1 = dr1["constraint_keys"].ToString();
			Sql.TableField f = (Sql.TableField)fields[keysStr1];
			string keysStr2 = dr2["constraint_keys"].ToString();
			string temp = keysStr2.Substring(keysStr2.LastIndexOf(".") + 1);
			string refTable = temp.Substring(0, temp.IndexOf("(") - 1);
			string refField = temp.Substring(temp.IndexOf("(") + 1);
			int end = refField.IndexOf(")");
			refField = refField.Substring(0, end);
			return new Sql.ForeignKeyConstraint(f, refTable, refField, name);
		}


		public Sql.UniqueConstraint MakeUniqueConstraint(DataRow dr, Hashtable fields)
		{
			string name = dr["constraint_name"].ToString();
			string keysStr = dr["constraint_keys"].ToString();
			string column = keysStr;
			return new Sql.UniqueConstraint((Sql.TableField)fields[column], name);			
		}

		public Sql.TableField MakeField(DataRow dr, Sql.Table parentTable)
		{
			string name = dr["COLUMN_NAME"].ToString();			
			bool isIdentity = false;

			string datatype = dr["TYPE_NAME"].ToString();
			if (datatype.IndexOf("identity") > -1)
			{
				isIdentity = true;
				datatype = datatype.Substring(0, datatype.IndexOf("identity")).Trim();
			}

			int length = int.Parse(dr["LENGTH"].ToString());	
			if (datatype == "nchar" || datatype == "nvarchar")
				length /= 2; //sql server doubles these for storing unicode values => so we need to split in half

			int precision = int.Parse(dr["PRECISION"].ToString());
			int scale= -1;
			string scaleStr = dr["SCALE"].ToString();
			if (scaleStr != "" && scaleStr != "NULL")
				scale = int.Parse(scaleStr);

			Sql.DatatypeInstance di = new DatatypeLoader(datatype, length, precision, scale).Datatype;

			return new Sql.TableField(parentTable, name, di.Name, di.FullSqlName, di.Length, di.Precision, di.Scale, isIdentity);		
		}

		private string sqlGetTables()
		{			
			return @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES " + 
				@"WHERE TABLE_TYPE = 'BASE TABLE' AND NOT TABLE_NAME = 'dtproperties' ORDER BY TABLE_NAME";
		}

		private string sqlGetColumnsByTable(string tableName)
		{
			return "EXEC sp_columns '" + tableName + "'";
		}	

		private string sqlGetConstraints(string tableName)
		{
			return @"EXEC sp_helpconstraint '" + tableName + "'";
		}	

		private DataSet GetDataSet(string sql)
		{
			DataSet ds = new DataSet();		
			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand(sql, connection);
			command.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = command;
			adapter.Fill(ds);
			connection.Close();
			return ds;
		}

		private string connString;
		private Sql.Database database;
	}
}
