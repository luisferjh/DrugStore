using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Store.Product
{
    public class UpdateViewModel
    {
        public int IdProduct { get; set; }
        public int IdCategory { get; set; }
        public int IdLaboratory { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not have more than 20 characters or less than 3 characters")]
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string Indicative { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Boolean Condition { get; set; }
    }
}
