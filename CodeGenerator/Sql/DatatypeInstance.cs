using System;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Represents a specific instance of a sql datatype with type/length/precision/scale values
    /// </summary>
    public class DatatypeInstance
    {
        public DatatypeInstance(
            string name,
            string fullSqlName,
            int length,
            int precision,
            int scale,
            bool isTextType)
        {
            this.name = name;
            this.fullSqlName = fullSqlName;
            this.length = length;
            this.precision = precision;
            this.scale = scale;
            this.isTextType = isTextType;
        }

        public string Name { get { return name; } }

        public string FullSqlName { get { return fullSqlName; } }

        public int Length { get { return length; } }

        public int Precision { get { return precision; } }

        public int Scale { get { return scale; } }

        public bool IsTextType { get { return isTextType; } }

        private string name;
        private string fullSqlName;
        private int length;
        private int precision;
        private int scale;
        private bool isTextType;
    }
}
