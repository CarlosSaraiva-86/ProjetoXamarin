using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        LoginAPI api = new LoginAPI();
        public ICommand EntrarCommand { get; private set; }
        public ICommand CadastrarCommand { get; private set; }
        public ICommand EntrarFBCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(
                async () =>
                {
                    //var login = new Usuario() { Email = usuario }
                    var login = new Login(usuario, senha);

                    var resposta = await api.FazerLogin(login);

                    if (resposta.IsSuccessStatusCode)
                    {
                        var resultado = await resposta.Content.ReadAsStringAsync();
                        Usuario usuarioJson = JsonConvert.DeserializeObject<Usuario>(resultado);
                        SalvarUsuario(usuarioJson);
                        MessagingCenter.Send<Login>(login, "SucessoLogin");                        
                    }
                    else
                        MessagingCenter.Send<LoginException>(new LoginException(), "FalhaLogin");
                },
            () =>
            {
                return !string.IsNullOrEmpty(usuario)
                    && !string.IsNullOrEmpty(senha);
            });

            EntrarFBCommand = new Command(
                async () =>
                {
                    MessagingCenter.Send<string>("", "LoginFacebook");
                });

            CadastrarCommand = new Command(
            async () =>
            {
                MessagingCenter.Send<UsuarioNuvem>(new UsuarioNuvem(), "CadastrarUsuario");
            });


            LoginAutomatico();
        }

        private async void LoginAutomatico()
        {
           var loginAuto = new Usuario();
           
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new UsuarioDAO(conexao);
                loginAuto = dao.UsuarioLogado;
            }
            //METODO BUSCAR USUARIO SQLITE

            if (!string.IsNullOrEmpty(loginAuto.IdToken))
            {
                var resposta = await api.FazerLogin(loginAuto.IdToken);

                if (resposta.IsSuccessStatusCode)
                {
                    var resultado = await resposta.Content.ReadAsStringAsync();
                    var login = JsonConvert.DeserializeObject<UserApi>(resultado);
                    MessagingCenter.Send<Login>(new Login(login.login.Usuario, login.login.Senha), "SucessoLogin");
                    //MessagingCenter.Send<Login>(new Login("Autenticado Via API", "Autenticado Via API"), "SucessoLogin");
                }
                else
                    MessagingCenter.Send<LoginException>(new LoginException(), "FalhaLogin");
            }
        }

        class LoginAuto
        {
            public int id { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }
        }

        private static void SalvarUsuario(Usuario usuario)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                dao.Salvar(usuario);
            }
        }

        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }


    }

    public class LoginException : Exception
    {
        public LoginException() : base() { }

        public LoginException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
