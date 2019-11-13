using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.API
{
    public class UsuarioAPI
    {
        Conexao con = new Conexao();

        public void CadastrarUsuario(Usuario user)
        {
            var json = JsonConvert.SerializeObject(user);
            var client = new RestClient(con.uri + "usuario");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //user = JsonConvert.DeserializeObject<UsuarioNuvem>(response.Content);
            if (response.IsSuccessful)
                MessagingCenter.Send<Usuario>(user, "SucessoCadastro");
            else
                MessagingCenter.Send<ArgumentException>(new ArgumentException(), "FalhaCadastro");
        }

        public UsuarioFB Consultar(string token)
        {
            var client = new RestClient(con.uri + "usuario/" + token + "/token");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            IRestResponse response = client.Execute(request);
            var user = JsonConvert.DeserializeObject<UsuarioFB>(response.Content);
            return user;
        }

        public Usuario Consultar(int id)
        {
            var client = new RestClient(con.uri + "usuario/" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            IRestResponse response = client.Execute(request);
            var user = JsonConvert.DeserializeObject<Usuario>(response.Content);
            return user;
        }

        public class UsuarioFB
        {
            public int Id { get; internal set; }
            public string Nome { get; set; }
            public string Telefone { get; set; }
            public bool Facebook { get; set; }
            public double Meta { get; set; }
            public double Consumo { get; set; }
            public string Email { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
            public string Cpf { get; internal set; }
            public byte[] ImgByte { get; set; }
            public string ImgPerfil { get; set; }
            public string IdToken { get; set; }
            public LoginFB Login { get; set; }
        }

        public class LoginFB
        {
            public int Id { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }
        }
    }
}
