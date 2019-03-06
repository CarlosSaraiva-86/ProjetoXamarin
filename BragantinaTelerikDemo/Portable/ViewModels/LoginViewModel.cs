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
        LoginAPI api = new LoginAPI();
        public ICommand EntrarCommand { get; private set; }
        public ICommand CadastrarCommand { get; private set; }

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
