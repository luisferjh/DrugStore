using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Sales.Delivery
{
    public class DeliveryViewModel
    {
        public int IdSale { get; set; }
        public int IdClient { get; set; }
        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
    }
}
