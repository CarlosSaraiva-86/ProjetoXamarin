using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Comanda
    {
        public int Cod { get; set; }

        public bool Aberto { get; set; }

        public double ValorTotal { get; set; }

        public string QrCode { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime HoraAbertura { get; set; }

        public DateTime DataFechamento { get; set; }

        public DateTime HoraFechamento { get; set; }

    }
}
