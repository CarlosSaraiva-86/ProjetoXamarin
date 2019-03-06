using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Facebook { get; set; }
        public double Meta { get; set; }
        public double Consumo { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }

    public class LoginResult
    {
        public Usuario usuario { get; set; }
    }
}
