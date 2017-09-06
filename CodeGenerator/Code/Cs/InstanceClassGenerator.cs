using System;
using System.Text;
using System.Collections;
using CodeGenerator.Application;

namespace CodeGenerator.Code.Cs
{
    public class InstanceClassGenerator : Code.InstanceClassGenerator
    {
        public InstanceClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace) { }

        protected override BaseHelper MakeHelper() { return new Helper(); }

        protected override void MakeHeader(StringBuilder sb)
        {
            sb.Append("using System;\n");
            sb.Append("using System.Collections;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.AppendFormat("using Param = {0}.Core.Parameter;\n\n", ParentNamespace);
            sb.AppendFormat("namespace {0}.{1}\n", ParentNamespace, Class.Namespace.Name);
            sb.Append("{\n");
            //sb.AppendFormat("{0}public class {1} : Core.DataClass\n", Tabs(1), Class.Name); 7/8/2007, use for prssa membernet: inherits from local dataclass
            sb.AppendFormat("{0}public class {1} : DataClass\n", Tabs(1), Class.Name);
            sb.AppendFormat("{0}{{\n", Tabs(1));
        }

        protected override void MakeConstructor(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region constructor\n", Tabs(2));
            sb.AppendFormat("{0}public {1}(int recordId) : base()\n", Tabs(2), Class.Name);
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}loadRecord(recordId);\n", Tabs(3));
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeFieldProperty(StringBuilder sb, ClassFieldObject f, bool isLocal)
        {
            PublicMethodCount++;

            string fieldName = f.Name;
            if (!isLocal)
                fieldName = f.ParentClass.Name + "_" + fieldName;

            string var = Helper.MakeVarName(fieldName);
            string datatype = Helper.GetDatatype(f.Datatype.Name, f.IsEncrypted, true);

            sb.AppendFormat("{0}#region {1} {2}\n", Tabs(2), datatype, fieldName);
            sb.AppendFormat("{0}public {1} {2}\n", Tabs(2), datatype, fieldName);
            sb.AppendFormat("{0}{{\n", Tabs(2));

            if (f.IsEncrypted)
                sb.AppendFormat("{0}get {{ return (new Core.EncryptionTool()).Decrypt((byte[]){1}); }}\n", Tabs(3), var);
            else
                sb.AppendFormat("{0}get {{ return {1}; }}\n", Tabs(3), var);

            if (isLocal && !f.IsIdentity && !f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType && Class.Type != ClassType.Readonly && !Class.IsExternal)
                if (f.IsEncrypted)
                    sb.AppendFormat("{0}set {{ {1} = (new Core.EncryptionTool()).Encrypt(value); }}\n", Tabs(3), var);
                else
                    sb.AppendFormat("{0}set {{ {1} = value; }}\n", Tabs(3), var);

            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeAddFieldProperty(StringBuilder sb, ClassAddFieldObject f, bool isLocal)
        {
            PublicMethodCount++;

            string fieldName = f.Name;
            if (!isLocal)
                fieldName = f.ParentClass.Name + "_" + fieldName;

            string var = Helper.MakeVarName(fieldName);
            string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);

            sb.AppendFormat("{0}#region {1} {2}\n", Tabs(2), datatype, fieldName);
            sb.AppendFormat("{0}public {1} {2}\n", Tabs(2), datatype, fieldName);
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}get {{ return {1}; }}\n", Tabs(3), var);
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeUpdate(StringBuilder sb)
        {
            PublicMethodCount++;

            string idFieldVar = Helper.MakeVarName(PrimaryKeyField.Name);

            sb.AppendFormat("{0}#region int Update()\n", Tabs(2));
            sb.AppendFormat("{0}public int Update()\n", Tabs(2));
            sb.AppendFormat("{0}{{\n", Tabs(2));

            //string sproc = SprocGenerator.GetName(Class, "Update"); 7/8/2007
            string sproc = Class.TableName + "_Update";
            sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
            sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));

            string val;
            foreach (ClassFieldObject f in Class.FieldsList)
                if (!f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType)
                {
                    if (f.IsEncrypted)
                        val = "(new Core.EncryptionTool()).Encrypt(" + f.Name + ")";
                    else
                        val = f.Name;

                    if (f.IsForeignKey)
                    {
                        if (Helper.IsConvertibleToPrimitiveType(f.Datatype.Name))
                            sb.AppendFormat("{0}if ({1} > 0)\n", Tabs(3), f.Name); //assume only numerics for fkeys
                        else
                            sb.AppendFormat("{0}if ({1} != null)\n", Tabs(3), f.Name);
                        sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(4), f.Name, val);
                        sb.AppendFormat("{0}else\n", Tabs(3));
                        sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", System.DBNull.Value));\n\n", Tabs(4), f.Name);
                    }
                    else
                    {
                        if (!Helper.IsConvertibleToPrimitiveType(f.Datatype.Name))
                        {
                            sb.AppendFormat("{0}if ({1} != null)\n", Tabs(3), f.Name);
                            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(4), f.Name, val);
                            sb.AppendFormat("{0}else\n", Tabs(3));
                            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", System.DBNull.Value));\n\n", Tabs(4), f.Name);
                        }
                        else if (Helper.IsDateTimeType(f.Datatype.Name))
                        {
                            sb.AppendFormat("{0}if ({1} > new DateTime(1753, 1, 1) && {1} < new DateTime(9999, 12, 31))\n", Tabs(3), f.Name);
                            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(4), f.Name, val);
                            sb.AppendFormat("{0}else\n", Tabs(3));
                            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", System.DBNull.Value));\n\n", Tabs(4), f.Name);
                        }
                        else
                            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(4), f.Name, val);
                    }
                }

            sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
            sb.AppendFormat("{0}int result = command.ExecuteNonQuery();\n", Tabs(3));
            sb.AppendFormat("{0}Connection.Close();\n\n", Tabs(3));
            sb.AppendFormat("{0}if (result >= 0)\n", Tabs(3));
            sb.AppendFormat("{0}loadRecord(id); //some values might have been changed by the db code\n", Tabs(4));
            sb.AppendFormat("{0}return result;\n", Tabs(3));
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakePrivate(StringBuilder sb)
        {
            sb.AppendFormat("{0}#region private\n\n", Tabs(2));
            makeLoadRecordMethod(sb);
            makePrivateVariables(sb);
            sb.AppendFormat("\n{0}#endregion\n", Tabs(2));
        }

        private void makeLoadRecordMethod(StringBuilder sb)
        {
            //string sproc = SprocGenerator.GetName(Class, "GetRecord"); 7/8/2007
            string sproc = Class.TableName + "_Get";
            sb.AppendFormat("{0}private void loadRecord(int recordId)\n", Tabs(2));
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
            sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
            sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", recordId));\n\n", Tabs(3), PrimaryKeyField.Name);
            sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
            sb.AppendFormat("{0}SqlDataReader reader = command.ExecuteReader();\n", Tabs(3));
            sb.AppendFormat("{0}if (!reader.HasRows)\n", Tabs(3));
            sb.AppendFormat("{0}throw new Core.AppException(\"Record with id = \" + recordId + \" not found.\");\n", Tabs(4));
            sb.AppendFormat("{0}reader.Read();\n\n", Tabs(3));

            int i = 0;
            foreach (BaseClassField f in Class.InstanceIDataFieldList)
            {
                string fieldName = f.Name;
                if (f.ParentClass != Class)
                    fieldName = f.ParentClass.Name + "_" + f.Name;

                string dataReaderMethod = DatatypeHelper.GetDataReaderMethodForSqlType(f.Datatype.FullSqlName);
                string convert = "";
                if (dataReaderMethod == "GetValue")
                {
                    string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);
                    convert = "(" + datatype + ")";
                }

                sb.AppendFormat("{0}if (reader[{1}] != System.DBNull.Value)\n", Tabs(3), i);
                sb.AppendFormat("{0}{1} = {2}reader.{3}({4});\n\n", Tabs(4), Helper.MakeVarName(fieldName), convert, dataReaderMethod, i++);
            }

            sb.AppendFormat("{0}reader.Close();\n", Tabs(3));
            sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
            sb.AppendFormat("{0}}}\n", Tabs(2));
        }

        private void makePrivateVariables(StringBuilder sb)
        {
            foreach (BaseClassField f in Class.InstanceIDataFieldList)
            {
                string fieldName = f.Name;
                if (f.ParentClass != Class)
                    fieldName = f.ParentClass.Name + "_" + f.Name;

                ClassFieldObject cf = f as ClassFieldObject;
                if (cf != null)
                    sb.AppendFormat("{0}private {1} {2};\n",
                        Tabs(2), Helper.GetDatatype(cf.Datatype.Name, cf.IsEncrypted, false), Helper.MakeVarName(fieldName));
                else
                {
                    //must use explicit conversion for additional fields: code doesn't know what sql datatype they are
                    string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);
                    sb.AppendFormat("{0}private {1} {2};\n",
                        Tabs(2), Helper.GetDatatype(f.Datatype.FullSqlName, false, false), Helper.MakeVarName(fieldName));
                }
            }
        }

        protected override void MakeFooter(StringBuilder sb)
        {
            sb.AppendFormat("{0}}}\n}}", Tabs(1));
        }
    }
}
