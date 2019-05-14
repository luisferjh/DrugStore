using DrugStore.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Users
{
    public class Client
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public string PhoneNumber { get; set; } 
        public Boolean Condition { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
