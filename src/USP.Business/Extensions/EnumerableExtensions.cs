using System;
using System.Collections.Generic;
using System.Linq;

namespace USP.Business.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Check whether a <paramref name="collection"/> of type
        /// <typeparamref name="T"/> contains only the objects specified
        /// in <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to check</param>
        /// <param name="elements">Elements to check for</param>
        /// <returns>True iff the collection contains only the objects in <paramref name="elements"/></returns>
        /// <remarks>
        /// Ensures that <paramref name="collection"/> is a subset of <paramref name="elements"/>
        /// </remarks>
        public static bool ContainsOnly<T>(this IEnumerable<T> collection, params T[] elements)
        {
            return collection.ContainsOnly(null, elements);
        }

        /// <summary>
        /// Check whether a <paramref name="collection"/> of type
        /// <typeparamref name="T"/> contains only the objects specified
        /// in <paramref name="elements"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to check</param>
        /// <param name="comparer">Comparer to use (null for default)</param>
        /// <param name="elements">Elements to check for</param>
        /// <returns>True iff the collection contains only the objects in <paramref name="elements"/></returns>
        public static bool ContainsOnly<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparer,
                                           params T[] elements)
        {
            return collection.All(i => elements.Contains(i, comparer));
        }

        /// <summary>
        /// Check whether a <paramref name="collection"/> of type
        /// <typeparamref name="T"/> is empty.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to check</param>
        /// <returns>True if the item is empty</returns>
        public static bool Empty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return true;
            }

            return !collection.Any();
        }

        /// <summary>
        /// Check whether a <paramref name="collection"/> of type
        /// <typeparamref name="T"/> is empty.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to check</param>
        /// <returns>True if the item is empty</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.Empty();
        }

        /// <summary>
        /// Removes any null values from a given <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to filter</param>
        /// <returns>A collection with no null elements</returns>
        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> collection)
            where T : class
        {
            return collection.Where(o => o != null);
        }

        /// <summary>
        /// An inverse Where method
        /// </summary>
        /// <typeparam name="T">An object</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="predicate">Predicate</param>
        /// <returns>The items from collection where the predicate returns false</returns>
        public static IEnumerable<T> ExceptWhere<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Where(o => !predicate(o));
        }

        /// <summary>
        /// Executes an action on each element inside a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="action">Action</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Check whether a <paramref name="collection"/> of type
        /// <typeparamref name="T"/> is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/></typeparam>
        /// <param name="collection">Collection to check</param>
        /// <returns>True if the item is null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Empty();
        }

        /// <summary>
        /// <para>
        /// Converts a <paramref name="collection"/> of objects of type
        /// <typeparamref name="T"/> to a formatted english list, delimited
        /// with <paramref name="delimiter"/> except for the final item,
        /// which is joined by <paramref name="conjunction"/>.
        /// </para>
        /// <para>
        /// Generate lists like "a, b, c and d".
        /// </para>
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="collection"/> items</typeparam>
        /// <param name="collection">Collection to format</param>
        /// <param name="converter">Converts objects of type <typeparamref name="T"/> to <see cref="string"/></param>
        /// <param name="delimiter">List delimiter</param>
        /// <param name="conjunction">Final conjunction</param>
        /// <returns>A formatted list of the form "a, b, c and d".</returns>
        public static string ToEnglishList<T>(this IEnumerable<T> collection, Func<T, string> converter, string delimiter, string conjunction)
        {
            return collection.Select(converter).ToEnglishList(delimiter, conjunction);
        }

        /// <summary>
        /// <para>
        /// Converts a <paramref name="collection"/> of collection
        /// to a formatted english list, delimited
        /// with <paramref name="delimiter"/> except for the final item,
        /// which is joined by <paramref name="conjunction"/>.
        /// </para>
        /// <para>
        /// Generate lists like "a, b, c and d".
        /// </para>
        /// </summary>
        /// <param name="collection">Collection to format</param>
        /// <param name="delimiter">List delimiter</param>
        /// <param name="conjunction">Final conjunction</param>
        /// <returns>A formatted list of the form "a, b, c and d".</returns>
        public static string ToEnglishList(this IEnumerable<string> collection, string delimiter, string conjunction)
        {
            var innerCollection = collection.Select(s => s.ToString()).ToList();
            if (innerCollection.Count <= 2)
            {
                return string.Join(conjunction, innerCollection);
            }

            return string.Format("{0} {1} {2}", string.Join(delimiter, innerCollection.Take(innerCollection.Count - 1)),
                                 conjunction,
                                 innerCollection.Last());
        }
    }
}