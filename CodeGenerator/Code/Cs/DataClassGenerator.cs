using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator.Code.Cs
{
	public class DataClassGenerator : ClassGenerator
	{
		public DataClassGenerator(ClassObject c, string parentNamespace) : base(c, parentNamespace) 
		{
			pkeyField = c.PrimaryKeyField1;
			className = c.Name + "Data";
		}

		public override string GetCode()
		{
			StringBuilder sb = new StringBuilder();

			makeHeader(sb);

			if (Class.Type != ClassType.Readonly && !Class.IsExternal)
			{
				makeCreateRecord(sb);
				makeDeleteRecord(sb);
			}

			makeGetList(sb);				
			
			makeGetRecords(sb);
			makeGetRecordsP(sb);
			if (Class.HasTextField)	
				makeGetRecordsPS(sb);

			makeGetByField(sb);
			makeGetByLink(sb);
			makeGetLinks(sb);
			makeCustomMethods(sb);

			sb.AppendFormat("{0}#region private\n\n", Tabs(2));
			makeLoadTableMethod(sb);
			makeLoadListMethod(sb);
			makeLoadCustomTableMethods(sb);			
			sb.AppendFormat("\n{0}#endregion\n", Tabs(2));
			makeFooter(sb);
			return sb.ToString();
		}

		public override string GetFileName()
		{
			return className + Helper.FileExtension;
		}

		protected ClassFieldObject PrimaryKeyField { get { return pkeyField; } }

		protected string ClassName { get { return className; } }

		protected override BaseHelper MakeHelper() { return new Helper(); }

		#region CreateRecord
		private void makeCreateRecord(StringBuilder sb)
		{
			string method = "Create";
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);

			PublicMethodCount++;

			sb.AppendFormat("{0}public int {1}(\n", Tabs(2), method);

			foreach(ClassFieldObject f in Class.FieldsList)				
				if (!f.IsIdentity && !f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType)
					sb.AppendFormat("{0}{1} {2},\n", 
						Tabs(3), Helper.GetDatatype(f.Datatype.Name, f.IsEncrypted, true), f.Name);

			RemoveLastComma(sb);			
			sb.Append(")\n");
			sb.AppendFormat("{0}{{\n", Tabs(2));

			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", 
				Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n\n", Tabs(3));

			string val;
			foreach(ClassFieldObject f in Class.FieldsList)			
				if (!f.IsIdentity && !f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType)
				{
					if (f.IsEncrypted)				
						val = "(new Core.EncryptionTool()).Encrypt(" + f.Name + ")";
					else
						val = f.Name;

					if (f.IsForeignKey)
					{
						if (Helper.IsConvertibleToPrimitiveType(f.Datatype.Name))
							sb.AppendFormat("{0}if ({1} > 0)\n", Tabs(3), f.Name); //assume only numerics for fkeys
						else
							sb.AppendFormat("{0}if ({1} != null)\n", Tabs(3), f.Name);
						sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(4), f.Name, val);
						sb.AppendFormat("{0}else\n", Tabs(3));
						sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", System.DBNull.Value));\n\n", Tabs(4), f.Name);
					}
					else
						sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(3), f.Name, val);
				}
			sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
			sb.AppendFormat("{0}int result = Convert.ToInt32(command.ExecuteScalar());\n", Tabs(3));
			sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
			sb.AppendFormat("{0}return result;\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		#region DeleteRecord
		private void makeDeleteRecord(StringBuilder sb)
		{
			PublicMethodCount++;

			string method = "Delete";			
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public int {1}(int {2}, bool DeleteDependents)\n", Tabs(2), method, PrimaryKeyField.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));

			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", 
				Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n",
				Tabs(3), PrimaryKeyField.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@DeleteDependents\", DeleteDependents));\n",
				Tabs(3));
			sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
			sb.AppendFormat("{0}int result = command.ExecuteNonQuery();\n", Tabs(3));
			sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
			sb.AppendFormat("{0}return result;\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}		
		#endregion

		#region GetList
		private void makeGetList(StringBuilder sb)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, "GetList");
			sb.AppendFormat("{0}#region GetList\n", Tabs(2));
			sb.AppendFormat("{0}public {1}List GetList()\n", Tabs(2), Class.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", 
				Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		#region GetRecords
		private void makeGetRecords(StringBuilder sb)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, "GetRecords");
			sb.AppendFormat("{0}#region GetRecords\n", Tabs(2));
			sb.AppendFormat("{0}public {1}Table GetRecords(string SortExp)\n", Tabs(2), Class.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", 
				Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		#region GetRecordsP
		private void makeGetRecordsP(StringBuilder sb)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, "GetRecordsP");
			sb.AppendFormat("{0}#region GetRecordsP\n", Tabs(2));
			sb.AppendFormat("{0}public {1}Table GetRecordsP(\n", Tabs(2), Class.Name);
			sb.AppendFormat("{0}string SortExp,\n", Tabs(3));
			sb.AppendFormat("{0}int PageSize,\n", Tabs(3));
			sb.AppendFormat("{0}int PageNum)\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		#region GetRecordsPS
		private void makeGetRecordsPS(StringBuilder sb)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, "GetRecordsPS");
			sb.AppendFormat("{0}#region GetRecordsPS\n", Tabs(2));
			sb.AppendFormat("{0}public {1}Table GetRecordsPS(\n", Tabs(2), Class.Name);
			sb.AppendFormat("{0}string SortExp,\n", Tabs(3));
			sb.AppendFormat("{0}int PageSize,\n", Tabs(3));
			sb.AppendFormat("{0}int PageNum,\n", Tabs(3));			
			sb.AppendFormat("{0}string SearchField,\n", Tabs(3));
			sb.AppendFormat("{0}string SearchKeyword)\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchField\", SearchField));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchKeyword\", SearchKeyword));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		private void makeGetByField(StringBuilder sb)
		{
			foreach(ClassFieldObject fkey in Class.ForeignKeyFieldList)
			{
				makeGetRecordsByField(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "Field", fkey);
				makeGetRecordsByFieldP(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "FieldP", fkey);

				if (Class.HasTextField)
					makeGetRecordsByFieldPS(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "FieldPS", fkey);
			}
		}

		private void makeGetByLink(StringBuilder sb)
		{
			foreach(ClassObject refClass in Class.ReferringLinkClassesHash.Values)						
				foreach(ClassFieldObject fkey in refClass.ForeignKeyFieldList)
					if (fkey.ReferencedClass != Class)  //i.e. - only if this key references another class
					{
						makeGetRecordsByField(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "Link", fkey);						
						makeGetRecordsByFieldP(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "LinkP", fkey);
						if (Class.HasTextField)
							makeGetRecordsByFieldPS(sb, "GetRecordsBy" + fkey.ReferencedClass.Name + "LinkPS", fkey);					
					}	
		}

		private void makeGetLinks(StringBuilder sb)
		{
			foreach(ClassObject refClass in Class.ReferringLinkClassesHash.Values)	//example: userDpt, userRole, userAcc, userUsrGroup						
				foreach(ClassFieldObject fkey in refClass.ForeignKeyFieldList) //should be just 1 other fkey
				{
					if (fkey.ReferencedClass != Class)  //i.e. - only if this key references another class
					{					
						makeGetLinks(sb, fkey);
						makeGetLinksP(sb, fkey);
						if (Class.HasTextField)
							makeGetLinksPS(sb, fkey);	

						foreach(ClassObject refClass2 in Class.ReferringLinkClassesHash.Values)	//example: userDpt, userRole, userAcc, userUsrGrou						
							if (refClass2 != refClass)							
								foreach(ClassFieldObject fkey2 in refClass2.ForeignKeyFieldList) //should be just 1 other fkey								
									if (fkey2.ReferencedClass != Class)  //i.e. - only if this key references another class
									{
										makeGetLinksByField(sb, fkey, fkey2);
										makeGetLinksByFieldP(sb, fkey, fkey2);
									}																					
					}	
				}
		}

		private void makeGetLinks(StringBuilder sb, ClassFieldObject link)
		{
			PublicMethodCount++;

			string method = "Get" + link.ReferencedClass.Name + "Links"; 
			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}List {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0} {1},", Helper.GetDatatype(link.Datatype.Name, link.IsEncrypted, false), link.Name);
			sb.Append(" string SortExp)\n");
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), link.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}

		private void makeGetLinksByField(StringBuilder sb, ClassFieldObject link, ClassFieldObject criteria)
		{
			PublicMethodCount++;

			string method = "Get" + link.ReferencedClass.Name + "LinksBy" + criteria.ReferencedClass.Name;
			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}List {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0} {1}, ", Helper.GetDatatype(link.Datatype.Name, link.IsEncrypted, false), link.Name);
			sb.AppendFormat("{0} {1}, \n", Helper.GetDatatype(criteria.Datatype.Name, criteria.IsEncrypted, false), criteria.Name);
			sb.Append(" string SortExp)\n");
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), link.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), criteria.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}

		private void makeGetLinksP(StringBuilder sb, ClassFieldObject link)
		{
			PublicMethodCount++;

			string method = "Get" + link.ReferencedClass.Name + "LinksP"; 
			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}List {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0} {1}, string SortExp, int PageSize, int PageNum)\n", Helper.GetDatatype(link.Datatype.Name, link.IsEncrypted, false), link.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), link.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}

		private void makeGetLinksPS(StringBuilder sb, ClassFieldObject link)
		{
			PublicMethodCount++;

			string method = "Get" + link.ReferencedClass.Name + "LinksPS"; 
			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}List {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0}{1} {2},\n", Tabs(3), Helper.GetDatatype(link.Datatype.Name, link.IsEncrypted, false), link.Name);
			sb.AppendFormat("{0}string SortExp,\n", Tabs(3));
			sb.AppendFormat("{0}int PageSize,\n", Tabs(3));
			sb.AppendFormat("{0}int PageNum,\n", Tabs(3));
			sb.AppendFormat("{0}string SearchField,\n", Tabs(3));
			sb.AppendFormat("{0}string SearchKeyword)\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), link.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchField\", SearchField));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchKeyword\", SearchKeyword));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}

		private void makeGetLinksByFieldP(StringBuilder sb, ClassFieldObject link, ClassFieldObject criteria)
		{
			PublicMethodCount++;

			string method = "Get" + link.ReferencedClass.Name + "LinksBy" + criteria.ReferencedClass.Name + "P"; 
			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}List {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0} {1}, ", Helper.GetDatatype(link.Datatype.Name, link.IsEncrypted, false), link.Name);
			sb.AppendFormat("{0} {1}, string SortExp, int PageSize, int PageNum)\n", Helper.GetDatatype(criteria.Datatype.Name, criteria.IsEncrypted, false), criteria.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), link.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), criteria.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadList(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}

		#region GetRecordsByField
		private void makeGetRecordsByField(StringBuilder sb, string method, ClassFieldObject fkey)
		{			
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}Table {2}(", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0} {1},", Helper.GetDatatype(fkey.Datatype.Name, fkey.IsEncrypted, false), fkey.Name);
			sb.Append(" string SortExp)\n");
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), fkey.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}
		#endregion

		#region GetRecordsByFieldP
		private void makeGetRecordsByFieldP(StringBuilder sb, string method, ClassFieldObject fkey)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}Table {2}(\n", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0}{1} {2}\n,", Tabs(3), Helper.GetDatatype(fkey.Datatype.Name, fkey.IsEncrypted, false), fkey.Name);
			sb.AppendFormat("{0}string SortExp,\n", Tabs(3));
			sb.AppendFormat("{0}int PageSize,\n", Tabs(3));
			sb.AppendFormat("{0}int PageNum)\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), fkey.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}	
		#endregion

		#region GetRecordsByFieldPS
		private void makeGetRecordsByFieldPS(StringBuilder sb, string method, ClassFieldObject fkey)
		{
			PublicMethodCount++;

			string sproc = SprocGenerator.GetName(Class, method);
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), method);
			sb.AppendFormat("{0}public {1}Table {2}(\n", Tabs(2), Class.Name, method);
			sb.AppendFormat("{0}{1} {2}\n,", Tabs(3), Helper.GetDatatype(fkey.Datatype.Name, fkey.IsEncrypted, false), fkey.Name);
			sb.AppendFormat("{0}string SortExp,\n", Tabs(3));
			sb.AppendFormat("{0}int PageSize,\n", Tabs(3));
			sb.AppendFormat("{0}int PageNum,\n", Tabs(3));
			sb.AppendFormat("{0}string SearchField,\n", Tabs(3));
			sb.AppendFormat("{0}string SearchKeyword)\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sproc);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {1}));\n", Tabs(3), fkey.Name);
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SortExp\", SortExp));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageSize\", PageSize));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@PageNum\", PageNum));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchField\", SearchField));\n", Tabs(3));
			sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@SearchKeyword\", SearchKeyword));\n", Tabs(3));
			sb.AppendFormat("{0}return LoadTable(command);\n", Tabs(3));
			makeMethodFooter(sb, 2, true);
		}		
		#endregion



		

		private void makeHeader(StringBuilder sb)
		{
			sb.Append("using System;\n");
			sb.Append("using System.Collections;\n");
			sb.Append("using System.Data;\n");
			sb.Append("using System.Data.SqlClient;\n\n");
			sb.AppendFormat("namespace {0}.{1}\n", ParentNamespace, Class.Namespace.Name);
			sb.Append("{\n");
			sb.AppendFormat("{0}public class {1} : Core.DataClass\n", Tabs(1), ClassName);
			sb.AppendFormat("{0}{{\n", Tabs(1));

			PublicMethodCount++;

			sb.AppendFormat("{0}#region constructor \n", Tabs(2));
			sb.AppendFormat("{0}public {1}() : base() {{}}\n", Tabs(2), ClassName);
			sb.AppendFormat("{0}#endregion\n\n", Tabs(2));
		}	

		private void makeCustomMethods(StringBuilder sb)
		{
			foreach (SprocObject sp in Class.StandardReturnTypeSprocsList)
				makeCustomMethod(sp, sb);
			foreach (SprocObject sp in Class.CustomReturnTypeSprocsList)			
				makeCustomMethod(sp, sb);			
		}

		private void makeCustomMethodHeader(SprocObject sp, StringBuilder sb)
		{
			sb.AppendFormat("{0}#region {1}\n", Tabs(2), sp.Name);

			PublicMethodCount++;

			sb.AppendFormat("{0}public {1} {2}(", Tabs(2), getSprocReturnType(sp), sp.Name);

			if (sp.ParamsList.Count > 0)
			{
				sb.Append("\n");
				foreach (SprocParamObject p in sp.ParamsList)				
					if (p.IsEncrypted)
						sb.AppendFormat("{0}string {1},\n", Tabs(3), p.Name);					
					else
						sb.AppendFormat("{0}{1} {2},\n", Tabs(3), p.CsDatatype, p.Name);	
									
				RemoveLastComma(sb);			
			}
			sb.Append(")\n");
			sb.AppendFormat("{0}{{\n", Tabs(2));
		}

		private void makeMethodFooter(StringBuilder sb, int tabs, bool includeRegion)
		{
			sb.AppendFormat("{0}}}\n", Tabs(tabs));
			if (includeRegion)
				sb.AppendFormat("{0}#endregion\n\n", Tabs(tabs));
		}

		private string getSprocReturnType(SprocObject sp)
		{
			if (sp.IsCustomReturnType)
				return Class.Name + "Table" + sp.Name;
			else
				return sp.ReturnType;
		}

		private void makeCustomMethod(SprocObject sp, StringBuilder sb)
		{
			makeCustomMethodHeader(sp, sb);
      
			string sprocName = SprocGenerator.GetCustomName(Class, sp.Name);			
			sb.AppendFormat("{0}SqlCommand command = new SqlCommand(\"{1}\", Connection);\n", Tabs(3), sprocName);
			sb.AppendFormat("{0}command.CommandType = CommandType.StoredProcedure;\n", Tabs(3));

			string val;
			foreach (SprocParamObject p in sp.ParamsList)
			{
				if (p.IsEncrypted)				
					val = "(new Core.EncryptionTool()).Encrypt(" + p.Name + ")";
				else
					val = p.Name;

				sb.AppendFormat("{0}command.Parameters.Add(new SqlParameter(\"@{1}\", {2}));\n", Tabs(3), p.Name, val);			
			}

			if (sp.IsCustomReturnType)
				sb.AppendFormat("{0}return load{1}Table{2}(command);\n", Tabs(3), Class.Name, sp.Name);

			else if (sp.ReturnType == DatatypeHelper.VoidReturnType)			
			{
				sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
				sb.AppendFormat("{0}command.ExecuteNonQuery();\n", Tabs(3));
				sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
			}			
			else if (sp.ReturnType == DatatypeHelper.DataSetReturnType)
				sb.AppendFormat("{0}return ExecDataSet(command);\n", Tabs(3));

			else if (sp.ReturnType == DatatypeHelper.ArrayListReturnType)
				sb.AppendFormat("{0}return ExecFirstColumn(command);\n", Tabs(3));

			else
			{
				sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
				sb.AppendFormat("{0}object result = command.ExecuteScalar();\n", Tabs(3));
				sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
				sb.AppendFormat("{0}return {1}(result);\n", Tabs(3), DatatypeHelper.GetConvertTo(sp.ReturnType));	
			}

			makeMethodFooter(sb, 2, true);
		}

		private void makeLoadTableMethod(StringBuilder sb)
		{			
			sb.AppendFormat("{0}private {1}Table LoadTable(SqlCommand command)\n", Tabs(2), Class.Name);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
			sb.AppendFormat("{0}SqlDataReader reader = command.ExecuteReader();\n", Tabs(3));
			sb.AppendFormat("{0}ArrayList rows = new ArrayList();\n\n", Tabs(3));

			sb.AppendFormat("{0}bool includeSelect = false;\n", Tabs(3));
			sb.AppendFormat("{0}if (reader.FieldCount > {1})\n", Tabs(3), Class.SetIDataFieldList.Count);
			sb.AppendFormat("{0}includeSelect = true;\n\n", Tabs(4));

			sb.AppendFormat("{0}while (reader.Read())\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(3));

			sb.AppendFormat("{0}{1}Row r = new {1}Row();\n", Tabs(4), Class.Name);
			sb.AppendFormat("{0}rows.Add(r);\n\n", Tabs(4));

			int i=0;
			foreach(BaseClassField f in Class.SetIDataFieldList)
			{
				string fieldName = f.Name;
				if (f.ParentClass != Class)
					fieldName = f.ParentClass.Name + "_" + f.Name;

				string dataReaderMethod = DatatypeHelper.GetDataReaderMethodForSqlType(f.Datatype.FullSqlName);
				string convert = "";
				if (dataReaderMethod == "GetValue")
				{
					string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);
					convert = "(" + datatype + ")";
				}

				sb.AppendFormat("{0}if (reader[{1}] != System.DBNull.Value)\n", Tabs(4), i);
				sb.AppendFormat("{0}r.{1} = {2}reader.{3}({4});\n\n", Tabs(5), fieldName, convert, dataReaderMethod, i++);
			}

			sb.AppendFormat("{0}r.Selected = false;\n", Tabs(4));
			sb.AppendFormat("{0}if (includeSelect && reader.GetBoolean({1}))\n", Tabs(4), Class.SetIDataFieldList.Count);			
			sb.AppendFormat("{0}r.Selected = true;\n\n", Tabs(5));			
			sb.AppendFormat("{0}}}\n", Tabs(3));
			sb.AppendFormat("{0}int totalCount = rows.Count;\n", Tabs(3));
			sb.AppendFormat("{0}if (reader.NextResult())\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(3));
			sb.AppendFormat("{0}reader.Read();\n", Tabs(4));
			sb.AppendFormat("{0}totalCount = reader.GetInt32(0);\n", Tabs(4));
			sb.AppendFormat("{0}}}\n", Tabs(3));
			sb.AppendFormat("{0}reader.Close();\n", Tabs(3));
			sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
			sb.AppendFormat("{0}return new {1}Table(rows, totalCount);\n", Tabs(3), Class.Name);
			sb.AppendFormat("{0}}}\n", Tabs(2));			
		}
		
		private void makeLoadCustomTableMethods(StringBuilder sb)
		{
			foreach (SprocObject sp in Class.CustomReturnTypeSprocsList)
			{
				string tableClassName = Class.Name + "Table" + sp.Name;
				string rowClassName = Class.Name + "Row" + sp.Name;

				sb.AppendFormat("\n{0}private {1} load{1}(SqlCommand command)\n", 
					Tabs(2), tableClassName);
				sb.AppendFormat("{0}{{\n", Tabs(2));
				sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
				sb.AppendFormat("{0}SqlDataReader reader = command.ExecuteReader();\n", Tabs(3));
				sb.AppendFormat("{0}ArrayList rows = new ArrayList();\n\n", Tabs(3));

				sb.AppendFormat("{0}while (reader.Read())\n", Tabs(3));
				sb.AppendFormat("{0}{{\n", Tabs(3));

				sb.AppendFormat("{0}{1} r = new {1}();\n", Tabs(4), rowClassName);
				sb.AppendFormat("{0}r.Selected = false;\n", Tabs(4));
				sb.AppendFormat("{0}rows.Add(r);\n\n", Tabs(4));

				int i=0;
				foreach(SprocReturnFieldObject rf in sp.ReturnFieldsList)
				{
					string fieldName = rf.Name;
					string dataReaderMethod = DatatypeHelper.GetDataReaderMethodForCsType(rf.CsDatatype);
					string convert = "";
					if (dataReaderMethod == "GetValue")
						convert = "(" + rf.CsDatatype + ")";

					sb.AppendFormat("{0}if (reader[{1}] != System.DBNull.Value)\n", Tabs(4), i);
					sb.AppendFormat("{0}r.{1} = {2}reader.{3}({4});\n\n", Tabs(5), fieldName, convert, dataReaderMethod, i++);
				}			
		
				sb.AppendFormat("{0}}}\n", Tabs(3));
				sb.AppendFormat("{0}int totalCount = rows.Count;\n", Tabs(3));
				sb.AppendFormat("{0}if (reader.NextResult())\n", Tabs(3));
				sb.AppendFormat("{0}{{\n", Tabs(3));
				sb.AppendFormat("{0}reader.Read();\n", Tabs(4));
				sb.AppendFormat("{0}totalCount = reader.GetInt32(0);\n", Tabs(4));
				sb.AppendFormat("{0}}}\n", Tabs(3));
				sb.AppendFormat("{0}reader.Close();\n", Tabs(3));
				sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
				sb.AppendFormat("{0}return new {1}(rows, totalCount);\n", Tabs(3), tableClassName);
				sb.AppendFormat("{0}}}\n", Tabs(2));	
			}
		}

		private void makeLoadListMethod(StringBuilder sb)
		{
			string listName = Class.Name + "List";
			string rowClassName =  Class.Name + "ListRow";

			sb.AppendFormat("\n{0}private {1} LoadList(SqlCommand command)\n", Tabs(2), listName);
			sb.AppendFormat("{0}{{\n", Tabs(2));
			sb.AppendFormat("{0}Connection.Open();\n", Tabs(3));
			sb.AppendFormat("{0}SqlDataReader reader = command.ExecuteReader();\n", Tabs(3));
			sb.AppendFormat("{0}ArrayList rows = new ArrayList();\n\n", Tabs(3));

			sb.AppendFormat("{0}bool includeSelect = false;\n", Tabs(3));
			sb.AppendFormat("{0}if (reader.FieldCount > {1})\n", Tabs(3), Class.IncludeWithListList.Count);
			sb.AppendFormat("{0}includeSelect = true;\n\n", Tabs(4));

			sb.AppendFormat("{0}while (reader.Read())\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(3));

			sb.AppendFormat("{0}{1} r = new {1}();\n", Tabs(4), rowClassName);
			sb.AppendFormat("{0}r.Selected = false;\n", Tabs(4));
			sb.AppendFormat("{0}rows.Add(r);\n\n", Tabs(4));

			int i=0;
			foreach(BaseClassField f in Class.IncludeWithListList)
			{
				string dataReaderMethod = DatatypeHelper.GetDataReaderMethodForSqlType(f.Datatype.FullSqlName);
				string convert = "";
				if (dataReaderMethod == "GetValue")
				{
					string datatype = Helper.GetDatatype(f.Datatype.FullSqlName, false, false);
					convert = "(" + datatype + ")";
				}

				sb.AppendFormat("{0}if (reader[{1}] != System.DBNull.Value)\n", Tabs(4), i);
				sb.AppendFormat("{0}r.{1} = {2}reader.{3}({4});\n\n", Tabs(5), f.Name, convert, dataReaderMethod, i++);
			}

			
			sb.AppendFormat("{0}r.Selected = false;\n", Tabs(4));
			sb.AppendFormat("{0}if (includeSelect && reader.GetBoolean({1}))\n", Tabs(4), Class.IncludeWithListList.Count);			
			sb.AppendFormat("{0}r.Selected = true;\n\n", Tabs(5));
	
			sb.AppendFormat("{0}}}\n", Tabs(3));
			sb.AppendFormat("{0}int totalCount = rows.Count;\n", Tabs(3));
			sb.AppendFormat("{0}if (reader.NextResult())\n", Tabs(3));
			sb.AppendFormat("{0}{{\n", Tabs(3));
			sb.AppendFormat("{0}reader.Read();\n", Tabs(4));
			sb.AppendFormat("{0}totalCount = reader.GetInt32(0);\n", Tabs(4));
			sb.AppendFormat("{0}}}\n", Tabs(3));
			sb.AppendFormat("{0}reader.Close();\n", Tabs(3));
			sb.AppendFormat("{0}Connection.Close();\n", Tabs(3));
			sb.AppendFormat("{0}return new {1}(rows, totalCount);\n", Tabs(3), listName);
			sb.AppendFormat("{0}}}\n", Tabs(2));	
		}




		private void makeFooter(StringBuilder sb)
		{
			sb.AppendFormat("{0}}}\n}}", Tabs(1));
		}



		
		
		private ClassFieldObject pkeyField;	
		private string className;
	}
}
