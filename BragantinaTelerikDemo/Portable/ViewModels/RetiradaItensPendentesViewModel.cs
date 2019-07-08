using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class RetiradaItensPendentesViewModel
    {
        string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
            }
        }
        public int CodComanda { get; set; }
        Pedido Comanda = new Pedido();

        public RetiradaItensPendentesViewModel(string titulo)
        {
            _titulo = titulo;
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                UsuarioDAO dao = new UsuarioDAO(conexao);
                CodComanda = dao.UsuarioLogado.Id;
            }
        }
    }
}
