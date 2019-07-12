using BragantinaTelerikDemo.Portable.API;
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
    public partial class PagamentoView : ContentPage
    {
        public PagamentoView(Pedido ped)
        {
            InitializeComponent();
            PagamentoViewModel vm = new PagamentoViewModel(ped);
            this.BindingContext = vm;
        }
    }
}