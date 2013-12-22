using NHibernate;
using NHibernate.Cfg;

namespace NHibernateSchemaController
{
    public class SchemaDefinition
    {
        public SchemaDefinition()
        {
            ShowSql = false;
        }

        public Configuration Configuration { get; set; }
        public SchemaFileDefinition FileDefinition { get; set; }
        public ISession Session { get; set; }
        public bool ShowSql { get; set; }
    }
}
