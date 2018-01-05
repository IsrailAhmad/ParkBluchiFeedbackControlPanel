using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }

        public string Role { get; set; }

        public string FranchiseeName { get; set; }

    }
}