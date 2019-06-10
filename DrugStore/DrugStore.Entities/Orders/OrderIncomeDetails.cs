using DrugStore.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Orders
{
    public class OrderIncomeDetails
    {
        public int IdOrderIncomeDetails { get; set; }
        public int IdOrderIncome { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public decimal UnitCost { get; set; }

        public Product Product { get; set; }
        public OrderIncome OrderIncome { get; set; }
    }
}
