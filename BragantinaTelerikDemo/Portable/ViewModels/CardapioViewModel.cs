using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BragantinaTelerikDemo.Portable.API;
using BragantinaTelerikDemo.Portable.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class CardapioViewModel:BaseViewModel
    {
        ProdutoAPI api = new ProdutoAPI();
        private LayoutOption selectedLayout;

        public LayoutOption SelectedLayout
        {
            get
            {
                return this.selectedLayout;
            }
            set
            {
                if (this.selectedLayout != value)
                {
                    this.selectedLayout = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Produto> Items { get; private set; }
        public ObservableCollection<LayoutOption> LayoutOptions { get; private set; }

        public CardapioViewModel()
        {
            this.Items = new ObservableCollection<Produto>();
            
            this.LayoutOptions = new ObservableCollection<LayoutOption>()
            {
                new LayoutOption(LayoutType.Grid, ""),
            };
            this.SelectedLayout = new LayoutOption(LayoutType.Grid, "");
            
        }

        private bool aguarde;
        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        public async Task GetProdutos()
        {
            Aguarde = true;
            this.Items.Clear();
            //var resultado = await api.BuscarProdutos();
            Conexao con = new Conexao();
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(con.uri + "produto");
            var produtosJson = JsonConvert.DeserializeObject<Produto[]>(resultado);
            foreach (var item in produtosJson)
            {
                this.Items.Add(new Cerveja(item.Id, item.Imagem, item.Descricao, item.Grupo, item.Titulo, item.Informacao, 
                    item.Cozinha, item.Valor));

            }
            Aguarde = false;
        }

        Produto produtoSelecionado;

        public Produto ProdutoSelecionado
        {
            get
            {
                return produtoSelecionado;
            }
            set
            {
                produtoSelecionado = value;
                if (value != null)
                {
                    MessagingCenter.Send(produtoSelecionado, "ProdutoSelecionado");
                }
            }
        }
    }
}
