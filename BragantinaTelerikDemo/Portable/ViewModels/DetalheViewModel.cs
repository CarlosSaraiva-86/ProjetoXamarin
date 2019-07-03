using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Dao;
using BragantinaTelerikDemo.Portable.Data;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Produto Produto { get; set; }

        private double qtde;

        public double Qtde
        {
            get { return qtde; }
            set { qtde = value; OnPropertyChanged(); }
        }

        public ICommand PedirCommand { get; private set; }

        public DetalheViewModel(Produto produto)
        {
            this.Produto = produto;
            int nComanda = BuscarUsuarioLogado();
            PedirCommand = new Command(
            async () =>
            {
                Item item = new Item() { IdProduto = Produto.Id, IdUser = nComanda, Status = 10, Qtde = Convert.ToInt32(Qtde)};

                var comandaApi = new ItemAPI();
                var resposta = await comandaApi.EnviarItem(item);
                var resultado = await resposta.Content.ReadAsStringAsync();
                if (resposta.IsSuccessStatusCode)
                    MessagingCenter.Send<string>(resultado, "SucessoEnvioItem");
                else
                    MessagingCenter.Send<string>(resultado, "FalhaEnvioItem");
            });
        }


        private int BuscarUsuarioLogado()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new UsuarioDAO(conexao);
                return dao.UsuarioLogado.Id;
            }
        }
    }
}

