using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace UsefulExtensions
{
    public static class StringExtension
    {
        public static string FirstUpperCase(this string input)
        {
            if (input != null)
            {
                string ret = string.Empty;
                for (int i = 0; i < input.Length; i++)
                {
                    if (i == 0)
                    {
                        ret += input[i].ToString().ToUpper();
                    }
                    else
                    {
                        ret += input[i].ToString().ToLower();
                    }
                }
                return ret;
            }
            return "Input parameter is not valid!";
        }

        public static string[] StringToArray(this string input)
        {
            if (input == null)
                return new string[] { };

            string[] arr = new string[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                arr[i] = input[i].ToString();
            }

            return arr;
        }

        public static List<string> ArrayToListString(this string[] input)
        {
            List<string> vs = new List<string>();
            if (input == null)
                return vs;

            foreach (var item in input)
            {
                vs.Add(item);
            }
            return vs;
        }

        public static bool IsNull(this string input)
        {
            if (input == null)
                return true;
            return string.IsNullOrWhiteSpace(input.Trim());
        }

        public static bool CheckTurkishChars(this string input)
        {
            var turkishChars = new char[] { 'ğ', 'Ğ', 'ş', 'Ş', 'i', 'İ', 'ü', 'Ü', 'ö', 'Ö', 'ç', 'Ç' };
            foreach (var item in turkishChars)
            {
                if (input.Contains(item))
                    return true;
            }
            return false;
        }

        public static string ReplaceTurkishChars(this string input)
        {
            input = input.Replace("ğ", "g")
                         .Replace("Ğ", "G")
                         .Replace("ş", "s")
                         .Replace("Ş", "S")
                         .Replace("İ", "i")
                         .Replace("İ", "I")
                         .Replace("ü", "u")
                         .Replace("Ü", "U")
                         .Replace("ö", "o")
                         .Replace("Ö", "O")
                         .Replace("ç", "c")
                         .Replace("Ç", "C");

            return input;
        }

        public static int WordCounter(this string input)
        {
            string[] str = input.Split(new char[] { ' ', '.', '?', ',', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
            return str.Length;
        }

        public static int CharCounter(this string input)
        {
            string[] stA = input.Split(' ');
            int ct = 0;
            foreach (string item in stA)
            {
                ct += item.Length;
            }
            return ct;
        }
        public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input?.Trim());

        public static bool IsNotNullOrEmpty(this string input) => !string.IsNullOrEmpty(input);

        public static bool IsNumeric(this string input)
        {
            if (input.IsNull()) return false;
            return decimal.TryParse(input, out _);
        }

        public static string ToLowerCase(this string input, CultureInfo cI)
        {
            return cI.TextInfo.ToTitleCase(input.ToLower());
        }

        public static bool IsNullEmptyOrWhiteSpace(this string input)
        {
            if (input.IsNull()) return false;
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsJson(this string input)
        {
            if (input.IsNullEmptyOrWhiteSpace()) return false;

            input = input.Trim();

            return (input.StartsWith("{") && input.EndsWith("}") || input.StartsWith("[") && input.EndsWith("]")) && IsCorrectFormJToken(input);
        }

        public static bool IsCorrectFormJToken(this string input)
        {
            try
            {
                JToken.Parse(input);
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static void ThrowIfNullEmptyOrWhiteSpace(this string input)
        {
            if (input.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException(nameof(input));
        }

        public static T XmlDeserialize<T>(this string input) where T : class
        {
            if (input.IsNull()) return null;

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var strReader = new StringReader(input);

                using (var reader = XmlReader.Create(strReader))
                {
                    var retVal = xmlSerializer.Deserialize(reader) as T;

                    if (retVal.IsNull())
                        throw new InvalidCastException("Invalid model casting!");
                    else
                        return retVal;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static T XmlDeserializerFile<T>(this string input) where T : class
        {
            if (File.Exists(input) == false)
                throw new FileNotFoundException("File '{0}' was not found".FormatString(input), input);

            var xmlStr = File.ReadAllText(input);
            return xmlStr.XmlDeserialize<T>();
        }

        public static string FormatString(this string input, params object[] objs)
        {
            var strList = objs.Select(a => a.ToString());
            return string.Format(input, strList.ToArray());
        }
    }
}
