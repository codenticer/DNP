using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
    public class RelatedProductData
    {
        public List<ProductList> RelatedProductList { get; set; }
        //public string Language { get; set; }
        //public int Amount { get; set; }
        //public int PageNumber { get; set; }
        //public CategoryList CategoryList { get; set; }
        public ProductList ProductDescription { get; set; }
        public List<string> ProductList { get; set; }
        public int NumberOfPage { get; set; }
        public int PageCount { get; set; }


    }
}
