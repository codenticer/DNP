using DNP.Models.Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Paykun
{
   public class PaykumUserViewModel
    {
        public string Username { get; set; }        
        public string Password { get; set; }        
        public string Email { get; set; }//
        public string Address { get; set; }//                      
        public string PhoneNumber { get; set; }//
        public string ZipCode { get; set; }//
        public int UserId { get; set; }
        public string CompanyName { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactDepartment { get; set; }
        public string ContactPosition { get; set; }
        public string ContactTelephone { get; set; }
        public string ContactFax { get; set; }
        public string ContactMobile { get; set; }
        public string ContactInstantMessaging { get; set; }
        public int ContactPersonId { get; set; }


        public string DeliveryCompanyName { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryTelephone { get; set; }
        public string DeliveryFax { get; set; }
        public string DeliveryLanguage { get; set; }
        public int DeliveryAddressId { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public string ProductName { get; set; }
        public double Amount { get; set; }
        public string FullName { get; set; }
        public CartDetailsViewModel cartDetailsViewModel { get; set; }

    }
}
