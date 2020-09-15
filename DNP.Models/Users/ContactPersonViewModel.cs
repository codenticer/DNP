using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace DNP.Models.Users
{
   public class ContactPersonViewModel
    {        
        public string ContactFirstName { get; set; }        
        public string ContactLastName { get; set; }
        public string ContactDepartment { get; set; }
        public string ContactPosition { get; set; }
        public string ContactTelephone { get; set; }
        public string ContactFax { get; set; }
        public string ContactMobile { get; set; }
        public string ContactInstantMessaging { get; set; }
        //public string InstantMessaging { get; set; }
        //[Required(ErrorMessage = "Enter your username")]
        //public string Username { get; set; }
        //[Required(ErrorMessage = "Enter your password")]
        //public string Password { get; set; }
        //[Required(ErrorMessage = "Enter your email address")]
        //[EmailAddress]
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public string ZipCode { get; set; }
        //public string Address { get; set; }
        //public int UserId { get; set; }
        //public int CountryId { get; set; }
        //public int StateId { get; set; }
        //public int CityId { get; set; }
        //public string OTP { get; set; }
        //public DateTime OTPDateTime { get; set; }
    }
}
