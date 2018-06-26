using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Models
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public long Timestamp { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}