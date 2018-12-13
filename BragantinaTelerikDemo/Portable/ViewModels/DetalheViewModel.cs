using BragantinaTelerikDemo.Portable.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Cardapio Cardapio { get; set; }

        public DetalheViewModel(Cardapio cardapio)
        {
            this.Cardapio = cardapio;
        }
    }
}
