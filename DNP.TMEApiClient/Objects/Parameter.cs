using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.TMEApiClient.Objects
{
   public class Parameter
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValueId { get; set; }
        public string ParameterValue { get; set; }
    }
}
