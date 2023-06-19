using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtlantidaMDAApi.Models
{
    public class LoginRes
    {
        public User user { get; set; }
        public string token { get; set; }
        public string error { get; set; }
    }
}