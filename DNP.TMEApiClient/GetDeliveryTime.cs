using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetDeliveryTime
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string[] SymbolList { get; private set; }
        public int[] AmountList { get; private set; }        

        public GetDeliveryTime(string country, string language, int[] amountList, params string[] symbolList)
        {
            this.Country = country;
            this.Language = language;
            this.SymbolList = symbolList;
            this.AmountList = amountList;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("Country", this.Country);
            apiParams.Add("Language", this.Language);
            for (int i = 0; i < this.SymbolList.Length; i++)
            {
                apiParams.Add($"SymbolList[{i}]", this.SymbolList[i]);
            }
            for (int i = 0; i < this.AmountList.Length; i++)
            {
                apiParams.Add($"AmountList[{i}]", Convert.ToString(this.AmountList[i]));
            }

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
