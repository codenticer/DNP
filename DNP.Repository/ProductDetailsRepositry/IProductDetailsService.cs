using DNP.Models.Shopping;
using DNP.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Repository.ProductDetailsRepositry
{
    public interface IProductDetailsService
    {
        MainClassViewModel GetPoductDetails(int? pageCount);
        MainClassViewModel SearchProduct(string id);
        MainClassViewModel GetPoductDetailsById(string id, int? pageCount);
        MainClassViewModel GetCategories();
        RelatedProductViewModel RelatedProduct(string symbol, int? pageCount);
        CartDetailsViewModel GetPoductDetailsBySymbol(string symbol);
        MainClassViewModel GetCategoriesById(string id);
        MainClassViewModel GetPriceAndStock(List<string> symbolList, int? pageCount);
        MainClassViewModel GetProductsFiles(List<string> symbolList, int? pageCount);
    }
}
