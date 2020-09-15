using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
   public class PriceList
    {
        public int Amount { get; set; }
        public double PriceValue { get; set; }
        public int PriceBase { get; set; }
        public bool Special { get; set; }
    }
}
