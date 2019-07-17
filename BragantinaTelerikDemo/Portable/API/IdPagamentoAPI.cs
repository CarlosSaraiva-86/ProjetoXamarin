using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.API
{
    public class IdPagamentoAPI
    {
        Conexao con = new Conexao();

        public Pgto Consultar()
        {
            var client = new RestClient(con.uri + "idpagamento");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.15.2");
            IRestResponse response = client.Execute(request);
            var id = JsonConvert.DeserializeObject<Pgto>(response.Content);
            return id;
        }

        public class Pgto
        {
            public string idSeller { get; set; }
            public string idClient { get; set; }
            public string ClientSecret { get; set; }
        }
    }
}
