using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetAutoComplete
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string Phrase { get; private set; }        

        public GetAutoComplete(string country, string language, string phrase)
        {
            this.Country = country;
            this.Language = language;
            this.Phrase = phrase;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("Country", this.Country);           
            apiParams.Add("Language", this.Language);
            apiParams.Add("Phrase", this.Phrase);

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
