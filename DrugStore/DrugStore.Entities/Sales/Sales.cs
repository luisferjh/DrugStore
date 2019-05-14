using DrugStore.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Sales
{
    public class Sale
    {
        public int IdSale { get; set; }
        public int IdUser { get; set; }
        public int? IdClient { get; set; }
        public string TypeSale { get; set; }
        public string VoucherSeries { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string State { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
        public User User { get; set; }
        public Client Client { get; set; }
        public Delivery Delivery { get; set; }
    }
}
