using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.User
{
    public class UserProfileViewModel
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
