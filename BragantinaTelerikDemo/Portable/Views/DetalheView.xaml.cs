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
        public Produto Produto { get; set; }

        public DetalheView(Produto produto)
        {
            InitializeComponent();
            this.Produto = produto;
            this.BindingContext = new DetalheViewModel(produto);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<string>("","SucessoEnvioItem",
                (msg)=> 
                    {
                        DisplayAlert("Atenção", "Item Enviado com Sucesso!","OK");
                        Navigation.PopAsync();
                    }
                );
            MessagingCenter.Subscribe<string>("", "FalhaEnvioItem",
                (msg) =>
                {
                    DisplayAlert("Atenção", "Falha ao enviar o item!\nVerifique sua conexão", "OK");
                }
                );
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>("","SucessoEnvioItem");
            MessagingCenter.Unsubscribe<string>("","FalhaEnvioItem");
        }
    }
}