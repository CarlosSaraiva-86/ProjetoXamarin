using BragantinaTelerikDemo.Portable.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Dao
{
    class CardDAO
    {
        readonly SQLiteConnection conexao;
        readonly string Token;

        public CardDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Cartao>();
        }

        public void Salvar(Cartao card)
        {
            conexao.Insert(card);
        }

        public void Deletar(Cartao card)
        {
            conexao.Delete(card);
        }

        public Cartao Recuperar()
        {
            var cartoes = conexao.Query<Cartao>("SELECT * FROM Cartao");
            foreach (var card in cartoes)
            {
                return card;
            }
            return null;
        }
    }
}
