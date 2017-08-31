using System;
using System.Collections;

namespace CodeGenerator.ParseTree
{
	public class FieldNode : BaseFieldNode
	{
		public FieldNode(
			string name,
			string sqldatatype, 
			bool identity,
			bool primarykey,
			string refTable,
			string refField,
			bool unique,
			bool encrypted,
			string display,
			bool excludefromtable,
			bool includewithparenttable,
			bool includeInList,
			bool isDefaultSort,
			string readonlytype) : base(
				name, sqldatatype, display, excludefromtable, includewithparenttable, includeInList, isDefaultSort)
		{
			this.identity = identity;
			this.primarykey = primarykey;
			this.refTable = refTable;
			this.refField = refField;
			this.unique = unique;
			this.encrypted = encrypted;
			this.readonlytype = readonlytype;
		}

		public override void Validate()
		{
			if (readonlytype != null && readonlytype != "")
				Application.ReadonlyFieldTypeFactory.GetReadonlyTypeClassType(readonlytype);

			Sql.DatatypeInstance di = new DatatypeLoader(SqlDatatype).Datatype;
				
			if (encrypted && 
				((di.Name != "binary" && di.Name != "varbinary") || di.Length < 8))
				throw new InvalidEncryptionFormatException("Encryption allowed only on binary/varbinary with length >= 8");
		}

		public bool IsIdentity { get { return identity; } }

		public bool IsPrimaryKey { get { return primarykey; } }

		public string RefTable { get { return refTable; } }

		public string RefField { get { return refField; } }

		public bool IsUnique { get { return unique; } }

		public bool IsEncrypted { get { return encrypted; } }

		public string ReadonlyType { get { return readonlytype; } }

		private bool identity;
		private bool primarykey;
		private string refTable;
		private string refField;
		private bool unique;
		private bool encrypted;
		private string readonlytype;
	}
}