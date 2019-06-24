using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class QRCodeViewModel : BaseViewModel
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

        public QRCodeViewModel(string titulo)
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
