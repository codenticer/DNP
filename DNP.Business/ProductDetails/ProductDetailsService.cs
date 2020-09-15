using DNP.Data;
using DNP.Models.Shopping;
using DNP.Repository.ProductDetailsRepositry;
using DNP.TMEApiClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Business.ProductDetails
{
    public class ProductDetailsService : IProductDetailsService
    {
        public string Country = "IN";
        public string Language = "EN";
        public string Currency = "USD";

        public MainClassViewModel GetCategories()
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                string result = apiClient.GetCategories(Country, Language);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    JObject json = JObject.Parse(result);
                    mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                    return mainClassViewModel;
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetCategoriesById(string id)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                string result = apiClient.GetCategories(Country, Language, id);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    JObject json = JObject.Parse(result);
                    mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                    return mainClassViewModel;
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetSymbol()
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                var categories = GetCategories();
                if (categories.Status == "OK")
                {
                    string result = apiClient.GetSymbols(Country, Language, categories.Data.CategoryTree.Id);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        //JObject json = JObject.Parse(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel.Data.CategoryTree = categories.Data.CategoryTree;
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetSymbolByCategorieId(string id)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                var categories = GetCategoriesById(id);
                if (categories.Status == "OK")
                {
                    string result = apiClient.GetSymbols(Country, Language, categories.Data.CategoryTree.Id);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        //JObject json = JObject.Parse(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel.Data.CategoryTree = categories.Data.CategoryTree;
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetPoductDetails(int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                var symbols = GetSymbol();
                string result = string.Empty;
                int skipProduct;
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                if (symbols.Status == "OK")
                {
                    if (pageCount == 1)
                    {
                        result = apiClient.GetProducts(Country, Language, symbols.Data.SymbolList.Take(50).ToList());
                    }
                    else
                    {
                        skipProduct = Convert.ToInt32((pageCount - 1) * 50);
                        result = apiClient.GetProducts(Country, Language, symbols.Data.SymbolList.Skip(skipProduct).Take(50).ToList());
                    }
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel.Data.SymbolList = symbols.Data.SymbolList;
                        mainClassViewModel.Data.CategoryTree = symbols.Data.CategoryTree;
                        var priceAndStock = GetPriceAndStock(symbols.Data.SymbolList, pageCount);
                        for (int i = 0; i < priceAndStock.Data.ProductList.Count; i++)
                        {
                            mainClassViewModel.Data.ProductList[i].Unit = priceAndStock.Data.ProductList[i].Unit;
                            mainClassViewModel.Data.ProductList[i].VatRate = priceAndStock.Data.ProductList[i].VatRate;
                            mainClassViewModel.Data.ProductList[i].VatType = priceAndStock.Data.ProductList[i].VatType;
                            mainClassViewModel.Data.ProductList[i].Amount = priceAndStock.Data.ProductList[i].Amount;
                            mainClassViewModel.Data.ProductList[i].PriceList = priceAndStock.Data.ProductList[i].PriceList;
                        }
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public CartDetailsViewModel GetPoductDetailsBySymbol(string symbol)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                string result = string.Empty;
                List<string> lstSymbol = new List<string>();
                lstSymbol.Add(symbol);
                CartDetailsViewModel cartDetailsViewModel = new CartDetailsViewModel();
                if (lstSymbol.Count > 0)
                {

                    result = apiClient.GetProducts(Country, Language, lstSymbol);

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        var data = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        //mainClassViewModel.Data.SymbolList = symbols.Data.SymbolList;
                        //mainClassViewModel.Data.CategoryTree = symbols.Data.CategoryTree;
                        cartDetailsViewModel.ProductList = data.Data.ProductList;
                        string priceAndStockResult = apiClient.GetPricesAndStocks(Country, Language,Currency,lstSymbol);
                        if (!string.IsNullOrWhiteSpace(priceAndStockResult))
                        {
                            var priceData = JsonConvert.DeserializeObject<MainClassViewModel>(priceAndStockResult);
                            for (int i = 0; i < priceData.Data.ProductList.Count; i++)
                            {
                                cartDetailsViewModel.ProductList[i].Unit = priceData.Data.ProductList[i].Unit;
                                cartDetailsViewModel.ProductList[i].VatRate = priceData.Data.ProductList[i].VatRate;
                                cartDetailsViewModel.ProductList[i].VatType = priceData.Data.ProductList[i].VatType;
                                cartDetailsViewModel.ProductList[i].Amount = priceData.Data.ProductList[i].Amount;
                                cartDetailsViewModel.ProductList[i].PriceList = priceData.Data.ProductList[i].PriceList;
                            }
                        }
                       
                        return cartDetailsViewModel;
                    }
                    else
                    {
                        return cartDetailsViewModel;
                    }
                }
                else
                {
                    return cartDetailsViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetPriceAndStock(List<string> symbolList, int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                //var symbols = GetSymbol();
                string result;
                int skipProduct;
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                if (symbolList.Count > 0)
                {
                    if (pageCount == 1)
                    {
                        result = apiClient.GetPricesAndStocks(Country, Language, Currency, symbolList.Take(50).ToList());
                    }
                    else
                    {
                        skipProduct = Convert.ToInt32((pageCount - 1) * 50);
                        result = apiClient.GetPricesAndStocks(Country, Language, Currency, symbolList.Skip(skipProduct).Take(50).ToList());
                    }
                    //string result = apiClient.GetPricesAndStocks(Country, Language, Currency, symbolList.Skip(50).Take(50).ToList());
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MainClassViewModel SearchProduct(string id)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                string result = apiClient.SearchProduct(Country,Language,id);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    JObject json = JObject.Parse(result);
                   var category= GetCategoriesById(id);
                    mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                    string priceResult = apiClient.GetPricesAndStocks(Country, Language, Currency,mainClassViewModel.Data.ProductList.Select(x=>x.Symbol).ToList());
                    var priceItem= JsonConvert.DeserializeObject<MainClassViewModel>(priceResult);
                    if (priceItem.Data != null)
                    {                        
                        var res = mainClassViewModel.Data.ProductList.Join(priceItem.Data.ProductList, a => a.Symbol, b => b.Symbol, (a, b) => new {b.PriceList }).ToList();
                        //mainClassViewModel.Data.ProductList = res;
                        if (category.Data != null)
                        {
                            mainClassViewModel.Data.CategoryTree = category.Data.CategoryTree;
                        }
                    }
                    return mainClassViewModel;
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public RelatedProductViewModel RelatedProduct(string symbol, int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                RelatedProductViewModel relatedProductViewModel = new RelatedProductViewModel();
                string result = apiClient.GetRelatedProducts(Country, Language, symbol);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    JObject json = JObject.Parse(result);
                    relatedProductViewModel = JsonConvert.DeserializeObject<RelatedProductViewModel>(result);
                    MainClassViewModel mainClassViewModel = GetRelatedPoductById(relatedProductViewModel.Data.ProductList, pageCount);
                    var productDetails = GetPoductDetailsBySymbol(symbol);
                    relatedProductViewModel.Data.RelatedProductList = mainClassViewModel.Data.ProductList;
                    relatedProductViewModel.Data.ProductDescription = productDetails.ProductList[0];
                    return relatedProductViewModel;
                }
                else
                {
                    return relatedProductViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MainClassViewModel GetPoductDetailsById(string id, int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                var symbols = GetSymbolByCategorieId(id);
                //var categories = GetCategoriesById(id);
                string result = string.Empty;
                int skipProduct;
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                if (symbols.Status == "OK")
                {
                    if (pageCount == 1)
                    {
                        result = apiClient.GetProducts(Country, Language, symbols.Data.SymbolList.Take(50).ToList());
                    }
                    else
                    {
                        skipProduct = Convert.ToInt32((pageCount - 1) * 50);
                        result = apiClient.GetProducts(Country, Language, symbols.Data.SymbolList.Skip(skipProduct).Take(50).ToList());
                    }
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                       // JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel.Data.SymbolList = symbols.Data.SymbolList;
                        mainClassViewModel.Data.CategoryTree = symbols.Data.CategoryTree;
                        var priceAndStock = GetPriceAndStock(symbols.Data.SymbolList, pageCount);
                        var productFile = GetProductsFiles(symbols.Data.SymbolList, pageCount);
                        for (int i=0;i< mainClassViewModel.Data.ProductList.Count;i++)
                        {
                            mainClassViewModel.Data.ProductList[i].PriceList = priceAndStock.Data.ProductList[i].PriceList;
                            mainClassViewModel.Data.ProductList[i].Files = productFile.Data.ProductList[i].Files;
                        }
                        using (var context = new DNPDBEntities())
                        {
                            var customPrice = context.spGetProductPriceBySymbol();
                           
                        }

                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public MainClassViewModel GetRelatedPoductById(List<string> symbolList, int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {
                //var symbols = GetSymbolByCategorieId(id);
                //var categories = GetCategoriesById(id);
                string result = string.Empty;
                int skipProduct;
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                if (symbolList.Count > 0)
                {
                    if (pageCount == 1)
                    {
                        result = apiClient.GetProducts(Country, Language, symbolList.Take(50).ToList());
                    }
                    else
                    {
                        skipProduct = Convert.ToInt32((pageCount - 1) * 50);
                        result = apiClient.GetProducts(Country, Language, symbolList.Skip(skipProduct).Take(50).ToList());
                    }
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        //mainClassViewModel.Data.SymbolList = symbols.Data.SymbolList;
                        //mainClassViewModel.Data.CategoryTree = symbols.Data.CategoryTree;
                        //var priceAndStock = GetPriceAndStock(symbols.Data.SymbolList, pageCount);
                        //for (int i = 0; i < priceAndStock.Data.ProductList.Count; i++)
                        //{
                        //    mainClassViewModel.Data.ProductList[i].Unit = priceAndStock.Data.ProductList[i].Unit;
                        //    mainClassViewModel.Data.ProductList[i].VatRate = priceAndStock.Data.ProductList[i].VatRate;
                        //    mainClassViewModel.Data.ProductList[i].VatType = priceAndStock.Data.ProductList[i].VatType;
                        //    mainClassViewModel.Data.ProductList[i].Amount = priceAndStock.Data.ProductList[i].Amount;
                        //    mainClassViewModel.Data.ProductList[i].PriceList = priceAndStock.Data.ProductList[i].PriceList;
                        //}
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public MainClassViewModel GetProductsFiles(List<string> symbolList, int? pageCount)
        {
            ApiClient apiClient = new ApiClient();
            try
            {                
                string result;
                int skipProduct;
                MainClassViewModel mainClassViewModel = new MainClassViewModel();
                if (symbolList.Count > 0)
                {
                    if (pageCount == 1)
                    {
                        result = apiClient.GetProductsFiles(Country, Language, symbolList.Take(50).ToList());
                    }
                    else
                    {
                        skipProduct = Convert.ToInt32((pageCount - 1) * 50);
                        result = apiClient.GetProductsFiles(Country, Language, symbolList.Skip(skipProduct).Take(50).ToList());
                    }
                    
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                       // JObject json = JObject.Parse(result);
                        //var myDetails = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        mainClassViewModel = JsonConvert.DeserializeObject<MainClassViewModel>(result);
                        return mainClassViewModel;
                    }
                    else
                    {
                        return mainClassViewModel;
                    }
                }
                else
                {
                    return mainClassViewModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
