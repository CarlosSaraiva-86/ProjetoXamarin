using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    class PerfilUsuarioViewModel : BaseViewModel
    {
        public PerfilUsuarioViewModel()
        {
            this.Avatar = "https://scontent.fbjp1-1.fna.fbcdn.net/v/t1.0-9/33923600_1395086363925833_547282230553083904_n.jpg?_nc_cat=111&_nc_ht=scontent.fbjp1-1.fna&oh=e812948c16474113a5d63428adaa65eb&oe=5CA7269C";
            this.Premio = "beer0.png";
            IndicePremio = 1;

            TesteCommand = new Command(() =>
            {
                if (IndicePremio == 0)
                {
                    this.Premio = "beer0.png";
                }
                if (IndicePremio == 1)
                {
                    this.Premio = "beer25.png";
                }
                if (IndicePremio == 2)
                {
                    this.Premio = "beer50.png";
                }
                if (IndicePremio == 3)
                {
                    this.Premio = "beer75.png";
                }
                if (IndicePremio == 4)
                {
                    this.Premio = "beer100.png";
                }

                if (IndicePremio >= 4)
                    IndicePremio = 0;
                else
                    IndicePremio++;
            });
        }


        public string Avatar { get; set; }

        private string premio;
        public string Premio
        {
            get { return premio; }
            private set
            {
                premio = value;
                OnPropertyChanged();
            }
        }
        public int IndicePremio { get; private set; }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public ICommand ClickCommand1 => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        public ICommand TesteCommand { get; private set; }
    }
}
