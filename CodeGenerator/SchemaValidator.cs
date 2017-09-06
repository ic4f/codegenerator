using System;
using System.Collections;
using t = CodeGenerator.ParseTree;

namespace CodeGenerator
{
    public class SchemaValidator
    {
        public SchemaValidator(t.ApplicationNode root)
        {
            this.root = root;
        }

        public void Validate()
        {
            checkDuplicateTables();
            checkForeignKeys();
            root.Validate();
        }

        private void checkDuplicateTables()
        {
            foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
                foreach (t.ClassNode csNode in nsNode.ClassNodesList)
                    findDuplicateTable(csNode.Table);
        }

        private void findDuplicateTable(t.TableNode tblNode)
        {
            foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
                foreach (t.ClassNode csNode in nsNode.ClassNodesList)
                    if (csNode.Table != tblNode && csNode.Table.Name == tblNode.Name)
                        throw new DuplicateTableException("Duplicate tables: " + tblNode.Name);
        }

        private void checkForeignKeys()
        {
            foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
                foreach (t.ClassNode csNode in nsNode.ClassNodesList)
                    foreach (t.FieldNode fNode in csNode.Table.FieldNodesList)
                        if (fNode.RefTable != null || fNode.RefField != null)
                        {
                            checkForeignKey(fNode);
                        }
        }

        private void checkForeignKey(t.FieldNode fkey)
        {
            bool tableFound = false;
            bool fieldFound = false;
            foreach (t.NamespaceNode nsNode in root.NamespaceNodesList)
                foreach (t.ClassNode csNode in nsNode.ClassNodesList)
                    if (csNode.Table.Name == fkey.RefTable)
                    {
                        tableFound = true;
                        foreach (t.FieldNode fNode in csNode.Table.FieldNodesList)
                            if (fNode.Name == fkey.RefField)
                            {
                                fieldFound = true;
                                if (!fNode.IsPrimaryKey && !fNode.IsUnique)
                                    throw new ForeignKeyRefFieldMissingConstraintException("field " + fNode.Name + " in table " +
                                        csNode.Table.Name + " must be either a primary key or unique to be referenced as a foreign key");

                                if (fNode.SqlDatatype != fkey.SqlDatatype)
                                    throw new ForeignKeyInvalidRefFieldDatatypeException("field " + fNode.Name + " in table " +
                                        csNode.Table.Name + " must have the same datatype as " + fkey.Name + " to be referenced as a foreign key");
                            }
                    }
            if (!tableFound)
                throw new ForeignKeyInvalidRefTableException("table " + fkey.RefTable + " referenced by " + fkey.Name + " not found");
            if (!fieldFound)
                throw new ForeignKeyInvalidRefFieldException("field " + fkey.RefField + " referenced by " + fkey.Name + " not found");
        }

        private t.ApplicationNode root;
    }
}
