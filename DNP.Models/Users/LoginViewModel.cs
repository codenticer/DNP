using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Users
{
   public class LoginViewModel
    {        
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }        
    }
}
