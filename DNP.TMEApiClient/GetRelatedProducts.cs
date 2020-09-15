using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetRelatedProducts
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string Symbol { get; private set; }       
        
        public GetRelatedProducts(string country, string language, string symbol)
        {
            this.Country = country;
            this.Language = language;
            this.Symbol = symbol;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("Country", this.Country);            
            apiParams.Add("Language", this.Language);
            apiParams.Add("Symbol", this.Symbol);

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
