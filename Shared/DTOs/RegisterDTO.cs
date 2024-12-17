using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name ="Email")]
        public string Email { get; set; }  
        
        [Required(ErrorMessage = "Email field is required.")]   
        [Display(Name ="UserName")]
        [StringLength(50,ErrorMessage = "Username must be between 5 and 50 characters.",MinimumLength =5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field is required.")]   
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirm field is required.")]
        [Display(Name = "Password Confirm")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}
