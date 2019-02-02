using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    class ItensComanda
    {
        public int CodItem {get; set;}

        public Produto Produto {get; set;}

        public Comanda Comanda {get; set;}

        public String Descricao {get; set;}

        public int Qtde {get; set;}

        public double ValorTotal {get; set;}

        //public enum Status {get; set;}

    }
}
