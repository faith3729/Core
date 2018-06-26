using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);

            }
            catch (Exception)
            {
                return default(T);
            }
        }

    }
}
