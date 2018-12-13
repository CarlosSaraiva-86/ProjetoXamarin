using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Cardapio
    {
        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Group { get; private set; }

        public Cardapio(string image, string title, string group)
        {
            this.Image = image;
            this.Title = title;
            this.Group = group;
        }
    }
}
