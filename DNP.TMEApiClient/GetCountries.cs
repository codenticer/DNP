using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetCountries
    {        
        public string Language { get; private set; }
        
        public GetCountries(string language)
        {           
            this.Language = language;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();                        
            apiParams.Add("Language", this.Language);

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
