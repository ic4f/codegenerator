using System;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Represents a table field in a SQL Server db
    /// </summary>
    public class TableField
    {
        public TableField(
            Table parent,
            string name,
            string datatype,
            string sqlDatatype,
            int length,
            int precision,
            int scale,
            bool isIdentity)
        {
            this.parent = parent;
            this.name = name;
            this.datatype = datatype;
            this.sqlDatatype = sqlDatatype;
            this.length = length;
            this.precision = precision;
            this.scale = scale;
            this.isIdentity = isIdentity;

            SqlHelper sh = new SqlHelper();
            sqlName = sh.GetSqlName(name);
        }

        public Table Table { get { return parent; } }

        public string Name { get { return name; } }

        public string SqlName { get { return sqlName; } }

        public string Datatype { get { return datatype; } }

        public string SqlDatatype { get { return sqlDatatype; } }

        public int Length { get { return length; } }

        public int Precision { get { return precision; } }

        public int Scale { get { return scale; } }

        public bool IsIdentity { get { return isIdentity; } }

        private Table parent;
        private string name;
        private string sqlName;
        private string datatype;
        private string sqlDatatype;
        private int length;
        private int precision;
        private int scale;
        private bool isIdentity;
    }
}