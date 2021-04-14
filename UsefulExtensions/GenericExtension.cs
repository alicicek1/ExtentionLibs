using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace UsefulExtensions
{
    /// <summary>
    /// Class based for object extension methods. 
    /// </summary>
    public static class GenericExtension
    {
        public static TOutput Convert<TInput, TOutput>(this TInput input)
        {
            try
            {
                var con = TypeDescriptor.GetConverter(typeof(TOutput));
                if (!con.IsNull())
                {
                    return (TOutput)con.ConvertFrom(input);
                }
                return default(TOutput);
            }
            catch (NotSupportedException)
            {
                return default(TOutput);
            }
        }

        public static bool When<T>(this T input, Predicate<T> predicate)
        {
            return predicate(input);
        }

        public static string ToJson<T>(this T input, bool encodeJsonString = false) where T : class
        {
            if (input.IsNull()) return string.Empty;
            string retVal;
            try
            {
                retVal = JsonConvert.SerializeObject(input);
                if (encodeJsonString)
                {
                    retVal = HttpUtility.JavaScriptStringEncode(retVal, true);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return retVal;
        }

        public static string ToXml<T>(this T input) where T : class
        {
            if (input.IsNull()) return string.Empty;
            string retVal;
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(input.GetType());
                using (var writer = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                    {
                        xmlSer.Serialize(xmlWriter, input);
                        retVal = writer.ToString();
                    }
                }
            }
            catch (XmlException)
            {
                return string.Empty;
            }
            return retVal;
        }

        public static bool IsNull<T>(this T input) where T : class
        {
            if (input == null)
                return true;
            return false;
        }
    }
}
