using System;

namespace CodeGenerator.Application
{
    public class ClassFieldObject : BaseClassField
    {
        public ClassFieldObject(
            ClassObject parent,
            string name,
            bool identity,
            bool primarykey,
            string refTable,
            string refField,
            bool unique,
            bool encrypted,
            string display,
            bool excludefromtable,
            bool includewithparenttable,
            bool includeinlist,
            bool isdefaultsort,
            ReadonlyFieldType readonlytype,
            Sql.DatatypeInstance datatype) :
                base(parent, name, display, excludefromtable, includewithparenttable, includeinlist, isdefaultsort, datatype)
        {
            this.identity = identity;
            this.primarykey = primarykey;
            this.refTable = refTable;
            this.refField = refField;
            this.unique = unique;
            this.encrypted = encrypted;
            this.readonlytype = readonlytype;
            refClassObject = null;
            refFieldObject = null;
        }

        public override string Sql
        {
            get { return ParentClass.TableSqlName + "." + SqlName + " AS " + ParentClass.TableName + "_" + Name; }
        }

        public bool IsIdentity { get { return identity; } }

        public bool IsPrimaryKey { get { return primarykey; } }

        public bool IsForeignKey { get { return refTable != null; } }

        public ClassObject ReferencedClass { get { return refClassObject; } }

        public ClassFieldObject ReferencedField { get { return refFieldObject; } }

        public bool IsUnique { get { return unique; } }

        public bool IsEncrypted { get { return encrypted; } }

        public ReadonlyFieldType ReadonlyType { get { return readonlytype; } }

        public bool IsCreatedType { get { return readonlytype == ReadonlyFieldType.Created; } }

        public bool IsModifiedType { get { return readonlytype == ReadonlyFieldType.Modified; } }

        public bool IsTimestampType { get { return readonlytype == ReadonlyFieldType.Timestamp; } }

        public void LoadReference(ApplicationObject app)
        {
            if (IsForeignKey)
            {
                refClassObject = (ClassObject)app.GetClassByTableName(refTable);
                refFieldObject = (ClassFieldObject)refClassObject.FieldsHash[refField];
                refClassObject.AddReferringClass(ParentClass);
            }
        }

        private bool identity;
        private bool primarykey;
        private string refTable;
        private string refField;
        private bool unique;
        private bool encrypted;
        private ReadonlyFieldType readonlytype;
        private ClassObject refClassObject;
        private ClassFieldObject refFieldObject;
    }
}
