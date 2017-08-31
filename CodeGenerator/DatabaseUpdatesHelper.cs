using System;
using System.Collections;
using System.Text;

namespace CodeGenerator
{
	public class DatabaseUpdatesHelper
	{
		public DatabaseUpdatesHelper(DatabaseComparer dbc)
		{
			this.dbc = dbc;
			this.sqlDatabase = dbc.SqlDatabase;
			this.schDatabase = dbc.SchemaDatabase;
		}

		#region GetDropConstraintsScript
		public string GetDropConstraintsScript()
		{
			StringBuilder sb = new StringBuilder();

			ArrayList consToDrop = getConstraintNamesToDrop(sqlDatabase);
			foreach(Sql.Constraint c in consToDrop)
			{				
				sb.AppendFormat("ALTER TABLE {0}\n", c.Field.Table.SqlName);
				sb.AppendFormat("  DROP CONSTRAINT {0}\n\n", c.Name);
			}		
			return sb.ToString();
		}
		#endregion

		#region GetDropTablesScript
		public string GetDropTablesScript()
		{
			StringBuilder sb = new StringBuilder();

			ArrayList tblsToDrop = dbc.DeleteTables;
			foreach(Sql.Table t in tblsToDrop)			
				sb.AppendFormat("DROP TABLE {0}\n\n", t.SqlName);

			return sb.ToString();
		}
		#endregion

		#region GetCreateTablesScript
		public string GetCreateTablesScript()
		{
			StringBuilder sb = new StringBuilder();

			ArrayList tblsToCreate = dbc.AddTables;
			foreach(Sql.Table t in tblsToCreate)	
				sb.Append(getCreateTableScript(t));

			return sb.ToString();
		}
		#endregion

		#region GetAlterTablesScript
		//2 runs: first - drop/add/alter without null/identity options (order matters - drop first!!
		//second - alter with options (INLCUDING THE NEWLY ADDED FIELDS!)
		public string GetAlterTablesScript(bool isFirstRun) 
		{
			StringBuilder sb = new StringBuilder();

			Sql.Table schTable;
			Sql.Table sqlTable;
			Sql.TableField schField;
			Sql.TableField sqlField;
			ArrayList addFields;
			ArrayList delFields;
			ArrayList updFields;

			foreach(Sql.Table t in dbc.RetainTables)
			{
				addFields = dbc.GetAddFields(t.Name);
				delFields = dbc.GetDeleteFields(t.Name);
				updFields = dbc.GetUpdateFields(t.Name);	
				if (addFields.Count > 0 || delFields.Count > 0 || updFields.Count > 0)
				{
					schTable = (Sql.Table)schDatabase.InternalTablesHash[t.Name];
					sqlTable = t;					
					
					if (delFields.Count > 0 && isFirstRun)
					{						
						foreach(string s in delFields)
						{
							sqlField = (Sql.TableField)sqlTable.FieldsHash[s];
							sb.AppendFormat("ALTER TABLE {0}\n  DROP COLUMN {1}\n\n", t.SqlName, sqlField.SqlName);
						}						
					}
					if (updFields.Count > 0)
					{
						foreach(string s in updFields)
						{
							schField = (Sql.TableField)schTable.FieldsHash[s];
							if (!isLocked(schField.Datatype))
							{
								sb.AppendFormat("ALTER TABLE {0}\n  ALTER COLUMN {1}", 
									t.SqlName, getColumnDescription(schField, isFirstRun));
								if (isPrimaryKey(schField))
									sb.Append(" NOT NULL");

								sb.Append("\n\n");
							}
						}
					}
					if (addFields.Count > 0)
					{
						foreach(string s in addFields)
						{
							schField = (Sql.TableField)schTable.FieldsHash[s];
							if (isFirstRun)
								sb.AppendFormat("ALTER TABLE {0}\n  ADD {1}", 
									t.SqlName, getColumnDescription(schField, true));
							else if (!isLocked(schField.Datatype))
								sb.AppendFormat("ALTER TABLE {0}\n  ALTER COLUMN {1}", 
									t.SqlName, getColumnDescription(schField, false));
							
							if (isPrimaryKey(schField))
								sb.Append(" NOT NULL");

							sb.Append("\n\n");
						}
					}
				}
			}
			return sb.ToString();
		}
		#endregion

		#region GetCreateConstraintsScript
		public string GetCreateConstraintsScript()
		{
			StringBuilder sb = new StringBuilder();

			Hashtable tables = schDatabase.InternalTablesHash;	
		
			foreach(Sql.Table t in tables.Values)
			{
				ArrayList cons = t.UniqueConstraints;
				foreach(Sql.UniqueConstraint c in cons)
				{				
					sb.AppendFormat("ALTER TABLE {0}\n", t.SqlName);
					sb.AppendFormat("  ADD CONSTRAINT UQ_{0}_{1} UNIQUE ({2})\n\n", t.Name, c.Field.Name, c.Field.SqlName);
				}
			}
			foreach(Sql.Table t in tables.Values)
			{
				Sql.PrimaryKeyConstraint c = t.PrimaryKey;
				sb.AppendFormat("ALTER TABLE {0}\n", t.SqlName);
				if (c.IsUnary)
					sb.AppendFormat("  ADD CONSTRAINT PK_{0}_{1} primary key ({2})\n\n", 
						t.Name, c.Field.Name, c.Field.SqlName);
				else
					sb.AppendFormat("  ADD CONSTRAINT PK_{0}_{1}{2} primary key ({3},{4})\n\n", 
						t.Name, c.Field.Name, c.Field2.Name, c.Field.SqlName, c.Field2.SqlName);
			}
			foreach(Sql.Table t in tables.Values)
			{
				ArrayList cons = t.ForeignKeyConstraints;
				foreach(Sql.ForeignKeyConstraint c in cons)
				{
					//check that it's not referring to an external table!
					Sql.Table refTable = (Sql.Table)schDatabase.TablesHash[c.RefTableName];
					if (!refTable.IsExternal)
					{
						sb.AppendFormat("ALTER TABLE {0}\n", t.SqlName);
						sb.AppendFormat("  ADD CONSTRAINT FK_{0}_{1} FOREIGN KEY ({2}) REFERENCES {3}({4})\n\n", 
							t.Name, c.Field.Name, c.Field.SqlName, c.RefTableSqlName, c.RefFieldSqlName);
					}
				}
			}
			return sb.ToString();
		}
		#endregion
		
		#region GetTableUpdatesSummary
		public string GetTableUpdatesSummary()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\nTABLE CHANGES:\n");
			sb.AppendFormat("\n  TABLES TO BE CREATED: {0}", dbc.AddTables.Count);
			if (dbc.AddTables.Count > 0)							
				foreach(Sql.Table t in dbc.AddTables)
					sb.AppendFormat("\n    {0}", t.Name);
			
			sb.AppendFormat("\n\n  TABLES TO BE DROPPED: {0}", dbc.DeleteTables.Count);
			if (dbc.DeleteTables.Count > 0)
				foreach(Sql.Table t in dbc.DeleteTables)
					sb.AppendFormat("\n    {0}", t.Name);

			return sb.ToString();
		}
		#endregion

		#region GetConstraintUpdatesSummary
		public string GetConstraintUpdatesSummary()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\nCONSTRAINT CHANGES:\n");
			sb.AppendFormat("\n  CONSTRAINTS TO BE CREATED: {0}", dbc.AddConstraints.Count);
			if (dbc.AddConstraints.Count > 0)							
				foreach(Sql.Constraint c in dbc.AddConstraints)
					sb.AppendFormat("\n    {0}", c.ToString());

			sb.AppendFormat("\n\n  CONSTRAINTS TO BE DROPPED: {0}", dbc.DeleteConstraints.Count);
			if (dbc.DeleteConstraints.Count > 0)							
				foreach(Sql.Constraint c in dbc.DeleteConstraints)
					sb.AppendFormat("\n    {0}", c.ToString());

			sb.AppendFormat("\n\n  CONSTRAINTS TO BE MODIFIED: {0}", dbc.UpdateConstraints.Count);
			if (dbc.UpdateConstraints.Count > 0)							
				foreach(Sql.Constraint c in dbc.UpdateConstraints)
					sb.AppendFormat("\n    {0}", c.ToString());

			return sb.ToString();
		}
		#endregion

		#region GetFieldUpdatesSummary
		public string GetFieldUpdatesSummary()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\nTABLE FIELD CHANGES: ");

			Sql.Table schTable;
			Sql.Table sqlTable;
			Sql.TableField schField;
			Sql.TableField sqlField;
			ArrayList addFields;
			ArrayList delFields;
			ArrayList updFields;
			foreach(Sql.Table t in dbc.RetainTables)
			{
				sb.AppendFormat("\n\n  TABLE {0}: ", t);
				addFields = dbc.GetAddFields(t.Name);
				delFields = dbc.GetDeleteFields(t.Name);
				updFields = dbc.GetUpdateFields(t.Name);				
				if (addFields.Count > 0 || delFields.Count > 0 || updFields.Count > 0)
				{
					schTable = (Sql.Table)schDatabase.InternalTablesHash[t.Name];
					sqlTable = t;					
					sb.AppendFormat("\n\n    FIELDS TO BE ADDED: {0}", addFields.Count);
					if (addFields.Count > 0)											
						foreach(string s in addFields)
						{
							schField = (Sql.TableField)schTable.FieldsHash[s];
							sb.AppendFormat("\n      {0} ({1})", s, getFieldInfo(schField));
						}
					
					sb.AppendFormat("\n\n    FIELDS TO BE DELETED: {0}", delFields.Count);
					if (delFields.Count > 0)											
						foreach(string s in delFields)
						{
							sqlField = (Sql.TableField)sqlTable.FieldsHash[s];
							sb.AppendFormat("\n      {0} ({1})", s, getFieldInfo(sqlField));
						}
					
					sb.AppendFormat("\n\n    FIELDS TO BE MODIFIED: {0}", updFields.Count);
					if (updFields.Count > 0)											
						foreach(string s in updFields)
						{
							schField = (Sql.TableField)schTable.FieldsHash[s];
							sqlField = (Sql.TableField)sqlTable.FieldsHash[s];
							sb.AppendFormat("\n      {0}:", s);
							sb.AppendFormat("\n{0}", compareFieldInfo("        ", sqlField, schField));
						}					
				}
				else
					sb.Append("none\n");
			}			
			return sb.ToString();
		}
		#endregion

		#region private
		private bool isLocked(string dt)
		{
			return (dt == "text" || dt == "ntext" || dt == "image" || dt == "timestamp");
		}

		private string getFieldInfo(Sql.TableField f)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("datatype: {0}; ", f.Datatype);
			sb.AppendFormat("length: {0}; ", f.Length);
			sb.AppendFormat("precision: {0}; ", f.Precision);
			sb.AppendFormat("scale: {0}; ", f.Scale);
			sb.AppendFormat("is identity: {0}; ", f.IsIdentity);
			return sb.ToString();
		}

		private string compareFieldInfo(string prefix, Sql.TableField sqlField, Sql.TableField schField)
		{
			StringBuilder sb = new StringBuilder();
			if (sqlField.Datatype != schField.Datatype)
				sb.AppendFormat("{0}DATATYPE: Current = {1}; Modified = {2}\n", prefix,sqlField.Datatype, schField.Datatype);
			if (sqlField.Length != schField.Length)
				sb.AppendFormat("{0}LENGTH: Current = {1}; Modified = {2}\n", prefix,sqlField.Length, schField.Length);
			if (sqlField.Precision != schField.Precision)
				sb.AppendFormat("{0}PRECISION: Current = {1}; Modified = {2}\n", prefix,sqlField.Precision, schField.Precision);
			if (sqlField.Scale != schField.Scale)
				sb.AppendFormat("{0}SCALE: Current = {1}; Modified = {2}\n", prefix,sqlField.Scale, schField.Scale);
			if (sqlField.IsIdentity != schField.IsIdentity)
				sb.AppendFormat("{0}IS IDENTITY: Current = {1}; Modified = {2}\n", prefix,sqlField.IsIdentity, schField.IsIdentity);
			return sb.ToString();
		}

		private ArrayList getConstraintNamesToDrop(Sql.Database sqlDatabase)
		{
			//add constraints in this order: defaults -> fkeys -> uniques -> pkeys
			ArrayList consToDrop = new ArrayList();
			Hashtable tables = sqlDatabase.InternalTablesHash;
			foreach(Sql.Table t in tables.Values)
			{
				ArrayList cons = t.ForeignKeyConstraints;
				foreach(Sql.ForeignKeyConstraint c in cons)
					consToDrop.Add(c);
			}
			foreach(Sql.Table t in tables.Values)
			{
				ArrayList cons = t.UniqueConstraints;
				foreach(Sql.UniqueConstraint c in cons)
					consToDrop.Add(c);
			}
			foreach(Sql.Table t in tables.Values)			
				if (t.PrimaryKey != null)
					consToDrop.Add(t.PrimaryKey);			

			return consToDrop;
		}

		private string getCreateTableScript(Sql.Table t)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("CREATE TABLE dbo.{0}", t.SqlName);
			sb.Append("\n(\n");
			foreach(Sql.TableField f in t.FieldsList)
			{
				sb.AppendFormat("\t{0} {1}", f.SqlName, f.SqlDatatype);
				if (f.IsIdentity)
					sb.Append(" IDENTITY");
				if (isPrimaryKey(f))
					sb.Append(" NOT NULL");
				sb.Append(",\n");
			}
			removeLastComma(sb);
			sb.Append("\n)\n");
			return sb.ToString();
		}

		private bool isPrimaryKey(Sql.TableField f)
		{
			Sql.PrimaryKeyConstraint pkey = f.Table.PrimaryKey;		
			if (pkey.IsUnary)
				return (pkey.Field.Name == f.Name);
			else
				return (pkey.Field.Name == f.Name || pkey.Field2.Name == f.Name);
		}

		
		private void removeLastComma(StringBuilder sb) 
		{ 
			string temp = sb.ToString();
			int comma = temp.LastIndexOf(",");
			if (comma > -1)
			{
				temp = temp.Substring(0, comma); 
				sb.Remove(0, sb.Length);
				sb.Append(temp);
			}
		}

		private string getColumnDescription(Sql.TableField f, bool isFirstRun)
		{
			if (isFirstRun)				
				return f.SqlName + " " + f.SqlDatatype;
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("{0} {1}", f.SqlName, f.SqlDatatype);
				if (f.IsIdentity)
					sb.Append(" IDENTITY");
				return sb.ToString();
			}
		}

		private DatabaseComparer dbc;
		private Sql.Database sqlDatabase;
		private Sql.Database schDatabase;
		#endregion

	}
}
