﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Orders.Order
{
    public class OrderViewModel
    {
        public int IdOrderIncome { get; set; }
        public int IdProvider { get; set; }
        public string Proveedor { get; set; }
        public int IdUser { get; set; }
        public string User { get; set; }
        public DateTime DateEntry { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Total { get; set; }
        public string State { get; set; }
    }
}
