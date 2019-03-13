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

        public UsuarioDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Usuario>();
        }

        public void Salvar(Usuario usuario)
        {
            conexao.Insert(usuario);
        }
    }
}
