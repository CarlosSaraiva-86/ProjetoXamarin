using BragantinaTelerikDemo.Portable.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class ComandaViewModel : BaseViewModel
    {
        private string statusComanda;
        private string textoBotao;
        private string numeroComanda;
        private bool pedido;

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
        public string TextoBotao
        {
            get { return textoBotao; }
            set
            {
                textoBotao = value;
                OnPropertyChanged();
            }
        }

        public string NumeroComanda
        {
            get { return numeroComanda; }
            set
            {
                numeroComanda = value;
                OnPropertyChanged();
            }
        }
        public ComandaViewModel()
        {
            statusComanda = "";
            BotaoPedido = false;
            textoBotao = "ABRIR COMANDA";
            numeroComanda = "Abra a comanda no caixa";
        }

        public ICommand Pedido => new Command(() => 
        {
            MessagingCenter.Send("Apresente o código no caixa", "QRCodePedido");
        });

        public ICommand BotaoPrincipal => new Command(() =>
        {
            if (StatusComanda == "")
            {                
                StatusComanda = "Aberta";
                TextoBotao = "PAGAMENTO";
                NumeroComanda = "Comanda: 16783";
                BotaoPedido = true;
                MessagingCenter.Send("Apresente o código no caixa", "QRCodeAberta");
            }
            else if (StatusComanda == "Aberta")
            {
                StatusComanda = "Pago";
                TextoBotao = "CHECKOUT";
                NumeroComanda = "";
                BotaoPedido = false;
                MessagingCenter.Send("", "AbrirPagamento");
            }
            else
            {
                TextoBotao = "ABRIR COMANDA";
                StatusComanda = "";
                NumeroComanda = "Abra a comanda no caixa";
                BotaoPedido = false;
                MessagingCenter.Send("Apresente código na saída", "QRCodeFechada");
            }
        });
    }
}
