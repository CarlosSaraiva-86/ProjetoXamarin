using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Facebook { get; set; }
        public double Meta { get; set; }
        public double Consumo { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Cpf { get; internal set; }
        public string ImgPerfil { get; set; }
    }

    public class LoginResult
    {
        public Usuario usuario { get; set; }
    }

    public class UsuarioApi
    {
        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Facebook { get; set; }
        public double Meta { get; set; }
        public double Consumo { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Cpf { get; internal set; }
        public byte[] ImgByte { get; set; }
        public Login Login { get; set; }
    }
}
