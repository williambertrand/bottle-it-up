using System;
using System.Collections.Generic;
using System.Linq;

namespace extensions
{
    public static class CollectionExtensions
    {

        public static IEnumerable<TR> Map<T, TR>(this IEnumerable<T> c, Func<T, TR> f)
        {
            return c.Select(f.Invoke).ToList();
        }

        public static Dictionary<TK, TV> ToDict<T, TK, TV>(this IEnumerable<T> c, Func<T, TK> keyFunc, Func<T, TV> valueFunc)
        {
            return c
                .Select(x => new KeyValuePair<TK, TV>(keyFunc.Invoke(x), valueFunc.Invoke(x)))
                .ToDictionary(p => p.Key, p => p.Value);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var element in list) action.Invoke(element);
        }

        public static void InvokeAll(this IEnumerable<Action> l) => l.ForEach(e => e?.Invoke());
    }
}