using DrugStore.Entities.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Store
{
    [Auditable]
    public class Losses
    {
        public int IdLosses { get; set; }
        public string Cause { get; set; }
        public string State { get; set; }

        public ICollection<LossDetail> LossDetails{ get; set; }

    }
}
