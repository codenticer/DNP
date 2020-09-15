using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;

namespace DNP.Models.Users
{
   public class DeliveryAddressViewModel
    {        
        public string DeliveryCompanyName { get; set; }        
        public string DeliveryCountry { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryTelephone { get; set; }
        public string DeliveryFax { get; set; }
        public string DeliveryLanguage { get; set; }        
    }
}
