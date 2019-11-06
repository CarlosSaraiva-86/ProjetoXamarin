using System.Collections.Generic;
using System.Threading.Tasks;
using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.Views;
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
            MessagingCenter.Subscribe<Login>(this, "SucessoLogin",
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
            MessagingCenter.Subscribe<UsuarioNuvem>(this, "CadastrarUsuarioFB",
                (msg)=> 
                {
                    MainPage = new CadastroUsuarioView(msg);
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

            MessagingCenter.Subscribe<UsuarioNuvem>(this, "SucessoCadastro", (msg) =>
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
            UsuarioAPI api = new UsuarioAPI();
            UsuarioNuvem usuario = new UsuarioNuvem { Nome = infoLoginFb[1], ImgPerfil = infoLoginFb[2],
                IdToken = infoLoginFb[0], Facebook = true };

            var user = api.Consultar(usuario.IdToken);

            if (user.Login != null)
            {                 
                SalvarUsuario(user);
                MessagingCenter.Send("", "SucessoLoginFB");
            }
            else
                MessagingCenter.Send<UsuarioNuvem>(usuario, "CadastrarUsuarioFB");
        }

        public static void SalvarUsuario(UsuarioFB user)
        {
            Usuario usuario = new Usuario
            {
                Cidade = user.Cidade,
                Consumo = user.Consumo,
                UF = user.UF,
                Telefone = user.Telefone,
                Nome = user.Nome,
                Cpf = user.Cpf,
                Email = user.Email,
                Facebook = user.Facebook,
                Id = user.Login.Id,
                IdToken = user.IdToken,
                ImgPerfil = user.ImgPerfil,
                Meta = user.Meta
            };
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                dao.Deletar();
                dao.Salvar(usuario);
            }
        }

        public static void SalvarUsuario(UsuarioNuvem user)
        {
            Usuario usuario = new Usuario
            {
                Cidade = user.Cidade,
                Consumo = user.Consumo,
                UF = user.UF,
                Telefone = user.Telefone,
                Nome = user.Nome,
                Cpf = user.Cpf,
                Email = user.Email,
                Facebook = user.Facebook,
                Id = user.Login.Id,
                IdToken = user.IdToken,
                ImgPerfil = user.ImgPerfil,
                Meta = user.Meta
            };
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                dao.Deletar();
                dao.Salvar(usuario);
            }
        }
    }
}
