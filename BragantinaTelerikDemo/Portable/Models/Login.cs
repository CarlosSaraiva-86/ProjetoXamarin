using BragantinaTelerikDemo.Portable.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Login
    {
        private bool AutenticadoAPI;
        public int Id { get; set; }
        //public string Usuario { get; set; }
        //public string Senha { get; set; }

        private string usuario;

        public string Usuario
        {
            get { return MD5Crypt.Descriptografar(usuario); }
            set { usuario = MD5Crypt.Criptografar(value); }
        }

        private string senha;

        public string Senha
        {
            get { return MD5Crypt.Descriptografar(senha); }
            set { senha = MD5Crypt.Criptografar(value); }
        }



        public Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(nameof(email));

            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException(nameof(senha));

            this.Usuario = email;
            this.Senha = senha;
        }


        public Login()
        {
            
        }
    }
}
