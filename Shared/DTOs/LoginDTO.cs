using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password field is required.")]
        [Display(Name = "Password")]
        [StringLength(30, ErrorMessage = "Password must be between 6 and 30 characters.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
