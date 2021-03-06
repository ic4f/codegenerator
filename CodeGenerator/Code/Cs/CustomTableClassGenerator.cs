using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;


namespace CodeGenerator.Code.Cs
{
    public class CustomTableClassGenerator : Code.CustomTableClassGenerator
    {
        public CustomTableClassGenerator(ClassObject c, string parentNamespace, SprocObject sproc) :
            base(c, parentNamespace, sproc)
        { }

        protected override BaseHelper MakeHelper() { return new Helper(); }

        protected override void MakeHeader(StringBuilder sb)
        {
            sb.Append("using System;\n");
            sb.Append("using System.Collections;\n");
            sb.AppendFormat("using c = {0}.Core;\n\n", ParentNamespace);
            sb.AppendFormat("namespace {0}.{1}\n", ParentNamespace, Class.Namespace.Name);
            sb.Append("{\n");
            sb.AppendFormat("{0}public class {1} : c.AbstractDataTable\n", Tabs(1), ClassName);
            sb.AppendFormat("{0}{{\n", Tabs(1));
        }

        protected override void MakeMainPart(StringBuilder sb)
        {
            MakeIDataFields(sb);
            MakeConstructor(sb);
            MakeCommonProperties(sb);
            MakePrivate(sb);
        }

        private void MakeConstructor(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region constructor \n", Tabs(2));
            sb.AppendFormat("{0}public {1}(ArrayList rows, int totalCount) : base(rows, totalCount) {{}}\n", Tabs(2), ClassName);
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        private void MakeIDataFields(StringBuilder sb)
        {
            foreach (SprocReturnFieldObject f in Sproc.ReturnFieldsList)
            {
                PublicMethodCount++;

                string fieldName = f.Name;
                sb.AppendFormat("{0}#region static IDataField {1}Field \n", Tabs(2), fieldName);
                sb.AppendFormat("{0}public static c.IDataField {1}Field {{ get {{ return new {1}FieldClass(); }} }}\n", Tabs(2), fieldName);
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            if (!HasSelectedField)
            {
                PublicMethodCount++;

                sb.AppendFormat("{0}#region static IDataField SelectedField \n", Tabs(2));
                sb.AppendFormat("{0}public static c.IDataField SelectedField {{ get {{ return new SelectedRecord(); }} }}\n", Tabs(2));
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }
        }

        private void MakeCommonProperties(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}#region int FieldCount\n", Tabs(2));
            sb.AppendFormat("{0}public override int FieldCount {{ get {{ return {1}; }} }}\n", Tabs(2), Sproc.ReturnFieldsList.Count);
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));

            PublicMethodCount++;

            sb.AppendFormat("{0}#region {1}Row{2} this[int row]\n", Tabs(2), Class.Name, Sproc.Name);
            sb.AppendFormat("{0}public {1}Row{2} this[int row] {{ get {{ return ({1}Row{2})Rows[row]; }} }}\n", Tabs(2), Class.Name, Sproc.Name);
            sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
        }

        private void MakeTableRow(StringBuilder sb)
        {
            PublicMethodCount++;

            sb.AppendFormat("{0}public class {1}Row{2} : c.IDataRow\n", Tabs(1), Class.Name, Sproc.Name);
            sb.AppendFormat("{0}{{\n", Tabs(1));

            foreach (SprocReturnFieldObject f in Sproc.ReturnFieldsList)
            {
                PublicMethodCount++;

                string fieldName = f.Name;
                string datatype = f.CsDatatype;
                string varName = Helper.MakeVarName(fieldName);

                sb.AppendFormat("{0}#region {1} {2}\n", Tabs(2), datatype, fieldName);
                sb.AppendFormat("{0}public {1} {2}\n", Tabs(2), datatype, fieldName);
                sb.AppendFormat("{0}{{\n", Tabs(2));
                sb.AppendFormat("{0}get {{ return {1}; }}\n", Tabs(3), varName);
                sb.AppendFormat("{0}set {{ {1} = value; }}\n", Tabs(3), varName);
                sb.AppendFormat("{0}}}\n", Tabs(2));
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            if (!HasSelectedField)
            {
                PublicMethodCount++;

                sb.AppendFormat("{0}#region bool Selected\n", Tabs(2));
                sb.AppendFormat("{0}public bool Selected\n", Tabs(2));
                sb.AppendFormat("{0}{{\n", Tabs(2));
                sb.AppendFormat("{0}get {{ return selected; }}\n", Tabs(3));
                sb.AppendFormat("{0}set {{ selected = value; }}\n", Tabs(3));
                sb.AppendFormat("{0}}}\n", Tabs(2));
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            if (!HasIdField) //neccessary hack: a row must have an "int Id" property - needed for link table updates on the frontend
            {
                PublicMethodCount++;

                sb.AppendFormat("{0}#region int Id\n", Tabs(2));
                sb.AppendFormat("{0}public int Id\n", Tabs(2));
                sb.AppendFormat("{0}{{\n", Tabs(2));
                sb.AppendFormat("{0}get {{ return -1; }}\n", Tabs(3));
                sb.AppendFormat("{0}}}\n", Tabs(2));
                sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
            }

            sb.AppendFormat("{0}#region private\n", Tabs(2));
            foreach (SprocReturnFieldObject f in Sproc.ReturnFieldsList)
            {
                string fieldName = f.Name;

                sb.AppendFormat("{0}private {1} {2};\n",
                    Tabs(2), f.CsDatatype, Helper.MakeVarName(fieldName));
            }

            if (!HasSelectedField)
                sb.AppendFormat("{0}private bool selected;\n", Tabs(2));

            sb.AppendFormat("{0}#endregion\n", Tabs(2));
            sb.AppendFormat("{0}}}\n", Tabs(1));
        }

        private void MakeFieldClass(SprocReturnFieldObject f, StringBuilder sb)
        {
            string fieldName = f.Name;
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

            foreach (SprocReturnFieldObject rf in Sproc.ReturnFieldsList)
                MakeFieldClass(rf, sb);

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

        protected override void MakeFooter(StringBuilder sb)
        {
            sb.AppendFormat("{0}}}\n\n", Tabs(1));
            MakeTableRow(sb);
            sb.Append("}");
        }
    }
}
