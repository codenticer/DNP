using DNP.TMEApiClient.ApiCore;
using DNP.TMEApiClient.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.TMEApiClient
{
    public class ApiClient
    {
        TmeApi tmeApi;
        Dictionary<string, string> apiParams;
        public ApiClient()
        {
            tmeApi = new TmeApi();
            apiParams = new Dictionary<string, string>()
                {
                    { "Country", "GB" },
                    { "Language", "EN"},
                    { "SymbolList[0]", "1N4007" },
                    { "SymbolList[1]", "1/4W1.1M" },
                    { "Token", ApiCredentials.Token },
                };
        }

        public List<object> GetProductParameter()
        {
            try
            {
                // Easy example (dictionary must be ordered by keys)

                string jsonContent = tmeApi.ApiCall("Products/GetParameters", apiParams);

                // Parsing whole .json from content returned by API
                // JObject json = JObject.Parse(jsonContent);
                // Console.WriteLine(json.ToString());
                // Console.WriteLine();

                // Another example with using objects
                //Console.WriteLine("Call api action: Products/GetParameters then parse .json and show result as list in format: \"ProductSymbol\"; \"ParameterName\"; \"ParameterValue\"");
                //        Console.WriteLine();

                // Created class to easily pass arguments
                GetParametersApiCallArgs getProductsArgs = new GetParametersApiCallArgs("GB", "EN", "1N4007", "1/4W1.1M");
                jsonContent = tmeApi.ApiCall("Products/GetParameters", getProductsArgs.BuildApiParams());

                // Checking result
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    // Another way to parse .json
                    // Created class which is equivalent of .json structure depending on documentation https://developers.tme.eu/en/
                    GetParametersJResult result = JsonConvert.DeserializeObject<GetParametersJResult>(jsonContent);
                    return null;
                    // Some function to show easy manipulation on data in custom class
                    //Console.WriteLine(result.GetParametersList());       
                }
                else
                {
                    return null;
                }


            }
            catch
            {
                throw;
            }


        }
        public List<object> GetProductPrices()
        {
            try
            {
                GetPricesApiCallArgs getPricesArgs = new GetPricesApiCallArgs("GB", "EN", "EUR", "1N4007", "1/4W1.1M");
                string jsonContent = tmeApi.ApiCall("Products/GetPrices", getPricesArgs.BuildApiParams());

                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<object> Autocomplete()
        {
            try
            {
                GetAutoComplete getAutoCompleteArgs = new GetAutoComplete("GB", "EN", "diode");
                string jsonContent = tmeApi.ApiCall("Products/Autocomplete", getAutoCompleteArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string SearchProduct(string country, string language, string id)
        {
            try
            {
                GetProductSearch getProductSearchArgs = new GetProductSearch(country, language, id);
                string jsonContent = tmeApi.ApiCall("Products/Search", getProductSearchArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {                    
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<object> SearchParameters()
        {
            try
            {
                GetProductSearch getProductSearchArgs = new GetProductSearch("GB", "EN", "100328");
                string jsonContent = tmeApi.ApiCall("Products/SearchParameters", getProductSearchArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string GetCategories(string country,string language,string id="")
        {
            try
            {                
                GetProductCategories getProductCategoriesArgs = new GetProductCategories(country, language, id);
                string jsonContent = tmeApi.ApiCall("Products/GetCategories", getProductCategoriesArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        //Failed
        public List<object> GetDeliveryTime()
        {
            try
            {
                int[] listOfAmount = { 1, 2 };
                GetDeliveryTime getDeliveryTimeArgs = new GetDeliveryTime("GB", "EN", listOfAmount, "1N4007", "1/4W1.1M");
                string jsonContent = tmeApi.ApiCall("Products/GetDeliveryTime", getDeliveryTimeArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string GetPricesAndStocks(string country, string language,string currency, List<string> listOfSymbol)
        {
            try
            {
                GetPricesAndStocks getPricesAndStocksArgs = new GetPricesAndStocks(country, language, currency, listOfSymbol);
                string jsonContent = tmeApi.ApiCall("Products/GetPricesAndStocks", getPricesAndStocksArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                   // JObject json = JObject.Parse(jsonContent);
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string GetProducts(string country,string language,List<string> listOfSymbol)
        {
            try
            {
                GetProducts getProductsArgs = new GetProducts(country, language, listOfSymbol);
                string jsonContent = tmeApi.ApiCall("Products/GetProducts", getProductsArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    //JObject json = JObject.Parse(jsonContent);
                    //return json.ToString();
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public string GetProductsFiles(string country, string language, List<string> listOfSymbol)
        {
            try
            {
                GetProducts getProductsArgs = new GetProducts(country, language, listOfSymbol);
                string jsonContent = tmeApi.ApiCall("Products/GetProductsFiles", getProductsArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return jsonContent;
                }
                else
                {
                    return null;
                }               
            }
            catch
            {
                throw;
            }
        }
        public List<object> GetStocks()
        {
            try
            {
                //GetProducts getProductsArgs = new GetProducts("GB", "EN", "1N4007", "1/4W1.1M");
                //string jsonContent = tmeApi.ApiCall("Products/GetStocks", getProductsArgs.BuildApiParams());
                //if (IsStatusOK(tmeApi, jsonContent))
                //{
                //    JObject json = JObject.Parse(jsonContent);
                //    return null;
                //}
                //else
                //{
                //    return null;
                //}
                return null;
            }
            catch
            {
                throw;
            }
        }
        public string GetSymbols(string country,string language,string categoryId)
        {
            try
            {
                GetSymbols getSymbolsArgs = new GetSymbols(country, language, categoryId);
                string jsonContent = tmeApi.ApiCall("Products/GetSymbols", getSymbolsArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<object> GetSimilarProducts()
        {
            try
            {
                //GetProducts getProductsArgs = new GetProducts("GB", "EN", "1N4005", "1N4007");
                //string jsonContent = tmeApi.ApiCall("Products/GetSimilarProducts", getProductsArgs.BuildApiParams());
                //if (IsStatusOK(tmeApi, jsonContent))
                //{
                //    JObject json = JObject.Parse(jsonContent);
                //    return null;
                //}
                //else
                //{
                //    return null;
                //}
                return null;
            }
            catch
            {
                throw;
            }
        }
        public string GetRelatedProducts(string country,string language, string symbol)
        {
            try
            {
                GetRelatedProducts getRelatedProductsArgs = new GetRelatedProducts(country, language, symbol);
                string jsonContent = tmeApi.ApiCall("Products/GetRelatedProducts", getRelatedProductsArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<object> GetCountries()
        {
            try
            {
                GetCountries getCountriesArgs = new GetCountries("IN");
                string jsonContent = tmeApi.ApiCall("Utils/GetCountries", getCountriesArgs.BuildApiParams());
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        public List<object> GetLanguages()
        {
            try
            {
                Dictionary<string, string> apiLanguagesParams = new Dictionary<string, string>()
                {
                    { "Token", ApiCredentials.Token },
                };
                string jsonContent = tmeApi.ApiCall("Utils/GetLanguages", apiLanguagesParams);
                if (IsStatusOK(tmeApi, jsonContent))
                {
                    JObject json = JObject.Parse(jsonContent);
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
        private static bool IsStatusOK(TmeApi tmeApi, string jsonContent)
        {
            ErrorJResult errorResult = null;
            if (!tmeApi.IsStatusOK(jsonContent, out errorResult))
            {
                Console.WriteLine($"Api returns error:");
                Console.WriteLine();
                Console.WriteLine($"Status: {errorResult.Status}");
                Console.WriteLine($"Error: {errorResult.Error}");
                return false;
            }

            return true;
        }


    }
}
