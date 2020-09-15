using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.TMEApiClient.Objects
{
   public class Price
    {
        public int Amount { get; set; }
        public float PriceValue { get; set; }
        public bool Special { get; set; }
    }
}
