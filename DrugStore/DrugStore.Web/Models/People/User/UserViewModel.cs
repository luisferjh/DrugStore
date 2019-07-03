using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.User
{
    public class UserViewModel
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }     
        public bool Condition { get; set; }
    }
}
