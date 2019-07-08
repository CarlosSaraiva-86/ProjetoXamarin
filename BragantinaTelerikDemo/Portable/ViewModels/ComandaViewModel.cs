using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using BragantinaTelerikDemo.Portable.Views;
using BragantinaTelerikDemo.Portable.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections;
using System.Collections.ObjectModel;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class ComandaViewModel : BaseViewModel
    {
        Usuario Usuario = new Usuario();
        private string statusComanda;
        private string codStatusComanda;
        private Color corStatusComanda;
        private string textoBotao;
        private string numeroComandaFormatado;
        private bool pedido;
        public int numeroComanda { get { return this.Usuario.Id; } }

        public bool BotaoPedido
        {
            get { return pedido; }
            set
            {
                pedido = value;
                OnPropertyChanged();
            }
        }

        public string StatusComanda
        {
            get { return statusComanda; }
            set
            {
                statusComanda = value;
                OnPropertyChanged();
            }
        }

        public string CodStatusComanda
        {
            get { return codStatusComanda; }
            set
            {
                codStatusComanda = value;
                OnPropertyChanged();
            }
        }

        public Color CorStatusComanda
        {
            get { return corStatusComanda; }
            set
            {
                corStatusComanda = value;
                OnPropertyChanged();
            }
        }

        public string TextoBotao
        {
            get { return textoBotao; }
            set
            {
                textoBotao = value;
                OnPropertyChanged();
            }
        }

        public string NumeroComandaFormatado
        {
            get { return numeroComandaFormatado; }
            set
            {
                numeroComandaFormatado = value;
                OnPropertyChanged();
            }
        }
        public ComandaViewModel()
        {
            statusComanda = "";
            BotaoPedido = false;
            //textoBotao = "ABRIR COMANDA";
            //numeroComandaFormatado = "Abra a comanda no caixa";
            this.Usuario = buscarUsuarioLogado();
            this.Itens = new ObservableCollection<Item>();
            ConsultaDadosComanda();
        }

        private Usuario buscarUsuarioLogado()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new UsuarioDAO(conexao);
                return dao.UsuarioLogado;
            }
        }

        public Pedido Comanda { get; set; }
        public ObservableCollection<Item> Itens { get; set; }
        public async Task
ConsultaDadosComanda()
        {
            var comandaApi = new ComandaApi();
            var resposta = await comandaApi.ConsultarComandaAtiva(numeroComanda);
            var resultado = await resposta.Content.ReadAsStringAsync();
            var pedido = JsonConvert.DeserializeObject<Pedido>(resultado);
            this.Comanda = pedido;

            NumeroComandaFormatado = "Nº: " + pedido.IdUsuario.ToString();
            if (string.IsNullOrEmpty(pedido.Id.ToString()))
                NumeroComandaFormatado = "Abra a comanda no caixa";



            Status _status = new Status();
            _status.GerarStatusComanda(pedido.Status);
            StatusComanda = _status.StatusComanda;
            CorStatusComanda = _status.Cor;
            CodStatusComanda = pedido.Status.ToString();
            if (pedido.Status.ToString() == "404")
            {
                StatusComanda = "";
                NumeroComandaFormatado = "";
            }

            //Aberto
            if (this.Comanda.Status == 10)
            {
                TextoBotao = "Pagar";
                //Clicar no botão para fechar e se dirigir ao pagamento
            }
            //Pago
            if (this.Comanda.Status == 30)
            {
                TextoBotao = "Checkout";
            }
            //Checkout
            if (this.Comanda.Status == 20)
            {
                TextoBotao = "Abrir Comanda";
            }
            //Not Found
            if (!resposta.IsSuccessStatusCode)
            {
                TextoBotao = "Abrir Comanda";
                StatusComanda = "";
                CodStatusComanda = "404";
            }
            //if (this.Comanda.Status == 404)
            //{

            //}


            Itens.Clear();
            var itemApi = new ItemAPI();
            var resposta2 = await itemApi.ConsultarItens(numeroComanda);
            var resultado2 = await resposta2.Content.ReadAsStringAsync();
            var itensJson = JsonConvert.DeserializeObject<ObservableCollection<Item>>(resultado2);
            foreach (var itemJson in itensJson)
            {
                Helpers.Status status = new Helpers.Status();
                status.GerarStatusItens(itemJson.Status);
                this.Itens.Add(new Item
                {
                    Descricao = itemJson.Descricao,
                    ValorTotal = itemJson.ValorTotal,
                    Qtde = itemJson.Qtde,
                    Status = itemJson.Status,
                    StatusFormatado = status.StatusComanda,
                    Cor = status.Cor
                });
            }
        }


        //public async Task ConsultaItens()
        //{
        //    var itemApi = new ItemAPI();
        //    var resposta = await itemApi.ConsultarItens(numeroComanda);
        //    var resultado = await resposta.Content.ReadAsStringAsync();
        //    Itens = JsonConvert.DeserializeObject<ObservableCollection<Item>>(resultado);

        //    //Status _status = new Status();
        //    //_status.GerarStatusItens(20);
        //    //StatusComanda = _status.StatusComanda;
        //    //CorStatusComanda = _status.Cor;
        //}

        public ICommand Pedido => new Command(() =>
        {
            MessagingCenter.Send("Apresente o código no caixa", "ItensPendentes");
        });

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await ConsultaDadosComanda();

                    IsRefreshing = false;
                });
            }
        }

        public ICommand BotaoPrincipal => new Command(() =>
        {
            if (CodStatusComanda == "10")
            {
                TextoBotao = "PAGAR";
                BotaoPedido = true;
                MessagingCenter.Send("", "AbrirPagamento");
                //Abrir tela QRCode como numero da comanda e mensagem de abre sua comanda no caixa
            }
            else if (CodStatusComanda == "20")
            {
                TextoBotao = "ABRIR COMANDA";
                BotaoPedido = false;
                MessagingCenter.Send("Apresente o código no caixa", "QRCodeAberta");
            }
            else if (CodStatusComanda == "30")
            {
                TextoBotao = "CHECKOUT";
                BotaoPedido = false;
                MessagingCenter.Send("Apresente código na saída", "QRCodeFechada");
            }
            else if (CodStatusComanda == "404")
            {
                TextoBotao = "ABRIR COMANDA";
                BotaoPedido = false;
                MessagingCenter.Send("Apresente o código no caixa", "QRCodeAberta");
            }


        });



    }
}
