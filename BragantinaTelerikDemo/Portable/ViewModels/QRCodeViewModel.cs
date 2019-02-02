using System;
using System.Collections.Generic;
using System.Text;

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

        public QRCodeViewModel(string titulo)
        {
            _titulo = titulo;
        }

    }
}
