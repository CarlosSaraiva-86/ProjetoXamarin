using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    class PerfilUsuarioViewModel : BaseViewModel
    {
        public Usuario Usuario { get; set; }
        public PerfilUsuarioViewModel()
        {
            MessagingCenter.Subscribe<Usuario>(this, "UsuarioFB", (usuario) =>
            {
                Avatar = usuario.ImgPerfil;
                Name = usuario.Nome;
            });

            
            this.Usuario = buscarUsuarioLogado();
            Avatar = this.Usuario.ImgPerfil;
        }

        private Usuario buscarUsuarioLogado()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new UsuarioDAO(conexao);
                return dao.UsuarioLogado;
            }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

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

        public ICommand LogoutCommand => new Command(() => 
        {
            MessagingCenter.Send("", "LogoutApp");            
        });
    }
}
