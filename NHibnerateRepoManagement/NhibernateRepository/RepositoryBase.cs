using NHibernate;
using System;

namespace NhibernateRepository
{
    public abstract class RepositoryBase<TD, TS>  
        where TD : IDefinitionLoader 
        where TS : ISchemaConfigurationLoader
    {
        private readonly ISessionFactory _sessionFactory;
        public static ISession SessionItem { get; protected set; }

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
        }

        protected ISession Session
        {
            get
            {
                if (SessionItem == null || !SessionItem.IsOpen)
                {
                    SessionItem = _sessionFactory.OpenSession();
                }

                return SessionItem;
            }
        }

        public void Dispose()
        {
            if (SessionItem != null)
            {
                Session.Dispose();
            }
        }


        public void BeginTransaction()
        {
            Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Session.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            Session.Transaction.Rollback();
        }

    }
}
