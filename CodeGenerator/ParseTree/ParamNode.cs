using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
    public class ParamNode : IParseTreeNode
    {
        public ParamNode(string name, string csDatatype, bool isEncrypted)
        {
            this.name = name;
            this.csDatatype = csDatatype;
            this.isEncrypted = isEncrypted;
        }

        public void Validate()
        {
            DatatypeHelper dtHelper = new DatatypeHelper();
            if (!dtHelper.IsSqlConvertible(csDatatype))
                throw new UnknownCsDatatypeException("Datatype cannot be converted to a Sql datatype: " + csDatatype);
        }

        public string Name { get { return name; } }

        public string CsDatatype { get { return csDatatype; } }

        public bool IsEncrypted { get { return isEncrypted; } }

        private string name;
        private string csDatatype;
        private bool isEncrypted;
    }
}