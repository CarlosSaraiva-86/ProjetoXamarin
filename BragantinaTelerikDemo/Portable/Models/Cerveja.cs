using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Cerveja : Produto
    {
        public string Tonalidade { get; set; }
        public string Amargor { get; set; }
        public string ImgTonalidade { get; set; }
        public string ImgAmargor { get; set; }


        public Cerveja(int id, string imagem, string descricao, string grupo, string titulo, string informacao, bool cozinha) 
            :base(id, imagem, descricao, grupo, titulo, informacao, cozinha)
        {
            this.ImgTonalidade = "http://191.252.64.46:5000/APIBragantina/Imagens/Produtos/Thumbs/icone-breja_01.png";
            this.ImgAmargor = "http://191.252.64.46:5000/APIBragantina/Imagens/Produtos/Thumbs/icone-breja_02.png";
            base.Info = true;
        }
    }
}
