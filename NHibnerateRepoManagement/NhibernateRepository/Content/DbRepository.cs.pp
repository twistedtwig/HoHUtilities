using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using NhibernateRepository;

namespace NhibernateRepository.Content
{
    public class DbRepository<T, U> : RepositoryBase<T, U> where T : IDefinitionLoader where U : ISchemaConfigurationLoader
    {

        public DbRepository(T definitionLoader, U schemaConfigurationLoader) : base(definitionLoader, schemaConfigurationLoader)
        {

        }
        
    }
}
