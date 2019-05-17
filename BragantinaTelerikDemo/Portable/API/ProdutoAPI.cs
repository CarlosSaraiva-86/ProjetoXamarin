using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BragantinaTelerikDemo.Portable.API
{
    public class ProdutoAPI
    {
        Conexao con = new Conexao();
        
        public async Task<string> BuscarProdutos()
        {
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(con.uri + "produto");
            return resultado;
        }
    }
}


//Aguarde = true;
        
 //Aguarde = false;