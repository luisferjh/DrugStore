using DrugStore.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Sales
{
    public class Delivery
    {
        public int IdDelivery { get; set; }
        public int IdClient { get; set; }
        public int IdSale { get; set; }
        public string State { get; set; }

        public Client Client { get; set; }
        public Sale Sale { get; set; }

    }
}
