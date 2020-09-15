using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
    public class Data
    {
        //public List<ProductList> ProductList { get; set; }
        //public string Language { get; set; }
        //public int Amount { get; set; }
        //public int PageNumber { get; set; }
        //public CategoryList CategoryList { get; set; }
        public List<ProductList> ProductList { get; set; }  
        //public List<string> productList { get; set; }
        public string Language { get; set; }
        public CategoryTree CategoryTree { get; set; }
        public List<string> SymbolList { get; set; }
        public int NumberOfPage { get; set; }
        public int PageCount { get; set; }
    }
}
