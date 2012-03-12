﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace CustomConfigurations
{
    public class CollectionsGroup
    {
        private CollectionsGroupCollection Collections;
        private IList<string> collectionNames = new List<string>();
        private ConfigSection Parent;

        internal CollectionsGroup(CollectionsGroupCollection collections, ConfigSection parent)
        {
            if (collections == null)
            {
                throw new ArgumentException("CollectionsGroupCollection object was null.");
            }
            Collections = collections;

            foreach (ConfigurationGroupElement configGroup in collections)
            {
                collectionNames.Add(configGroup.Name);
            }

            Parent = parent;
        }

        /// <summary>
        /// Determines if the collections group has any child collections.
        /// </summary>
        public bool HasCollections { get { return Collections.Count > 0; } }

        /// <summary>
        /// Returns a readonly collection of the section names available.
        /// </summary>
        public IEnumerable<string> SectionNames
        {
            get { return new ReadOnlyCollection<string>(collectionNames); }
        }

        /// <summary>
        /// Determines if there is a collection with the given name attribute.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsKey(string name)
        {
            return collectionNames.Contains(name);
        }

        /// <summary>
        /// Returns a config section for the given name attribute for the inner collection.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ConfigSection GetCollection(string name)
        {
            if (!ContainsKey(name))
            {
                return null;
            }

            return new ConfigSection(Collections[name], Parent);
        }
    }
}
