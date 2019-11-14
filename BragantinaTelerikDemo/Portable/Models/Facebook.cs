using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    class Facebook
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public picture picture { get; set; }
    }

    class picture
    {
        public string url { get; set; }
    }
}
