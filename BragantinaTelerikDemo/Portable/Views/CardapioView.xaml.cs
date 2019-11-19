using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BragantinaTelerikDemo.Portable.ViewModels;
using BragantinaTelerikDemo.Portable.Models;


namespace BragantinaTelerikDemo.Portable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardapioView : ContentPage
    {
        public CardapioViewModel ViewModel { get; set; }

        public CardapioView()
        {
            InitializeComponent();
            this.ViewModel = new CardapioViewModel();
            this.BindingContext = this.ViewModel;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Produto>(this, "ProdutoSelecionado",
                (msg) =>
                {
                    Navigation.PushAsync(new DetalheView(msg));
                });
            await this.ViewModel.GetProdutos();

            var dataView = this.listView.GetDataView();
            dataView.CollapseAll();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Produto>(this, "ProdutoSelecionado");
        }
    }
}