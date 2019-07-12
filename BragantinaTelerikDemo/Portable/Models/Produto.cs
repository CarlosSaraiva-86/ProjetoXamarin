using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public string Grupo { get; set; }
        public string Titulo { get; set; }
        public string Informacao { get; set; }
        public bool Info { get; set; }
        public bool Cozinha { get; set; }

        public Produto(int id, string imagem, string descricao, string grupo, string titulo, string informacao, bool cozinha)
        {
            this.Id = id;
            this.Imagem = imagem;
            this.Descricao = descricao;
            this.Grupo = grupo;
            this.Titulo = titulo;
            this.Informacao = informacao;
            this.Cozinha = !cozinha;            
        }

        public Produto()
        {

        }
    }
    
}
