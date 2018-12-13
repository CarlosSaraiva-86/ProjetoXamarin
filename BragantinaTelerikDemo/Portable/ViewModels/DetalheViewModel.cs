using BragantinaTelerikDemo.Portable.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Produto Produto { get; set; }

        public DetalheViewModel(Produto produto)
        {
            this.Produto = produto;
        }
    }
}
