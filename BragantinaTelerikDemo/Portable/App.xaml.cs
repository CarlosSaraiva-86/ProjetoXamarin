using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MessagingCenter.Subscribe<Login>(this, "SucessoLogin",
               (usuario) =>
               {
                   MainPage = new NavigationPage(new MenuView());
               });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoLoginFB",
               (usuario) =>
               {
                   MainPage = new NavigationPage(new MenuView());
                   MessagingCenter.Send<Usuario>(usuario,"UsuarioFB");
               });

            MessagingCenter.Subscribe<string>(this, "LoginFacebook",
               (usuario) =>
               {
                   MainPage = new NavigationPage(new LoginFbView());
               });

            MessagingCenter.Subscribe<UsuarioNuvem>(this, "CadastrarUsuario",
               (msg) =>
               {
                   MainPage = new CadastroUsuarioView(msg);
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

        public async static Task NavigateToProfile(List<string> infoLoginFb)
        {
            Usuario usuario = new Usuario { Nome = infoLoginFb[1], ImgPerfil = infoLoginFb[2]};
            MessagingCenter.Send<Usuario>(usuario, "SucessoLoginFB");
        }
    }
}
