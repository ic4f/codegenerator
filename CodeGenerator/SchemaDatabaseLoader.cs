using System;
using System.Collections;
using t = CodeGenerator.ParseTree;
using s = CodeGenerator.Sql;

namespace CodeGenerator
{
    /// <summary>
    /// loads a Sql.Database object from a schema file
    /// </summary>
    public class SchemaDatabaseLoader
    {
        public SchemaDatabaseLoader(t.ApplicationNode root)
        {
            database = new Sql.Database();
            loadDatabase(root);
        }

        public Sql.Database Database { get { return database; } }

        private void loadDatabase(t.ApplicationNode root)
        {
            foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
                foreach (t.ClassNode csNode in nsNode.ClassNodesList)
                    loadTable(csNode.Table, csNode.Type);
        }

        private void loadTable(t.TableNode tblNode, string type)
        {
            s.Table sqlTable = new s.Table(tblNode.Name, database, tblNode.IsExternal);
            database.AddTable(sqlTable);
            foreach (t.FieldNode fieldNode in tblNode.FieldNodesList)
                loadField(fieldNode, sqlTable);
        }

        private void loadField(t.FieldNode fieldNode, s.Table sqlTable)
        {
            Sql.DatatypeInstance di = new DatatypeLoader(fieldNode.SqlDatatype).Datatype;
            s.TableField tblField = new s.TableField(sqlTable, fieldNode.Name, di.Name,
                di.FullSqlName, di.Length, di.Precision, di.Scale, fieldNode.IsIdentity);
            sqlTable.AddField(tblField);

            if (fieldNode.IsPrimaryKey)
                sqlTable.AddConstraint(new Sql.PrimaryKeyConstraint(tblField, null));
            if (fieldNode.RefTable != null && fieldNode.RefTable != string.Empty)
                sqlTable.AddConstraint(new Sql.ForeignKeyConstraint(tblField, fieldNode.RefTable, fieldNode.RefField, null));
            if (fieldNode.IsUnique)
                sqlTable.AddConstraint(new Sql.UniqueConstraint(tblField, null));
        }

        private Sql.Database database;
    }
}
