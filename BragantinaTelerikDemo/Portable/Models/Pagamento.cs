using System;
using System.Collections.Generic;
using System.Text;

namespace BragantinaTelerikDemo.Portable.Models
{
    public class Pagamento
    {
        public string seller_id { get; set; }
        public int amount { get; set; }
        public Order order { get; set; }
        public Customer customer { get; set; }
        public Devices device { get; set; }
        //public Shippings shippings { get; set; }
        public Credit credit { get; set; }

        public Pagamento(double valor, string idPedido, string idCliente, Credit cred)
        {
            Order _order = new Order(idPedido);
            Customer _customer = new Customer(idCliente);
            IdPagamento pgto = new IdPagamento();
            seller_id = pgto.idSeller;
            int vlr = Convert.ToInt32(valor * 100);
            amount = vlr;
            order = _order;
            customer = _customer;
            credit = cred;
        }
    }

    public class Order
    {
        public string order_id { get; set; }
        public Order(string id)
        {
            order_id = id;
        }
    }

    public class Customer
    {
        public string customer_id { get; set; }
        public Billing_Address billing_address { get; set; }
        public Customer(string id)
        {
            customer_id = id;
        }
    }

    public class Billing_Address
    {

    }
    public class Devices
    {

    }

    public class Shippings
    {
        public Address address { get; set; }
    }

    public class Address
    {

    }
}
