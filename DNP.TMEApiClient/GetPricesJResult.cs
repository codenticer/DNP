using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP.TMEApiClient.Objects;

namespace DNP.TMEApiClient
{
    public enum PriceType { NET, GROSS }
    public class GetPricesJResult: TmeApiJResult<GetPricesJResultData>
    {
    }
    public class GetPricesJResultData
    {
        public string Language { get; set; }
        public string Currency { get; set; }
        public PriceType PriceType { get; set; }
        public ProductWithPrices[] ProductList { get; set; }
    }
}
