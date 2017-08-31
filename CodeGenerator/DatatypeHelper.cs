using System;
using System.Collections;

namespace CodeGenerator
{
	public class DatatypeHelper
	{
		public static string GenerateReturnType = "generate"; //indicates that the type will be auto-generated
		public static string VoidReturnType = "void";
		public static string DataSetReturnType = "DataSet";
		public static string ArrayListReturnType = "ArrayList";

		public DatatypeHelper() {}

		public bool IsSqlConvertible(string csDatatype)
		{
			return 
				csDatatype == "bool" || 
				csDatatype == "byte" ||
				csDatatype == "sbyte" ||
				csDatatype == "char" ||
				csDatatype == "decimal" ||
				csDatatype == "double" ||
				csDatatype == "float" ||
				csDatatype == "int" ||
				csDatatype == "uint" ||
				csDatatype == "long" ||
				csDatatype == "ulong" ||
				csDatatype == "short" ||
				csDatatype == "ushort" ||
				csDatatype == "string" ||
				csDatatype == "DateTime";
		}

		public bool IsValidSprocReturnType(string returnType)
		{
			return 
				returnType == GenerateReturnType ||
				returnType == VoidReturnType || 
				returnType == DataSetReturnType || 
				returnType == ArrayListReturnType || 
				IsSqlConvertible(returnType);
		}

		public string GetConvertTo(string csDatatype) //expand to include more types!
		{
			if (csDatatype == "int")
				return "Convert.ToInt32";
			else if (csDatatype == "string")
				return "Convert.ToString";
			else if (csDatatype == "double")
				return "Convert.ToDouble";
			else if (csDatatype =="decimal")
				return "Convert.ToDecimal";
			else if (csDatatype == "DateTime")
				return "Convert.ToDateTime";
			else if (csDatatype == "byte")
				return "Convert.ToByte";
			else if (csDatatype == "bool")
				return "Convert.ToBoolean";
			else						
				throw new UnknownCsDatatypeException("unknown datatype: " + csDatatype);
		}

		public string GetCsDatatype(string sqlDatatype)
		{
			if (sqlDatatype == "bigint")
				return "long";

			else if (sqlDatatype ==  "int")
				return "int";

			else if (sqlDatatype ==  "smallint")
				return "short";

			else if (sqlDatatype ==  "tinyint")
				return "byte";

			else if (sqlDatatype.StartsWith("decimal") || sqlDatatype.StartsWith("numeric") || 
				sqlDatatype ==  "money" || sqlDatatype ==  "smallmoney")
				return "decimal"; 

			else if (sqlDatatype ==  "real")
				return "float";

			else if (sqlDatatype ==  "float")
				return "double"; 

			else if (sqlDatatype.StartsWith("char") || sqlDatatype.StartsWith("varchar") || 
				sqlDatatype.StartsWith("nchar") || sqlDatatype.StartsWith("nvarchar") ||
				sqlDatatype ==  "text" || sqlDatatype ==  "ntext")
				return "string";

			else if (sqlDatatype ==  "datetime" || sqlDatatype ==  "smalldatetime")
				return "DateTime";

			else if (sqlDatatype.StartsWith("binary") || sqlDatatype.StartsWith("varbinary")) 
				return "byte[]";

			else if (sqlDatatype ==  "timestamp")
				return "byte[]"; 

			else if (sqlDatatype ==  "image")
				return "byte[]"; 

			else if (sqlDatatype ==  "uniqueidentifier")
				return "int"; 

			else if (sqlDatatype ==  "bit")
				return "bool";

			else				
				throw new UnknownCsDatatypeException("unknown datatype: " + sqlDatatype);
		}

		public string GetDataReaderMethodForSqlType(string sqlDatatype)
		{
			if (sqlDatatype == "bigint")
				return "GetInt64";

			else if (sqlDatatype == "int")
				return "GetInt32";

			else if (sqlDatatype == "smallint")
				return "GetInt16";

			else if (sqlDatatype == "tinyint")
				return "GetByte";

			else if (sqlDatatype.StartsWith("decimal") || sqlDatatype.StartsWith("numeric") || 
				sqlDatatype == "money" || sqlDatatype == "smallmoney")
				return "GetDecimal"; 

			else if (sqlDatatype == "real")
				return "GetFloat";

			else if (sqlDatatype == "float")
				return "GetDouble"; 

			else if (sqlDatatype.StartsWith("char") || sqlDatatype.StartsWith("varchar") || 
				sqlDatatype.StartsWith("nchar") || sqlDatatype.StartsWith("nvarchar") ||
				sqlDatatype == "text" || sqlDatatype == "ntext")
				return "GetString";

			else if (sqlDatatype == "datetime" || sqlDatatype == "smalldatetime")
				return "GetDateTime";

			else if (sqlDatatype.StartsWith("binary") || sqlDatatype.StartsWith("varbinary")) 
				return "GetValue";

			else if (sqlDatatype == "timestamp")
				return "GetValue"; 

			else if (sqlDatatype == "image")
				return "GetValue"; 

			else if (sqlDatatype == "uniqueidentifier")
				return "GetInt32"; 

			else if (sqlDatatype == "bit")
				return "GetBoolean";
					
			throw new UnknownSqlDatatypeException("unknown datatype: " + sqlDatatype);
		}		

		public string GetDataReaderMethodForCsType(string csDatatype)
		{
			if (csDatatype == "char")
				return "GetChar";

			else if (csDatatype == "int" || csDatatype == "uint")
				return "GetInt32";

			else if (csDatatype == "short" || csDatatype == "ushort")
				return "GetInt16";

			else if (csDatatype == "byte" || csDatatype == "sbyte")
				return "GetByte";

			else if (csDatatype == "decimal")
				return "GetDecimal"; 

			else if (csDatatype == "float")
				return "GetFloat";

			else if (csDatatype == "double")
				return "GetDouble"; 

			else if (csDatatype == "string")
				return "GetString";

			else if (csDatatype == "DateTime")
				return "GetDateTime";

			else if (csDatatype == "long" || csDatatype == "ulong")
				return "GetLong";		

			else if (csDatatype == "bool")
				return "GetBoolean";
					
			throw new UnknownCsDatatypeException("unknown cs datatype: " + csDatatype);
		}		
	}
}
