using System.Collections.Generic;
using System.Threading.Tasks;
using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.Views;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using static BragantinaTelerikDemo.Portable.API.UsuarioAPI;

namespace BragantinaTelerikDemo.Portable
{
    public partial class App : Application
    {        
        public App()
        {
            InitializeComponent();

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

            MessagingCenter.Subscribe<string>(this, "SucessoLoginFB",
               (msg) =>
               {
                   MainPage = new NavigationPage(new LoginView());
                   //MessagingCenter.Send<Usuario>(usuario,"UsuarioFB");
               });
            MessagingCenter.Subscribe<Usuario>(this, "CadastrarUsuarioFB",
                (msg)=> 
                {
                    MainPage = new CadastroUsuarioView(msg);
                });

            MessagingCenter.Subscribe<string>(this, "LoginFacebook",
               (usuario) =>
               {
                   MainPage = new NavigationPage(new LoginFbView());
               });

            MessagingCenter.Subscribe<Usuario>(this, "CadastrarUsuario",
               (msg) =>
               {
                   MainPage = new CadastroUsuarioView(msg);
               });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoCadastro", (msg) =>
            {
                SalvarUsuario(msg);
                MainPage = new NavigationPage(new LoginView());
            });

            MessagingCenter.Subscribe<string>(this, "LogoutApp", (msg) =>
            {
                DeletarUsuario();
                MainPage = new NavigationPage(new LoginView());
            });
        }

        private void DeletarUsuario()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                dao.Deletar();
            }
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
            LoginAPI api = new LoginAPI();
            Usuario usuario = new Usuario { Nome = infoLoginFb[1], ImgPerfil = infoLoginFb[2],
                FacebookId = infoLoginFb[0], Email = infoLoginFb[3] ,Facebook = true };

            var resposta = await api.FazerLogin(usuario);

            if (resposta.IsSuccessStatusCode)
            {
                var resultado = await resposta.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Usuario>(resultado);
                if (user != null)
                {
                    SalvarUsuario(user);
                    MessagingCenter.Send("", "SucessoLoginFB");
                }    
            }
            else
            {
                MessagingCenter.Send<Usuario>(usuario, "CadastrarUsuarioFB");
            }
        }

        public static void SalvarUsuario(Usuario user)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                dao.Deletar();
                dao.Salvar(user);
            }
        }

        //public static void SalvarUsuario(Usuario usuario)
        //{
        //    using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
        //    {
        //        UsuarioDAO dao = new UsuarioDAO(conexao);
        //        dao.Deletar();
        //        dao.Salvar(usuario);
        //    }
        //}
    }
}
