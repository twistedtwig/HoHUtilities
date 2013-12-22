using System;
using System.IO;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateSchemaController
{
    public class SchemaManagementController
    {

        public void CreateDatabase(SchemaDefinition definition)
        {
            CheckDefinitionIsValid(definition);

            var schemaExport = new SchemaExport(definition.Configuration);
            if (definition.FileDefinition != null && !string.IsNullOrWhiteSpace(definition.FileDefinition.FileName))
            {
                //output the sql to create the db to a file.                
                string filename = Path.Combine(string.IsNullOrWhiteSpace(definition.FileDefinition.OutputFolder) ? "" : definition.FileDefinition.OutputFolder, definition.FileDefinition.FileName);
                schemaExport = schemaExport.SetOutputFile(filename);
            }

            if (definition.Session == null)
            {
                schemaExport.Execute(definition.ShowSql, true, false);
            }
            else
            {
                schemaExport.Execute(definition.ShowSql, true, false, definition.Session.Connection, null);
            }
        }


        public void UpdateExistingDatabase(SchemaDefinition definition)
        {
            CheckDefinitionIsValid(definition);

            var schemaUpdate = new SchemaUpdate(definition.Configuration);
            if (definition.FileDefinition != null && !string.IsNullOrWhiteSpace(definition.FileDefinition.FileName))
            {
                //output the sql to create the db to a file.                
                string filename = Path.Combine(string.IsNullOrWhiteSpace(definition.FileDefinition.OutputFolder) ? "" : definition.FileDefinition.OutputFolder, definition.FileDefinition.FileName);

                Action<string> updateExport = x =>
                {
                    using (var file = new FileStream(filename, FileMode.Append, FileAccess.Write))
                    using (var sw = new StreamWriter(file))
                    {
                        sw.Write(x);
                        sw.Close();
                    }
                };

                schemaUpdate.Execute(updateExport, true);
            }
            else
            {
                schemaUpdate.Execute(false, true);
            }
        }

        public bool IsSchemaValid(Configuration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            SchemaValidator validator = new SchemaValidator(config);
            try
            {
                validator.Validate();
                return true;
            }
            catch (HibernateException)
            {
                // not valid, needs to updating
                return false;
            }
        }

        private static void CheckDefinitionIsValid(SchemaDefinition definition)
        {
            if (definition == null || definition.Configuration == null)
            {
                throw new ArgumentNullException();
            }
        }

    }
}