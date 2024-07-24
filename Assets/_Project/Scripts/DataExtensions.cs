using System.Collections.Generic;
using UnityEngine;

namespace OctanGames.Data
{
    public static class DataExtensions
    {
        public static TValue GetValueOrNull<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> collection, TKey key)
            where TValue : class =>
            collection.TryGetValue(key, out TValue value)
                ? value
                : null;
    }
}