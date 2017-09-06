using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code.Cs
{
    public class ListClassGenerator : ClassGenerator
    {
        public ListClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace)
        {
            className = c.Name + "List";
        }

        protected override BaseHelper MakeHelper() { return new Helper(); }

        public override string GetCode()
        {
            StringBuilder sb = new StringBuilder();
            MakeHeader(sb);
            MakeMainPart(sb);
            MakeFooter(sb);
            return sb.ToString();
        }

        public override string GetFileName()
        {
            return className + Helper.FileExtension;
        }

        protected string ClassName { get { return className; } }

        protected void MakeHeader(StringBuilder sb)
        {
            sb.Append("using System;\n");
            sb.Append("using System.Collections;\n");
            sb.AppendFormat("using c = {0}.Core;\n\n", ParentNamespace);
            sb.AppendFormat("namespace {0}.{1}\n", ParentNamespace, Class.Namespace.Name);
            sb.Append("{\n");
            sb.AppendFormat("{0}public class {1} : c.AbstractDataTable\n", Tabs(1), ClassName);
            sb.AppendFormat("{0}{{\n", Tabs(1));
        }

        protected void MakeMainPart(StringBuilder sb)
        {
            MakeIDataFields(sb);
            MakeConstructor(sb);
            MakeCommonProperties(sb);
            MakePrivate(sb);
        }

        private void MakeIDataFields(StringBuilder sb)
        {
            foreach (BaseClassField f in Class.IncludeWithListList)
            {
                string fieldName = f.Name;
                if (f.ParentClass != Class)
                    fieldName = f.ParentClass.Name + "_" + f.Name;

                PublicMethodCount++;

                sb.AppendFormat("{0}#region static IDataField {1}Field \n", Tabs(2), fieldName);
                sb.AppendFormat("{0}public static c.IDataField {1}Field {{ get {{ return new {1}FieldClass(); }} }}\n", Tabs(2), fieldName);
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            PublicMethodCount++;

            sb.AppendFormat("{0}#region static IDataField SelectedField \n", Tabs(2));
            sb.AppendFormat("{0}public static c.IDataField SelectedField {{ get {{ return new SelectedRecord(); }} }}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        private void MakeConstructor(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region constructor \n", Tabs(2));
            sb.AppendFormat("{0}public {1}(ArrayList rows, int totalCount) : base(rows, totalCount) {{}}\n", Tabs(2), ClassName);
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        private void MakeCommonProperties(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region int FieldCount\n", Tabs(2));
            sb.AppendFormat("{0}public override int FieldCount {{ get {{ return {1}; }} }}\n", Tabs(2), Class.SetIDataFieldList.Count + 1); //+1 for the Selected field (retrieved by some sprocs, false by default)
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));

            PublicMethodCount++;

            sb.AppendFormat("{0}#region {1}ListRow this[int row]\n", Tabs(2), Class.Name);
            sb.AppendFormat("{0}public {1}ListRow this[int row] {{ get {{ return ({1}ListRow)Rows[row]; }} }}\n", Tabs(2), Class.Name);
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        private void MakeTableRow(StringBuilder sb)
        {
            sb.AppendFormat("{0}public class {1}ListRow : c.IDataRow\n", Tabs(1), Class.Name);
            sb.AppendFormat("{0}{{\n", Tabs(1));

            foreach (BaseClassField f in Class.IncludeWithListList)
            {
                string fieldName = f.Name;
                string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);
                string varName = Helper.MakeVarName(fieldName);

                PublicMethodCount++;

                sb.AppendFormat("{0}#region {1} {2}\n", Tabs(2), datatype, fieldName);
                sb.AppendFormat("{0}public {1} {2}\n", Tabs(2), datatype, fieldName);
                sb.AppendFormat("{0}{{\n", Tabs(2));
                sb.AppendFormat("{0}get {{ return {1}; }}\n", Tabs(3), varName);
                sb.AppendFormat("{0}set {{ {1} = value; }}\n", Tabs(3), varName);
                sb.AppendFormat("{0}}}\n", Tabs(2));
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            PublicMethodCount++;

            sb.AppendFormat("{0}#region bool Selected\n", Tabs(2));
            sb.AppendFormat("{0}public bool Selected\n", Tabs(2));
            sb.AppendFormat("{0}{{\n", Tabs(2));
            sb.AppendFormat("{0}get {{ return selected; }}\n", Tabs(3));
            sb.AppendFormat("{0}set {{ selected = value; }}\n", Tabs(3));
            sb.AppendFormat("{0}}}\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));

            sb.AppendFormat("{0}#region private\n", Tabs(2));
            foreach (BaseClassField f in Class.IncludeWithListList)
            {
                string fieldName = f.Name;

                sb.AppendFormat("{0}private {1} {2};\n",
                    Tabs(2), Helper.GetDatatype(f.Datatype.FullSqlName, false, false), Helper.MakeVarName(fieldName));
            }
            sb.AppendFormat("{0}private bool selected;\n", Tabs(2));
            sb.AppendFormat("{0}#endregion\n", Tabs(2));
            sb.AppendFormat("{0}}}\n", Tabs(1));
        }

        private void MakeFieldClass(BaseClassField f, StringBuilder sb)
        {
            string fieldName = f.Name;
            if (f.ParentClass != Class)
                fieldName = f.ParentClass.Name + "_" + f.Name;

            sb.AppendFormat("{0}private class {1}FieldClass : c.IDataField\n", Tabs(2), fieldName);
            sb.AppendFormat("{0}{{\n", Tabs(2));

            PublicMethodCount++;
            PublicMethodCount++;
            PublicMethodCount++;

            sb.AppendFormat("{0}public string DataField {{ get {{ return \"{1}\"; }} }}\n", Tabs(3), fieldName);
            sb.AppendFormat("{0}public string SortExpression {{ get {{ return \"{1}\"; }} }}\n", Tabs(3), f.SortExpression);
            sb.AppendFormat("{0}public string Display {{ get {{ return \"{1}\"; }} }}\n", Tabs(3), f.Display);
            sb.AppendFormat("{0}}}\n\n", Tabs(2));
        }

        private void MakePrivate(StringBuilder sb)
        {
            sb.AppendFormat("{0}#region private\n", Tabs(2));

            foreach (BaseClassField f in Class.IncludeWithListList)
                MakeFieldClass(f, sb);

            sb.AppendFormat("{0}private class SelectedRecord : c.IDataField\n", Tabs(2));
            sb.AppendFormat("{0}{{\n", Tabs(2));

            PublicMethodCount++;
            PublicMethodCount++;
            PublicMethodCount++;

            sb.AppendFormat("{0}public string DataField {{ get {{ return \"Selected\"; }} }}\n", Tabs(3));
            sb.AppendFormat("{0}public string SortExpression {{ get {{ return \"Selected\"; }} }}\n", Tabs(3));
            sb.AppendFormat("{0}public string Display {{ get {{ return \"Selected\"; }} }}\n", Tabs(3));
            sb.AppendFormat("{0}}}\n", Tabs(2));

            sb.AppendFormat("{0}#endregion\n", Tabs(2));
        }

        protected void MakeFooter(StringBuilder sb)
        {
            sb.AppendFormat("{0}}}\n\n", Tabs(1));
            MakeTableRow(sb);
            sb.Append("}");
        }

        private string className;
    }
}
