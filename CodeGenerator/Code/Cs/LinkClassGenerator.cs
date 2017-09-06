using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code.Cs
{
    public class LinkClassGenerator : Code.LinkClassGenerator
    {
        public LinkClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace)
        {
        }

        protected override BaseHelper MakeHelper() { return new Helper(); }

        protected override void MakeHeader(StringBuilder sb)
        {
            sb.Append("using System;\n");
            sb.Append("using System.Collections;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n\n");
            sb.AppendFormat("namespace {0}.{1}\n", ParentNamespace, Class.Namespace.Name);
            sb.Append("{\n");
            sb.AppendFormat("{0}public class {1} : Core.DataClass\n", Tabs(1), Class.Name);
            sb.AppendFormat("{0}{{\n", Tabs(1));
        }

        protected override void MakeConstructor(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region constructor\n\n", Tabs(2));
            sb.AppendFormat("{0}public {1}() : base() {{}}\n", Tabs(2), Class.Name);
            sb.AppendFormat("\n{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeCreateDelete(StringBuilder sb, string method)
        {
            PublicMethodCount++;

            string sproc = SprocGenerator.GetName(Class, method);
            sb.AppendFormat("{0}#region {1} \n", Tabs(2), method);
            sb.AppendFormat("{0}public int {1}(", Tabs(2), method);
            sb.AppendFormat("{1} {0}, ", FKey1.Name, Helper.GetDatatype(FKey1.Datatype.Name, false, false));
            sb.AppendFormat("{1} {0})\n", FKey2.Name, Helper.GetDatatype(FKey2.Datatype.Name, false, false));
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}SqlParameter[] parameters = {{ \n", Tabs(3));
            sb.AppendFormat("{0}new SqlParameter(\"@{1}\", {1}), \n", Tabs(4), FKey1.Name);
            sb.AppendFormat("{0}new SqlParameter(\"@{1}\", {1})}};\n\n", Tabs(4), FKey2.Name);
            sb.AppendFormat("{0}{1} x = new {1}();\n", Tabs(3), Class.Name);
            sb.AppendFormat("{0}return (int)x.ExecNonQuery(\"{1}\", parameters);\n",
                Tabs(3), sproc);
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeCreateDeleteByField(StringBuilder sb, ClassFieldObject FKey, string method)
        {
            PublicMethodCount++;

            string sproc = SprocGenerator.GetName(Class, method);
            sb.AppendFormat("{0}#region {1} \n", Tabs(2), method);
            sb.AppendFormat("{0}public int {1}({2} {3})\n",
                Tabs(2), method, Helper.GetDatatype(FKey1.Datatype.Name, false, false), FKey.Name);
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}SqlParameter[] parameters = {{", Tabs(3));
            sb.AppendFormat("new SqlParameter(\"@{0}\", {0})}};\n", FKey.Name);
            sb.AppendFormat("{0}{1} x = new {1}();\n", Tabs(3), Class.Name);
            sb.AppendFormat("{0}return (int)x.ExecNonQuery(\"{1}\", parameters);\n",
                Tabs(3), sproc);
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        //		protected override void MakeCreateDeleteByFieldWithCriteria(StringBuilder sb, ClassFieldObject FKey, string method)
        //		{	
        //			sb.AppendFormat("{0}#region {1} \n", Tabs(2), method);
        //			sb.AppendFormat("{0}public static int {1}({2} {3}, string @Query)\n", 
        //				Tabs(2), method, Helper.GetDatatype(FKey1.Datatype.Name, false, false), FKey.Name);
        //			sb.AppendFormat("{0}{{\n", Tabs(2));
        //			sb.AppendFormat("{0}SqlParameter[] parameters = {{\n", Tabs(3));
        //			sb.AppendFormat("{0}new SqlParameter(\"@{1}\", {1}),\n", Tabs(4), FKey.Name);
        //			sb.AppendFormat("{0}new SqlParameter(\"@Query\", Query)}};\n\n", Tabs(4));
        //			sb.AppendFormat("{0}{1} x = new {1}();\n", Tabs(3), Class.Name);
        //			sb.AppendFormat("{0}return (int)x.ExecNonQuery(\"{1}_{2}\", parameters);\n", 
        //				Tabs(3), Class.TableName, method);
        //			sb.AppendFormat("{0}}}\n", Tabs(2));
        //			sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        //		}

        protected override void MakeUpdateBy1(StringBuilder sb)
        {
            PublicMethodCount++;

            string method = "UpdateBy" + FKey1.ReferencedClass.Name;
            string datatype1 = Helper.GetDatatype(FKey1.Datatype.Name, false, false);
            string datatype2 = Helper.GetDatatype(FKey2.Datatype.Name, false, false);
            sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
            sb.AppendFormat("{0}public void {1}(", Tabs(2), method);
            sb.AppendFormat("{0} {1}, ", datatype1, FKey1.Name);
            sb.Append("ArrayList toDelete, ArrayList toAdd)\n");
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}for (int i=0; i<toDelete.Count; i++)\n", Tabs(3));
            sb.AppendFormat("{0}Delete({1}, {2}(toDelete[i]));\n", Tabs(4), FKey1.Name, DatatypeHelper.GetConvertTo(datatype2));
            sb.AppendFormat("{0}for (int i=0; i<toAdd.Count; i++)\n", Tabs(3));
            sb.AppendFormat("{0}Create({1}, {2}(toAdd[i]));\n", Tabs(4), FKey1.Name, DatatypeHelper.GetConvertTo(datatype2));
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeUpdateBy2(StringBuilder sb)
        {
            PublicMethodCount++;

            string method = "UpdateBy" + FKey2.ReferencedClass.Name;
            string datatype1 = Helper.GetDatatype(FKey1.Datatype.Name, false, false);
            string datatype2 = Helper.GetDatatype(FKey2.Datatype.Name, false, false);
            sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
            sb.AppendFormat("{0}public void {1}(", Tabs(2), method);
            sb.AppendFormat("{0} {1}, ", datatype2, FKey2.Name);
            sb.Append("ArrayList toDelete, ArrayList toAdd)\n");
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}for (int i=0; i<toDelete.Count; i++)\n", Tabs(3));
            sb.AppendFormat("{0}Delete({2}(toDelete[i]), {1});\n", Tabs(4), FKey2.Name, DatatypeHelper.GetConvertTo(datatype1));
            sb.AppendFormat("{0}for (int i=0; i<toAdd.Count; i++)\n", Tabs(3));
            sb.AppendFormat("{0}Create({2}(toAdd[i]), {1});\n", Tabs(4), FKey2.Name, DatatypeHelper.GetConvertTo(datatype1));
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        protected override void MakeFooter(StringBuilder sb)
        {
            sb.AppendFormat("{0}}}\n}}", Tabs(1));
        }
    }
}
