using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Please Enter your Handle")]
        public string Handle { get; set; }
        [Required(ErrorMessage = "Please Enter Vaild Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Both Passwords should be Identical")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
