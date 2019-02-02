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
        string statusComanda;
        string textoBotao;
        string numeroComanda;

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
    }
}
