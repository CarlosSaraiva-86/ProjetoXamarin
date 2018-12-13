using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BragantinaTelerikDemo.Portable.Models;

namespace BragantinaTelerikDemo.Portable.ViewModels
{
    public class CardapioViewModel:BaseViewModel
    {
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

        public ObservableCollection<Cardapio> Items { get; private set; }
        public ObservableCollection<LayoutOption> LayoutOptions { get; private set; }

        public CardapioViewModel()
        {
            this.Items = new ObservableCollection<Cardapio>()
            {
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_03.jpg", "Brown Ale",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_05.jpg", "Prainha", "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/breja_07.png", "Milk Robust Porter",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_12.jpg", "Weiss Ipa",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_14.jpg", "Session Ipa",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_16.jpg", "Joker Ipa",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_20.jpg", "Red Ipa",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_21.jpg", "Witbier",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/nossabreja_23.jpg", "Blond Ale",  "CERVEJAS"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/8.jpg", "Batata Rustica",  "PORÇÕES"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/2.jpg", "Liguiça Autentica",  "PORÇÕES"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/4.jpg", "Pizza",  "PORÇÕES"),
                new Cardapio("http://br960.teste.website/~cervej27/wp-content/uploads/2018/11/5.jpg", "Frango",  "PORÇÕES"),
                new Cardapio("https://http2.mlstatic.com/coca-cola-original-350ml-D_NQ_NP_965923-MLB28675051466_112018-O.webp", "Coca Cola",  "REFRIGERANTES"),
            };
            this.LayoutOptions = new ObservableCollection<LayoutOption>()
            {
                new LayoutOption(LayoutType.Grid, ""),
                //new LayoutOption(LayoutType.Linear, "ListView_LinearLayout.png")
            };
            //this.SelectedLayout = this.LayoutOptions[0];
            this.SelectedLayout = new LayoutOption(LayoutType.Grid, "") ;
        }
    }
}
