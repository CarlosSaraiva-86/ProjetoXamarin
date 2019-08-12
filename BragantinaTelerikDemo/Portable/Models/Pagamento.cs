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

        public Pagamento(double valor, string idPedido, Usuario user, Credit cred)
        {
            Order _order = new Order(idPedido);
            Customer _customer = new Customer(user);
            Devices _device = new Devices(idPedido);
            IdPagamento pgto = new IdPagamento();
            seller_id = pgto.idSeller;
            int vlr = Convert.ToInt32(valor * 100);
            amount = vlr;
            order = _order;
            customer = _customer;
            credit = cred;
            device = _device;
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
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string document_type { get; set; }
        public string document_number { get; set; }
        public string phone_number { get; set; }

        public Billing_Address billing_address { get; set; }
        public Customer(Usuario user)
        {
            customer_id = user.Id.ToString();
            first_name = user.Nome;
            last_name = user.Sobrenome;
            name = user.Nome + " " + user.Sobrenome;
            email = user.Email;
            document_type = "CPF";
            document_number = user.Cpf;
            phone_number = user.Telefone;
            billing_address.city = user.Cidade;
            billing_address.street = user.Logradouro;
            billing_address.number = user.Numero;
            billing_address.complement = "";
            billing_address.country = "Brasil";
            billing_address.district = user.Bairro;
            billing_address.state = user.UF;
            billing_address.postal_code = user.CEP;
        }
    }

    public class Billing_Address
    {
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
    }

    public class Devices
    {
        public string ip_address { get; set; }
        public string device_id { get; set; }

        public Devices(string idPedido)
        {

        }
    }

    public class Shippings
    {
        public Address address { get; set; }
    }

    public class Address
    {

    }
}
