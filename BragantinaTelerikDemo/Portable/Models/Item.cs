using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Item
    {
        public int Id { get; set; }
        //public int IdUser { get; set; }
        public Produto Produto { get; set; }
        public int IdPedido { get; set; }
        //public string Descricao { get { return Descricao.PadRight(20,' '); } set { }; }
        private string descricao;

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public int Qtde { get; set; }
        public double ValorTotal { get; set; }
        public int Status { get; set; }
        public string StatusFormatado { get; set; }
        public string ValorTotalFormatado { get { return "R$ " + ValorTotal.ToString("N2"); } }
        public string QtdeFormatada { get { return "(" + Qtde + ")"; } }
        public Color Cor { get; set;}
        public int Cozinha { get; set; }

        /*
         * STATUS DO ITEM
         * 10 - PENDENTE
         * 20 - PREPARANDO
         * 30 - ENTREGUE
        */
        public Item()
        {

        }

        public string doubleParaString(double valor)
        {
            return valor.ToString().Replace(".", "").Replace(",", ".");
        }
    }
}
