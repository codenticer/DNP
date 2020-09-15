using DNP.Models.Shopping;
using DNP.Repository.ProductDetailsRepositry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNP.WebApplication.Controllers
{
    
    public class ShoppingController : Controller
    {
        private readonly IProductDetailsService _IProductDetailsService;
        public ShoppingController(IProductDetailsService IProductDetailsService)
        {
            _IProductDetailsService = IProductDetailsService;
        }
        // GET: Shopping
        public ActionResult shop(int? pageCount)
        {
            pageCount= pageCount == null ? 1 : pageCount;
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel=_IProductDetailsService.GetPoductDetails(pageCount);
            mainClassViewModel.Data.NumberOfPage = mainClassViewModel.Data.SymbolList.Count / 50;
            return View(mainClassViewModel);
        }
        
        // Get all sub category by category id.
        public ActionResult Product(string id)
        {
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.GetCategoriesById(id);
            return View(mainClassViewModel);
        }

        public ActionResult Search(string id)
        {
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.SearchProduct(id);
            return View(mainClassViewModel);
        }

        //[HttpPost]
        //public ActionResult AddToCart(int quantity)
        //{
        //    int sessionCount = 0;
        //    if (Session["CartCount"] == null)
        //    {
        //        Session["CartCount"] = 1;
        //        sessionCount = 1;
        //    }
        //    else
        //    {
        //        sessionCount = Convert.ToInt32(Session["CartCount"]);
        //        sessionCount += 1;
        //        Session["CartCount"] = sessionCount;
        //    }
        //    ViewBag.CartCounts = Session["CartCount"];


        //    return Json(sessionCount, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult AutoComplete()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            //Note : you can bind same list from database  
            List<SubTree> ObjList = new List<SubTree>()
            {

                new SubTree {Id="1",Name="Latur" },
                new SubTree {Id="2",Name="Mumbai" },
                new SubTree {Id="3",Name="Pune" },
                new SubTree {Id="4",Name="Delhi" },
                new SubTree {Id="5",Name="Dehradun" },
                new SubTree {Id="6",Name="Noida" },
                new SubTree {Id="7",Name="New Delhi" }

        };
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.Name.ToLower().StartsWith(Prefix)
                            select new { N.Name });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductList(string id, int? pageCount)
        {
            pageCount = pageCount == null ? 1 : pageCount;
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.GetPoductDetailsById(id.TrimEnd(),pageCount);
            if (mainClassViewModel.Data.SymbolList.Count % 50 > 0)
            {
                mainClassViewModel.Data.NumberOfPage = (mainClassViewModel.Data.SymbolList.Count / 50) + 1;
            }
            else
            {
                mainClassViewModel.Data.NumberOfPage = mainClassViewModel.Data.SymbolList.Count / 50;
            }
            mainClassViewModel.Data.PageCount = Convert.ToInt32(pageCount);
            return View(mainClassViewModel);

        }
        public ActionResult ProductDetails(string id, int? pageCount)
        {
            pageCount = pageCount == null ? 1 : pageCount;
            RelatedProductViewModel relatedProductViewModel = new RelatedProductViewModel();
            relatedProductViewModel = _IProductDetailsService.RelatedProduct(id, pageCount);
            relatedProductViewModel.Data.PageCount = Convert.ToInt32(pageCount);
            if (relatedProductViewModel.Data.ProductList.Count % 50 > 0)
            {
                relatedProductViewModel.Data.NumberOfPage = (relatedProductViewModel.Data.ProductList.Count / 50) + 1;
            }
            else
            {
                relatedProductViewModel.Data.NumberOfPage = relatedProductViewModel.Data.ProductList.Count / 50;
            }
            
            return View(relatedProductViewModel);

        }
        public PartialViewResult SideMenu()
        {
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.GetCategories();
            return PartialView("_SideMenu", mainClassViewModel);
        }
        public PartialViewResult SideMenuItem(string id)
        {
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.GetCategoriesById(id);
            return PartialView("_SideMenuItem", mainClassViewModel);
        }

        public PartialViewResult MainPageSideMenu()
        {
            MainClassViewModel mainClassViewModel = new MainClassViewModel();
            mainClassViewModel = _IProductDetailsService.GetCategories();
            return PartialView("_MainPageSideMenu", mainClassViewModel);
        }

        public ActionResult CartDetails()
        {
            CartDetailsViewModel cartDetailsViewModel = new CartDetailsViewModel();
            cartDetailsViewModel = (CartDetailsViewModel)Session["_CartValue"];
            if (cartDetailsViewModel != null)
            {
                foreach(var data in cartDetailsViewModel.ProductList)
                {
                    foreach(var item in data.PriceList.OrderByDescending(x=>x.Amount).ToList())
                    {
                        if(data.ProductCount>=item.Amount)
                        {
                            //data.BaseAmount = (item.PriceValue / item.Amount) + (item.PriceValue % item.Amount);
                            data.BaseAmount = item.PriceValue;
                            break;
                        }
                        
                    }
                }
                Session["CartCount"] = cartDetailsViewModel.ProductList.Sum(x => Convert.ToInt32(x.ProductCount));
            }
            return View(cartDetailsViewModel);
        }

        [HttpPost]
        public JsonResult AddToCart(string id,int productCount)
        {
            //double productTotalPrice=0.0;
            if (Session["_CartValue"]==null)
            {
                CartDetailsViewModel cartDetailsViewModel = _IProductDetailsService.GetPoductDetailsBySymbol(id);
                //for(int i=0; i< cartDetailsViewModel.ProductList[i].PriceList.Count;i++)
                //{
                //   if(productCount>= cartDetailsViewModel.ProductList[i].PriceList[i].Amount && productCount <= cartDetailsViewModel.ProductList[i+1].PriceList[i+1].Amount)
                //    {
                //        productTotalPrice = productTotalPrice+ (productCount * cartDetailsViewModel.ProductList[i].PriceList[i].PriceValue);
                //    }
                //}
                cartDetailsViewModel.ProductList[0].ProductCount = productCount;
                Session["CartCount"] = cartDetailsViewModel.ProductList.Sum(x => Convert.ToInt32(x.ProductCount));
                Session["_CartValue"] = cartDetailsViewModel;
            }
            else
            {
                CartDetailsViewModel cartDetailsViewModel = (CartDetailsViewModel)Session["_CartValue"];
                if(cartDetailsViewModel.ProductList.Any(x=>x.Symbol==id))
                {
                    foreach(var item in cartDetailsViewModel.ProductList.Where(x=>x.Symbol==id))
                    {
                        item.ProductCount += productCount;
                    }
                    Session["CartCount"] = cartDetailsViewModel.ProductList.Sum(x => Convert.ToInt32(x.ProductCount));
                }
                else
                {
                    var productDetails = _IProductDetailsService.GetPoductDetailsBySymbol(id);
                    productDetails.ProductList[0].ProductCount = productCount;
                    cartDetailsViewModel.ProductList.Add(productDetails.ProductList[0]);
                    Session["CartCount"] = cartDetailsViewModel.ProductList.Sum(x => Convert.ToInt32(x.ProductCount));
                    Session["_CartValue"] = cartDetailsViewModel;
                }
               
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(string id)
        {
            if (Session["_CartValue"] != null)
            {
                CartDetailsViewModel cartDetailsViewModel = (CartDetailsViewModel)Session["_CartValue"];
                
                foreach (var item in cartDetailsViewModel.ProductList.Where(x => x.Symbol == id))
                {
                    cartDetailsViewModel.ProductList.Remove(item);
                    break;
                }
                Session["CartCount"] = cartDetailsViewModel.ProductList.Sum(x => Convert.ToInt32(x.ProductCount));
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowPdf()
        {
            if (System.IO.File.Exists(Server.MapPath("www.tme.eu/Document/1d56b131325d12217d6344d8d359dc21/1.5KExxxA-taiwan-DTE.pdf")))
            {
                string pathSource = Server.MapPath("https://www.tme.eu/Document/1d56b131325d12217d6344d8d359dc21/1.5KExxxA-taiwan-DTE.pdf");
                FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);

                return new FileStreamResult(fsSource, "application/pdf");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}