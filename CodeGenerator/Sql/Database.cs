using System;
using System.Collections;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Represents a SQL Server db
    /// </summary>
    public class Database
    {
        public Database()
        {
            tablesList = new ArrayList();
            tablesHash = new Hashtable();
            intTablesList = new ArrayList();
            intTablesHash = new Hashtable();
        }

        public ArrayList TablesList { get { return tablesList; } }

        public Hashtable TablesHash { get { return tablesHash; } }

        public ArrayList InternalTablesList { get { return intTablesList; } }

        public Hashtable InternalTablesHash { get { return intTablesHash; } }

        public void AddTable(Table t)
        {
            tablesList.Add(t);
            tablesHash[t.Name] = t;

            if (!t.IsExternal)
            {
                intTablesList.Add(t);
                intTablesHash[t.Name] = t;
            }
        }

        public Table GetTable(string name)
        {
            if (!tablesHash.Contains(name))
                throw new TableNotFoundException("Database does not contain table " + name);
            return (Table)tablesHash[name];
        }

        private ArrayList tablesList;
        private Hashtable tablesHash;
        private ArrayList intTablesList;
        private Hashtable intTablesHash;
    }
}
