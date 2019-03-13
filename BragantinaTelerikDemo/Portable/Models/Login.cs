using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Login
    {
        public string Usuario { get; private set; }
        public string Senha { get; private set; }       

        public Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(nameof(email));

            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException(nameof(senha));

            this.Usuario = email;
            this.Senha = senha;
        }
    }
}
