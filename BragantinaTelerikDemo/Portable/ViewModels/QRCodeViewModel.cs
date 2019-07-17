using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class QRCodeViewModel : BaseViewModel
    {
        string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
            }
        }
        public int CodComanda { get; set; }
        Pedido Comanda = new Pedido();

        public QRCodeViewModel(QRCodePedido qr)
        {
            _titulo = qr.titulo;
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                CodComanda = dao.UsuarioLogado.Id;
            }
            if (qr.status == 10)
                buscarComandaAberta();
            if (qr.status == 20)
                buscarCheckout();
        }

        private void buscarCheckout()
        {
            ConsultaComandaFechada();
        }

        bool comandaAberta = false;

        private void buscarComandaAberta()
        {
            Comanda comanda = new Comanda();
            ComandaApi comandaApi = new ComandaApi();
            ConsultaComandaAberta();
        }

        private async void ConsultaComandaFechada()
        {
            var comandaApi = new ComandaApi();
            var resposta = await comandaApi.ConsultarComandaAtiva(CodComanda);
            var resultado = await resposta.Content.ReadAsStringAsync();
            var comanda = JsonConvert.DeserializeObject<Pedido>(resultado);
            this.Comanda = comanda;
            if (resposta.IsSuccessStatusCode)
                ConsultaComandaFechada();
            else
                MessagingCenter.Send("", "SucessoFechadoComanda");
        }

        private async void ConsultaComandaAberta()
        {
            var comandaApi = new ComandaApi();
            var resposta = await comandaApi.ConsultarComandaAberta(CodComanda);
            var resultado = await resposta.Content.ReadAsStringAsync();
            var comanda = JsonConvert.DeserializeObject<Pedido>(resultado);
            this.Comanda = comanda;
            if (!resposta.IsSuccessStatusCode)
                ConsultaComandaAberta();
            else
                MessagingCenter.Send<Pedido>(this.Comanda, "SucessoAberturaComanda");
        }
    }

}
