using System;
using System.Collections.Generic;

namespace USP.Business.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<T, S>(this Dictionary<T, S> source, Dictionary<T, S> collection, Action<S, S> duplicateKeyCallback = null)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
                else
                {
                    duplicateKeyCallback?.Invoke(source[item.Key], item.Value);
                }
            }
        }

        public static void Merge(this Dictionary<string, string> source, Dictionary<string, string> collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                if (!source.ContainsKey(item.Key))
                {
                    source.Add(item.Key, item.Value);
                }
                else
                {
                    source[item.Key] += item.Value;
                }
            }
        }

        public static void Merge(this Dictionary<string, string> source, IDictionary<string, object> collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                var val = item.Value as string;

                if (!string.IsNullOrWhiteSpace(val))
                {
                    if (!source.ContainsKey(item.Key))
                    {
                        source.Add(item.Key, val);
                    }
                    else
                    {
                        source[item.Key] += val;
                    }
                }
            }
        }
    }
}
