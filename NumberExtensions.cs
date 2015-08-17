using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toarray.utils
{
    public static class NumberExtensions
    {
        #region "  Is Zero  "

        public static bool IsZero(this int value)
        {
            return value == default(int);
        }

        public static bool IsZero(this decimal value)
        {
            return value == default(decimal);
        }

        public static bool IsZero(this float value)
        {
            return value == default(float);
        }

        public static bool IsZero(this long value)
        {
            return value == default(long);
        }

        #endregion

        #region "  Format  "

        public static string ToCurrency(this decimal value)
        {
            return String.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR").NumberFormat, "{0:C2}", value);
        }

        public static string ToCurrencyWithoutSign(this decimal value)
        {
            return String.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR").NumberFormat, "{0:N2}", value);
        }

        public static string ToAmericanCurrency(this decimal value)
        {
            return String.Format(System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:N2}", value).ClearSpecialCharacters();
        }

        #endregion

        #region "  Cast  "

        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;

            if (item is Enum)
                result = Convert.ToInt32(item);
            else if (!int.TryParse(item.ToString(), out result))
                result = defaultInt;

            return result;
        }

        public static long AsLong(this object item, long defaultLong = default(long))
        {
            if (item == null)
                return defaultLong;

            long result;
            if (!long.TryParse(item.ToString(), out result))
                return defaultLong;

            return result;
        }

        public static decimal AsDecimal(this object value, decimal defaultDecimal = default(decimal))
        {
            if (value == null)
                return defaultDecimal;

            decimal result;
            if (!decimal.TryParse(value.ToString(), out result))
                return defaultDecimal;

            return result;
        }

        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }

        #endregion
    }
}
