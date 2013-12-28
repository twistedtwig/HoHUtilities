using CustomConfigurations;
using System;

namespace NhibernateRepository
{
    public abstract class DbSchemaDefinitionConfigLoader : IDefinitionLoader
    {
        public virtual DbSchemaDefinition CreateDefinition()
        {
            try
            {
                return new Config().GetSection("DbSchemaDefinition").Create<DbSchemaDefinition>();
            }
            catch (Exception)
            {
                //incase no definition setup
                return new DbSchemaDefinition();
            }
        }
    }
}
