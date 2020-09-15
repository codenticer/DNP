using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using DNP.Models.Paykun;

namespace DNP.Models.Users
{
   public class UsersViewModel
    {
        public UserRegistrationViewModel userRegistrationViewModel { get; set; }
        public ContactPersonViewModel contactPersonViewModel { get; set; }
        public DeliveryAddressViewModel deliveryAddressViewModel { get; set; }
        [Required(ErrorMessage = "Enter your first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter your last name")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string OTP { get; set; }
        public DateTime OTPDateTime { get; set; }
        public TransactionDetailsViewModel transactionDetailsViewModel { get; set; }
        
    }
}
