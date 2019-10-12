using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.People.User
{
    public class UpdateViewModel
    {
        [Required(ErrorMessage = "Please enter id user")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Please enter id role")]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [StringLength(100, ErrorMessage = "Name must not have more than 100 characters.")]
        public string UserName { get; set; }

        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(70, ErrorMessage = "Email must not have more than 70 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Act_Password { get; set; }      
    }
}
