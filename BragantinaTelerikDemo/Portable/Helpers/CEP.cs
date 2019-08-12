using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.Helpers
{
    public class CEP
    {
        public ViaCEP Consultar(string cep)
        {
            var client = new RestClient("https://viacep.com.br/ws/" + cep + "/json/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<ViaCEP>(response.Content);
                return result;
            }
            else
                return null;
        }

        public class ViaCEP
        {
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string complemento { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
            public string unidade { get; set; }
            public string ibge { get; set; }
            public string gia { get; set; }
        }
    }
}
