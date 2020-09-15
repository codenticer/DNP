using DNP.Business.Account;
using DNP.Models.Users;
using DNP.Repository.AccountRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DNP.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _IAccountSerice;
        public AccountController(IAccountService IAccountService)
        {
            _IAccountSerice = IAccountService;
        }
        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            if (Request.Cookies["Login"] != null)
            {
                loginViewModel = GetCookies();
            }
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {            
            var result = _IAccountSerice.Login(loginViewModel);
            if(!string.IsNullOrWhiteSpace(result.Email))
            {
                FormsAuthentication.SetAuthCookie(result.Email, loginViewModel.RememberMe);
                // Create Cookie and Store the Login Detail in it if check box is checked
                HttpCookie cookie = new HttpCookie("Login");
                if ((loginViewModel.RememberMe == true))
                {
                    SetCookies(loginViewModel);
                }
                else
                {
                    DeleteCookies();
                }
                Session["_LogInSession"] = result.Email;
                // return RedirectToAction("Index", "Home");
                //return Json(true, JsonRequestBehavior.AllowGet);
                return Json(data: new { success = true, message = "Login successfully" });
            }
            else
            {
                //return Json(false, JsonRequestBehavior.AllowGet);
                return Json(data: new { success = false, message = "Email or Password wrong" });
            }
            //ModelState.AddModelError("","Invalid Username and Password!");
            //return View();
        }

        [HttpPost]
        public ActionResult IsUserLogin()
        {
            string url = Request.UrlReferrer.AbsolutePath;
            if(User.Identity.IsAuthenticated)
            {
                Session["_LogInSession"] = User.Identity.Name;
            }
            
            LoginViewModel loginViewModel = new LoginViewModel();            
            string email = Session["_LogInSession"] !=null ? Session["_LogInSession"].ToString() :"";
            if(!string.IsNullOrWhiteSpace(email))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["_LogInSession"] = null;
            Session["CartCount"] = null;
            Session["_CartValue"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {
            ViewBag.Country = _IAccountSerice.GetCountry();
            return View();
        }
        public ActionResult GetState(int id)
        {
            List<SelectListItem> states= _IAccountSerice.GetState(id);
            return Json(states, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCity(int id)
        {
            List<SelectListItem> cities = _IAccountSerice.GetCity(id);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SignIn(UserRegistrationViewModel userRegistrationViewModel)
        {
            bool result = false;
            if (_IAccountSerice.GetUserByUsernameAndEmail(userRegistrationViewModel))
            {
                result = _IAccountSerice.CreateUser(userRegistrationViewModel);
                if (result)
                {
                    //return RedirectToAction("Login");
                    // return Json("User created successfully!", JsonRequestBehavior.AllowGet);
                    return Json(data: new { success = true, message = "User created successfully" });
                }
                else
                {
                    //return Json("User not created.", JsonRequestBehavior.AllowGet);
                    return Json(data: new { success = false, message = "User not created." });
                }
            }
            else
            {
                //return Json("Username and email must be unique.", JsonRequestBehavior.AllowGet);
                return Json(data: new { success = false, message = "Username and email must be unique." });
            }
            //ModelState.AddModelError("", "Something went wrong!");
            //return View();
        }

        [HttpPost]
        public ActionResult SendEmail(UsersViewModel usersViewModel)
        {
            Session["_ChangePasswordEmail"] = usersViewModel.Email;
            bool result = _IAccountSerice.SendEmailAsync(usersViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatePassword(UsersViewModel usersViewModel)
        {
            usersViewModel.Email = Session["_ChangePasswordEmail"].ToString();
            bool result = _IAccountSerice.UpdatePassword(usersViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            changePasswordViewModel.Email = Convert.ToString(Session["_LogInSession"]);
            bool result = _IAccountSerice.ChangePassword(changePasswordViewModel);
            if (result)
            {
                Session["_LogInSession"] = null;
                FormsAuthentication.SignOut();                
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Old password don't match!");
            return View();
        }

        public void SetCookies( LoginViewModel loginViewModel)
        {            
            if ((loginViewModel.RememberMe == true))
            {
                HttpCookie cookie = new HttpCookie("Login");
                cookie.Values.Add("Email", loginViewModel.Email);
                cookie.Values.Add("Password", loginViewModel.Password);
                cookie.Expires = DateTime.Now.AddDays(15);
                Response.Cookies.Add(cookie);
            }            
        }

        public LoginViewModel GetCookies()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Email = Request.Cookies["Login"].Values["Email"];
            loginViewModel.Password = Request.Cookies["Login"].Values["Password"];
            loginViewModel.RememberMe = true;
            return loginViewModel;
        }

        public void DeleteCookies()
        {
            if (HttpContext.Request.Cookies["Login"] != null)
            {
                var c = new HttpCookie("Login")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                HttpContext.Response.Cookies.Add(c);
            }
        }


    }
}