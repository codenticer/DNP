using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword",ErrorMessage ="Confirm password must be same as new password")]
        public string ConfirmPassword { get; set; }

    }
}
