using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class RestSharpHelper
    {

        /// <summary>
        /// RestSharp Post with BasicAuthentication
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string PostHttpWithBasicAuthentication(string url, Dictionary<string, string> parameters, out string cookies)
        {
            try
            {
                string userName = "";
                string password = "";
                var client = new RestClient(url)
                {
                    BaseUrl = new Uri(url),
                    Authenticator = new HttpBasicAuthenticator(userName, password)
                };

                var request = new RestRequest("", Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", parameters, ParameterType.RequestBody);

                if (parameters != null)
                {
                    foreach (string key in parameters.Keys)
                    {
                        request.AddCookie("JSESSIONID", parameters[key]);
                    }
                }

                var response = client.Execute(request);

                CookieContainer _cookieJar = new CookieContainer();
                client.CookieContainer = _cookieJar;

                var sessionCookie = response.Cookies.SingleOrDefault(x => x.Name == "JSESSIONID");
                if (sessionCookie != null)
                {
                    cookies = sessionCookie.Value;
                    //_cookieJar.Add(new Cookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path, sessionCookie.Domain));
                }
                else
                {
                    cookies = null;
                }

                var rtn = (response.StatusCode == System.Net.HttpStatusCode.OK) ? response.Content : "{\"Result\":\"False\"}";
                return rtn;

            }
            catch (Exception ex)
            {
                var a = ex;
                cookies = "";
                //TODO: Log
                return "{\"Result\":\"False\"}";
            }

        }

        /// <summary>
        /// Post with cookies
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string PostHttpWithCookie(string url, Dictionary<string, string> parameters)
        {
            try
            {
                var client = new RestClient(url)
                {
                    BaseUrl = new Uri(url),
                };

                var request = new RestRequest("", Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //request.AddParameter("application/x-www-form-urlencoded", parameters, ParameterType.RequestBody);

                if (parameters != null)
                {
                    foreach (string key in parameters.Keys)
                    {
                        request.AddCookie("JSESSIONID", parameters[key]);
                    }
                }
                else
                {
                    return "{\"Result\":\"Cookie/Session expires \"}";
                }

                var response = client.Execute(request);

                var rtn = (response.StatusCode == System.Net.HttpStatusCode.OK) ? response.Content : "{\"Result\":\"False\"}";
                return rtn;

            }
            catch (Exception ex)
            {
                var a = ex;
                return "{\"Result\":\"False\"}";
            }

        }
    }
}
