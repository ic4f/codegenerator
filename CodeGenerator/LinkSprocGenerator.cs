using System;
using System.Collections;
using System.Text;
using CodeGenerator.Application;

namespace CodeGenerator
{
    public class LinkSprocGenerator : SprocGenerator
    {
        public LinkSprocGenerator(ClassObject c, DatabaseTool dbTool) : base(c, dbTool)
        {
            //we assume only 2 foreign keys in a link table
            ArrayList temp = new ArrayList();
            foreach (ClassFieldObject f in c.FieldsList)
                if (f.IsForeignKey)
                    temp.Add(f);

            fkey1 = (ClassFieldObject)temp[0];
            fkey2 = (ClassFieldObject)temp[1];
        }

        public void CreateProcedures()
        {
            makeCreateProc();
            makeCreateAllBy1Proc();
            makeCreateAllBy2Proc();
            makeDeleteProc();
            makeDeleteAllBy1Proc();
            makeDeleteAllBy2Proc();
        }

        private void makeCreateProc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nVALUES (@{0}, @{1})", fkey1.Name, fkey2.Name);

            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("\nIF (SELECT COUNT(*) FROM {0} WHERE {1} = @{1} AND {2} = @{2}) > 0\n",
                Class.TableSqlName, fkey1.Name, fkey2.Name);
            sb2.Append("\tRETURN -16\n");

            CreateProcedure("Create",
                MakeFieldParam(fkey1) + ",\n" + MakeFieldParam(fkey2),
                sb2.ToString() + MakeCreateRecordStatements(sb.ToString()));
        }

        private void makeCreateAllBy1Proc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nSELECT @{0}, {1} FROM {2} ",
                fkey1.Name, fkey2.ReferencedField.SqlName, fkey2.ReferencedClass.TableSqlName);

            sb.AppendFormat("WHERE NOT EXISTS (SELECT * FROM {0} WHERE {1} = @{1} AND {2} = {3})\n",
                Class.TableSqlName, fkey1.Name, fkey2.Name, fkey2.ReferencedField.SqlName);

            CreateProcedure("CreateAllBy" + fkey1.ReferencedClass.Name,
                MakeFieldParam(fkey1),
                MakeCreateRecordStatements(sb.ToString()));
        }

        private void makeCreateAllBy2Proc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nSELECT {0}, @{1} FROM {2} ",
                fkey1.ReferencedField.SqlName, fkey2.Name, fkey1.ReferencedClass.TableSqlName);

            sb.AppendFormat("WHERE NOT EXISTS (SELECT * FROM {0} WHERE {2} = {3} AND {1} = @{1})\n",
                Class.TableSqlName, fkey2.Name, fkey1.Name, fkey1.ReferencedField.SqlName);

            CreateProcedure("CreateAllBy" + fkey2.ReferencedClass.Name,
                MakeFieldParam(fkey2),
                MakeCreateRecordStatements(sb.ToString()));
        }

        //		private void makeCreateBy1With2CriteriaProc()
        //		{
        //			StringBuilder sb = new StringBuilder();
        //			sb.AppendFormat("\nEXEC ('SELECT ' + @{0} + ', {1} FROM {2} ' + @Query)", 
        //				fkey1.Name, fkey2.ReferencedField.SqlName, fkey2.ReferencedClass.TableSqlName);	
        //			CreateProcedure(
        //				"CreateBy" + fkey1.ReferencedClass.Name + fkey2.ReferencedClass.Name, 
        //				MakeFieldParam(fkey1) + ",\n  @Query varchar(4000)", 
        //				MakeCreateRecordStatements(sb.ToString()));
        //		}
        //
        //		private void makeCreateBy2With1CriteriaProc()
        //		{
        //			StringBuilder sb = new StringBuilder();
        //			sb.AppendFormat("\nEXEC ('SELECT {0}, ' + @{1} + ' FROM {2} ' + @Query)", 
        //				fkey1.ReferencedField.SqlName, fkey2.Name, fkey1.ReferencedClass.TableSqlName);	
        //			CreateProcedure(
        //				"CreateBy" + fkey2.ReferencedClass.Name + fkey1.ReferencedClass.Name, 
        //				MakeFieldParam(fkey2) + ",\n  @Query varchar(4000)", 
        //				MakeCreateRecordStatements(sb.ToString()));
        //		}

        private void makeDeleteProc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} = @{1} AND {2} = @{3}", fkey1.SqlName, fkey1.Name, fkey2.SqlName, fkey2.Name);
            CreateProcedure("Delete",
                MakeFieldParam(fkey1) + ",\n" + MakeFieldParam(fkey2),
                MakeDeleteRecordStatements(sb.ToString()));
        }

        private void makeDeleteAllBy1Proc()
        {
            CreateProcedure("DeleteAllBy" + fkey1.ReferencedClass.Name,
                MakeFieldParam(fkey1),
                MakeDeleteRecordStatements(fkey1.SqlName + " = @" + fkey1.Name));
        }

        private void makeDeleteAllBy2Proc()
        {
            CreateProcedure("DeleteAllBy" + fkey2.ReferencedClass.Name,
                MakeFieldParam(fkey2),
                MakeDeleteRecordStatements(fkey2.SqlName + " = @" + fkey2.Name));
        }

        private string MakeCreateRecordStatements(string sqlToInsert)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nINSERT INTO {0}", Class.TableSqlName);
            sb.AppendFormat("\n({0}, {1})", fkey1.SqlName, fkey2.SqlName);
            sb.Append(sqlToInsert);
            sb.Append("\n\nRETURN 0");
            return sb.ToString();
        }

        private string MakeDeleteRecordStatements(string sqlToDelete)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\nDELETE FROM {0}\nWHERE ", Class.TableSqlName);
            sb.Append(sqlToDelete);
            sb.Append("\n\nRETURN @@ROWCOUNT");
            return sb.ToString();
        }

        private ClassFieldObject fkey1;
        private ClassFieldObject fkey2;
    }
}
