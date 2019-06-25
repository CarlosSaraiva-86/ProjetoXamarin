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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                MessagingCenter.Subscribe<LoginException>(this, "FalhaLogin",
                async (exc) =>
                {
                    await DisplayAlert("Login", @"Falha ao efetuar o login. 
Verifique os dados e tente novamente mais tarde.", "Ok");
                });

            }
            catch (Exception)
            {
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<LoginException>(this, "FalhaLogin");
        }
    }
}