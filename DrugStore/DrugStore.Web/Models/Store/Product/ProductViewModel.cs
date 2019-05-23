using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Services.Store
{
    public class ProductViewModel
    {
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public string Category { get; set; }
        public int IdLaboratory { get; set; }
        public string Laboratory { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string Indicative { get; set; }
        public int Stock { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }
        public Boolean Condition { get; set; }
    }
}
