using System.Collections.Generic;
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

        public static IList<T> ConvertToList<T>(Dictionary<int, T> dic)
        {
            return dic != null ? dic.Select(item => item.Value).ToList() : null;
        }
    }
}
