using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Client
    {
        public Client()
        {
            scope = "oob";
            grant_type = "client_credentials";

        }
        public string scope { get; set; }
        public string grant_type { get; set; }
    }
}
