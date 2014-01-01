using NHibernate;
using System;

namespace NhibernateRepository
{
    public abstract class RepositoryBase<TD, TS>  : IDisposable
        where TD : IDefinitionLoader 
        where TS : ISchemaConfigurationLoader
    {
        private readonly ISessionFactory _sessionFactory;

        private ITransaction Transaction { get; set; }

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

            Session = _sessionFactory.OpenSession();
            Transaction = Session.BeginTransaction();
        }

        protected ISession Session { get; private set; }

        public void Dispose()
        {
            if (Session != null)
            {
                Session.Close();
                Session = null;
            }
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            if (Transaction.IsActive) Transaction.Rollback();
        }
    }
}
