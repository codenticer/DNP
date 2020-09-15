using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
   public class ProductList
    {
        public string Symbol { get; set; }
        public string CustomerSymbol { get; set; }
        public string OriginalSymbol { get; set; }
        public string Producer { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string Photo { get; set; }
        public string Thumbnail { get; set; }
        public double Weight { get; set; }
        public string WeightUnit { get; set; }
        public int SuppliedAmount { get; set; }
        public int MinAmount { get; set; }
        public int Multiples { get; set; }
        public List<string> ProductStatusList { get; set; }
        public string Unit { get; set; }
        public string ProductInformationPage { get; set; }
        public object Guarantee { get; set; }
        public object OfferId { get; set; }
        public List<ParameterList> ParameterList { get; set; }

        public List<PriceList> PriceList { get; set; }
        public int VatRate { get; set; }
        public string VatType { get; set; }
        public int Amount { get; set; }
        public int ProductCount { get; set; }
        public double BaseAmount { get; set; }
        public double TotalPriceQty { get; set; }
        
        public Files Files { get; set; }
    }
}
