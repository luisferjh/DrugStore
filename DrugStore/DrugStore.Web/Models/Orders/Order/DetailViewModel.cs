using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Orders.Order
{
    public class DetailViewModel
    {              
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal UnitCost { get; set; }
    }
}
