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
using static BragantinaTelerikDemo.Portable.API.PagamentoAPI;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>(this, "PagamentoAprovado", (mgs)=>
            {
                DisplayAlert("Atenção", "Pagamento efetuado com Sucesso!", "OK");
                Navigation.PopAsync();
            });
            MessagingCenter.Subscribe<string>(this, "InformacoesFaltantes", (msg) =>
            {
                DisplayAlert("Atenção", "Digite o código de segurança para efetuar o pagamento!", "OK");
            });
            MessagingCenter.Subscribe<string>(this, "PagamentoRecusado", (mgs) =>
            {
                DisplayAlert("Atenção", "Pagamento recusado tente novamente!", "OK");
                Navigation.PopAsync();
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>("", "PagamentoAprovado");
            MessagingCenter.Unsubscribe<string>("", "PagamentoRecusado");
            MessagingCenter.Unsubscribe<string>("", "InformacoesFaltantes");
        }
    }
}