using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code
{
    /// <summary>
    /// Generates [class name] for data layer (link class)
    /// Assumes only 2 fkeys in a link table
    /// </summary>
    public abstract class LinkClassGenerator : ClassGenerator
    {
        public LinkClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace)
        {
            ArrayList temp = new ArrayList();
            foreach (ClassFieldObject f in Class.FieldsList)
                if (f.IsForeignKey)
                    temp.Add(f);

            fkey1 = (ClassFieldObject)temp[0];
            fkey2 = (ClassFieldObject)temp[1];
        }

        public override string GetCode()
        {
            StringBuilder sb = new StringBuilder();
            MakeHeader(sb);
            MakeConstructor(sb);
            makeCreateMethods(sb);
            makeUpdateMethods(sb);
            makeDeleteMethods(sb);
            MakeFooter(sb);
            return sb.ToString();
        }

        protected ClassFieldObject FKey1 { get { return fkey1; } }

        protected ClassFieldObject FKey2 { get { return fkey2; } }

        private void makeCreateMethods(StringBuilder sb)
        {
            MakeCreateDelete(sb, "Create");
            MakeCreateDeleteByField(sb, fkey1, "CreateAllBy" + fkey1.ReferencedClass.Name);
            MakeCreateDeleteByField(sb, fkey2, "CreateAllBy" + fkey2.ReferencedClass.Name);
        }

        private void makeUpdateMethods(StringBuilder sb)
        {
            MakeUpdateBy1(sb);
            MakeUpdateBy2(sb);
        }

        private void makeDeleteMethods(StringBuilder sb)
        {
            MakeCreateDelete(sb, "Delete");
            MakeCreateDeleteByField(sb, fkey1, "DeleteAllBy" + fkey1.ReferencedClass.Name);
            MakeCreateDeleteByField(sb, fkey2, "DeleteAllBy" + fkey2.ReferencedClass.Name);
        }

        protected abstract void MakeHeader(StringBuilder sb);

        protected abstract void MakeCreateDelete(StringBuilder sb, string method);

        protected abstract void MakeCreateDeleteByField(StringBuilder sb, ClassFieldObject fkey, string method);

        protected abstract void MakeUpdateBy1(StringBuilder sb);

        protected abstract void MakeUpdateBy2(StringBuilder sb);

        protected abstract void MakeConstructor(StringBuilder sb);

        protected abstract void MakeFooter(StringBuilder sb);

        private ClassFieldObject fkey1;
        private ClassFieldObject fkey2;
    }
}
