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
            textoBotao = "ABRIR COMANDA";
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
            var resposta = await comandaApi.ConsultarComandaAberta(numeroComanda);
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
            MessagingCenter.Send("Apresente o código no caixa", "QRCodePedido");
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
            if (StatusComanda == "")
            {                
                StatusComanda = "Aberta";
                TextoBotao = "PAGAMENTO";
                NumeroComandaFormatado = "Comanda: " + numeroComanda;
                BotaoPedido = true;
                MessagingCenter.Send("Apresente o código no caixa", "QRCodeAberta");
            }
            else if (StatusComanda == "Aberta")
            {
                StatusComanda = "Pago";
                TextoBotao = "CHECKOUT";
                NumeroComandaFormatado = "";
                BotaoPedido = false;
                MessagingCenter.Send("", "AbrirPagamento");
            }
            else
            {
                TextoBotao = "ABRIR COMANDA";
                StatusComanda = "";
                NumeroComandaFormatado = "Abra a comanda no caixa";
                BotaoPedido = false;
                MessagingCenter.Send("Apresente código na saída", "QRCodeFechada");
            }
        });



    }
}
