using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
   public class ParameterList
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public int ParameterValueId { get; set; }
        public string ParameterValue { get; set; }
    }
}
