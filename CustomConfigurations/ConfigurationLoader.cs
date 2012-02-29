using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CustomConfigurations
{
    /// <summary>
    /// Deals with Loading Configuration values from the config file.
    /// </summary>
    public class ConfigurationLoader : ConfigurationSection
    {
        public ConfigurationLoader()
        {
            
        }
        /// <summary>
        /// Colleciton of ValueItemElements.
        /// </summary>
        [ConfigurationProperty("ValueItems", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ValueItemElementCollection), AddItemName = "ValueItem")]
        public ValueItemElementCollection ValueItems
        {
            get { return this["ValueItems"] as ValueItemElementCollection; }
        }
        
//        [ConfigurationProperty("ValueItem")]
//        public ValueItemElement ValueItem
//        {
//            get { return this["ValueItem"] as ValueItemElement; }
//        }
    }




    /// <summary>
    /// Object to hold a list of valueItemElements
    /// </summary>
    public class ValueItemElementCollection: ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ValueItemElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for. 
        ///                 </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValueItemElement) element).Key;
        }

        public ValueItemElement this[int index]
        {
            get { return BaseGet(index) as ValueItemElement; }
        }

        public new ValueItemElement this[string key]
        {
            get { return base.BaseGet(key) as ValueItemElement; }
        }
    }





    /// <summary>
    /// An Object to Hold a single key value pair.
    /// </summary>
    public class ValueItemElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public String Key
        {
            get
            {
                return this["key"].ToString();
            }            
        }

        [ConfigurationProperty("value", IsRequired = true, IsKey = false)]
        public String Value
        {
            get
            {
                return this["value"].ToString();
            }
        }
    }


}
