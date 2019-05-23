using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Sales.Sale
{
    public class DetailViewModel
    {
        [Required]
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public Decimal Discount { get; set; }
        [Required]
        public Decimal UnitCost { get; set; }
        [Required]
        public Decimal UnitPrice { get; set; }
    }
}
