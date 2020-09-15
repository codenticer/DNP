using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetPricesAndStocks
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string Currency { get; private set; }
        public bool GrossPrices { get; private set; }
        public List<string> SymbolList { get; private set; }

        public GetPricesAndStocks(string country, string language, string currency, List<string> symbolList)
        {
            this.Country = country;
            this.Language = language;
            this.Currency = currency;
            this.SymbolList = symbolList;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("Country", this.Country);
            apiParams.Add("Currency", this.Currency);
            apiParams.Add("Language", this.Language);            
            for (int i = 0; i < SymbolList.Count; i++)
            {
                apiParams.Add($"SymbolList[{i}]", this.SymbolList[i]);
            }

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
