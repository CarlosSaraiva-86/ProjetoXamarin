using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class QRCodePedido
    {
        public string titulo { get; set; }
        public int status { get; set; }
        /*
         * 10 - Abertura
         * 20 - Checkout
         */
    }
}
