using DNP.Business.Account;
using DNP.Models.Paykun;
using DNP.Models.Shopping;
using DNP.Models.Users;
using DNP.Repository.AccountRepositry;
using DNP.Repository.PaykumRepositry;
using DNP.Repository.ProductDetailsRepositry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNP.WebApplication.Controllers
{
    
    public class PayKunController : Controller
    {        
        private readonly IPaykumService _IPaykumService;
        private readonly IAccountService _IAccountSerice;
        public PayKunController(IPaykumService IPaykumService, IAccountService IAccountService)
        {
            _IPaykumService = IPaykumService;
            _IAccountSerice = IAccountService;
        }
        public string Message { get; set; }
        // GET: PayKun
        public ActionResult ProcessPayment()
        {
            string email = Session["_LogInSession"].ToString();
            PaykumUserViewModel paykumUserViewModel = new PaykumUserViewModel();
            var cartDetails = (CartDetailsViewModel)Session["_CartValue"];
            paykumUserViewModel = _IPaykumService.GetUserDetailsByEmail(email);
            Session["_UserId"] = paykumUserViewModel.UserId;
            string productName = string.Empty;
            //double totalAmount = 0;
            //double amount = 0;
            //double totalAmountPerUnit = 0.0;
            //double basePrice = 0.0;
            //for (int proCount= 0;proCount< cartDetails.ProductList.Count;proCount++)
            //{

            //    foreach (var price in cartDetails.ProductList[proCount].PriceList)
            //    {
            //        if(cartDetails.ProductList[proCount].ProductCount>= price.Amount)
            //        {
            //            basePrice = price.PriceValue;
            //        }
            //    }
            //    cartDetails.ProductList[proCount].BaseAmount = basePrice;
            //    cartDetails.ProductList[proCount].TotalPriceQty = cartDetails.ProductList[proCount].BaseAmount * cartDetails.ProductList[proCount].ProductCount;
            //}

            foreach (var item in cartDetails.ProductList)
            {
                productName += item.Symbol + ",";
                //amount = item.BaseAmount * item.ProductCount;
                //totalAmount += amount;
            }
            paykumUserViewModel.ProductName = productName.Remove(productName.Length - 1, 1);
            //paykumUserViewModel.Amount = totalAmount;
            paykumUserViewModel.cartDetailsViewModel = cartDetails;
            return View(paykumUserViewModel);
        }

        [HttpPost]
        public JsonResult ProcessPayment(PaymentDetailsRequestModel paymentDetailsRequestModel)
        {
            //Response.Write(name.Text);
            string _orderId = "ORD" + (new Random()).Next(111111111, 999999999).ToString();
            //Your return success page url
            string successUrl = ConfigurationManager.AppSettings["SuccessURL"];
            string failureUrl = ConfigurationManager.AppSettings["FailureURL"];
            //string _successUrl = "http://localhost:53209/Paykun/Success";

            //string _failureUrl = "http://localhost:53209/Paykun/Failed";

            // Change _isLive to false for sandbox mode, While using sandbox mode you will need to provide credintials for sandbox and not of live environment
            PaykunPayment _payment = new PaykunPayment("503915034147269", "C24E2FB3F6AACA4CBB4641163E605A08", "82DBFB702389FD4CCF01CC6DCA41DABF", _isLive: false);

            //Mandatory //Set 3d currency code as per your requirement like 'INR', 'USD', 'AUD' default is 'INR'
            _payment.InitOrder(_orderId, paymentDetailsRequestModel.Price, paymentDetailsRequestModel.ProductName, successUrl, failureUrl, currency: "INR");

            //Mandatory
            _payment.AddCustomer(paymentDetailsRequestModel.Name, paymentDetailsRequestModel.Email, paymentDetailsRequestModel.ContactNumber);

            //Add here your shipping detail (Optional)
            //If you want to ignore the shipping or billing address, just make all the params an empty string like 
            _payment.AddShippingAddress("", "", "", "", "");
            //_payment.AddShippingAddress("address", "country", "state", "city", "pincode");

            //Add here your billing detail (Optional)
            //If you want to ignore the shipping or billing address, just make all the params an empty string like 
            _payment.AddBillingAddress("", "", "", "", "");
            //_payment.AddBillingAddress("address", "country", "state", "city", "pincode");

            //You can set your custom fields here. for ex. you can set order id for which this transaction is initiated
            //You will get the same order id when you will call the method  _payment.GetTransactionStatus(_reqId)
            _payment.SetCustomField(_orderId, "", "", "", "");

            Message = _payment.Submit();
            //Response.Write(Message);
            // PaymentinnerForm.Text = Message;
            return Json(Message);
        }

        public ActionResult Success()
        {
            string successData = string.Empty;
            string _reqId = this.Request.Params.Get("payment-id");
            //string controlId = this.FindControl(this.Request.Params.Get("__EVENTTARGET")).ID
            //string _reqId = HttpContext.Request.Query["payment-id"].ToString();
            PaykunPayment _payment = new PaykunPayment("503915034147269", "C24E2FB3F6AACA4CBB4641163E605A08", "82DBFB702389FD4CCF01CC6DCA41DABF", _isLive: false);   // Change _isLive to false for sandbox mode, While using sandbox mode you will need to provide credintials for sandbox and not of live environment
            TransactionStatusRes transRes = _payment.GetTransactionStatus(_reqId);
            if (transRes.status == true && transRes.data.transaction.status == "Success")
            {
                TransactionDetailsViewModel transactionDetailsViewModel = new TransactionDetailsViewModel();
                transactionDetailsViewModel.TransactionId = transRes.data.transaction.payment_id;
                transactionDetailsViewModel.Amount = transRes.data.transaction.order.gross_amount;
                transactionDetailsViewModel.OrderId = transRes.data.transaction.order.order_id;
                transactionDetailsViewModel.CustomerName = transRes.data.transaction.customer.name;
                transactionDetailsViewModel.CustomerEmail = transRes.data.transaction.customer.email_id;
                transactionDetailsViewModel.Mobile = transRes.data.transaction.customer.mobile_no;
                transactionDetailsViewModel.ProductName = transRes.data.transaction.order.product_name;
                transactionDetailsViewModel.UserID = Convert.ToInt32(Session["_UserId"]);
                transactionDetailsViewModel.PaymentStatus = "Success";
                transactionDetailsViewModel.TransactionId = transRes.data.transaction.payment_id;
                _IPaykumService.CreateTransactionDetails(transactionDetailsViewModel);
                var cartDetails = (CartDetailsViewModel)Session["_CartValue"];                
                foreach (var item in cartDetails.ProductList)
                {
                    transactionDetailsViewModel.ProductName = item.Symbol;
                    transactionDetailsViewModel.Photo = item.Thumbnail;
                    transactionDetailsViewModel.Amount = item.Amount;
                    _IPaykumService.CreateUserOrderDetails(transactionDetailsViewModel);                    
                }
                UsersViewModel usersViewModel = new UsersViewModel();
                usersViewModel.Email = transactionDetailsViewModel.CustomerEmail;
                usersViewModel.transactionDetailsViewModel = transactionDetailsViewModel;
                _IAccountSerice.SendEmailAsync(usersViewModel,"Order Summary Details");
                return View(transRes);

            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult Failed()
        {
            string _reqId = this.Request.Params.Get("payment-id");
            //string controlId = this.FindControl(this.Request.Params.Get("__EVENTTARGET")).ID
            //string _reqId = HttpContext.Request.Query["payment-id"].ToString();
            PaykunPayment _payment = new PaykunPayment("503915034147269", "C24E2FB3F6AACA4CBB4641163E605A08", "82DBFB702389FD4CCF01CC6DCA41DABF", _isLive: false);    // Change _isLive to false for sandbox mode, While using sandbox mode you will need to provide credintials for sandbox and not of live environment
            TransactionStatusRes transRes = _payment.GetTransactionStatus(_reqId);
            TransactionDetailsViewModel transactionDetailsViewModel = new TransactionDetailsViewModel();
            transactionDetailsViewModel.TransactionId = transRes.data.transaction.payment_id;
            transactionDetailsViewModel.Amount = transRes.data.transaction.order.gross_amount;
            transactionDetailsViewModel.OrderId = transRes.data.transaction.order.order_id;
            transactionDetailsViewModel.CustomerName = transRes.data.transaction.customer.name;
            transactionDetailsViewModel.CustomerEmail = transRes.data.transaction.customer.email_id;
            transactionDetailsViewModel.Mobile = transRes.data.transaction.customer.mobile_no;
            transactionDetailsViewModel.ProductName = transRes.data.transaction.order.product_name;
            transactionDetailsViewModel.UserID = Convert.ToInt32(Session["_UserId"]);
            transactionDetailsViewModel.PaymentStatus = "Failed";
            transactionDetailsViewModel.TransactionId = transRes.data.transaction.payment_id;
            _IPaykumService.CreateTransactionDetails(transactionDetailsViewModel);
            return View(transRes);
        }
    }
}