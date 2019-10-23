using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Sales.Sale
{
    public class SaleViewModel
    {
        public int IdSale { get; set; }
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public int? IdClient { get; set; }
        public string ClientName { get; set; }    
        public string TypeSale { get; set; }
        public string VoucherSeries { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime SaleDateUpdate { get; set; }
        public decimal TotalPrice { get; set; }
        public string State { get; set; }
        
    }
}
