using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.API
{
    public class ItemAPI
    {
        Conexao con = new Conexao();

        public async Task<HttpResponseMessage> EnviarItem(Item item)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("item", content);
            return resposta;
        }

        public async Task<HttpResponseMessage> ConsultarItens(int codComanda)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var resposta = await httpClient.GetAsync("item/" + codComanda);
            return resposta;
        }
    }
}
