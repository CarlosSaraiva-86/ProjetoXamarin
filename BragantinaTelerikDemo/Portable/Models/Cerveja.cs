using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Cerveja : Produto
    {
        public string Alcool { get; set; }
        public string Tonalidade { get; set; }
        public string Amargor { get; set; }

        public Cerveja(string image, string title, string group) : base(image, title, group)
        {
            this.Alcool = "http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/icone-breja_03.jpg";
            this.Tonalidade = "http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/icone-breja_05.jpg";
            this.Amargor = "http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/icone-breja_07.png";
        }
    }
}
