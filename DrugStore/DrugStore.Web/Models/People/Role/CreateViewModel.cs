using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.Role
{
    public class CreateViewModel
    {
        [Required(ErrorMessage = "Please enter role name")]
        [StringLength(30, ErrorMessage = "Name must not have more than 30 characters.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        [StringLength(100, ErrorMessage = "Name must not have more than 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        public bool State { get; set; }
    }
}
