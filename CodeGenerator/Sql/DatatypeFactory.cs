using System;
using System.Collections;

namespace CodeGenerator.Sql
{
	/// <summary>
	/// returns a sql datatype object based on its name
	/// </summary>
	public class DatatypeFactory
	{
		public static BaseDatatype MakeDatatype(string name)
		{
			switch(name) 
			{					
				case "bigint" :
					return new Bigint();
				case "binary" :
					return new Binary();
				case "bit" :
					return new Bit();
				case "char" :
					return new Char();
				case "datetime" :
					return new Datetime();
				case "decimal" :
					return new Decimal();
				case "float" :
					return new Float();
				case "image" :
					return new Image();
				case "int" :
					return new Int();
				case "money" :
					return new Money();
				case "nchar" :
					return new Nchar();
				case "ntext" :
					return new Ntext();
				case "numeric" :
					return new Numeric();
				case "nvarchar" :
					return new Nvarchar();
				case "real" :
					return new Real();
				case "smalldatetime" :
					return new Smalldatetime();
				case "smallint" :
					return new Smallint();
				case "smallmoney" :
					return new Smallmoney();
				case "text" :
					return new Text();
				case "timestamp" :
					return new Timestamp();
				case "tinyint" :
					return new Tinyint();
				case "uniqueidentifier" :
					return new Uniqueidentifier();
				case "varbinary" :
					return new Varbinary();
				case "varchar" :
					return new Varchar();
				default:
					throw new UnknownSqlDatatypeException("Unknown datatype: " + name);
			}
		}

		private DatatypeFactory() {}
	}
}
