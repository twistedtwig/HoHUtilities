using System;
using NhibernateRepository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions.Helpers;

namespace $rootnamespace$
{
    public class UserDbSchemaConfigurationController : DbSchemaConfigurationController
    {
        public override void Configure(FluentConfiguration configuration)
        {
            //This is where you can configure your database with NHibnerate Fluent configuration.
            throw new NotImplementedException();
        }        
    }
}
