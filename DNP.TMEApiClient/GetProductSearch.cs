using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetProductSearch
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string SearchPlain { get; private set; }
        public string SearchCategory { get; private set; }
        public int SearchPage { get; private set; }
        public bool SearchWithStock { get; private set; }
        public string SearchOrder { get; private set; }
        public string SearchOrderType { get; private set; }
        

        public GetProductSearch(string country, string language, string searchCategory)
        {
            this.Country = country;
            this.Language = language;
           // this.SearchPlain = searchPlain;
            this.SearchCategory = searchCategory;
            //this.SearchPage = searchPage;
           // this.SearchOrder = searchOrder;

        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            apiParams.Add("Country", this.Country);           
            apiParams.Add("Language", this.Language);
            //apiParams.Add("SearchCategory", this.SearchCategory);
            //apiParams.Add("SearchOrder", this.SearchOrder);
            //apiParams.Add("SearchPage", Convert.ToString(this.SearchPage));
            //apiParams.Add("SearchOrder", this.SearchOrder);

            apiParams.Add("Token", ApiCredentials.Token);
            return apiParams;
        }
    }
}
