using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
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

        public async Task<HttpResponseMessage> ConsultarComandaAberta(int codComanda)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(con.uri);
            var resposta = await httpClient.GetAsync("pedido/" + codComanda);
            return resposta;
        }
    }
    
}
