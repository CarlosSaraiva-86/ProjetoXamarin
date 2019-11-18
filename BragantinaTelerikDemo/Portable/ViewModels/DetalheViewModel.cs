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
                var api = new ComandaApi();
                var comanda = await api.ConsultarComanda(nComanda);
                if (comanda.IsSuccessStatusCode)
                {
                    var resultado = await comanda.Content.ReadAsStringAsync();
                    var pedido = JsonConvert.DeserializeObject<Pedido>(resultado);
                    pedido.Itens.Add(new Item()
                    {
                        Produto = Produto, Status = 10,
                        Qtde = Convert.ToInt32(Qtde),
                        ValorTotal = Produto.Valor * Qtde,
                    });

                    var resposta = await api.Update(pedido);

                    if (resposta.IsSuccessStatusCode)
                        MessagingCenter.Send<string>(resultado, "SucessoEnvioItem");
                    else
                        MessagingCenter.Send<string>(resultado, "FalhaEnvioItem");
                }
                else
                    MessagingCenter.Send<string>("", "FalhaEnvioItem");
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

