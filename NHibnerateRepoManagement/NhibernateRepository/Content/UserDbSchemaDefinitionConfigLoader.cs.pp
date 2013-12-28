using NhibernateRepository;

namespace $rootnamespace$
{
    public class UserDbSchemaDefinitionConfigLoader : DbSchemaDefinitionConfigLoader
    {

        /// <summary>
        /// The base class will try to load a config section called "DbSchemaDefinition", if none is found it will create a blank model.
        /// You can override the loader with your own implementation here if required.
        /// </summary>
        /// <returns></returns>
        public override DbSchemaDefinition CreateDefinition()
        {
            return base.CreateDefinition();
        }
    }
}
