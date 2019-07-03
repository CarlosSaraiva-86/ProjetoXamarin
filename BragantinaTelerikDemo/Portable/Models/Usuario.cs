﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, NotNull]
        public int Id { get; set; }
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
        public string IdToken { get; set; }
    }

    public class LoginResult
    {
        public Usuario usuario { get; set; }
    }

    public class UsuarioNuvem
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
        public string ImgPerfil { get; set; }
        public Login Login {
            get
            {
                return new Login(this.Email, this.Senha);
            }
        }
        public string Senha { get; set; }

        public UsuarioNuvem()
        {
            
        }
    }

    public class UserApi
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
        public string ImgPerfil { get; set; }
        public string IdToken { get; set; }
        public Login login { get; set; }
    }
}
