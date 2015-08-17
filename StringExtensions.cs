using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace toarray.utils
{
    public static class StringExtensions
    {
        #region "  Validation  "

        public static bool IsValidCPF(this string CPF)
        {
            CPF = CPF.ClearStrings();

            if (CPF.Length < 11)
                return false;

            for (int i = 0; i < CPF.Length; i++)
                Convert.ToInt32(CPF[i].ToString());

            if (CPF == "00000000000" || CPF == "11111111111" || CPF == "22222222222" || CPF == "33333333333" || CPF == "44444444444" || CPF == "55555555555" || CPF == "66666666666" || CPF == "77777777777" || CPF == "88888888888" || CPF == "99999999999")
                return false;

            int[] a = new int[11];
            int b = 0;
            int c = 10;
            int x = 0;

            for (int i = 0; i < 9; i++)
            {
                a[i] = Convert.ToInt32(CPF[i].ToString());

                b += (a[i] * c);
                c--;
            }


            x = b % 11;

            if (x < 2)
                a[9] = 0;
            else
                a[9] = 11 - x;

            b = 0;
            c = 11;

            for (int i = 0; i < 10; i++)
            {
                b += (a[i] * c);
                c--;
            }

            x = b % 11;

            if (x < 2)
                a[10] = 0;
            else
                a[10] = 11 - x;

            if ((Convert.ToInt32(CPF[9].ToString()) != a[9]) || (Convert.ToInt32(CPF[10].ToString()) != a[10]))
                return false;

            return true;
        }

        public static bool IsValidCNPJ(this string CNPJ)
        {
            CNPJ = CNPJ.ClearStrings();

            if (CNPJ.Length < 14)
                return false;

            for (int i = 0; i < 14; i++)
                Convert.ToInt32(CNPJ[i].ToString());

            int[] a = new int[14];
            int b = 0;
            int[] c = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int x = 0;

            for (int i = 0; i < 12; i++)
            {
                a[i] = Convert.ToInt32(CNPJ[i].ToString());
                b += a[i] * c[i + 1];
            }

            x = b % 11;

            if (x < 2)
                a[12] = 0;
            else
                a[12] = 11 - x;


            b = 0;
            for (int j = 0; j < 13; j++)
                b += (a[j] * c[j]);

            x = b % 11;

            if (x < 2)
                a[13] = 0;
            else
                a[13] = 11 - x;

            if ((Convert.ToInt32(CNPJ[12].ToString()) != a[12]) || (Convert.ToInt32(CNPJ[13].ToString()) != a[13]))
                return false;

            return true;
        }

        public static bool IsValidEmail(this string email)
        {
            Match match = Regex.Match(email, @"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$");

            return match.Success;
        }

        private static bool IsCreditCard(string cardNumber)
        {
            if (cardNumber.IsNullorEmpty()) return false;

            if (cardNumber.Length > 19 || cardNumber.Length < 10) return false;

            int sum = 0, mul = 1, l = cardNumber.Length;

            for (int i = 0; i < l; i++)
            {
                string digit = cardNumber.Substring(l - i - 1, 1);

                int tproduct = Convert.ToInt32(digit) * mul;

                if (tproduct >= 10)
                    sum += (tproduct % 10) + 1;
                else
                    sum += tproduct;

                if (mul == 1)
                    mul++;
                else
                    mul--;
            }

            if ((sum % 10) == 0)
                return true;
            else
                return false;
        }

        public static bool IsNullorEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNumeric(this string value)
        {
            Match match = Regex.Match(value, "^[0-9]*$");

            return match.Success;
        }

        public static bool IsJSONRequest(this String contentType)
        {
            return contentType.Contains("application/json");
        }

        public static bool IsURL(this string url)
        {
            Uri uriResult = null;

            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        public static bool IsUnicode(this string input)
        {
            const int MaxAnsiCode = 255;

            return input.Any(c => c > MaxAnsiCode);
        }

        #endregion

        #region "  Strings Ajustment  "

        public static string ClearStrings(this string value)
        {
            if (value.IsNullorEmpty()) return value;

            return value.Replace(".", string.Empty)
                        .Replace(",", string.Empty)
                        .Replace(":", string.Empty)
                        .Replace("/", string.Empty)
                        .Replace(@"\", string.Empty)
                        .Replace("\"", string.Empty)
                        .Replace("'", string.Empty)
                        .Replace("-", string.Empty);
        }

        public static string ClearAccents(this string value)
        {
            if (value.IsNullorEmpty()) return value;

            value = Regex.Replace(value, "[áàâãª]", "a");
            value = Regex.Replace(value, "[ÁÀÂÃ]", "A");
            value = Regex.Replace(value, "[éèê]", "e");
            value = Regex.Replace(value, "[ÉÈÊ]", "e");
            value = Regex.Replace(value, "[íìî]", "i");
            value = Regex.Replace(value, "[ÍÌÎ]", "I");
            value = Regex.Replace(value, "[óòôõº]", "o");
            value = Regex.Replace(value, "[ÓÒÔÕ]", "O");
            value = Regex.Replace(value, "[úùû]", "u");
            value = Regex.Replace(value, "[ÚÙÛ]", "U");
            value = Regex.Replace(value, "[ç]", "c");
            value = Regex.Replace(value, "[Ç]", "C");

            return value;
        }

        public static string ClearHyphen(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("-", " ");

            return value;
        }

        public static string ClearSpecialCharacters(this string value, bool keepWhiteSpaces = false, bool keepBar = false)
        {
            if (value.IsNullOrWhiteSpace())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') | c == '.' || c == '_' || c == '-' || (c == ' ' && keepWhiteSpaces) || (c == '/' && keepBar) || (c == '\\' && keepBar))
                    sb.Append(c);
            }

            return sb.ToString();
        }


        public static string InputHTTPonURL(this string URL, bool inputWWW = false)
        {
            if (URL.IsNullorEmpty()) return URL;

            if (inputWWW)
                return String.Format("http://{0}", Regex.Replace(URL, "http://", string.Empty, RegexOptions.IgnoreCase).InputWWWonURL());
            else
                return String.Format("http://{0}", Regex.Replace(URL, "http://", string.Empty, RegexOptions.IgnoreCase));
        }

        public static string InputWWWonURL(this string URL)
        {
            if (URL.IsNullorEmpty()) return URL;

            return String.Format("www.{0}", Regex.Replace(URL, "www.", string.Empty, RegexOptions.IgnoreCase));
        }

        public static int OnlyNumbers(this string value)
        {
            return Regex.Match(value, @"\d+").Value.AsInt();
        }

        public static string OnlyAlphanumeric(this string value)
        {
            return Regex.Replace(value, @"[^A-Za-z0-9 ]", string.Empty).Trim();
        }

        #endregion

        #region "  Format  "

        public static string ToCNPJ(this string CNPJ)
        {
            if (CNPJ.IsNullorEmpty() || CNPJ.Length < 14) return CNPJ;

            return string.Format("{0}.{1}.{2}/{3}-{4}",
                CNPJ.Substring(0, 2), CNPJ.Substring(2, 3), CNPJ.Substring(5, 3), CNPJ.Substring(8, 4), CNPJ.Substring(12, 2));
        }

        public static string ToCPF(this string CPF)
        {
            if (CPF.IsNullorEmpty() || CPF.Length < 11) return CPF;

            return string.Format("{0}.{1}.{2}-{3}",
                CPF.Substring(0, 3), CPF.Substring(3, 3), CPF.Substring(6, 3), CPF.Substring(9, 2));
        }

        public static string ToCreditCardNumber(this string value)
        {
            if (value.Length < 10)
                return "---";
            else
                return string.Format("**** **** **** {0}", value.Substring(0, 6), value.Substring(value.Length - 4), 4);
        }

        public static string ToHtml(this string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        public static string ToText(this string value)
        {
            return WebUtility.HtmlDecode(value);
        }

        public static string URLEncode(this string url)
        {
            return WebUtility.UrlEncode(url);
        }

        public static string URLDecode(this string url)
        {
            return WebUtility.UrlDecode(url).Replace(" ", "+");
        }

        #endregion

        #region "  Compress  "

        public static byte[] Zip(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    msi.CopyTo(mso);

                return mso.ToArray();
            }
        }

        public static string Unzip(this byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    msi.CopyTo(mso);

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static string Unzip(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            return Unzip(bytes);
        }

        public static string HTMLPacker(this string html)
        {
            string htmlPacker = string.Empty;

            Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
            Regex RegexLineBreaks = new Regex(@"([\n\s])+?(?<= {2,})<", RegexOptions.Compiled);

            htmlPacker = RegexBetweenTags.Replace(html, ">");
            htmlPacker = RegexLineBreaks.Replace(htmlPacker, "<");
            htmlPacker = Regex.Replace(htmlPacker, @"\s+", " ");

            return htmlPacker;
        }

        #endregion

        #region "  Cast  "

        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }

        #endregion
    }
}
