using System;
using System.Collections.Generic;
using System.Text;

namespace DrugStore.Entities.Users
{
    public class Role
    {
        public int IdRole { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public Boolean Condition { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
