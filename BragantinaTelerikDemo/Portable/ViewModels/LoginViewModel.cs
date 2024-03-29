﻿using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public bool Busy { get; set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(
                async () =>
                {
                    //var login = new Usuario() { Email = usuario }
                    var login = new Usuario() { User = usuario, Senha = senha };

                    var resposta = await api.FazerLogin(login);

                    if (resposta.IsSuccessStatusCode)
                    {
                        var resultado = await resposta.Content.ReadAsStringAsync();
                        var usuarioJson = JsonConvert.DeserializeObject<Usuario>(resultado);
                        //Usuario usuario = new Usuario { Id = usuarioJson.login.Id, Cidade = usuarioJson.Cidade, Consumo = usuarioJson.Consumo,
                        //    Cpf = usuarioJson.Cpf, Email = usuarioJson.Email, Facebook = usuarioJson.Facebook,
                        //    ImgPerfil = usuarioJson.ImgPerfil, Nome = usuarioJson.Nome, Telefone = usuarioJson.Telefone, UF = usuarioJson.UF};
                        SalvarUsuario(usuarioJson);
                        MessagingCenter.Send<Usuario>(login, "SucessoLogin");                        
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
                MessagingCenter.Send<Usuario>(new Usuario(), "CadastrarUsuario");
            });

            LoginAutomatico();
        }

       
        private async void LoginAutomatico()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (status == PermissionStatus.Granted)
            {
                var loginAuto = new Usuario();

                using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
                {
                    var dao = new UsuarioDAO(conexao);
                    loginAuto = dao.UsuarioLogado;
                }
                //METODO BUSCAR USUARIO SQLITE

                if (!string.IsNullOrEmpty(loginAuto.User))
                {
                    var resposta = await api.FazerLogin(loginAuto);

                    if (resposta.IsSuccessStatusCode)
                    {
                        var resultado = await resposta.Content.ReadAsStringAsync();
                        var login = JsonConvert.DeserializeObject<Usuario>(resultado);
                        MessagingCenter.Send<Usuario>(login, "SucessoLogin");
                        //MessagingCenter.Send<Login>(new Login("Autenticado Via API", "Autenticado Via API"), "SucessoLogin");
                    }
                    else
                        MessagingCenter.Send<LoginException>(new LoginException(), "FalhaLogin");
                }
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
                dao.Deletar();
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
