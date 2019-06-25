using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.API
{
    public class LoginAPI
    {
        Conexao con = new Conexao();

        public async Task<HttpResponseMessage> FazerLogin(Login login)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("login", content);
            return resposta;
        }

        public async Task<HttpResponseMessage> FazerLogin(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            //var json = JsonConvert.SerializeObject(tok);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("login/" + token, content);
            return resposta;
        }
    }
}
