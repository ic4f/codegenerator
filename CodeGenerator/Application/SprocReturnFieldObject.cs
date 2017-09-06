using System;

namespace CodeGenerator.Application
{
    public class SprocReturnFieldObject
    {
        public SprocReturnFieldObject(
            SprocObject parent,
            string name,
            string csDatatype,
            string sortExpression,
            string display)
        {
            this.parent = parent;
            this.name = name;
            this.csDatatype = csDatatype;
            this.sortExpression = sortExpression;
            this.display = display;
        }

        public SprocObject Sproc { get { return parent; } }

        public string Name { get { return name; } }

        public string CsDatatype { get { return csDatatype; } }

        public string SortExpression { get { return sortExpression; } }

        public string Display { get { return display; } }

        private SprocObject parent;
        private string name;
        private string csDatatype;
        private string sortExpression;
        private string display;
    }
}
