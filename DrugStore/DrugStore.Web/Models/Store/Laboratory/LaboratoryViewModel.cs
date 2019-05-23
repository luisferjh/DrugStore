using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Store
{
    public class LaboratoryViewModel
    {
        public int IdLaboratory { get; set; }
        public string LaboratoryName { get; set; }
        public string Description { get; set; }
        public Boolean Condition { get; set; }

    }
}
