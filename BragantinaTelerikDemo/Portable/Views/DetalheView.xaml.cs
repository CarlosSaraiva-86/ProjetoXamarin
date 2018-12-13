using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BragantinaTelerikDemo.Portable.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
	{
        public Cardapio Cardapio { get; set; }

        public DetalheView(Cardapio cardapio)
        {
            InitializeComponent();
            this.Cardapio = cardapio;
            this.BindingContext = new DetalheViewModel(cardapio);
        }
	}
}