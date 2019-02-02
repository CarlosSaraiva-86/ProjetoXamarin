﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Produto
    {
        public int Cod { get; private set; }
        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Group { get; private set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Info { get; set; }
        public bool Cozinha { get; set; }

        public Produto(int cod, string image, string title, string group)
        {
            this.Cod = cod;
            this.Image = image;
            this.Title = title;
            this.Group = group;
            this.Titulo = "The Real Ale!";
            this.Descricao = "Refrescante e saborosa, com perfil maltado que remete ao pão combinado com um sutil e suave amargor do nobre lúpulo Saaz.";
            if (group == "PORÇÕES")
            {
                Cozinha = true;
            }
        }
    }
}
