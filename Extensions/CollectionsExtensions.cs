using System.Collections.Generic;
using System.Linq;

namespace HoHUtilities.Extensions
{
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Will add each unique item to the collection, (i.e. no duplicates "==" comparision objects will be added).
        /// </summary>
        /// <typeparam name="T">Object of Type of T</typeparam>
        /// <param name="collection">Collection of objects to add values to.</param>
        /// <param name="collectionToAdd">Collection to add to other list.</param>
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> collectionToAdd)
        {
            collection.AddRange(collectionToAdd, true);
        }

        /// <summary>
        /// Will add a collection to the current collection.  Duplicate objects can be added if wanted.
        /// </summary>
        /// <typeparam name="T">Object of Type of T</typeparam>
        /// <param name="collection">Collection of objects to add values to.</param>
        /// <param name="collectionToAdd">Collection to add to other list.</param>
        /// <param name="uniqueOnly">bool should only unique items be added</param>
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> collectionToAdd, bool uniqueOnly)
        {
            foreach (T item in collectionToAdd)
            {
                if (uniqueOnly && collection.Contains(item))
                {
                    continue;
                }

                collection.Add(item);
            }
        }

        /// <summary>
        /// Add An item to the collection if it is not present in the Second collection.
        /// </summary>
        /// <typeparam name="T">Object of Type of T</typeparam>
        /// <param name="firstList">Collection of objects to have an item added to</param>
        /// <param name="item">Object of Type T to be added to the first collection</param>
        /// <param name="secondList">Colelction of Type T that will be checked against to see if the item exists in that collection</param>
        public static void AddIfNotInSecondList<T>(this ICollection<T> firstList, T item, ICollection<T> secondList)
        {
            if (secondList.Contains(item))
            {
                firstList.Remove(item);
                return;
            }

            if (!firstList.Contains(item))
                firstList.Add(item);
        }

        /// <summary>
        /// Adds the item given to the collection specified.  If Unique only is specified then collection can not already contain item, if it does nothing will happen, otherwise it will be added.
        /// </summary>
        /// <typeparam name="T">Object of Type of T</typeparam>
        /// <param name="collection">Collection of objects to have an item added to</param>
        /// <param name="item">Object of Type T to be added to the first collection</param>
        /// <param name="uniqueOnly">bool should only unique items be added</param>
        public static void Add<T>(this ICollection<T> collection, T item, bool uniqueOnly)
        {
            if(uniqueOnly && collection.Contains(item))
                return;

            collection.Add(item);
        }

        /// <summary>
        /// removes all instances of an item in the collection and returns the number of items removed.
        /// </summary>
        /// <typeparam name="T">Object Type of T</typeparam>
        /// <param name="collection">ICollection</param>
        /// <param name="item">Item of Type T</param>
        /// <returns>int number of items removed.</returns>
        public static int RemoveAll<T>(this ICollection<T> collection, T item)
        {
            int counter = 0;
            int count = collection.Count;
            for (int i = 0; i < count; i++)
            {                
                if(collection.Contains(item))
                {
                    collection.Remove(item);
                    i--;
                    count--;
                    counter++;
                }
            }

            return counter;
        }

        /// <summary>
        /// Will remove all instances of every item in the second collection from the first.
        /// </summary>
        /// <typeparam name="T">Object Type of T</typeparam>
        /// <param name="collection">ICollection to have the items removed from</param>
        /// <param name="secondCollection">ICollection List of items to remove from the first collection.</param>
        /// <returns></returns>
        public static int RemoveAll<T>(this ICollection<T> collection, ICollection<T> secondCollection)
        {
            return secondCollection.Sum(item => collection.RemoveAll(item));
        }
    }
}
