
namespace NhibernateRepository
{
     public class DbRepositoryBase<T, U> : RepositoryBase<T, U>
        where T : IDefinitionLoader
        where U : ISchemaConfigurationLoader
    {

        public DbRepositoryBase(T definitionLoader, U schemaConfigurationLoader)
            : base(definitionLoader, schemaConfigurationLoader)
        {

        }

    }
}
