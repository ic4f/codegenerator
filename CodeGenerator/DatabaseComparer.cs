using System;
using System.Collections;

namespace CodeGenerator
{
    /// <summary>
    /// compares the schema to the existing database
    /// </summary>
    public class DatabaseComparer
    {
        public DatabaseComparer(Sql.Database sqlDatabase, Sql.Database schDatabase)
        {
            this.sqlDatabase = sqlDatabase;
            this.schDatabase = schDatabase;
            addTables = new Sql.TableArrayList();
            deleteTables = new Sql.TableArrayList();
            retainTables = new Sql.TableArrayList();
            addConstraints = new ArrayList();
            deleteConstraints = new ArrayList();
            updateConstraints = new ArrayList();
            fieldUpdates = new Hashtable();
            init();
        }

        public Sql.Database SqlDatabase { get { return sqlDatabase; } }

        public Sql.Database SchemaDatabase { get { return schDatabase; } }

        public Sql.TableArrayList AddTables { get { return addTables; } }

        public Sql.TableArrayList DeleteTables { get { return deleteTables; } }

        public Sql.TableArrayList RetainTables { get { return retainTables; } }

        public ArrayList AddConstraints { get { return addConstraints; } }

        public ArrayList DeleteConstraints { get { return deleteConstraints; } }

        public ArrayList UpdateConstraints { get { return updateConstraints; } }

        public ArrayList GetAddFields(string table)
        {
            return (ArrayList)((ArrayList)fieldUpdates[table])[0];
        }

        public ArrayList GetDeleteFields(string table)
        {
            return (ArrayList)((ArrayList)fieldUpdates[table])[1];
        }

        public ArrayList GetUpdateFields(string table)
        {
            return (ArrayList)((ArrayList)fieldUpdates[table])[2];
        }

        #region private

        private void init()
        {
            loadTableUpdates();
            loadConstraintUpdates();
            loadFieldUpdates();
        }

        private void loadTableUpdates()
        {
            foreach (Sql.Table t in sqlDatabase.InternalTablesList)
                if (!schDatabase.InternalTablesHash.Contains(t.Name))
                    deleteTables.Add(t);
                else
                    retainTables.Add(t);

            foreach (Sql.Table t in schDatabase.InternalTablesList)
                if (!sqlDatabase.InternalTablesHash.Contains(t.Name))
                    addTables.Add(t);
        }

        private void loadConstraintUpdates()
        {
            //add all constraints from tables to be deleted to deleteConstraints (use sqlDatabase)
            foreach (Sql.Table t in deleteTables)
                foreach (Sql.Constraint c in t.Constraints)
                    deleteConstraints.Add(c);

            //add all constraints from tables to be added to addConstraints (use schDatabase)
            foreach (Sql.Table t in addTables)
                foreach (Sql.Constraint c in t.Constraints)
                    addInternalConstraint(addConstraints, c);

            //check updateTables - add constraints which appear in the sql db, but not in schema db and visa-versa
            foreach (Sql.Table t in retainTables)
            {
                Sql.Table sqlTable = t;
                Sql.Table schTable = (Sql.Table)schDatabase.InternalTablesHash[t.Name];

                foreach (Sql.Constraint c in sqlTable.Constraints)
                    if (!schTable.Constraints.Contains(c))
                        addInternalConstraint(deleteConstraints, c);

                foreach (Sql.Constraint c in schTable.Constraints)
                    if (!sqlTable.Constraints.Contains(c))
                        addInternalConstraint(addConstraints, c);

                Sql.Constraint c1;
                foreach (Sql.Constraint c in schTable.Constraints)
                {
                    int a = sqlTable.Constraints.IndexOf(c);
                    if (a > -1)
                    {
                        c1 = (Sql.Constraint)sqlTable.Constraints[a];
                        if (!c.IsIdenticalTo(c1)) //checks constraint values - not just table and field names
                            addInternalConstraint(updateConstraints, c);
                    }
                }
            }
        }

        //checks foreign key constraints: if a fkey referes to an external table, it won't be created in the db.
        private void addInternalConstraint(ArrayList constraints, Sql.Constraint c)
        {
            Sql.ForeignKeyConstraint fkey = c as Sql.ForeignKeyConstraint;
            if (fkey == null)
                constraints.Add(c);
            else
            {
                Sql.Table refTable = (Sql.Table)schDatabase.TablesHash[fkey.RefTableName];
                if (!refTable.IsExternal)
                    constraints.Add(c);
            }
        }

        private void loadFieldUpdates()
        {
            /*	for each table:
				- store field names in 3 arraylists: add/delete/update
				- store 3 arraylists in the fields arraylist
				- store the fields arraylist in the fieldUpdates hashtable by table name
				*/
            foreach (Sql.Table t in retainTables)
            {
                Sql.Table schTable = (Sql.Table)schDatabase.InternalTablesHash[t.Name];
                Sql.Table sqlTable = t;

                ArrayList addFields = new ArrayList();
                ArrayList deleteFields = new ArrayList();
                ArrayList updateFields = new ArrayList();

                foreach (Sql.TableField sqlField in sqlTable.FieldsList)
                    if (!schTable.FieldsHash.Contains(sqlField.Name))
                        deleteFields.Add(sqlField.Name);
                    else
                    {
                        Sql.TableField schField = (Sql.TableField)schTable.FieldsHash[sqlField.Name];
                        compareFields(updateFields, sqlField, schField);
                    }

                foreach (Sql.TableField f in schTable.FieldsList)
                    if (!sqlTable.FieldsHash.Contains(f.Name))
                        addFields.Add(f.Name);

                ArrayList fields = new ArrayList();
                fields.Add(addFields);
                fields.Add(deleteFields);
                fields.Add(updateFields);
                fieldUpdates.Add(t.Name, fields);
            }
        }

        private void compareFields(ArrayList updateFields, Sql.TableField sqlField, Sql.TableField schField)
        {
            bool areIdentical = true;
            if (sqlField.Datatype != schField.Datatype ||
                    sqlField.Length != schField.Length ||
                    sqlField.Precision != schField.Precision ||
                    sqlField.Scale != schField.Scale ||
                    sqlField.IsIdentity != schField.IsIdentity)
                areIdentical = false;

            if (!areIdentical)
            {
                if (schField.Datatype != sqlField.Datatype)
                {
                    if (!datatypesAreCompatible(schField.Datatype, sqlField.Datatype))
                        throw new FieldCompatibilityException("The field " + schField.Name + " stored in the database is of datatype " +
                            sqlField.Datatype + " which is not compatible with datatype " + schField.Datatype);
                }
                if (schField.Length < sqlField.Length)
                    throw new FieldCompatibilityException("The length " + schField.Length + " of field " + schField.Name +
                        " cannot be less than the length of this field already stored in the database (" +
                        sqlField.Length + ").  Please modify the databse directly.");

                if (schField.Precision < sqlField.Precision)
                    throw new FieldCompatibilityException("The precision " + schField.Precision + " of field " + schField.Name +
                        " cannot be less than the length of this field already stored in the database (" +
                        sqlField.Precision + ").  Please modify the databse directly.");

                if (schField.Scale < sqlField.Scale)
                    throw new FieldCompatibilityException("The scale " + schField.Scale + " of field " + schField.Name +
                        " cannot be less than the length of this field already stored in the database (" +
                        sqlField.Scale + ").  Please modify the databse directly.");

                if (!sqlField.IsIdentity && schField.IsIdentity)
                    throw new FieldCompatibilityException("The field " + schField.Name + " stored in the database is not an identity field. " +
                        "You cannot change this property because it may result in invalid data. " +
                        "Please modify the databse directly.");

                updateFields.Add(sqlField.Name);
            }
        }

        private bool datatypesAreCompatible(string schType, string sqlType)
        {
            /* Rules of datatype compatability:
			 * tinyint -> smallint -> int -> bigint
			 * char <-> varchar
			 * nchar <-> nvarchar
			 * binary <-> varbinary
			 * smalldatetime -> datetime
			 * smallmoney -> money
			 */
            bool areCompatible = false;
            if (schType == "smallint")
                areCompatible = (sqlType == "tinyint");
            else if (schType == "int")
                areCompatible = (sqlType == "tinyint" || sqlType == "smallint");
            else if (schType == "bigint")
                areCompatible = (sqlType == "tinyint" || sqlType == "smallint" || sqlType == "int");
            else if (schType == "char" || schType == "varchar")
                areCompatible = (sqlType == "char" || sqlType == "varchar");
            else if (schType == "nchar" || schType == "nvarchar")
                areCompatible = (sqlType == "nchar" || sqlType == "nvarchar");
            else if (schType == "binary" || schType == "varbinary")
                areCompatible = (sqlType == "binary" || sqlType == "varbinary");
            else if (schType == "datetime")
                areCompatible = (sqlType == "smalldatetime");
            else if (schType == "money")
                areCompatible = (sqlType == "smallmoney");

            return areCompatible;
        }

        private Sql.Database sqlDatabase;
        private Sql.Database schDatabase;
        private Sql.TableArrayList addTables;           //tables to create
        private Sql.TableArrayList deleteTables;        //tables to drop
        private Sql.TableArrayList retainTables;        //tables to alter or leave unchaged
        private ArrayList addConstraints;   //constraints to create
        private ArrayList deleteConstraints;//constraints to drop
        private ArrayList updateConstraints;//default and foreign key constraints whose values will be changed
        private Hashtable fieldUpdates;

        #endregion
    }
}
