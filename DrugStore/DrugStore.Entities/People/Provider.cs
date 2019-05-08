using DrugStore.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Users
{
    public class Provider
    {
        public int IdProvider { get; set; }
        public string ProviderName { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<OrderIncome> OrderIncomes { get; set; }
    }
}
