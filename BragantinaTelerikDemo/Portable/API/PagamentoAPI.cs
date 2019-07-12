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
    public class PagamentoAPI
    {
        string client_id = "a20a063a-f177-4d2f-8d13-3540135fef8f";
        string client_secret = "a61e2296-b92f-404e-a372-82562f36e3d5";
        Conexao con = new Conexao();

        public Auth Autenticar()
        {
            string autorization = Base64Encode(client_id + ":" + client_secret);

            autorization = "Basic " + autorization;

            var client = new RestClient(con.uriGetNet + "auth/oauth/v2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "39");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", autorization);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "scope=oob&grant_type=client_credentials", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var token = JsonConvert.DeserializeObject<Auth>(response.Content);

            return token;
        }

        public Card Tokenizar(string auth, Card cartao)
        {
            cartao.card_number = cartao.card_number.Replace(" ", "");
            var client = new RestClient(con.uriGetNet + "v1/tokens/card");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "38");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"card_number\": \"" + cartao.card_number + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var json = JsonConvert.DeserializeObject<Card>(response.Content);
            cartao.number_token = json.number_token;
            return cartao;
        }

        public bool Verificar(string auth, Card cartao)
        {
            var client = new RestClient(con.uriGetNet + "v1/cards/verification");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "38");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(cartao);
            json = json.Replace("\"brand\":null,", "").Replace("\"card_number\":null,", "");
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Verify>(response.Content);

            return true;
        }

        public CardRec Recuperar(string auth, Cartao cartao)
        {
            var client = new RestClient(con.uriGetNet + "/v1/cards/" + cartao.card_id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var card = JsonConvert.DeserializeObject<CardRec>(response.Content);

            return card;
        }

        private string Base64Encode(string v)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(v);
            return Convert.ToBase64String(plainTextBytes);
        }

        public Payment Pagamento(string auth, Pagamento pagamento)
        {
            var json = JsonConvert.SerializeObject(pagamento);
            json = json.Replace("\"card_number\":null,", "").Replace("\"brand\":null,", "").Replace("null", "{}");
            var client = new RestClient(con.uriGetNet + "/v1/payments/credit");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "38");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api-sandbox.getnet.com.br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");            
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Payment>(response.Content);

            return result;
        }

        public Cartao Armazenar(string auth, CardArm card)
        {
            var json = JsonConvert.SerializeObject(card);
            var client = new RestClient(con.uriGetNet + "/v1/cards");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "38");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api-sandbox.getnet.com.br");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Authorization", auth);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Cartao>(response.Content);

            return result;
        }

        public class Verify
        {
            public string status { get; set; }
            public string verification_id { get; set; }
            public string authorization_code { get; set; }
        }

        public class Payment
        {
            public string payment_id { get; set; }
            public string seller_id { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string order_id { get; set; }
            public string status { get; set; }
            public string received_at { get; set; }
            public Credito credit { get; set; }
        }

        public class Credito
        {
            public bool delayed { get; set; }
            public string authorization_code { get; set; }
            public string authorized_at { get; set; }
            public string reason_code { get; set; }
            public string reason_message { get; set; }
            public string acquirer { get; set; }
            public string soft_descriptor { get; set; }
            public string terminal_nsu { get; set; }
            public string acquirer_transaction_id { get; set; }
            public string transaction_id { get; set; }
        }
    }
}
