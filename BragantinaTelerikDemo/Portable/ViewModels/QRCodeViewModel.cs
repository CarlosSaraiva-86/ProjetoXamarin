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

        public QRCodeViewModel(string titulo)
        {
            _titulo = titulo;
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                CodComanda = dao.UsuarioLogado.Id;
            }
            buscarComandaAberta();
        }

        bool comandaAberta = false;
        private void buscarComandaAberta()
        {
            Comanda comanda = new Comanda();
            ComandaApi comandaApi = new ComandaApi();

            //Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            //{

                ConsultaComandaAberta();
                //if (comandaAberta)
                //    return false;
                //return true;
                //if (_vezesTimer == 4)
                //{
                //    _vezesTimer = 0;
                //    Button.IsEnabled = true;
                //    Entry.Text = string.Empty;
                //    return false;
                //}

                //Entry.Text = $"Timer foi executado {++_vezesTimer} vezes";
                //Button.IsEnabled = false;
                //return true;
            //});
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
