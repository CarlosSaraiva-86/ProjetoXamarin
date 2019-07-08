using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BragantinaTelerikDemo.Portable.Helpers
{
    public class Status
    {
        public string StatusComanda { get; set; }
        public Color Cor { get; set; }

        public  void GerarStatusComanda(int status)
        {

            if (status == 10)
            {
                StatusComanda = "Aberta";
                Cor = Color.Green;
            }
            else if (status == 20)//Pago e feito checkout
            {
                StatusComanda = "Pago";
                Cor = Color.Orange;
            }
            else if (status == 30)
            {
                StatusComanda = "Pago";
                Cor = Color.Blue;
            }
        }

        public void GerarStatusItens(int status)
        {

            if (status == 10)
            {
                StatusComanda = "Pendente";
                Cor = Color.Orange;
            }
            else if (status == 20)
            {
                StatusComanda = "Preparando";
                Cor = Color.Orange;
            }
            else if (status == 30)
            {
                StatusComanda = "Entregue";
                Cor = Color.Green;
            }
        }
    }

}