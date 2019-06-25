using BragantinaTelerikDemo.Portable.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Dao
{
    class UsuarioDAO
    {
        readonly SQLiteConnection conexao;
        readonly string Token;

        public UsuarioDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Usuario>();
        }

        //public UsuarioDAO(SQLiteConnection conexao, string token)
        //{
        //    this.conexao = conexao;
        //    this.Token = token;
        //    this.conexao.CreateTable<Usuario>();
        //}

        public void Salvar(Usuario usuario)
        {
            conexao.Insert(usuario);
        }

        private Usuario usuarioLogado;

        public Usuario UsuarioLogado
        {
            get
            {
                var usuarioDB = conexao.Table<Usuario>(); ;

                usuarioLogado = new Usuario();
                var list = conexao.Query<Usuario>("select * from Usuario");
                if (list.Count > 0)
                    usuarioLogado = list[0];

                return usuarioLogado;
            }
        }
    }
}
