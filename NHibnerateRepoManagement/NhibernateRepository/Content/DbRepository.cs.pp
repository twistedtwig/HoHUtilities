using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NhibernateRepository;
using NHibernate.Linq;

namespace $rootnamespace$
{
    public class DbRepository : DbRepositoryBase<IDefinitionLoader, ISchemaConfigurationLoader>
    {
        public DbRepository(IDefinitionLoader definitionLoader, ISchemaConfigurationLoader schemaConfigurationLoader) : base(definitionLoader, schemaConfigurationLoader)
        {
        }
    }
}
