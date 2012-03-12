using System.Configuration;

namespace CustomConfigurations
{
    /// <summary>
    /// Deals with Loading Configuration values from the config file.
    /// </summary>
    public class CollectionsSectionLoader : ConfigurationSection
    {
        /// <summary>
        /// Colleciton of ValueItemElements.
        /// </summary>
        [ConfigurationProperty("Collections")]
        public ConfigurationCollectionElementCollection ConfigGroups
        {
            get { return this["Collections"] as ConfigurationCollectionElementCollection; }
        }

    }

    /// <summary>
    /// holds the collection of all configuraiton groups, i.e. all clients, the name attribute for each group is the key to the collection
    /// </summary>
    public class ConfigurationCollectionElementCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationCollectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfigurationCollectionElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "Collection"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        public ConfigurationCollectionElement this[int index]
        {
            get { return BaseGet(index) as ConfigurationCollectionElement; }
        }

        public new ConfigurationCollectionElement this[string key]
        {
            get { return BaseGet(key) as ConfigurationCollectionElement; }
        }
    }



    public class ConfigurationCollectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
        }

        [ConfigurationProperty("ValueItems")]
        public ValueItemElementCollection ValueItemCollection
        {
            get { return this["ValueItems"] as ValueItemElementCollection; }
        }
    }
}
