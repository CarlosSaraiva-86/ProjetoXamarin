using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
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

        public async void CadastrarUsuario(UsuarioNuvem user)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("usuario", content);

            if (resposta.IsSuccessStatusCode)
                MessagingCenter.Send<UsuarioNuvem>(user, "SucessoCadastro");
            else
                MessagingCenter.Send<ArgumentException>(new ArgumentException(), "FalhaCadastro");
        }
    }
}
