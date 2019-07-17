using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    [Table("Cartao")]
    public class Cartao
    {
        [PrimaryKey, NotNull]
        public int ID { get; set; }
        public string card_id { get; set; }
        public string number_token { get; set; }
    }

    public class Card
    {
        public string number_token { get; set; }
        public string brand { get; set; }
        public string card_number { get; set; }
        public string cardholder_name { get; set; }
        public string expiration_month { get; set; }
        public string expiration_year { get; set; }
        public string security_code { get; set; }        
    }

    public class CardArm
    {
        public string number_token { get; set; }
        public string cardholder_name { get; set; }
        public string expiration_month { get; set; }
        public string expiration_year { get; set; }
        public string customer_id { get; set; }
    }

    public class CardRec
    {
        public string card_id { get; set; }
        public string last_four_digits { get; set; }
        public string expiration_month { get; set; }
        public string expiration_year { get; set; }
        public string brand { get; set; }
        public string cardholder_name { get; set; }
        public string customer_id { get; set; }
        public string number_token { get; set; }
        public string used_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string status { get; set; }
    }
}
