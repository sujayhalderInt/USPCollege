using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Convert = System.Convert;

namespace USP.Business.Extensions
{
    public static class StringExtensions
    {
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        public static string UrlPathEncode(this string s)
        {
            return HttpUtility.UrlPathEncode(s);
        }

        public static string UrlDecode(this string s)
        {
            return HttpUtility.UrlDecode(s);
        }

        /// <summary>
        /// Capitalises the first letter of a string
        /// </summary>
        /// <param name="value">String to capitalise</param>
        /// <returns>A capitalised string</returns>
        public static string Capitalise(this string value)
        {
            return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
        }

        /// <summary>
        /// Capitalises the first letter of every word in
        /// a string
        /// </summary>
        /// <param name="value">String to capitalise</param>
        /// <returns>A capitalised string</returns>
        public static string CapitaliseAll(this string value)
        {
            return string.Join(" ", value.Split(' ').Select(Capitalise));
        }

        /// <summary>
        /// Formats a string with given parameters
        /// </summary>
        /// <param name="value">String to format</param>
        /// <param name="args">arguments for the format pattern</param>
        /// <returns>A formatted string</returns>
        public static string FormatWith(this string value, params object[] args)
        {
            return args.Empty() ? value : string.Format(value, args);
        }

        /// <summary>
        /// Determines whether a string is null or empty
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns>True if the string is null or empty</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Determines whether a string is null or whitespace
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns>True if the string is null or whitespace</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Determines whether a string is null or whitespace
        /// </summary>
        /// <param name="value">String to check</param>
        /// <param name="valueWhenEmpty"> </param>
        /// <returns>True if the string is null or whitespace</returns>
        public static string IfNullOrWhiteSpace(this string value, string valueWhenEmpty)
        {
            return string.IsNullOrWhiteSpace(value) ? valueWhenEmpty : value;
        }

        /// <summary>
        /// Supports a StringComparison operator for Contains, supporting Case Insensitive searching
        /// </summary>
        /// <param name="target">the string to search against</param>
        /// <param name="value">the string to search for</param>
        /// <param name="comparison">The StringComparison type to use</param>
        /// <returns></returns>
        public static bool Contains(this string target, string value, StringComparison comparison)
        {
            return target.IndexOf(value, comparison) >= 0;
        }

        public static bool ContainsUrl(this string s, string url)
        {
            if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(url)) return false;
            s = s.Trim('/');
            url = url.Trim('/');
            var values = s.Split('/');
            if (values.Length < 1) return false;
            var urls = url.Split('/');
            if (urls.Length < 1) return false;
            return values[0] == urls[0];
        }

        public static float AsFloat(this string value)
        {
            float f;
            if (!float.TryParse(value, out f))
            {
                return 0.00f;
            }
            return f;
        }

        public static decimal AsDecimal(this string value)
        {
            decimal d;
            if (!decimal.TryParse(value, out d))
            {
                return 0.00M;
            }
            return d;
        }

        public static double AsDouble(this string value)
        {
            double d;
            if (!double.TryParse(value, out d))
            {
                return 0.00D;
            }
            return d;
        }

        public static int AsInt(this string value)
        {
            int i;
            if (!int.TryParse(value, out i))
            {
                return 0;
            }
            return i;
        }


        public static bool AsBool(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            if (value.Length == 1 && (int)value[0] == 49)
                return true;
            else
                return value.IsInvariantEqual("true");
        }

        public static Guid AsGuid(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(Guid);
            Guid output;
            Guid.TryParse(value, out output);
            return output;
        }

        public static string ToBoolString(this bool value)
        {
            return !value ? "0" : "1";
        }

        public static bool IsInvariantEqual(this string entry, string value)
        {
            return entry.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        //        /// <summary>
        //        ///Try to get the main domain from a website address e.g. http://www.site.org
        //        /// </summary>
        //        /// <returns></returns>
        //        public static string GetDomain(this string webAddress)
        //        {
        //            var domain = webAddress.Trim().ToLowerInvariant().Replace("http://", "");
        //            domain = webAddress.Trim().ToLowerInvariant().Replace("www.", "");
        //
        //            domain = WebUtil.RemoveQueryString(domain);
        //            if (domain.LastIndexOf('/') > 0)
        //            {
        //                domain = domain.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
        //            }
        //
        //            return domain;
        //        }

        public static string GetReletiveUrl(this string webAddress)
        {
            var uri = new Uri(webAddress, UriKind.RelativeOrAbsolute);
            return uri.AbsolutePath;
        }

        public static string ArrayToString(this string[] strings)
        {
            return string.Join(", ", strings);
        }

        public static string[] SplitJointString(this string value, bool toLower = true)
        {
            var array = (value ?? String.Empty).Split(new[] { ';', ',', '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (toLower)
            {
                return array.Select(s => s.Trim().ToLower()).ToArray();
            }

            return array.Select(s => s.Trim()).ToArray();
        }

        public static string JoinString(this IEnumerable<string> stringList, string separator = "|")
        {
            if (stringList == null)
            {
                stringList = new List<string>();
            }

            var joinedString = string.Join(separator, stringList);
            return joinedString;
        }

        public static string AddOrdinal(this int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        //        public static int CountWordInHtml(this string htmlString)
        //        {
        //            var removedTags = Sitecore.StringUtil.RemoveTags(htmlString ?? string.Empty);
        //            var count = removedTags.CountWords();
        //            return count;
        //        }

        public static int CountWords(this string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }

        public static string GetSizeString(this string value)
        {
            long size;
            if (!long.TryParse(value, out size))
            {
                return string.Empty;
            }

            string str = "";
            if (size >= 1024L)
            {
                size /= 1024L;
                str = " KB";
                if (size >= 1024L)
                {
                    size /= 1024L;
                    str = " MB";
                    if (size >= 1024L)
                    {
                        size /= 1024L;
                        str = " GB";
                    }
                }
            }
            else
            {
                if (size > 0)
                {
                    var kbs = Math.Round(size / 1024M, 2);
                    return kbs + " KB";
                }
            }
            return size + str;
        }

        public static string RemoveSitecoreRoot(this string value)
        {
            return value.Replace("sitecore/content/home/", "");
        }

        public static string RemoveSitecoreMediaRoot(this string value)
        {
            return value.Replace("sitecore/shell/", "");
        }

        public static string CsvEscapeExcel(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value.Trim()))
            {
                return string.Empty;
            }

            value = value.CsvRemoveEscapeExcel();
            value = value.CsvQuotes();
            value = "=" + value;
            return value;
        }

        public static string CsvRemoveEscapeExcel(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value.Trim()))
            {
                return string.Empty;
            }

            value = value.Trim();

            if (value.StartsWith("="))
            {
                value = value.Substring(1, value.Length - 1);
            }

            return value.CsvRemoveQuotes();
        }

        public static string CsvQuotes(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value.Trim()))
            {
                return string.Empty;
            }

            value = value.Trim();

            if (!value.StartsWith("\""))
            {
                value = "\"" + value;
            }

            if (!value.EndsWith("\""))
            {
                value = value + "\"";
            }

            return value;
        }

        public static string CsvRemoveQuotes(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value.Trim()))
            {
                return string.Empty;
            }

            value = value.Trim();

            while (value.StartsWith("\""))
            {
                value = value.Substring(1, value.Length - 1);
            }

            while (value.EndsWith("\""))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }

        public static string ZipString(this string text)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, CompressionLevel.Optimal, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string UnZipString(this string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return System.Text.Encoding.Unicode.GetString(buffer, 0, buffer.Length);
            }
        }

        public static string RemoveContentInParenthesis(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var exitString = value;
            var parenthesisSegmnents = value.Split('(');
            if (parenthesisSegmnents.Any())
            {
                exitString = parenthesisSegmnents[0];
            }

            return exitString;
        }

        public static string JoinToStringFlat(this string[] values)
        {
            if (values.Empty())
            {
                return string.Empty;
            }

            return string.Join(", ", values);
        }

        public static string JoinToStringFlat(this string[] values, string joinString)
        {
            if (values.Empty())
            {
                return string.Empty;
            }

            return string.Join(joinString, values);
        }

        public static string JoinToStringFlat(this IEnumerable<string> values)
        {
            return values.ToArray().JoinToStringFlat();
        }

        public static string JoinToStringFlat(this IEnumerable<string> values, string joinString)
        {
            return values.ToArray().JoinToStringFlat(joinString);
        }

        public static int ToInt(this string value, int defaultValue)
        {
            int parsed;
            if (int.TryParse(value, out parsed))
            {
                return parsed;
            }

            return defaultValue;
        }

        public static string FillEndString(this string value, int length, char fill)
        {
            var startArray = Enumerable.Repeat(fill, length).ToArray();
            for (int i = 0; i < value.Length; i++)
            {
                startArray[i] = value[i];
            }

            var str = new string(startArray);
            return str;
        }

        //        public static string Sanitize(this string entry)
        //        {
        //            string clean = entry;
        //
        //            //decode html
        //            clean = HttpUtility.HtmlDecode(clean);
        //
        //            //decode url
        //            //clean = HttpUtility.UrlDecode(clean);
        //
        //            //decode javascript
        //            clean = Regex.Replace(clean, @"\\u[0-9a-fA-F]{4}", string.Empty);
        //
        //            //remove html tags
        //            clean = StringUtil.RemoveTags(clean);
        //
        //            return clean;
        //        }

        public static string SafeTrim(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
        }

        public static List<string> ExtractFromString(this string text, string start, string end)
        {
            var Matched = new List<string>();
            bool exit = false;
            while (!exit)
            {
                int index_start = text.IndexOf(start);
                int index_end = text.IndexOf(end);
                if (index_start != -1 && index_end != -1)
                {
                    Matched.Add(text.Substring(index_start + start.Length, index_end - index_start - start.Length));
                    text = text.Substring(index_end + end.Length);
                }
                else
                    exit = true;
            }
            return Matched;
        }

        public static string Clip(this string text, int length, bool ellipsis)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return String.Empty;
            }
            text = text.Replace("&nbsp;", " ").Replace("  ", " ");
            if (text.Length > length)
            {
                if (ellipsis)
                    length -= 3;
                int length1 = text.LastIndexOf(" ", length, StringComparison.InvariantCulture);
                if (length1 < 0)
                    length1 = length;
                text = text.Substring(0, length1);
                if (ellipsis)
                    text += "...";
            }
            return text;
        }

        public static string SingleLineAddress(this string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return string.Empty;
            }

            var lines = address.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            return string.Join(", ", lines);
        }

        public static string MultiLineAddress(this string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return string.Empty;
            }

            var lines = address.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            return string.Join(", <br />", lines);
        }

        public static string ToIndexDate(this string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;

            DateTime dateTime;
            if (!DateTime.TryParse(date, out dateTime))
                return null;

            return dateTime.ToString("yyyyMMdd");
        }
    }
}