using System;
using System.Collections;
using Extensions;
using System.Collections.Generic;

namespace ClassLocator
{
    /// <summary>
    /// Collection of baseClass Types and the list of their implementations.
    /// </summary>
    public class ClassCollection<T> : IEnumerable<T>  where T : ClassSettings
    {
        protected IDictionary<Type, T> Collection { get; set; }

        public ClassCollection() : this(new Dictionary<Type, T>()) { }
        public ClassCollection(IDictionary<Type, T> collection)
        {
            Collection = collection;
        }

        /// <summary>
        /// Will add settings to the collection, (if the type has already been entered the results wil be merged, (duplicate implemenatations will be removed).
        /// </summary>
        /// <param name="classSetting"><see cref="ClassSettings"/> settings to be added to the collection</param>
        public void AddClassSetting(T classSetting)
        {
            if(Collection.ContainsKey(classSetting.BaseClass))
            {
                Collection[classSetting.BaseClass].Implemenations.AddRange(classSetting.Implemenations, true);                
            }
            else
            {
                Collection.Add(classSetting.BaseClass, classSetting);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.Values.GetEnumerator();
        }
    }
}
