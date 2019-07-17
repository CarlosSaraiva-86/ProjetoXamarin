using BragantinaTelerikDemo.Portable.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class IdPagamento
    {
        IdPagamentoAPI api = new IdPagamentoAPI();
        public string idSeller { get; set; }
        public string idClient { get; set; }
        public string ClientSecret { get; set; }

        public IdPagamento()
        {
            var id = api.Consultar();
            idSeller = id.idSeller;
            idClient = id.idClient;
            ClientSecret=id.ClientSecret;
        }
    }
}
