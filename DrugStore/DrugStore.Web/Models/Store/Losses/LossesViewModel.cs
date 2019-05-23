using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Store.Losses
{
    public class LossesViewModel
    {
        public int IdLosses { get; set; }
        public DateTime DateLoss { get; set; }
        public string Cause { get; set; }
        public string State { get; set; }
       
    }
}
