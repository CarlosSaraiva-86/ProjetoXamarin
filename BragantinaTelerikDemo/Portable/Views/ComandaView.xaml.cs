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
	public partial class ComandaView : ContentPage
	{
        
        ComandaViewModel vm = new ComandaViewModel();
        public ComandaView ()
		{
			InitializeComponent ();
            this.BindingContext = vm;
		}

       
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("Pagamento com Cartão", "Integração Getnet", "OK");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var QRcode = new QRcodeView();



            MessagingCenter.Subscribe<QRCodePedido>(this, "QRCodeAberta", (msg) =>
            {
                QRcode.setMensagem(msg);
                Navigation.PushModalAsync(QRcode);
            });

            MessagingCenter.Subscribe<QRCodePedido>(this, "QRCodeFechada", (msg) =>
            {
                QRcode.setMensagem(msg);
                Navigation.PushModalAsync(QRcode);
            });

            MessagingCenter.Subscribe<QRCodePedido>(this, "QRCodePedido", (msg) =>
            {
                QRcode.setMensagem(msg);
                Navigation.PushModalAsync(QRcode);
            });

            MessagingCenter.Subscribe<Pedido>(this, "AbrirPagamento", (msg) =>
            {
                Navigation.PushAsync(new PagamentoView(msg));
            });

            MessagingCenter.Subscribe<string>(this, "ItensPendentes", (msg) =>
            {
                Navigation.PushModalAsync(new RetiradaItensPendentesView(msg));
            });
            await this.vm.ConsultaDadosComanda();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "QRCodeAberta");
            MessagingCenter.Unsubscribe<string>(this, "QRCodeFechada");
            MessagingCenter.Unsubscribe<string>(this, "QRCodePedido");
            MessagingCenter.Unsubscribe<Pedido>(this, "AbrirPagamento");
            MessagingCenter.Unsubscribe<string>(this, "ItensPendentes");
        }
    }
}