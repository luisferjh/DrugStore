using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Store.Laboratory
{
    public class CreateViewModel
    {      
        public string LaboratoryName { get; set; }
        public string Description { get; set; }
        public Boolean Condition { get; set; }
    }
}
