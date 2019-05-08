using DrugStore.Entities.Store;
using DrugStore.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Orders
{
    public class OrderIncome
    {
        public int IdOrder { get; set; }
        public int IdProvider { get; set; }
        public int IdUser { get; set; }
        public DateTime DateEntry { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public string State { get; set; }

        public ICollection<OrderIncomeDetails> OrderIncomeDetails { get; set; }
        public User User { get; set; }
        public Provider provider { get; set; }
    }
}
