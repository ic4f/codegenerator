using System;
using System.Text;
using System.Collections;
using CodeGenerator.Application;

namespace CodeGenerator.Code
{
    /// <summary>
    /// Generates [class name] for data layer (instance class)
    /// assume only 1 primary key
    /// </summary>
    public abstract class InstanceClassGenerator : ClassGenerator
    {
        public InstanceClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace)
        {
            pkeyField = c.PrimaryKeyField1;
        }

        public override string GetCode()
        {
            StringBuilder sb = new StringBuilder();
            MakeHeader(sb);
            MakeConstructor(sb);
            MakeProperties(sb);
            if (Class.Type != ClassType.Readonly && Class.Type != ClassType.Final && !Class.IsExternal)
                MakeUpdate(sb);
            MakePrivate(sb);
            MakeFooter(sb);
            return sb.ToString();
        }

        private void MakeProperties(StringBuilder sb)
        {
            foreach (BaseClassField f in Class.InstanceIDataFieldList)
            {
                bool isLocal = false;
                if (f.ParentClass == Class)
                    isLocal = true;

                ClassFieldObject cf = f as ClassFieldObject;
                if (cf != null)
                    MakeFieldProperty(sb, cf, isLocal);
                else
                    MakeAddFieldProperty(sb, (ClassAddFieldObject)f, isLocal);
            }
        }

        protected ClassFieldObject PrimaryKeyField { get { return pkeyField; } }

        protected abstract void MakeFieldProperty(StringBuilder sb, ClassFieldObject f, bool isLocal);

        protected abstract void MakeAddFieldProperty(StringBuilder sb, ClassAddFieldObject f, bool isLocal);

        protected abstract void MakeHeader(StringBuilder sb);

        protected abstract void MakeConstructor(StringBuilder sb);

        protected abstract void MakeUpdate(StringBuilder sb);

        protected abstract void MakePrivate(StringBuilder sb);

        protected abstract void MakeFooter(StringBuilder sb);

        private ClassFieldObject pkeyField;
    }
}
