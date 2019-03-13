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
        private readonly string uri = "http://192.168.1.104:5000/APIBragantina/";

        public async Task<HttpResponseMessage> FazerLogin(Login login)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("login", content);
            return resposta;
        }
    }
}
