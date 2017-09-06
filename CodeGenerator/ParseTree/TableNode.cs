using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
    public class TableNode : IParseTreeNode
    {
        public static int MaxBytes = 8060;

        public TableNode(string name, bool isExternal)
        {
            this.name = name;
            this.isExternal = isExternal;
            fieldNodesList = new ArrayList();
            fieldNodesHash = new Hashtable();
            addFieldNodesList = new ArrayList();
            addFieldNodesHash = new Hashtable();
        }

        public void Validate()
        {
            checkIdentityField();
            checkMaxRowSize();
            checkDuplicateFields();

            foreach (FieldNode node in fieldNodesList)
                node.Validate();
            foreach (AddFieldNode node in addFieldNodesList)
                node.Validate();
        }

        public string Name { get { return name; } }

        public bool IsExternal { get { return isExternal; } }

        public void AddField(FieldNode node)
        {
            fieldNodesList.Add(node);
            try
            {
                fieldNodesHash.Add(node.Name, node);
            }
            catch (Exception e)
            {
                throw new DuplicateFieldException("Duplicate field error; " + e.Message);
            }
        }

        public void AddAdditionalField(AddFieldNode node)
        {
            addFieldNodesList.Add(node);
            try
            {
                addFieldNodesHash.Add(node.Name, node);
            }
            catch (Exception e)
            {
                throw new DuplicateAdditionalFieldException("Duplicate additionalField error; " + e.Message);
            }
        }

        public ArrayList FieldNodesList { get { return fieldNodesList; } }
        public Hashtable FieldNodesHash { get { return fieldNodesHash; } }

        public ArrayList AddFieldNodesList { get { return addFieldNodesList; } }
        public Hashtable AddFieldNodesHash { get { return addFieldNodesHash; } }

        private void checkIdentityField()
        {
            bool hasIdentity = false;
            foreach (FieldNode node in fieldNodesList)
                if (!hasIdentity && node.IsIdentity)
                    hasIdentity = true;
                else if (hasIdentity && node.IsIdentity)
                    throw new MultipleIdentityException("Table " + name + " already has an identity field");
        }

        private void checkMaxRowSize()
        {
            int bytes = initBytes;
            foreach (FieldNode node in fieldNodesList)
            {
                Sql.DatatypeInstance di = new DatatypeLoader(node.SqlDatatype).Datatype;
                bytes += di.Length;
            }

            if (bytes > MaxBytes)
                throw new ExceededTableRowCapacityException("Class " + name + ": table maximum row size (" +
                    bytes + ") exceeds the maximum number of bytes per row (" + MaxBytes + ")");
        }

        private void checkDuplicateFields()
        {
            foreach (BaseFieldNode fieldNode in fieldNodesList)
                findDuplicateField(fieldNode);
            foreach (BaseFieldNode addfieldNode in addFieldNodesList)
                findDuplicateField(addfieldNode);
        }

        private void findDuplicateField(BaseFieldNode node)
        {
            foreach (FieldNode fieldNode in fieldNodesList)
                if (fieldNode != node && fieldNode.Name == node.Name)
                    throw new DuplicateFieldAdditionalFieldException("Duplicate fields: " + fieldNode.Name);
            foreach (AddFieldNode addFieldNode in addFieldNodesList)
                if (addFieldNode != node && addFieldNode.Name == node.Name)
                    throw new DuplicateFieldAdditionalFieldException("Duplicate fields: " + addFieldNode.Name);
        }

        private int initBytes { get { return 100; } }

        private string name;
        private bool isExternal;
        private ArrayList fieldNodesList;
        private Hashtable fieldNodesHash;
        private ArrayList addFieldNodesList;
        private Hashtable addFieldNodesHash;
    }
}
