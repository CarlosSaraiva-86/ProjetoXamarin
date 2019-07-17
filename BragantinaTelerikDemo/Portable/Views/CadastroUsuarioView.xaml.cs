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
    public partial class CadastroUsuarioView : ContentPage
    {
        public CadastroUsuarioViewModel ViewModel { get; set; }
        public CadastroUsuarioView(UsuarioNuvem usuario)
        {
            InitializeComponent();
            this.ViewModel = new CadastroUsuarioViewModel(usuario);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaCadastro",
                async (msg) =>
                {
                    DisplayAlert("Cadastro", "Falha ao cadastrar usuario! Verifique os dados e tente novamente mais tarde!", "ok");
                });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<UsuarioNuvem>(this, "SucessoCadastro");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaCadastro");
        }
    }
}