using System;

namespace CodeGenerator.Sql
{
    /// <summary>
    /// Defines sql datatypes
    /// </summary>
    public class Bigint : BaseDatatype
    {
        public override string Name { get { return "bigint"; } }
        public override int DefaultFieldLength { get { return 8; } }
        public override object ConvertValue(string input) { return Convert.ToInt64(input); }
    }

    public class Binary : BaseDatatype
    {
        public override string Name { get { return "binary"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 8000; } }
        public override object ConvertValue(string input) { return Convert.ToByte(input); }
    }

    public class Bit : BaseDatatype
    {
        public override string Name { get { return "bit"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override object ConvertValue(string input)
        {
            if (input == "1")
                return true;
            else if (input == "0")
                return false;
            else
                return Convert.ToBoolean(input);
        }
    }

    public class Char : BaseDatatype
    {
        public override string Name { get { return "char"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 8000; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }

    public class Datetime : BaseDatatype
    {
        public override string Name { get { return "datetime"; } }
        public override int DefaultFieldLength { get { return 8; } }
        public override object ConvertValue(string input) { return Convert.ToDateTime(input); }
    }

    public class Decimal : BaseDatatype
    {
        public override string Name { get { return "decimal"; } }
        public override int DefaultFieldLength { get { return 17; } } //can be 9, but 17 is maximum	
        public override bool SetPrecisionScale { get { return true; } }
        public override int DefaultPrecision { get { return 18; } }
        public override int MaxPrecision { get { return 38; } }
        public override int DefaultScale { get { return 0; } }
        public override object ConvertValue(string input) { return Convert.ToDecimal(input); }
    }

    public class Float : BaseDatatype
    {
        public override string Name { get { return "float"; } }
        public override int DefaultFieldLength { get { return 8; } }
        public override object ConvertValue(string input) { return Convert.ToDouble(input); }
    }

    public class Image : BaseDatatype
    {
        public override string Name { get { return "image"; } }
        public override int DefaultFieldLength { get { return 16; } }
        public override object ConvertValue(string input) { return Convert.ToByte(input); }
    }

    public class Int : BaseDatatype
    {
        public override string Name { get { return "int"; } }
        public override int DefaultFieldLength { get { return 4; } }
        public override object ConvertValue(string input) { return Convert.ToInt32(input); }
    }

    public class Money : BaseDatatype
    {
        public override string Name { get { return "money"; } }
        public override int DefaultFieldLength { get { return 8; } }
        public override object ConvertValue(string input) { return Convert.ToDouble(input); }
    }

    public class Nchar : BaseDatatype
    {
        public override string Name { get { return "nchar"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 4000; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }

    public class Ntext : BaseDatatype
    {
        public override string Name { get { return "ntext"; } }
        public override int DefaultFieldLength { get { return 16; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }

    public class Numeric : BaseDatatype
    {
        public override string Name { get { return "numeric"; } }
        public override int DefaultFieldLength { get { return 17; } } //can be 9, but 17 is maximum	
        public override bool SetPrecisionScale { get { return true; } }
        public override int DefaultPrecision { get { return 18; } }
        public override int MaxPrecision { get { return 38; } }
        public override int DefaultScale { get { return 0; } }
        public override object ConvertValue(string input) { return Convert.ToDecimal(input); }
    }

    public class Nvarchar : BaseDatatype
    {
        public override string Name { get { return "nvarchar"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 4000; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }

    public class Real : BaseDatatype
    {
        public override string Name { get { return "real"; } }
        public override int DefaultFieldLength { get { return 4; } }
        public override object ConvertValue(string input) { return Convert.ToDouble(input); }
    }

    public class Smalldatetime : BaseDatatype
    {
        public override string Name { get { return "smalldatetime"; } }
        public override int DefaultFieldLength { get { return 4; } }
        public override object ConvertValue(string input) { return Convert.ToDateTime(input); }
    }

    public class Smallint : BaseDatatype
    {
        public override string Name { get { return "smallint"; } }
        public override int DefaultFieldLength { get { return 2; } }
        public override object ConvertValue(string input) { return Convert.ToInt16(input); }
    }

    public class Smallmoney : BaseDatatype
    {
        public override string Name { get { return "smallmoney"; } }
        public override int DefaultFieldLength { get { return 4; } }
        public override object ConvertValue(string input) { return Convert.ToDouble(input); }
    }

    public class Text : BaseDatatype
    {
        public override string Name { get { return "text"; } }
        public override int DefaultFieldLength { get { return 16; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }

    public class Timestamp : BaseDatatype
    {
        public override string Name { get { return "timestamp"; } }
        public override int DefaultFieldLength { get { return 8; } }
        public override object ConvertValue(string input) { return Convert.ToByte(input); }
    }

    public class Tinyint : BaseDatatype
    {
        public override string Name { get { return "tinyint"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override object ConvertValue(string input) { return Convert.ToInt16(input); }
    }

    public class Uniqueidentifier : BaseDatatype
    {
        public override string Name { get { return "uniqueidentifier"; } }
        public override int DefaultFieldLength { get { return 16; } }
        public override object ConvertValue(string input) { return input; } //no conversion
    }

    public class Varbinary : BaseDatatype
    {
        public override string Name { get { return "varbinary"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 8000; } }
        public override object ConvertValue(string input) { return Convert.ToByte(input); }
    }

    public class Varchar : BaseDatatype
    {
        public override string Name { get { return "varchar"; } }
        public override int DefaultFieldLength { get { return 1; } }
        public override bool SetCharLength { get { return true; } }
        public override int MaxCharLength { get { return 8000; } }
        public override object ConvertValue(string input) { return Convert.ToString(input); }
        public override bool IsTextType { get { return true; } }
    }
}
