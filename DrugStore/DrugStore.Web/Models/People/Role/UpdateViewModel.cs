using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.Role
{
    public class UpdateViewModel
    {
        public int IdRole { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
