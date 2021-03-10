using System.Collections.Generic;

namespace Core.Test
{
    static class Helpers
    {
        public static List<T> List<T>(params T[] items)
        {
            return new List<T>(items);
        }
    }
}
