using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;
using static BragantinaTelerikDemo.Portable.API.PagamentoAPI;
using static BragantinaTelerikDemo.Portable.API.UsuarioAPI;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class PagamentoViewModel : BaseViewModel
    {
        PagamentoAPI api = new PagamentoAPI();
        UsuarioAPI apiUsuario = new UsuarioAPI();
        ComandaApi apiComanda = new ComandaApi();
        ItemAPI apiItem = new ItemAPI();
        Pedido pedido = new Pedido();
        Card card = new Card();

        string autorization;
        bool armazenado;
        double valorTotal;
        public double ValorTotal
        {
            get
            {
                return valorTotal;
            }
            set
            {
                valorTotal = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }
        string valorTotalFormatado;
        public string ValorTotalFormatado
        {
            get
            {
                return valorTotalFormatado;
            }
            set
            {
                valorTotalFormatado = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }

        public string Nome
        {
            get
            {
                return card.cardholder_name;
            }
            set
            {
                card.cardholder_name = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }
        public string NumCartao
        {
            get
            {
                return card.card_number;
            }
            set
            {
                card.card_number = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }

        public string Mes
        {
            get
            {
                return card.expiration_month;
            }
            set
            {
                card.expiration_month = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }

        public string Ano
        {
            get
            {
                return card.expiration_year;
            }
            set
            {
                card.expiration_year = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }

        public string CodSeg
        {
            get
            {
                return card.security_code;
            }
            set
            {
                card.security_code = value;
                OnPropertyChanged();
                //((Command)PagarCommand).ChangeCanExecute();
            }
        }

        public PagamentoViewModel(Pedido ped)
        {
            pedido = ped;
            ConsultarTotal(pedido.Usuario.Id);
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                CardDAO dao = new CardDAO(conexao);
                var cartao = dao.Recuperar();
                if (cartao != null)
                {
                    armazenado = true;
                    var token = api.Autenticar();
                    autorization = "Bearer " + token.access_token;
                    var cardRec = api.Recuperar(autorization, cartao);
                    card.number_token = cardRec.number_token;
                    card.brand = cardRec.brand;
                    Mes = cardRec.expiration_month;
                    Ano = cardRec.expiration_year;
                    Nome = cardRec.cardholder_name;
                    NumCartao = "**** **** ****" + cardRec.last_four_digits;
                }
            }
        }

        private async void ConsultarTotal(int idUsuario)
        {
            var resposta = await apiItem.ConsultarItens(idUsuario);
            var resultado = await resposta.Content.ReadAsStringAsync();
            var itens = JsonConvert.DeserializeObject<ObservableCollection<Item>>(resultado);

            foreach (var item in itens)
            {
                if (item.Status == 30 || item.Status == 20)
                    valorTotal += item.ValorTotal;
            }
            ValorTotal = valorTotal;
            valorTotalFormatado = "Valor Total: " + valorTotal.ToString("n2");
            ValorTotalFormatado = valorTotalFormatado;
        }

        public ICommand PagarCommand => new Command(() =>
        {
            var token = api.Autenticar();
            autorization = "Bearer " + token.access_token;
            
            if (!armazenado)
            {
                card = api.Tokenizar(autorization, card);
                CardArm cardArm = new CardArm
                {
                    cardholder_name = card.cardholder_name,
                    customer_id = "cliente_"+pedido.Usuario.Id.ToString(),
                    expiration_month = card.expiration_month,
                    expiration_year = card.expiration_year,
                    number_token = card.number_token
                };

                var cartao = api.Armazenar(autorization, cardArm);
                if (cartao.card_id != null)
                {
                    using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
                    {
                        CardDAO dao = new CardDAO(conexao);
                        dao.Salvar(cartao);
                    }
                }
            }
            if (card.number_token == null)
                card = api.Tokenizar(autorization, card);

            card.card_number = null;
            if (!string.IsNullOrEmpty(card.number_token) && !string.IsNullOrEmpty(card.security_code))
            {
                if (api.Verificar(autorization, card))
                {
                    Usuario user = apiUsuario.Consultar(pedido.Usuario.Id);
                    Credit credit = new Credit(card);
                    Pagamento pagamento = new Pagamento(ValorTotal, pedido.Id.ToString(), user, credit);
                    var pgto = api.Pagamento(autorization, pagamento);

                    if (pgto != null)
                    {
                        if (pgto.status == "APPROVED")
                        {
                            if (api.Inserir(pgto))
                            {
                                if (apiComanda.AlterarStatus(pedido.Id, 20))
                                {
                                    ConsultaComandaAberta(pedido.Usuario.Id);
                                }
                            }
                        }
                        else
                        {
                            MessagingCenter.Send("", "PagamentoRecusado");
                        }
                    }
                    else
                    {
                        MessagingCenter.Send("", "PagamentoRecusado");
                    }
                }
            }
            else
            {
                MessagingCenter.Send("", "InformacoesFaltantes");
            }
        });

        private async void ConsultaComandaAberta(int CodComanda)
        {
            var comandaApi = new ComandaApi();
            var resposta = await comandaApi.ConsultarComanda(CodComanda);
            var resultado = await resposta.Content.ReadAsStringAsync();
            var comanda = JsonConvert.DeserializeObject<Pedido>(resultado);
            
            if (comanda.Status != 30)
                ConsultaComandaAberta(CodComanda);
            else
                MessagingCenter.Send("", "PagamentoAprovado");
        }

        public ICommand DeletarCommand => new Command(() =>
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                CardDAO dao = new CardDAO(conexao);
                var cartao = dao.Recuperar();
                if (cartao != null)
                {
                    api.Deletar(autorization, cartao);
                    dao.Deletar(cartao);
                    Ano = "";
                    Mes = "";
                    Nome = "";
                    NumCartao = "";
                }
            }
        });
    }
}
