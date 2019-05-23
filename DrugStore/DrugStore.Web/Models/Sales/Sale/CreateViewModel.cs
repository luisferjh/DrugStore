using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Sales.Sale
{
    public class CreateViewModel
    {        
        public int IdUser { get; set; }
        public int? IdClient { get; set; }
        public string TypeSale { get; set; }
        public string VoucherSeries { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string State { get; set; }
       
        // Details Property
        public List<DetailViewModel> Details { get; set; }
    }
}
