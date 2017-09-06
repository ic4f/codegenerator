using System;

namespace CodeGenerator.Code
{
    public abstract class BaseHelper
    {
        public abstract string FileExtension { get; }
        public abstract string MakeVarName(string s);
        public abstract bool IsConvertibleToPrimitiveType(string sqlDatatype);
        public abstract bool IsDateTimeType(string sqlDatatype);
        public abstract string GetDatatype(string t, bool isEncrypted, bool convertEncrypted);
    }
}
