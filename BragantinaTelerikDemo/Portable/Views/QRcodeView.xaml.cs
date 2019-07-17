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
        public QRcodeView (QRCodePedido qr)
		{
			InitializeComponent ();
            this.BindingContext = new QRCodeViewModel(qr);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Pedido>(this, "SucessoAberturaComanda",
             async (msg) =>
             {
                 DisplayAlert("Comanda", "Comanda aberta com sucesso!", "ok");
                 await Navigation.PopToRootAsync();
             });

            MessagingCenter.Subscribe<string>("", "SucessoFechadoComanda",
             async (msg) =>
             {
                 DisplayAlert("Comanda", "Obrigado pela preferencia!", "ok");
                 await Navigation.PopToRootAsync();
             });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>("", "SucessoAberturaComanda");
            MessagingCenter.Unsubscribe<string>("", "SucessoFechadoComanda");
        }
    }
}