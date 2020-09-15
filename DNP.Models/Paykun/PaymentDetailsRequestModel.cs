using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNP.Models.Paykun
{
    public class PaymentDetailsRequestModel
    {
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; }
        public double Price { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }

    }
}