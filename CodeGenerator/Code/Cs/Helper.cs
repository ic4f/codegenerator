using System;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code.Cs
{
    public class Helper : BaseHelper
    {
        public Helper() : base() { }

        public override string FileExtension { get { return ".cs"; } }

        public override string MakeVarName(string s)
        {
            char c = Char.ToLower(s[0]);
            s = s.Remove(0, 1);
            s = s.Insert(0, c.ToString());
            return s;
        }

        public override string GetDatatype(string sqlDatatype, bool isEncrypted, bool convertEncrypted)
        {
            if (isEncrypted && convertEncrypted)
                return "string";
            else
                return new DatatypeHelper().GetCsDatatype(sqlDatatype);
        }

        public override bool IsConvertibleToPrimitiveType(string sqlDatatype)
        {
            return (
                sqlDatatype == "bigint" ||
                sqlDatatype == "int" ||
                sqlDatatype == "smallint" ||
                sqlDatatype == "tinyint" ||
                sqlDatatype == "decimal" ||
                sqlDatatype == "numeric" ||
                sqlDatatype == "money" ||
                sqlDatatype == "smallmoney" ||
                sqlDatatype == "real" ||
                sqlDatatype == "float" ||
                sqlDatatype == "uniqueidentifier" ||
                sqlDatatype == "bit" ||
                sqlDatatype == "datetime" ||
                sqlDatatype == "smalldatetime");
        }

        public override bool IsDateTimeType(string sqlDatatype)
        {
            return (sqlDatatype == "datetime" || sqlDatatype == "smalldatetime");
        }
    }
}
