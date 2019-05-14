using DrugStore.Entities.Orders;
using DrugStore.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Users
{
    public class User
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Boolean Condition { get; set; }

        public Role Role { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<OrderIncome> OrderIncomes { get; set; }
    }
}
