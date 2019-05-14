using System;
using System.Collections.Generic;

namespace DrugStore.Entities.Store
{
    public class Category
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Condition { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
