using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using System;

namespace NhibernateRepository
{
    public abstract class DbSchemaConfigurationController : ISchemaConfigurationLoader
    {
        private Configuration _configurationItem;
        private ISessionFactory _sessionFactoryItem;
        

        public ISchemaConfigurationLoader CreateConfiguration(DbSchemaDefinition definition)
        {
            FluentConfiguration configuration = Fluently.Configure();

            //let user choose db configuration
            var dbConfiguration = ConfigureDatabase(definition);
            if (dbConfiguration == null)
            {
                throw new ArgumentNullException("dbConfiguration");
            }

            configuration.Database(dbConfiguration);

            //let user configure database.
            Configure(configuration);

            _configurationItem = configuration.BuildConfiguration();

            return this;
        }

        public Configuration Configuration
        {
            get
            {
                if (_configurationItem == null)
                {
                    throw new ArgumentNullException("Configuration");
                }

                return _configurationItem;
            }
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactoryItem ?? (_sessionFactoryItem = Configuration.BuildSessionFactory()); }
        }

        public void Dispose()
        {
            _configurationItem = null;

            if (_sessionFactoryItem != null)
            {
                _sessionFactoryItem.Dispose();
                _sessionFactoryItem = null;
            }
        }



        protected IPersistenceConfigurer ConfigureMsDatabase(DbSchemaDefinition definition)
        {
            var dbConfig = MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(definition.ConnectionStringName));
            if (definition.ShowSql)
            {
                dbConfig.ShowSql();
                dbConfig.FormatSql();
            }

            return dbConfig;
        }


        public abstract void Configure(FluentConfiguration configuration);
        public virtual IPersistenceConfigurer ConfigureDatabase(DbSchemaDefinition definition)
        {
            return ConfigureMsDatabase(definition);
        }
    }
}
