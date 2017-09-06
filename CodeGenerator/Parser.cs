using System;
using System.Collections;
using System.Xml;
using t = CodeGenerator.ParseTree;

namespace CodeGenerator
{
    public class Parser
    {
        public Parser(string schemaFile) { this.schemaFile = schemaFile; }

        public t.ApplicationNode ParseSchema()
        {
            t.ApplicationNode appNode = new t.ApplicationNode();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(schemaFile);
            loadNamespaces(appNode, xmlDoc.GetElementsByTagName("namespace"));
            return appNode;
        }

        private void loadNamespaces(t.ApplicationNode appNode, XmlNodeList xmlNamespaces)
        {
            foreach (XmlElement xmlNamespace in xmlNamespaces)
            {
                string name = capitalizeFirstLetter(getText(xmlNamespace, "name"));
                if (name == null)
                    throw new MissingFieldException("namespace name field not found");
                t.NamespaceNode nsNode = new t.NamespaceNode(name);
                appNode.AddNamespace(nsNode);
                loadClasses(nsNode, xmlNamespace.GetElementsByTagName("class"));
            }
        }

        private void loadClasses(t.NamespaceNode nsNode, XmlNodeList xmlClasses)
        {
            foreach (XmlElement xmlClass in xmlClasses)
            {
                string name = capitalizeFirstLetter(getText(xmlClass, "name"));
                if (name == null)
                    throw new MissingFieldException("class name field not found");

                string type = getText(xmlClass, "type");
                if (type == null)
                    throw new MissingFieldException("class type field not found");

                t.ClassNode clNode = new t.ClassNode(name, type);
                nsNode.AddClass(clNode);
                loadTable(clNode, (XmlElement)xmlClass.GetElementsByTagName("table")[0]);
                loadAdditionalSprocs(clNode, xmlClass.GetElementsByTagName("additionalsproc"));
            }
        }

        private void loadTable(t.ClassNode csNode, XmlElement xmlTable)
        {
            string name = getText(xmlTable, "name");
            if (name == null)
                throw new MissingFieldException("table name field not found");

            bool isExternal = getBoolValue(xmlTable, "external");
            t.TableNode tblNode = new t.TableNode(name, isExternal);
            csNode.Table = tblNode;
            loadFields(tblNode, xmlTable.GetElementsByTagName("field"));
            loadAdditionalFields(tblNode, xmlTable.GetElementsByTagName("additionalfield"));
        }

        private void loadFields(t.TableNode tblNode, XmlNodeList xmlFields)
        {
            foreach (XmlElement xmlField in xmlFields)
            {
                string name = capitalizeFirstLetter(getText(xmlField, "name"));
                if (name == null)
                    throw new MissingFieldException("field name field not found");

                string sqldatatype = getText(xmlField, "sqldatatype");
                if (sqldatatype == null)
                    throw new MissingFieldException("field sqldatatype field not found");

                bool identity = getBoolValue(xmlField, "identity");
                bool primarykey = getBoolValue(xmlField, "primarykey");
                bool unique = getBoolValue(xmlField, "unique");
                bool encrypted = getBoolValue(xmlField, "encrypted");
                string display = getText(xmlField, "display");
                bool excludefromtable = getBoolValue(xmlField, "excludefromtable");
                bool includewithparenttable = getBoolValue(xmlField, "includewithparenttable");
                bool includeInList = getBoolValue(xmlField, "includeinlist");
                bool isDefaultSort = getBoolValue(xmlField, "defaultsort");
                string readonlytype = getText(xmlField, "readonlytype");

                string refTable = null;
                string refField = null;
                XmlElement xmlFkey = (XmlElement)xmlField.GetElementsByTagName("foreignkey")[0];
                if (xmlFkey != null)
                {
                    refTable = getText(xmlFkey, "reftable");
                    if (refTable == null)
                        throw new MissingFieldException("foreignkey reftable not found");

                    refField = getText(xmlFkey, "reffield");
                    if (refField == null)
                        throw new MissingFieldException("foreignkey reffield not found");
                }

                t.FieldNode fldNode = new t.FieldNode(
                    name, sqldatatype, identity, primarykey, refTable, refField, unique, encrypted, display,
                    excludefromtable, includewithparenttable, includeInList, isDefaultSort, readonlytype);
                tblNode.AddField(fldNode);
            }
        }

        private void loadAdditionalFields(t.TableNode tblNode, XmlNodeList xmlAddFields)
        {
            foreach (XmlElement xmlAddField in xmlAddFields)
            {
                string name = capitalizeFirstLetter(getText(xmlAddField, "name"));
                if (name == null)
                    throw new MissingFieldException("addfield name field not found");

                string sqldatatype = getText(xmlAddField, "sqldatatype");
                if (sqldatatype == null)
                    throw new MissingFieldException("addfield sqldatatype field not found");

                string sql = getText(xmlAddField, "sql");
                if (sql == null)
                    throw new MissingFieldException("addfield sql field not found");

                string sortexpression = getText(xmlAddField, "sortexpression");
                if (sortexpression == null)
                    throw new MissingFieldException("addfield sortexpression field not found");

                string display = getText(xmlAddField, "display");

                bool excludefromtable = getBoolValue(xmlAddField, "excludefromtable");
                bool includewithparenttable = getBoolValue(xmlAddField, "includewithparenttable");
                bool includeInList = getBoolValue(xmlAddField, "includeinlist");
                bool isDefaultSort = getBoolValue(xmlAddField, "defaultsort");

                t.AddFieldNode addfldNode = new t.AddFieldNode(
                    name, sqldatatype, sql, sortexpression, display, excludefromtable,
                    includewithparenttable, includeInList, isDefaultSort);
                tblNode.AddAdditionalField(addfldNode);
            }
        }

        private void loadAdditionalSprocs(t.ClassNode csNode, XmlNodeList xmlAddSprocs)
        {
            foreach (XmlElement xmlAddSproc in xmlAddSprocs)
            {
                string name = capitalizeFirstLetter(getText(xmlAddSproc, "name"));
                if (name == null)
                    throw new MissingFieldException("addsproc name field not found");

                string returnType = getText(xmlAddSproc, "returntype");
                if (returnType == null)
                    throw new MissingFieldException("addsproc returnType field not found");

                t.AddSprocNode sprocNode = new t.AddSprocNode(name, returnType);
                csNode.AddAdditionalSproc(sprocNode);
                loadParams(sprocNode, xmlAddSproc.GetElementsByTagName("param"));
                loadReturnFields(sprocNode, xmlAddSproc.GetElementsByTagName("returnfield"));
            }
        }

        private void loadParams(t.AddSprocNode sprocNode, XmlNodeList xmlParams)
        {
            foreach (XmlElement xmlParam in xmlParams)
            {
                string name = getText(xmlParam, "name");
                if (name == null)
                    throw new MissingFieldException("param name field not found");

                string csDatatype = getText(xmlParam, "csdatatype");
                if (csDatatype == null)
                    throw new MissingFieldException("param csdatatype field not found");

                bool isEncrypted = getBoolValue(xmlParam, "encrypted");
                t.ParamNode paramNode = new t.ParamNode(name, csDatatype, isEncrypted);
                sprocNode.AddParam(paramNode);
            }
        }

        private void loadReturnFields(t.AddSprocNode sprocNode, XmlNodeList xmlReturnFields)
        {
            foreach (XmlElement xmlReturnField in xmlReturnFields)
            {
                string test = getText(xmlReturnField, "name");
                if (test != null && test != "")
                    loadCustomReturnField(sprocNode, xmlReturnField, test);
                else
                    loadReturnField(sprocNode, xmlReturnField);
            }
        }

        private void loadReturnField(t.AddSprocNode sprocNode, XmlElement xmlReturnField)
        {
            string name = capitalizeFirstLetter(xmlReturnField.InnerText);
            t.SprocReturnFieldNode rfieldNode = new t.SprocReturnFieldNode(name);
            sprocNode.AddReturnField(rfieldNode);
        }

        private void loadCustomReturnField(t.AddSprocNode sprocNode, XmlElement xmlReturnField, string name)
        {
            name = capitalizeFirstLetter(name);

            string csDatatype = getText(xmlReturnField, "csdatatype");
            if (csDatatype == null)
                throw new MissingFieldException("addReturnField csdatatype field not found");

            string sortExpression = getText(xmlReturnField, "sortexpression");
            if (sortExpression == null)
                throw new MissingFieldException("addReturnField sortExpression field not found");

            string display = getText(xmlReturnField, "display");

            t.SprocCustomReturnFieldNode rFieldNode = new t.SprocCustomReturnFieldNode(name, csDatatype, sortExpression, display);
            sprocNode.AddCustomReturnField(rFieldNode);
        }

        private string getText(XmlElement xml, string tag)
        {
            XmlElement e = (XmlElement)xml.GetElementsByTagName(tag).Item(0);
            if (e != null)
                return e.InnerText.Trim();
            else
                return null;
        }

        private bool getBoolValue(XmlElement xml, string tag)
        {
            string s = getText(xml, tag);
            if (s != null)
                return Convert.ToBoolean(s.Trim());
            else
                return false;
        }

        private string capitalizeFirstLetter(string s)
        {
            if (s != null && s != "")
            {
                char c = Char.ToUpper(s[0]);
                s = s.Remove(0, 1);
                s = s.Insert(0, c.ToString());
            }
            return s;
        }

        private string schemaFile;
    }
}
