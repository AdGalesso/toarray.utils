using System.Collections;

namespace toarray.utils
{
    public static class CollectionExtensions
    {
        public static bool IsZero(this IList list)
        {
            return list.Count > 0;
        }
    }
}