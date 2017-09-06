using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator
{
    /// <summary>
    /// MUST USE Created or Modified as field names in tables (if using such fields)
    /// it is also assumed that each record has an identity field which is the primary key!!! - 
    /// later change this hack.
    /// </summary>
    public class RecordSprocGenerator : SprocGenerator
    {
        public RecordSprocGenerator(ClassObject c, DatabaseTool dbTool) : base(c, dbTool)
        {
            pkeyField = c.PrimaryKeyField1;
        }

        public void CreateProcedures()
        {
            makeBasicProcedures();
            makeGetByFieldProcedures();
            makeGetByLinkProcedures();
            makeGetLinksProcedures();
        }

        private void makeBasicProcedures()
        {
            if (Class.Type != ClassType.Readonly && !Class.IsExternal)
            {
                CreateProcedure("Create", makeCreateRecordParams(), makeCreateRecordStatements());
                CreateProcedure("Delete", makeDeleteRecordParams(pkeyField), makeDeleteRecordStatements());

                if (Class.Type != ClassType.Final && !Class.IsExternal)
                    CreateProcedure("Update", makeUpdateRecordParams(), makeUpdateRecordStatements());
            }
            CreateProcedure("GetRecord", MakeFieldParam(pkeyField), makeGetRecordStatements());
            CreateProcedure("GetRecords", makeGetRecordsParams(), makeGetRecordsStatements());

            if (Class.IncludeWithListList.Count > 0)
                CreateProcedure("GetList", "", makeGetListStatements());

            CreateProcedure("GetRecordsP", makeGetRecordsPParams(), makeGetRecordsPStatements());

            if (Class.HasTextField)
                CreateProcedure("GetRecordsPS", makeGetRecordsPSParams(), makeGetRecordsPSStatements());
        }

        private void makeGetByFieldProcedures()
        {
            foreach (ClassFieldObject fkey in Class.ForeignKeyFieldList)
            {
                CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "Field",
                    makeGetRecordsByFieldParams(fkey), makeGetRecordsByFieldStatements(fkey));
                CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "FieldP",
                    makeGetRecordsByFieldPParams(fkey), makeGetRecordsByFieldPStatements(fkey));

                if (Class.HasTextField)
                    CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "FieldPS",
                        makeGetRecordsByFieldPSParams(fkey), makeGetRecordsByFieldPSStatements(fkey));
            }
        }

        private void makeGetByLinkProcedures()
        {
            foreach (ClassObject refClass in Class.ReferringLinkClassesHash.Values)
                foreach (ClassFieldObject fkey in refClass.ForeignKeyFieldList) //there should only 2 fkeys in a link class					
                    if (fkey.ReferencedClass != Class)
                    {
                        CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "Link",
                            makeGetRecordsByFieldParams(fkey),
                            makeGetRecordsByLinkStatements(fkey));
                        CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "LinkP",
                            makeGetRecordsByFieldPParams(fkey),
                            makeGetRecordsByLinkPStatements(fkey));

                        if (Class.HasTextField)
                            CreateProcedure("GetRecordsBy" + fkey.ReferencedClass.Name + "LinkPS",
                                makeGetRecordsByFieldPSParams(fkey),
                                makeGetRecordsByLinkPSStatements(fkey));
                    }
        }

        private void makeGetLinksProcedures()
        {
            ClassFieldObject link1 = null;
            ClassFieldObject link2 = null;

            foreach (ClassObject refClass in Class.ReferringLinkClassesHash.Values)
                foreach (ClassFieldObject fkey in refClass.ForeignKeyFieldList)
                {
                    if (fkey.ReferencedClass != Class)
                    {
                        string sprocName = "Get" + fkey.ReferencedClass.Name + "Links";
                        CreateProcedure(sprocName, makeGetLinksParams(fkey), makeGetLinksStatements(fkey));
                        CreateProcedure(sprocName + "P", makeGetLinksPParams(fkey), makeGetLinksPStatements(fkey));

                        if (Class.HasTextField)
                            CreateProcedure("Get" + fkey.ReferencedClass.Name + "LinksPS",
                                makeGetLinksPSParams(fkey),
                                makeGetLinksPSStatements(fkey));

                        foreach (ClassObject refClass2 in Class.ReferringLinkClassesHash.Values)
                            if (refClass2 != refClass)
                            {
                                foreach (ClassFieldObject fkey2 in refClass2.ForeignKeyFieldList)
                                {
                                    if (fkey2.ReferencedClass != Class)
                                        link1 = fkey2;
                                    else
                                        link2 = fkey2;
                                }
                                sprocName = "Get" + fkey.ReferencedClass.Name + "LinksBy" + link1.ReferencedClass.Name;
                                CreateProcedure(sprocName, makeGetLinksByFieldParams(fkey, link1), makeGetLinksByFieldStatements(fkey, link1, link2));
                                CreateProcedure(sprocName + "P", makeGetLinksByFieldPParams(fkey, link1), makeGetLinksByFieldPStatements(fkey, link1, link2));
                            }
                    }
                }
        }

        #region parameter makers

        private string makeDeleteRecordParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", MakeFieldParam(fkey));
            sb.Append(",\n  @DeleteDependents bit");
            return sb.ToString();
        }

        private string makeGetLinksParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", MakeFieldParam(fkey));
            sb.Append(",\n  @SortExp varchar(100)");
            return sb.ToString();
        }

        private string makeGetLinksPParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(makeGetLinksParams(fkey));
            sb.Append(",\n  @PageSize int,");
            sb.Append("\n  @PageNum int");
            return sb.ToString();
        }

        private string makeGetLinksPSParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetLinksPParams(fkey));
            sb.Append(",\n  @SearchField varchar(25),");
            sb.Append("\n  @SearchKeyword varchar(25)");
            return sb.ToString();
        }

        private string makeGetLinksByFieldParams(ClassFieldObject fkey, ClassFieldObject fkey2)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},\n", MakeFieldParam(fkey));
            sb.AppendFormat("{0},\n", MakeFieldParam(fkey2));
            sb.Append("  @SortExp varchar(100)");
            return sb.ToString();
        }

        private string makeGetLinksByFieldPParams(ClassFieldObject fkey, ClassFieldObject fkey2)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetLinksByFieldParams(fkey, fkey2));
            sb.Append(",\n  @PageSize int,");
            sb.Append("\n  @PageNum int");
            return sb.ToString();
        }

        private string makeCreateRecordParams()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ClassFieldObject f in Class.FieldsList)
                if (!f.IsIdentity && !f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType)
                    sb.AppendFormat("{0},\n", MakeFieldParam(f));
            removeLastComma(sb);

            return sb.ToString();
        }

        private string makeUpdateRecordParams()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ClassFieldObject f in Class.FieldsList)
                if (!f.IsCreatedType && !f.IsModifiedType && !f.IsTimestampType)
                    sb.AppendFormat("{0},\n", MakeFieldParam(f));
            removeLastComma(sb);
            return sb.ToString();
        }

        private string makeGetRecordsParams()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  @SortExp varchar(100)");
            return sb.ToString();
        }

        private string makeGetRecordsPParams()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetRecordsParams());
            sb.Append(",\n  @PageSize int,");
            sb.Append("\n  @PageNum int");
            return sb.ToString();
        }

        private string makeGetRecordsPSParams()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetRecordsPParams());
            sb.Append(",\n  @SearchField varchar(25),");
            sb.Append("\n  @SearchKeyword varchar(25)");
            return sb.ToString();
        }

        private string makeGetRecordsByFieldParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},\n", MakeFieldParam(fkey));
            sb.AppendFormat("{0}", makeGetRecordsParams());
            return sb.ToString();
        }

        private string makeGetRecordsByFieldPParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetRecordsByFieldParams(fkey));
            sb.Append(",\n  @PageSize int,");
            sb.Append("\n  @PageNum int");
            return sb.ToString();
        }

        private string makeGetRecordsByFieldPSParams(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", makeGetRecordsByFieldPParams(fkey));
            sb.Append(",\n  @SearchField varchar(25),");
            sb.Append("\n  @SearchKeyword varchar(25)");
            return sb.ToString();
        }
        #endregion

        #region CreateRecord sproc statements
        private string makeCreateRecordStatements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(makeCheckConstraints(true));
            sb.AppendFormat("\nINSERT INTO {0}", Class.TableSqlName);
            sb.Append(listFields(true));
            sb.AppendFormat("\nVALUES{0}\n", listFields(false));
            sb.Append("\nSELECT @@IDENTITY");
            return sb.ToString();
        }

        private string listFields(bool areFields)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n(");
            foreach (ClassFieldObject f in Class.FieldsList)
                if (!f.IsIdentity)
                {
                    if (areFields)
                    {
                        if (!f.IsTimestampType)
                            sb.AppendFormat("\n\t{0}, ", f.SqlName);
                    }
                    else //are values/variables
                    {
                        if (f.IsCreatedType || f.IsModifiedType)
                            sb.Append("\n\tgetDate(), ");
                        else if (!f.IsTimestampType)
                            sb.AppendFormat("\n\t@{0}, ", f.Name);
                    }
                }
            removeLastComma(sb);
            sb.Append("\n)");
            return sb.ToString();
        }

        #endregion

        #region DeleteRecord sproc statements
        private string makeDeleteRecordStatements()
        {
            StringBuilder sb = new StringBuilder();

            //check if there are non-link tables referring to this record IF !DeleteDependents. If there are, we can't delete
            ClassFieldObject fkey;
            if (Class.ReferringNonLinkClassesHash.Count > 0)
            {
                sb.AppendFormat("\nIF (@DeleteDependents = 0)\n\tBEGIN\n");

                int i = MinimumErrorCode;
                foreach (ClassObject c in Class.ReferringNonLinkClassesHash.Values)
                {
                    fkey = c.GetForeignKeyFieldForClass(Class);
                    sb.AppendFormat("\n\tIF ((SELECT COUNT(*) FROM {0} WHERE {1} = @{2}) > 0)\n\t\tRETURN {3}\n",
                        c.TableSqlName, fkey.SqlName, pkeyField.Name, i--);
                }
                sb.AppendFormat("\n\tEND \nELSE \n\tBEGIN\n");

                i = MinimumErrorCode;
                foreach (ClassObject c in Class.ReferringNonLinkClassesHash.Values)
                {
                    fkey = c.GetForeignKeyFieldForClass(Class);
                    sb.AppendFormat("\n\tDELETE FROM {0} WHERE {1} = @{2}\n", c.TableSqlName, fkey.SqlName, pkeyField.Name);
                }

                sb.AppendFormat("\n\tEND \n");
            }
            else
            {
                int i = MinimumErrorCode;
                foreach (ClassObject c in Class.ReferringNonLinkClassesHash.Values)
                {
                    fkey = c.GetForeignKeyFieldForClass(Class);
                    sb.AppendFormat("\nIF ((SELECT COUNT(*) FROM {0} WHERE {1} = @{2}) > 0)\n\tRETURN {3}\n",
                        c.TableSqlName, fkey.SqlName, pkeyField.Name, i--);
                }
            }

            //delete all link table referrals
            foreach (ClassObject c in Class.ReferringLinkClassesHash.Values)
            {
                fkey = c.GetForeignKeyFieldForClass(Class);
                sb.AppendFormat("\nDELETE FROM {0} WHERE {1} = @{2}", c.TableSqlName, fkey.SqlName, pkeyField.Name);
            }

            sb.AppendFormat("\nDELETE FROM {0}", Class.TableSqlName);
            sb.AppendFormat("\nWHERE {0} = @{1}", pkeyField.SqlName, pkeyField.Name);
            sb.Append("\n\nRETURN @@ROWCOUNT");
            return sb.ToString();
        }
        #endregion

        #region UpdateRecord sproc statements
        private string makeUpdateRecordStatements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(makeCheckConstraints(false));
            sb.AppendFormat("\nUPDATE {0}", Class.TableSqlName);
            sb.Append("\nSET\n");

            StringBuilder sb1 = new StringBuilder();
            foreach (ClassFieldObject f in Class.FieldsList)
                if (f.IsModifiedType)
                    sb1.AppendFormat("\t{0} = getDate(),\n", f.SqlName);
                else if (!f.IsPrimaryKey && !f.IsIdentity && !f.IsCreatedType && !f.IsTimestampType)
                    sb1.AppendFormat("\t{0} = @{1},\n", f.SqlName, f.Name);
            removeLastComma(sb1);
            sb.Append(sb1.ToString());

            sb.AppendFormat("\nWHERE {0} = @{1}", pkeyField.SqlName, pkeyField.Name);
            sb.Append("\n\nRETURN 0");
            return sb.ToString();
        }
        #endregion

        #region GetRecord sproc statements
        private string makeGetRecordStatements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nSELECT\n");

            foreach (BaseClassField f in Class.InstanceIDataFieldList)
                sb.AppendFormat("\t{0},\n", f.Sql);
            removeLastComma(sb);

            sb.AppendFormat("\nFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "");
            sb.AppendFormat("\nWHERE {0}.{1} = @{2}", Class.TableSqlName, pkeyField.SqlName, pkeyField.Name);
            return sb.ToString();
        }
        #endregion

        #region GetRecords sproc statements
        private string makeGetRecordsStatements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nEXEC\n('\n\tSELECT\n");

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            return sb.ToString();
        }
        #endregion

        #region GetList sproc statements
        private string makeGetListStatements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nSELECT\n");

            foreach (BaseClassField f in Class.IncludeWithListList)
                sb.AppendFormat("\t{0},\n", f.Sql);

            removeLastComma(sb);

            sb.AppendFormat("\nFROM {0}", Class.TableSqlName);
            sb.AppendFormat("\nORDER BY {0}\n", Class.DefaultSortField.SortExpression);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsP sproc statements
        private string makeGetRecordsPStatements()
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");

            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsPS sproc statements
        private string makeGetRecordsPSStatements()
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tWHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''");
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");

            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByField sproc statements
        private string makeGetRecordsByFieldStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nEXEC('\n\tSELECT\n");

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tWHERE {0}.{1} = ' + @{2} + ' ", Class.TableSqlName, fkey.SqlName, fkey.Name);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByFieldP sproc statements
        private string makeGetRecordsByFieldPStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tWHERE {0}.{1} = ' + @{2} + '", Class.TableSqlName, fkey.SqlName, fkey.Name);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");

            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByFieldPS sproc statements
        private string makeGetRecordsByFieldPSStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tWHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''");
            sb.AppendFormat("\n\tAND {0}.{1} = ' + @{2} + '", Class.TableSqlName, fkey.SqlName, fkey.Name);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");

            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByLink sproc statements
        private string makeGetRecordsByLinkStatements(ClassFieldObject linkTableFkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nEXEC\n('\n\tSELECT\n");
            makeGetByLinkFragment(sb, linkTableFkey);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByLinkP sproc statements
        private string makeGetRecordsByLinkPStatements(ClassFieldObject linkTableFkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());
            makeGetByLinkFragment(sb, linkTableFkey);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion

        #region GetRecordsByLinkPS sproc statements
        private string makeGetRecordsByLinkPSStatements(ClassFieldObject linkTableFkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, false, Class.SetIDataFieldList, makeExecSelect());
            makeGetByLinkFragment(sb, linkTableFkey);
            sb.AppendFormat("\n\tWHERE ' + @searchField + ' LIKE ''' + @searchKeyword + '%''");
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            AppendPagingFooter(sb, false, Class.SetIDataFieldList);
            return sb.ToString();
        }
        #endregion


        #region GetLinksStatements
        private string makeGetLinksStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nEXEC\n('\n\tSELECT\n");
            makeGetLinksExecFragment(sb, fkey);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            return sb.ToString();
        }
        #endregion

        #region GetLinksPStatements
        private string makeGetLinksPStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, true, Class.IncludeWithListList, makeExecSelect());
            makeGetLinksExecFragment(sb, fkey);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            AppendPagingFooter(sb, true, Class.IncludeWithListList);
            return sb.ToString();
        }
        #endregion

        #region GetLinksPSStatements
        private string makeGetLinksPSStatements(ClassFieldObject fkey)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, true, Class.IncludeWithListList, makeExecSelect());
            makeGetLinksExecFragment(sb, fkey);
            sb.AppendFormat("\n\tWHERE {0}.' + @searchField + ' LIKE ''' + @searchKeyword + '%''", Class.TableSqlName);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            AppendPagingFooter(sb, true, Class.IncludeWithListList);
            return sb.ToString();
        }
        #endregion

        #region GetLinksByFieldStatements
        private string makeGetLinksByFieldStatements(ClassFieldObject fkey, ClassFieldObject fkey2, ClassFieldObject fkey3)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nEXEC\n('\n\tSELECT\n");
            makeGetLinksExecFragment(sb, fkey);
            sb.AppendFormat("\n\tINNER JOIN {0} ON {0}.{1} = {2}.{3} AND {0}.{4} = ' + @{5} + ' ",
                fkey2.ParentClass.TableSqlName, fkey3.SqlName, Class.TableSqlName, Class.PrimaryKeyField1.SqlName, fkey2.SqlName, fkey2.SqlName);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            return sb.ToString();
        }
        #endregion

        #region GetLinksByFieldPStatements
        private string makeGetLinksByFieldPStatements(ClassFieldObject fkey, ClassFieldObject fkey2, ClassFieldObject fkey3)
        {
            StringBuilder sb = new StringBuilder();
            AppendPagingHeader(sb, true, Class.IncludeWithListList, makeExecSelect());
            makeGetLinksExecFragment(sb, fkey);
            sb.AppendFormat("\n\tINNER JOIN {0} ON {0}.{1} = {2}.{3} AND {0}.{4} = ' + @{5} + ' ",
                fkey2.ParentClass.TableSqlName, fkey3.SqlName, Class.TableSqlName, Class.PrimaryKeyField1.SqlName, fkey2.SqlName, fkey2.SqlName);
            sb.AppendFormat("\n\tORDER BY ' + @SortExp\n)");
            AppendPagingFooter(sb, true, Class.IncludeWithListList);
            return sb.ToString();
        }
        #endregion


        #region other private 

        private void joinReferredTables(StringBuilder sb, string tabs)
        {
            //retrieve fields from referenced tables (NOT link tables because those are many-to-many!)
            if (Class.ForeignKeyFieldList.Count > 0)
                foreach (ClassFieldObject fkey in Class.ForeignKeyFieldList)
                    if (fkey.ReferencedClass.Type != ClassType.Link)
                        sb.AppendFormat("\n{0}LEFT OUTER JOIN {1} ON {2}.{3} = {1}.{4} ",
                            tabs, fkey.ReferencedClass.TableSqlName, Class.TableSqlName,
                            fkey.SqlName, fkey.ReferencedField.SqlName);
        }

        private void makeGetByLinkFragment(StringBuilder sb, ClassFieldObject fkey)
        {
            ClassObject linkClass = fkey.ParentClass;

            ClassFieldObject fkey2 = null;
            foreach (ClassFieldObject f in linkClass.ForeignKeyFieldList)
                if (f != fkey)
                    fkey2 = f;

            string mainTablePrefix = Class.TableSqlName + ".";
            string linkTablePrefix = linkClass.TableSqlName + ".";

            foreach (BaseClassField f in Class.SetIDataFieldList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }
            removeLastComma(sb);

            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            joinReferredTables(sb, "\t");
            sb.AppendFormat("\n\tJOIN {0} ON ", linkClass.TableSqlName);
            sb.AppendFormat("{0}{1} = {2}{3} AND ", linkTablePrefix, fkey2.SqlName, mainTablePrefix, pkeyField.SqlName);
            sb.AppendFormat("{0}{1} = ' + @{2} + ' ", linkTablePrefix, fkey.Name, fkey.Name);
        }

        private void makeGetLinksFragment(StringBuilder sb, ClassFieldObject fkey)
        {
            ClassObject linkClass = fkey.ParentClass;

            ClassFieldObject fkey2 = null;
            foreach (ClassFieldObject f in linkClass.ForeignKeyFieldList)
                if (f != fkey)
                    fkey2 = f;

            string mainTablePrefix = Class.TableSqlName + ".";
            string linkTablePrefix = linkClass.TableSqlName + ".";

            foreach (BaseClassField f in Class.IncludeWithListList)
                sb.AppendFormat("\t{0},\n", f.Sql);

            sb.AppendFormat("\tCAST (ISNULL({0}{1}, 0) as bit) AS Selected", linkTablePrefix, fkey.SqlName);
            sb.AppendFormat("\nFROM {0}", Class.TableSqlName);
            sb.AppendFormat("\nLEFT OUTER JOIN {0} ON ", linkClass.TableSqlName);
            sb.AppendFormat("{0}{1} = {2}{3} AND ", linkTablePrefix, fkey2.SqlName, mainTablePrefix, pkeyField.SqlName);
            sb.AppendFormat("{0}{1} = @{2}", linkTablePrefix, fkey.SqlName, fkey.Name);
        }

        private void makeGetLinksExecFragment(StringBuilder sb, ClassFieldObject fkey)
        {
            ClassObject linkClass = fkey.ParentClass;

            ClassFieldObject fkey2 = null;
            foreach (ClassFieldObject f in linkClass.ForeignKeyFieldList)
                if (f != fkey)
                    fkey2 = f;

            string mainTablePrefix = Class.TableSqlName + ".";
            string linkTablePrefix = linkClass.TableSqlName + ".";

            foreach (BaseClassField f in Class.IncludeWithListList)
            {
                string sql = f.Sql.Replace("'", "''");
                sb.AppendFormat("\t\t{0},\n", sql);
            }

            sb.AppendFormat("\t\tCAST (ISNULL({0}{1}, 0) as bit) AS Selected", linkTablePrefix, fkey.SqlName);
            sb.AppendFormat("\n\tFROM {0}", Class.TableSqlName);
            sb.AppendFormat("\n\tLEFT OUTER JOIN {0} ON ", linkClass.TableSqlName);
            sb.AppendFormat("{0}{1} = {2}{3} AND ", linkTablePrefix, fkey2.SqlName, mainTablePrefix, pkeyField.SqlName);
            sb.AppendFormat("{0}{1} = ' + @{2} + ' ", linkTablePrefix, fkey.SqlName, fkey.Name);
        }

        private void removeLastComma(StringBuilder sb)
        {
            string temp = sb.ToString();
            int comma = temp.LastIndexOf(",");
            if (comma > -1)
            {
                temp = temp.Substring(0, comma);
                sb.Remove(0, sb.Length);
                sb.Append(temp);
            }
        }

        private string makeCheckConstraints(bool isCreate)
        {
            StringBuilder sb = new StringBuilder();
            int i = MinimumErrorCode;
            bool addElse = false;
            foreach (ClassFieldObject c in Class.UniqueFieldList)
            {
                i -= 1;
                if (addElse)
                    sb.Append("\nELSE");
                sb.AppendFormat("\nIF EXISTS (SELECT * FROM {0}", Class.TableSqlName);
                sb.AppendFormat(" WHERE {0} = @{1}", c.SqlName, c.Name);

                if (!isCreate)
                    sb.AppendFormat(" AND {0} !=  @{1}", pkeyField.SqlName, pkeyField.Name);

                sb.Append(")\n\tBEGIN\n");
                sb.AppendFormat("\t\tSELECT {0}\n", i);
                sb.Append("\t\tRETURN\n");
                sb.Append("\tEND\n");

                addElse = true;
            }
            return sb.ToString();
        }

        private void AppendPagingHeader(StringBuilder sb, bool includeAll, ArrayList fields, string select)
        {
            string tmpTable = makeTempTableName();
            sb.AppendFormat("\nCREATE TABLE {0}", tmpTable);
            sb.Append("\n(\n");
            sb.Append("\tTempId int IDENTITY PRIMARY KEY,\n");

            foreach (BaseClassField f in fields)
            {
                if (f.Datatype.FullSqlName == "timestamp")
                    sb.AppendFormat("\t{0}_{1} varbinary(8),\n", f.ParentClass.TableName, f.Name);
                else
                    sb.AppendFormat("\t{0}_{1} {2},\n", f.ParentClass.TableName, f.Name, f.Datatype.FullSqlName);
            }

            removeLastComma(sb);

            if (includeAll)
                sb.Append(",\n\tselected bit");

            sb.Append("\n)\n");
            sb.AppendFormat("\nINSERT INTO {0}", tmpTable);
            sb.Append("\n(\n");

            foreach (BaseClassField f in fields)
                sb.AppendFormat("\t{0}_{1},\n", f.ParentClass.TableName, f.Name);

            removeLastComma(sb);

            if (includeAll)
                sb.Append(",\n\tselected");

            sb.Append("\n)");
            sb.Append(select);
        }

        private void AppendPagingFooter(StringBuilder sb, bool includeAll, ArrayList fields)
        {
            sb.Append("\n\nDECLARE @rows int \n");
            sb.Append("SET @rows = @@ROWCOUNT \n\n");
            sb.Append("DECLARE @first int \n");
            sb.Append("DECLARE @last int \n");
            sb.Append("SET @first = (@pageNum-1) * @pageSize + 1 \n");
            sb.Append("SET @last = @first + @pageSize - 1 \n");
            sb.Append("\nSELECT\n");

            foreach (BaseClassField f in fields)
                sb.AppendFormat("\t{0}_{1},\n", f.ParentClass.TableName, f.Name);

            removeLastComma(sb);

            if (includeAll)
                sb.Append(",\n\tselected");

            sb.AppendFormat("\nFROM {0}", makeTempTableName());
            sb.Append("\nWHERE TempId >= @first AND TempId <= @last \n");
            sb.Append("\nSELECT @rows");
        }

        private string makeTempTableName() { return "#Temp" + Class.Name; }

        private string makeExecSelect() { return "\nEXEC\n('\n\tSELECT\n"; }

        private string makeSelect() { return "\nSELECT\n"; }

        private ClassFieldObject pkeyField;

        #endregion
    }
}
