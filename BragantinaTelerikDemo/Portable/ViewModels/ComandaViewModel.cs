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
            textoBotao = "ABRIR COMANDA";
            numeroComanda = "Abra a comanda no caixa";
        }

        public ICommand BotaoPrincipal => new Command(() =>
        {
            if (StatusComanda == "")
            {
                //Navigation.PushAsync(new QRcodeView("Apresente o código no caixa"));
                
                StatusComanda = "Aberta";
                TextoBotao = "PAGAMENTO";
                NumeroComanda = "Comanda: 16783";
                MessagingCenter.Send("Apresente o código no caixa", "QRCodeAberta");
            }
            else if (StatusComanda == "Aberta")
            {
                StatusComanda = "Pago";
                TextoBotao = "CHECKOUT";
                NumeroComanda = "";
                //Navigation.PushAsync(new PagamentoView());
                MessagingCenter.Send("", "AbrirPagamento");
            }
            else
            {
                //Navigation.PushAsync(new QRcodeView("Apresente código na saída"));
                TextoBotao = "ABRIR COMANDA";
                StatusComanda = "";
                NumeroComanda = "Abra a comanda no caixa";
                MessagingCenter.Send("Apresente código na saída", "QRCodeFechada");
            }
        });
    }
}
