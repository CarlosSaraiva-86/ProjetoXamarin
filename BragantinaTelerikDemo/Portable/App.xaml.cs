using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.Views;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MenuView());
            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MessagingCenter.Subscribe<Usuario>(this, "SucessoLogin",
               (usuario) =>
               {
                   MainPage = new NavigationPage(new MenuView());
               });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
