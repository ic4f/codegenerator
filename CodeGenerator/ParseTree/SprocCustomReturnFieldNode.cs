using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
    public class SprocCustomReturnFieldNode : ISprocReturnFieldNode
    {
        public SprocCustomReturnFieldNode(string name, string csDatatype, string sortExpression, string display)
        {
            this.name = name;
            this.csDatatype = csDatatype;
            this.sortExpression = sortExpression;
            this.display = display;
        }

        public void Validate()
        {
            DatatypeHelper dtHelper = new DatatypeHelper();
            if (!dtHelper.IsSqlConvertible(csDatatype))
                throw new UnknownCsDatatypeException("Datatype cannot be derived (converted from) a Sql datatype: " + csDatatype);
        }

        public string Name { get { return name; } }

        public string CsDatatype { get { return csDatatype; } }

        public string SortExpression { get { return sortExpression; } }

        public string Display { get { return display; } }

        private string name;
        private string csDatatype;
        private string sortExpression;
        private string display;
    }
}