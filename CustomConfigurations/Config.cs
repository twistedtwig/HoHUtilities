using System;
using System.ComponentModel;

namespace CustomConfigurations
{
    /// <summary>
    /// The Config class is a wrapper around the ConfigurationLoader.  It deals with creating and loading in the variables.
    /// It allows easier access to any Value for a given key.
    /// 
    /// For a given key it will return NULL or the value if found.
    /// </summary>
    public class Config
    {
        private ConfigurationLoader ConfigLoader = null;
        

        /// <summary>
        /// Constructor with the full confiuration path given.
        /// </summary>
        /// <exception cref="ArgumentException">error if configuraiton path is null or empty string</exception>
        /// <exception cref="ApplicationException">error if fails to load given configuration section</exception>
        /// <param name="configurationPath"></param>
        public Config(string configurationPath)
        {
            if(string.IsNullOrEmpty(configurationPath.Trim()))
            {
                throw new ArgumentException(configurationPath);
            }

            ConfigLoader = System.Configuration.ConfigurationManager.GetSection(configurationPath) as ConfigurationLoader;
            if(ConfigLoader == null)
            {
                throw new ApplicationException(string.Format("ConfigurationLoader failed to load with path '{0}' null object returned.", configurationPath));
            }
        }

        /// <summary>
        /// Returns the number of Configuration Items found.
        /// </summary>
        public int Count { get { return ConfigLoader.ValueItems.Count; } }

        /// <summary>
        /// Checks to see if a given key is in the list of configuration items found
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return ConfigLoader.ValueItems[key] != null;
        }

        public string this[string key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    return null;
                }

                ValueItemElement item = ConfigLoader.ValueItems[key];
                return item != null ? item.Value : null;
            }
        }

        /// <summary>
        /// Tries to parse the value for the given key and return the type converted into the generic Type provided.
        /// 
        /// The OUT Result indicates if the conversion was successful or not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public T TryParse<T>(string key, out bool result)
        {
            result = false;
            //check we have real value first.
            string value = this[key];
            if (!ContainsKey(key))
            {
                return default(T);
            }

            T instance = Activator.CreateInstance<T>();
            Type type = instance.GetType();
            TypeConverter converter = TypeDescriptor.GetConverter(instance.GetType());
            if (converter.CanConvertFrom(typeof(string)))
            {
                try
                {
                    object val = converter.ConvertFromInvariantString(value);
                    if (val == null)
                        return default(T);

                    result = true;
                    return (T) val;
                }
                catch (Exception)
                {
                    return default(T);
                }
            }

            return default(T);
        }

    }
}
