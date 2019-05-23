using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Orders.Order
{
    public class CreateViewModel
    {
        // Master properties 
        [Required]
        public int IdProvider { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public decimal Total { get; set; }

        // Details Properties
        [Required]
        public List<DetailViewModel> Details { get; set; }
       
    }
}
