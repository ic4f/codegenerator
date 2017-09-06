using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code
{
    public abstract class CustomTableClassGenerator : ClassGenerator
    {
        public CustomTableClassGenerator(ClassObject c, string parentNamespace, SprocObject sproc) :
            base(c, parentNamespace)
        {
            this.sproc = sproc;
            className = c.Name + "Table" + sproc.Name;
            hasSelectedField = sproc.ReturnFieldsHash["Selected"] != null;
            hasIdField = sproc.ReturnFieldsHash["Id"] != null;
        }


        public bool HasSelectedField { get { return hasSelectedField; } }

        public bool HasIdField { get { return hasIdField; } }

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

        protected SprocObject Sproc { get { return sproc; } }

        protected abstract void MakeHeader(StringBuilder sb);

        protected abstract void MakeMainPart(StringBuilder sb);

        protected abstract void MakeFooter(StringBuilder sb);

        private string className;
        private SprocObject sproc;
        private bool hasSelectedField;
        private bool hasIdField;
    }
}
