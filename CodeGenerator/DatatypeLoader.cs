using System;
using System.Text.RegularExpressions;

namespace CodeGenerator
{
    public class DatatypeLoader
    {
        //input string from schema
        public DatatypeLoader(string input)
        {
            parseInput(input);
            validateDatatype();
            constructInstance();
        }

        //values retrieved from the database
        public DatatypeLoader(string dbName, int dbLength, int dbPrecision, int dbScale)
        {
            datatype = Sql.DatatypeFactory.MakeDatatype(dbName);

            if (datatype.SetCharLength)
                length = dbLength;
            else
                length = datatype.DefaultFieldLength;

            if (datatype.SetPrecisionScale)
            {
                precision = dbPrecision;
                scale = dbScale;
            }
            else //ignores unnessesary sql server detail
            {
                precision = datatype.DefaultPrecision;
                scale = datatype.DefaultScale;
            }
            validateDatatype();
            constructInstance();
        }

        public Sql.DatatypeInstance Datatype { get { return dtInstance; } }

        private void parseInput(string s)
        {
            string name = s;
            int a = s.IndexOf("(");
            if (a == -1) //use default values
                loadDatatype(name, -1, -1);
            else //must parse number(s) in parens
            {
                int b = s.IndexOf(")");
                if (b == -1)
                    throw new SqlDatatypeFormatException("wrong data format in schema: " + s);

                name = s.Substring(0, a);
                datatype = Sql.DatatypeFactory.MakeDatatype(name);

                string numbers = s.Substring(a + 1);
                numbers = numbers.Substring(0, numbers.Length - 1);
                int param1 = -1;
                int param2 = -1;
                int c = numbers.IndexOf(",");
                try
                {
                    if (c == -1)
                        param1 = int.Parse(numbers);
                    else
                    {
                        param1 = int.Parse(numbers.Substring(0, c));
                        param2 = int.Parse(numbers.Substring(c + 1));
                    }
                    loadDatatype(name, param1, param2);
                }
                catch (System.FormatException e)
                {
                    throw new SqlDatatypeFormatException("wrong data format in schema: " + s + "\n" + e.ToString());
                }
            }
        }

        private void loadDatatype(string name, int param1, int param2)
        {
            datatype = Sql.DatatypeFactory.MakeDatatype(name);

            if (param1 == -1 && param2 == -1)
            {
                length = datatype.DefaultFieldLength;
                precision = datatype.DefaultPrecision;
                scale = datatype.DefaultScale;
            }
            else
            {
                if (!datatype.SetCharLength && !datatype.SetPrecisionScale && (param1 != -1 || param2 != -1))
                    throw new SqlDatatypeFormatException("Cannot specify column length or precision and/or scale on data type " + datatype.Name);
                if (!datatype.SetPrecisionScale && param1 != -1 && param2 != -1)
                    throw new SqlDatatypeFormatException("Cannot specify column precision and/or scale on data type " + datatype.Name);

                if (datatype.SetPrecisionScale)
                {
                    length = datatype.DefaultFieldLength;
                    precision = param1;
                    if (param2 == -1)
                        scale = datatype.DefaultScale;
                    else
                        scale = param2;
                }
                else if (datatype.SetCharLength)
                {
                    length = param1;
                    precision = datatype.DefaultPrecision;
                    scale = datatype.DefaultScale;
                }
            }
        }

        private void validateDatatype()
        {
            if (datatype.SetCharLength && length > datatype.MaxCharLength)
                throw new SqlDatatypeFormatException("The size (" + length + ") exceeds the maximum allowed (" + datatype.MaxCharLength +
                    ") for column " + datatype.Name + ")");

            if (datatype.SetPrecisionScale && precision > datatype.MaxPrecision)
                throw new SqlDatatypeFormatException("The size (" + precision + ") exceeds the maximum allowed (" + datatype.MaxPrecision +
                    ") for column " + datatype.Name + ")");

            if (datatype.SetPrecisionScale && scale > precision)
                throw new SqlDatatypeFormatException("The size (" + scale + ") exceeds precision (" + precision +
                    ") for column " + datatype.Name + ")");
        }

        private void constructInstance()
        {
            string sqlName;
            if (datatype.SetCharLength)
            {
                if (length == -1)
                    sqlName = datatype.Name;
                else
                    sqlName = datatype.Name + "(" + length + ")";
            }
            else if (datatype.SetPrecisionScale)
            {
                if (precision <= 0 && scale <= 0)
                    sqlName = datatype.Name;
                else if (precision > 0 && scale <= 0)
                    sqlName = datatype.Name + "(" + precision + ")";
                else
                    sqlName = datatype.Name + "(" + precision + "," + scale + ")";
            }
            else
                sqlName = datatype.Name;

            dtInstance = new Sql.DatatypeInstance(datatype.Name, sqlName, length, precision, scale, datatype.IsTextType);
        }

        private Sql.DatatypeInstance dtInstance;
        private Sql.BaseDatatype datatype;
        private int length;
        private int precision;
        private int scale;
    }
}
