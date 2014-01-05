using NHibernate;
using System;
using NHibernateSchemaController;

namespace NhibernateRepository
{
    public abstract class RepositoryBase<TD, TS>  : IDisposable
        where TD : IDefinitionLoader 
        where TS : ISchemaConfigurationLoader
    {
        private readonly ISessionFactory _sessionFactory;

        private ISession _session;

        protected RepositoryBase(TD definitionLoader, TS schemaConfigurationLoader) 
        {
            if (definitionLoader == null)
            {
                throw new ArgumentNullException("definitionLoader");
            }

            if (schemaConfigurationLoader == null)
            {
                throw new ArgumentNullException("schemaConfigurationLoader");
            }

            var schemaDefinition = definitionLoader.CreateDefinition();
            var configLoader = schemaConfigurationLoader.CreateConfiguration(schemaDefinition);
            _sessionFactory = configLoader.SessionFactory;

            _session = _sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Never;


            var schemaManagementDefinition = new SchemaDefinition
                {
                    Configuration = configLoader.Configuration,
                    Session = _session,
                    ShowSql = schemaDefinition.ShowSql
                };

            if (schemaDefinition.AutoCreateDatabase)
            {
                var schema = new SchemaManagementController();
                if (!schema.IsSchemaValid(configLoader.Configuration))
                {
                    schema.UpdateExistingDatabase(schemaManagementDefinition);
                }
            }
        }

        public ISession ReadOnlySession { get { return _session; } }

        public virtual IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(_sessionFactory);
        }


        public void Dispose()
        {
            if (_session != null)
            {
                _session.Close();
                _session = null;
            }
        }        
    }
}
