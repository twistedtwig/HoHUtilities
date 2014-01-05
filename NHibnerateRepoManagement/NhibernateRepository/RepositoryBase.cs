using NHibernate;
using System;

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
            _sessionFactory = schemaConfigurationLoader.CreateConfiguration(schemaDefinition).SessionFactory;

            _session = _sessionFactory.OpenSession();
            _session.FlushMode = FlushMode.Never;

        }

        protected ISession ReadOnlySession { get { return _session; } }

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
