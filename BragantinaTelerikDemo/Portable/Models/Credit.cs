using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Credit
    {
        public bool delayed { get; set; }
        public bool authenticated { get; set; }
        public bool pre_autorization { get; set; }
        public bool save_card_data { get; set; }
        public string transaction_type { get; set; }
        public int number_installments { get; set; }
        public Card card { get; set; }

        public Credit(Card crd)
        {
            delayed = false;
            authenticated = false;
            save_card_data = false;
            transaction_type = "FULL";
            number_installments = 1;
            card = crd;
        }
    }
}
