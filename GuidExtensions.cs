using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toarray.utils
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsEmpty(this Guid? guid)
        {
            return guid.IsNull() || guid == Guid.Empty;
        }

        public static Guid? ToDBNull(this Guid guid)
        {
            return (guid == Guid.Empty ? null : (Guid?)guid);
        }

        public static Guid AsGuid(this object item)
        {
            try { return new Guid(item.ToString()); }
            catch { return Guid.Empty; }
        }
    }
}
