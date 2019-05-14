using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Store
{
    public class LossDetail
    {
        public int IdDetailLosses { get; set; }
        public int IdLosses { get; set; }
        public int IdProduct { get; set; }
        public decimal UnitCost { get; set; }
        public int Amount { get; set; }

        public Losses Losses { get; set; }
        public Product Product { get; set; }
    }
}
