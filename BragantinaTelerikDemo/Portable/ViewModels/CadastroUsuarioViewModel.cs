using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Helpers;
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
        public UsuarioNuvem Usuario { get; set; }
        public Login login { get; set; }
        ContentPage page;

       

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

        public string CPF
        {
            get
            {
                return Usuario.Cpf;
            }
            set
            {
                Usuario.Cpf = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        //public string Senha { get; set; }
        public string Senha
        {
            get
            {
                return Usuario.Senha;
            }
            set
            {
                Usuario.Senha = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Cidade
        {
            get
            {
                return Usuario.Cidade;
            }
            set
            {
                Usuario.Cidade = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Cep
        {
            get
            {
                return Usuario.CEP;
            }
            set
            {
                Usuario.CEP = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string UF
        {
            get
            {
                return Usuario.UF;
            }
            set
            {
                Usuario.UF = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Bairro
        {
            get
            {
                return Usuario.Bairro;
            }
            set
            {
                Usuario.Bairro = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Endereco
        {
            get
            {
                return Usuario.Logradouro;
            }
            set
            {
                Usuario.Logradouro = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Sobrenome
        {
            get
            {
                return Usuario.Sobrenome;
            }
            set
            {
                Usuario.Sobrenome = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public string Numero
        {
            get
            {
                return Usuario.Numero;
            }
            set
            {
                Usuario.Numero = value;
                OnPropertyChanged();
                ((Command)CadastrarCommand).ChangeCanExecute();
            }
        }

        public ICommand CadastrarCommand { get; set; }

        private void LocalizarCEP(object sender, FocusEventArgs e)
        {
            if (Usuario.CEP != "")
            {
                CEP cep = new CEP();
                var info = cep.Consultar(Usuario.CEP);
                this.Endereco = info.logradouro;
                this.UF = info.uf;
                this.Cidade = info.localidade;
                this.Bairro = info.bairro;
            }
        }


        public CadastroUsuarioViewModel(UsuarioNuvem usuario, ContentPage view)
        {
            try
            {
                this.Usuario = usuario;

                page = view;
                Entry cepEntry = page.FindByName<Entry>("CEP");
                cepEntry.Unfocused += LocalizarCEP;

                CadastrarCommand = new Command(() =>
                {                    
                    UsuarioAPI api = new UsuarioAPI();
                    api.CadastrarUsuario(Usuario);
                }, () =>
                {
                    return !string.IsNullOrEmpty(this.Nome)
                     && !string.IsNullOrEmpty(this.Fone)
                     && !string.IsNullOrEmpty(this.Email)
                     && !string.IsNullOrEmpty(this.CPF)
                     && !string.IsNullOrEmpty(this.Cidade)
                     && !string.IsNullOrEmpty(this.UF)
                     && !string.IsNullOrEmpty(this.Senha);
                    ;
                });
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
