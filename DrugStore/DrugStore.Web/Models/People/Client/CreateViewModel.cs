using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.Client
{
    public class CreateViewModel
    {      
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public Boolean Condition { get; set; }
    }
}
