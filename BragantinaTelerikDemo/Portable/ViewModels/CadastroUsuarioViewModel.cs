using BragantinaTelerikDemo.Portable.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class CadastroUsuarioViewModel : BaseViewModel
    {
        public UsuarioApi Usuario { get; set; }
        public string Nome
        {
            get
            {
                return Usuario.Nome;
            }
            set
            {
                Usuario.Nome = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Fone
        {
            get
            {
                return Usuario.Telefone;
            }
            set
            {
                Usuario.Telefone = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }

        }
        public string Email
        {
            get
            {
                return Usuario.Email;
            }
            set
            {
                Usuario.Email = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public ICommand CadastrarCommand { get; set; }

        public CadastroUsuarioViewModel(UsuarioApi usuario)
        {
            this.Usuario = usuario;
            CadastrarCommand = new Command(() =>
            {
                MessagingCenter.Send<UsuarioApi>(this.Usuario
                    , "CadastrarUsuarioNuvem");
            }, () =>
            {
                return !string.IsNullOrEmpty(this.Nome)
                 && !string.IsNullOrEmpty(this.Fone)
                 && !string.IsNullOrEmpty(this.Email);
            });
        }
    }
}
