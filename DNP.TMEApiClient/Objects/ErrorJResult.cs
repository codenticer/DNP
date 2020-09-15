using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.TMEApiClient.Objects
{
  public  class ErrorJResult
    {
        public string Status { get; private set; }
        public string Error { get; private set; }

        public ErrorJResult(string status, string errorMessage)
        {
            this.Status = status;
            this.Error = errorMessage;
        }
    }
}
