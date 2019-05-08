using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Store
{
    public class Laboratory
    {
        public int IdLaboratory { get; set; }
        public string LaboratoryName { get; set; }
        public string Description { get; set; }
        public Boolean Condition { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
