using System;

namespace CodeGenerator.ParseTree
{
    public abstract class BaseFieldNode
    {
        public BaseFieldNode(
            string name,
            string sqldatatype,
            string display,
            bool excludefromtable,
            bool includewithparenttable,
            bool includeInList,
            bool isDefaultSort)
        {
            this.name = name;
            this.sqldatatype = sqldatatype;
            this.display = display;
            this.excludefromtable = excludefromtable;
            this.includewithparenttable = includewithparenttable;
            this.includeInList = includeInList;
            this.isDefaultSort = isDefaultSort;
        }

        public virtual void Validate()
        {
            DatatypeLoader dl = new DatatypeLoader(sqldatatype);
        }

        public string Name { get { return name; } }

        public string SqlDatatype { get { return sqldatatype; } }

        public string Display { get { return display; } }

        public bool ExcludeFromTable { get { return excludefromtable; } }

        public bool IncludeWithParentTable { get { return includewithparenttable; } }

        public bool IncludeInList { get { return includeInList; } }

        public bool IsDefaultSort { get { return isDefaultSort; } }

        private string name;
        private string sqldatatype;
        private string display;
        private bool excludefromtable;
        private bool includewithparenttable;
        private bool includeInList;
        private bool isDefaultSort;
    }
}