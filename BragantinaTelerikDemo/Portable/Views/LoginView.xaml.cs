using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                if (result.ContainsKey(Permission.Storage))
                    status = result[Permission.Storage];
            }

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