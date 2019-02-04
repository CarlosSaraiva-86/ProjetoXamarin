using BragantinaTelerikDemo.Portable.API;
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
        public ICommand EntrarCommand { get; private set; }
        public ICommand CadastrarCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(
                async () =>
                {
                    var login = new Usuario() { Email = usuario };
                    MessagingCenter.Send<Usuario>(login, "SucessoLogin");
                    //var loginService = new LoginAPI();
                    //HttpResponseMessage resultado = null;
                    //try
                    //{
                    //    resultado = await loginService.FazerLogin(new Login(usuario, senha));
                    //}
                    //catch (Exception exc)
                    //{
                    //    MessagingCenter.Send<LoginException>(new
                    //        LoginException("Erro de comunicação com o servidor.", exc), "FalhaLogin");
                    //}
                    //if (resultado.IsSuccessStatusCode)
                    //{
                    //    string resultContent = resultado.Content.ReadAsStringAsync().Result;
                    //    LoginResult resultadoLogin =
                    //        JsonConvert.DeserializeObject<LoginResult>(resultContent);

                    //    MessagingCenter.Send<Usuario>(resultadoLogin.usuario, "SucessoLogin");
                    //}
                    //else
                    //    MessagingCenter.Send<LoginException>(new LoginException(), "FalhaLogin");

                },
            () =>
            {
                return !string.IsNullOrEmpty(usuario)
                    && !string.IsNullOrEmpty(senha);
            });

            CadastrarCommand = new Command(
                async () =>
                {
                    MessagingCenter.Send<Usuario>(new Usuario(), "CadastrarUsuario");
                });
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
