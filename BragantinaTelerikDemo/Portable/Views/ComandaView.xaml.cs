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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (vm.StatusComanda == "")
            {
                Navigation.PushAsync(new QRcodeView("Apresente o código no caixa"));
                vm.StatusComanda = "Aberta";
                vm.TextoBotao = "PAGAMENTO";
                vm.NumeroComanda = "Comanda: 16783";
            }
            else if (vm.StatusComanda == "Aberta")
            {
                vm.StatusComanda = "Pago";
                vm.TextoBotao = "CHECKOUT";
                vm.NumeroComanda = "";
                DisplayAlert("Pagamento", "", "ok");
            }
            else
            {
                Navigation.PushAsync(new QRcodeView("Apresente código na saída"));
                vm.TextoBotao = "ABRIR COMANDA";
                vm.StatusComanda = "";
                vm.NumeroComanda = "Abra a comanda no caixa";
            }
        }
    }
}