﻿using DrugStore.Entities.Orders;
using DrugStore.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Store
{
    public class Product
    {
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public int IdLaboratory { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string Indicative { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SalePrice { get; set; }
        public Boolean Condition { get; set; }

        public Category Category { get; set; }
        public Laboratory Laboratory { get; set; }
        public ICollection<OrderIncomeDetails> OrderIncomeDetails { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
        public ICollection<LossDetail> LossDetails { get; set; }
    }
}
