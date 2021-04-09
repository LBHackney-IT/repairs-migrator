using System.Collections.Generic;

namespace RepairsMigrator
{
    public static class Extensions
    {
        public static string GetMaybe<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key)
            where TValue : class
        {
            if (source.TryGetValue(key, out var value)
                && !string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return value.ToString();
            }

            return null;
        }

        public static bool IsNotNull(this object o)
        {
            return o != null;
        }

        public static bool IsNull(this object o)
        {
            return o is null;
        }
    }
}
