using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.ApiCore;

namespace DNP.TMEApiClient
{
   public class GetProductCategories
    {
        public string Country { get; private set; }
        public string Language { get; private set; }
        public string CategoryId { get; private set; } 
        public bool Tree { get; private set; }

        public GetProductCategories(string country, string language,string categoryId="")
        {
            this.Country = country;
            this.Language = language;
            this.CategoryId = categoryId;
           // this.Tree = false;
        }

        public Dictionary<string, string> BuildApiParams()
        {
            // Dictionary must be ordered by keys
            Dictionary<string, string> apiParams = new Dictionary<string, string>();
            if (this.CategoryId != "")
            {
                apiParams.Add("CategoryId", this.CategoryId);
            }
            apiParams.Add("Country", this.Country);
            apiParams.Add("Language", this.Language);
            
            //apiParams.Add("Tree",this.Tree.ToString());            
            //apiParams.Add("CategoryId", Convert.ToString(this.CategoryId));

            apiParams.Add("Token", ApiCredentials.Token);
            
            return apiParams;
        }
    }
}
