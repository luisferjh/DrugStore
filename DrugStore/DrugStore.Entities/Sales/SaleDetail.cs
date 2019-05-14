using DrugStore.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Sales
{
    public class SaleDetail        
    {
        public int IdSaleDetail { get; set; }
        public int IdSale { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }

        public Sale Sale { get; set; }
        public Product Products { get; set; }
    }
}
