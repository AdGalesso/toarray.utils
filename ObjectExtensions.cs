using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toarray.utils
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static T IsDBNull<T>(this object value)
        {
            if (!Convert.IsDBNull(value))
                return (T)value;

            return default(T);
        }

        public static T IsDBNull<T>(this object value, T defaultValue)
        {
            if (!Convert.IsDBNull(value))
                return (T)value;

            return defaultValue;
        }

        public static T Transfer<T>(this object value) where T : new()
        {
            if (value.IsNull()) value = new T();

            return (T)value;
        }

        public static IDictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k => source[k]);
        }

        public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> source)
        {
            if (!source.IsNull())
            {
                return source.Aggregate(new NameValueCollection(), (seed, current) =>
                {
                    seed.Add(current.Key, current.Value);
                    return seed;
                });
            }
            else
                return null;
        }

        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;

            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }

        public static bool AsBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return new List<string>() { "yes", "y", "true", "1" }.Contains(item.ToString().ToLower());
        }
    }
}
