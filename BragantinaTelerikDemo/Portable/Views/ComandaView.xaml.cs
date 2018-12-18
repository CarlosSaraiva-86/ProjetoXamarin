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
		public ComandaView ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QRcodeView());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("Pagamento com Cartão", "Integração Getnet", "OK");
        }
    }
}