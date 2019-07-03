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
	public partial class QRcodeView : ContentPage
	{
        public QRcodeView (string titulo)
		{
			InitializeComponent ();
            this.BindingContext = new QRCodeViewModel(titulo);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Pedido>(this, "SucessoAberturaComanda",
             async (msg) =>
             {
                 await DisplayAlert("Comanda", "Comanda aberta com sucesso!", "ok");
                 await Navigation.PopToRootAsync();
                 //this.Navigation.PopAsync();
             });

            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}