using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Paykun
{
   public class TransactionDetailsViewModel
    {
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Mobile { get; set; }
        public string ProductName { get; set; }
        public int UserID { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public string Photo { get; set; }
    }
}
