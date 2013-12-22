using NHibernate;
using NHibernate.Cfg;
using System;

namespace NhibernateRepository
{
    public interface ISchemaConfigurationLoader : IDisposable
    {
        ISchemaConfigurationLoader CreateConfiguration(DbSchemaDefinition definition);
        Configuration Configuration { get; }
        ISessionFactory SessionFactory { get; }
    }
}
