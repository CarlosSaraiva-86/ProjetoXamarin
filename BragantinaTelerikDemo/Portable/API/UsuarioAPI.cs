using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.API
{
    public class UsuarioAPI
    {
        Conexao con = new Conexao();

        public async Task<HttpResponseMessage> CadastrarUsuario(UsuarioApi user)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("usuario", content);

            return resposta;
        }
    }
}
