using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

namespace MyShows.Api
{
    internal static class Helper
    {
        
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static string GetValueByName(this IList<Parameter> list, string name)
        {
            Parameter p = list.FirstOrDefault(parameter => parameter.Name == name);
            return p != null ? p.Value.ToString() : null;
        }

        public static string GetValueByName(this IList<RestResponseCookie> list, string name)
        {
            RestResponseCookie p = list.FirstOrDefault(parameter => parameter.Name == name);
            return p != null ? p.Value : null;
        }

        public static IList<T> ConvertToList<T>(IDictionary<object, T> dic)
        {
            return dic.Select(item => item.Value).ToList();
        }

        public static string ToCommaString(int[] episodes)
        {
            return string.Join(",", Array.ConvertAll(episodes, Convert.ToString));
        }

        #region Newtonsoft.Json/Utilities

        public static bool IsNullable(Type t)
        {
            ArgumentNotNull(t, "t");

            if (t.IsValueType)
                return IsNullableType(t);

            return true;
        }

        public static void ArgumentNotNull(object value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static bool IsNullableType(Type t)
        {
            ArgumentNotNull(t, "t");

            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            ArgumentNotNull(format, "format");

            return string.Format(provider, format, args);
        }


        #endregion

        public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression) where T : Attribute
        {
            T attribute = enumeration.GetType().GetMember(enumeration.ToString(CultureInfo.InvariantCulture))[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
            
            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }
    }
}
