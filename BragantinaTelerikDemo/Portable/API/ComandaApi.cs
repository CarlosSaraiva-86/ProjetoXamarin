using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.API
{
    public class ComandaApi
    {
        Conexao con = new Conexao();

        public async Task<HttpResponseMessage> ConsultarComanda(int codComanda)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var resposta = await httpClient.GetAsync("pedido/" + codComanda);
            return resposta;
        }

        //public async Task<HttpResponseMessage> ConsultarComandaAberta(int codComanda)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri(con.uri);
        //    var resposta = await httpClient.GetAsync("pedido/" + codComanda + "/aberto");
        //    return resposta;
        //}

        public bool AlterarStatus(int idPedido, int status)
        {
            var client = new RestClient(con.uri + "pedido/" + idPedido + "/status/" + status);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.15.2");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
                return true;
            return false;
        }

        public async Task<HttpResponseMessage> Update(Pedido pedido)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var json = JsonConvert.SerializeObject(pedido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await httpClient.PostAsync("pedido/update", content);
            return resposta;
        }
    }
    
}
