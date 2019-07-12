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
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class PagamentoViewModel : BaseViewModel
    {
        PagamentoAPI api = new PagamentoAPI();
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
            }
        }

        public PagamentoViewModel(Pedido ped)
        {            
            pedido = ped;
            ConsultarTotal(pedido.IdUsuario);
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
                    card.card_number = null;
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
                if (item.Status == 30)
                    valorTotal += item.ValorTotal;
            }
            ValorTotal = valorTotal;
            valorTotalFormatado = "Valor Total: "+valorTotal.ToString("n2");
            ValorTotalFormatado = valorTotalFormatado;
        }

        public ICommand PagarCommand => new Command(() =>
        {
            var token = api.Autenticar();
            autorization = "Bearer " + token.access_token;            

            if (!armazenado)
            {
                card = api.Tokenizar(autorization, card);
                card.card_number = null;
                CardArm cardArm = new CardArm
                {
                    cardholder_name = card.cardholder_name,
                    customer_id = pedido.IdUsuario.ToString(),
                    expiration_month = card.expiration_month,
                    expiration_year = card.expiration_year,
                    number_token = card.number_token
                };

                var cartao = api.Armazenar(autorization, cardArm);
                using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
                {
                    CardDAO dao = new CardDAO(conexao);
                    dao.Salvar(cartao);                    
                }
            }
            
            if (api.Verificar(autorization, card))
            {
                Credit credit = new Credit(card);
                Pagamento pagamento = new Pagamento(ValorTotal, pedido.Id.ToString(), pedido.IdUsuario.ToString(), credit);
                var pgto = api.Pagamento(autorization, pagamento);
            }
        });

    }
}
