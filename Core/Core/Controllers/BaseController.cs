using Common;
using Common.Extensions;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Core.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// UserToken
        /// </summary>
        public string UserToken
        {
            get
            {
                IEnumerable<string> key = new List<string>();
                bool res = Request.Headers.TryGetValues("userToken", out key);
                return key?.FirstOrDefault();
            }
        }


        public UserModel UserModel
        {

            get
            {
                if (UserToken == null)
                {
                    return null;
                }
                var key = AppSettings.Aeskey;
                var UserModel = UserToken.FromJson<UserModel>();
                return UserModel;
            }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName => UserModel?.UserName;

        /// <summary>
        /// Password
        /// </summary>
        public string Password => UserModel?.Password;

        /// <summary>
        /// IsLoggedIn
        /// </summary>
        public bool IsLoggedIn => UserName != null;
    }
}
