using System;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Represents a SQL Server datatype
    /// </summary>
    public abstract class BaseDatatype
    {
        //datatype name according to sql server syntax
        public abstract string Name { get; }

        //field length in bytes assigned by default
        public abstract int DefaultFieldLength { get; }

        //converts a string into an apropriate datatype. Although it returns an object datatype, 
        //this method allows to catch an exception in case of incompatible types (i.e. converting "abc" to an int)
        //example of usage: construction of DefaultConstraint: allows to check the datatype of the supplied default value
        public abstract object ConvertValue(string input);

        //permission to set length (true for binary, char, float, nchar, nvarchar, varchar, varbinary)
        public virtual bool SetCharLength { get { return false; } }

        //maximum allowed length or -1 if SetCharLength = false
        public virtual int MaxCharLength { get { return -1; } }

        //permission to set numeric precision and/or scale (true for decimal, numeric)
        public virtual bool SetPrecisionScale { get { return false; } }

        //numeric precision in bytes assigned by default or -1 if SetPrecisionScale = false
        public virtual int DefaultPrecision { get { return -1; } }

        //maximum allowed numeric precision or -1 if SetPrecisionScale = false (max scale depends on precision)
        public virtual int MaxPrecision { get { return -1; } }

        //numeric scale assigned by default or -1 if SetPrecisionScale = false
        public virtual int DefaultScale { get { return -1; } }

        //true if the value of this type can be represented in code as a string without conversion
        public virtual bool IsTextType { get { return false; } }
    }
}
