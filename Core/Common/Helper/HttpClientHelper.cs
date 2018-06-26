using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class HttpClientHelper
    {
        public HttpClientHelper()
        {

        }

        //private static readonly HttpClient client = new HttpClient();

        public static async Task<T> PostHttp<T>(string url, object data)
        {
            using (var client = new HttpClient())
            {
                string str = data.ToJson();
                var content = new StringContent(str)
                {
                    //Headers = { ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded") }
                    Headers = { ContentType = new MediaTypeHeaderValue("Application/json") }
                };
                HttpResponseMessage msg = client.PostAsync(url, content).GetAwaiter().GetResult();

                if (msg.IsSuccessStatusCode)
                {
                    return await msg.Content.ReadAsAsync<T>();
                }

                return default(T);
            }
        }



    }
}
