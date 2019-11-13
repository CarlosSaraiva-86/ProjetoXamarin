using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
        public List<Item> Itens { get; set; }
        /*
         * STATUS DA COMANDA
         * 10 - ABERTO
         * 20 - FECHADO
         * 30 - PAGO
        */
    }
}
