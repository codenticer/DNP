using DNP.Data;
using DNP.Models.Paykun;
using DNP.Models.Users;
using DNP.Repository.AccountRepositry;
using DNP.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace DNP.Business.Account
{
   public class AccountService:IAccountService
    {
        public UsersViewModel Login(LoginViewModel loginViewModel)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            try
            {
                using (var context = new DNPDBEntities())
                {
                    var result = context.spUserLogin(loginViewModel.Username, PasswordEncryption.GetMD5Hash(loginViewModel.Password)).FirstOrDefault();
                    if(result!=null)
                    {
                        usersViewModel.FirstName = result.FirstName;
                        usersViewModel.LastName = result.LastName;
                        usersViewModel.MiddleName = result.MiddleName;
                        usersViewModel.Username = result.Username;
                        usersViewModel.Email = result.Email;
                        usersViewModel.Password = result.Password;
                        usersViewModel.PhoneNumber = result.PhoneNumber;
                        usersViewModel.Address = result.Address;
                        usersViewModel.ZipCode = result.ZipCode;                        
                    }
                    return usersViewModel;
                }
            }
            catch(Exception ex)
            {
                Logger.Info(ex.ToString());
                throw;
            }
            
        }

        public bool CreateUser(UserRegistrationViewModel userRegistrationViewModel)
        {
            try
            {                   
                XDocument userRegistrtionXMLFile = new XDocument();
                //userRegistrtionXMLFile = new XDocument(
                //    new XElement("UserDeails",
                //    new XElement("UserId", usersViewModel.UserId),
                //    new XElement("Username", usersViewModel.Username),
                //    new XElement("Password", PasswordEncryption.GetMD5Hash(usersViewModel.Password)),
                //    new XElement("Email", usersViewModel.Email),
                //    new XElement("FirstName", usersViewModel.FirstName),
                //    new XElement("MiddleName", usersViewModel.MiddleName),
                //    new XElement("LastName", usersViewModel.LastName),
                //    new XElement("PhoneNumber", usersViewModel.PhoneNumber),
                //    new XElement("CountryId", usersViewModel.CountryId),
                //    new XElement("StateId", usersViewModel.StateId),
                //    new XElement("CityId", usersViewModel.CityId),
                //    new XElement("Address", usersViewModel.Address),
                //    new XElement("ZipCode", usersViewModel.ZipCode)
                //    ));
                userRegistrtionXMLFile = new XDocument(
                    new XElement("UserDeails",
                    new XElement("UserId", userRegistrationViewModel.UserId),
                    new XElement("Username", userRegistrationViewModel.Username),
                    new XElement("Password", PasswordEncryption.GetMD5Hash(userRegistrationViewModel.Password)),
                    new XElement("Email", userRegistrationViewModel.Email),
                    new XElement("CompanyName", userRegistrationViewModel.CompanyName),
                    new XElement("ZipCode", userRegistrationViewModel.ZipCode),
                    new XElement("Address", userRegistrationViewModel.Address),
                    new XElement("PhoneNumber", userRegistrationViewModel.PhoneNumber),
                    new XElement("FAX", userRegistrationViewModel.FAX),
                    new XElement("CountryVAT", userRegistrationViewModel.CountryVAT),
                    new XElement("Language", userRegistrationViewModel.Language),
                    new XElement("BusinessActivity", userRegistrationViewModel.BusinessActivity),
                    new XElement("MainPage", userRegistrationViewModel.MainPage),
                    new XElement("Currency", userRegistrationViewModel.Currency),
                    new XElement("CountryId", userRegistrationViewModel.CountryId),
                    new XElement("StateId", userRegistrationViewModel.StateId),
                    new XElement("CityId", userRegistrationViewModel.CityId),
                    new XElement("DeliveryMethod", userRegistrationViewModel.DeliveryMethod),
                    new XElement("ContactFirstName", userRegistrationViewModel.ContactFirstName),
                    new XElement("ContactLastName", userRegistrationViewModel.ContactLastName),
                    new XElement("ContactDepartment", userRegistrationViewModel.ContactDepartment),
                    new XElement("ContactPosition", userRegistrationViewModel.ContactPosition),
                    new XElement("ContactTelephone", userRegistrationViewModel.ContactTelephone),
                    new XElement("ContactFax", userRegistrationViewModel.ContactFax),
                    new XElement("ContactMobile", userRegistrationViewModel.ContactMobile),
                    new XElement("ContactInstantMessaging", userRegistrationViewModel.ContactInstantMessaging),
                    new XElement("DeliveryCompanyName", userRegistrationViewModel.DeliveryCompanyName),
                    new XElement("DeliveryCountry", userRegistrationViewModel.DeliveryCountry),
                    new XElement("DeliveryCity", userRegistrationViewModel.DeliveryCity),
                    new XElement("DeliveryZip", userRegistrationViewModel.DeliveryZipCode),
                    new XElement("DeliveryAddress", userRegistrationViewModel.DeliveryAddress),
                    new XElement("DeliveryTelephone", userRegistrationViewModel.DeliveryTelephone),
                    new XElement("DeliveryFax", userRegistrationViewModel.DeliveryFax),
                    new XElement("DeliveryLanguage", userRegistrationViewModel.DeliveryLanguage)
                    ));
                string xmlData = userRegistrtionXMLFile.ToString();
                using (var context = new DNPDBEntities())
                {
                   int resultResponse= context.spUserRegistration(xmlData);
                    if(resultResponse!=0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }                    
            }
            catch
            {
                throw;
            }
        }

        public bool SendEmailAsync(UsersViewModel usersViewModel,string Subject= "Forgot Password")
        {
            // Initialization.  
            bool isSend = false;

            try
            {

                // Any random number of eight digits.                                  
                usersViewModel.OTP = CreateRandomNumber(8);
                usersViewModel.OTPDateTime = DateTime.Now;
                var msg = "Dear " + usersViewModel.Email + ", <br /><br /> Thist is your OTP: <b style='color: red'> "+ usersViewModel.transactionDetailsViewModel.OrderId + " </b> <br /><br /> Thanks & Regards, <br />Dipak Singh";
                var body = msg;
                var message = new MailMessage();
                string hostName = ConfigurationManager.AppSettings["EMAIL_HOST_Name"];
                string portNumber = ConfigurationManager.AppSettings["EMAIL_PORT"];
                string account = ConfigurationManager.AppSettings["EMAIL_ACCOUNT"];
                string password = ConfigurationManager.AppSettings["EMAIL_PASSWORD"];
                // Settings.  
                message.To.Add(new MailAddress(usersViewModel.Email));
                message.From = new MailAddress(account);
                message.Subject =  "";
                message.Body = body;
                message.IsBodyHtml = true;
                
                using (var smtp = new SmtpClient())
                {
                    // Settings.  
                    smtp.UseDefaultCredentials = false;
                    var credential = new NetworkCredential
                    {
                        UserName = account,
                        Password = password
                    };

                    // Settings.  
                    smtp.Credentials = credential;
                    smtp.Host = hostName;
                    smtp.Port = Convert.ToInt32(portNumber);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    
                    //smtp.EnableSsl = true;

                    // Sending  
                    smtp.Send(message);
                   bool isUpdate= UpdateOTP(usersViewModel);

                    // Settings.  
                    isSend = isUpdate;
                }
            }
            catch (Exception ex)
            {
                // Info  
                throw ex;
            }

            // info.  
            return isSend;
        }

        public string CreateRandomNumber(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public bool UpdateOTP(UsersViewModel usersViewModel)
        {
            try
            {
                using (var context = new DNPDBEntities())
                {
                    int resultResponse = context.spUpdateUserOTP(usersViewModel.Email, usersViewModel.OTP, usersViewModel.OTPDateTime);
                    if (resultResponse != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public bool IsValidOTP(UsersViewModel usersViewModel)
        {            
            try
            {
                bool isTrue = false;
                using (var context = new DNPDBEntities())
                {
                    var result = context.spGetUserByEmail(usersViewModel.Email).FirstOrDefault();
                    if(result!=null)
                    {
                        if(usersViewModel.OTP == result.OTP && DateTime.Now <= Convert.ToDateTime(result.OTPDateTIme).AddMinutes(30))
                        {
                            isTrue = true;
                            return isTrue;
                        }                        
                    }
                    return isTrue;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdatePassword(UsersViewModel usersViewModel)
        {
            try
            {
                bool isTrue = false;
                isTrue = IsValidOTP(usersViewModel);
                if(isTrue)
                {
                    using (var context=new DNPDBEntities())
                    {
                        int resultResponse = context.spUpdateNewPassword(usersViewModel.Email, PasswordEncryption.GetMD5Hash(usersViewModel.Password));
                        if(resultResponse !=0)
                        {
                            isTrue = true;
                            return isTrue;
                        }
                        else
                        {
                            isTrue = false;
                            return isTrue;
                        }
                    }
                }
                return isTrue;
            }
            catch
            {
                throw;
            }            
        }

        public bool ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            bool isTrue = false;
            using (var context = new DNPDBEntities())
            {
                var result = context.spGetUserByEmail(changePasswordViewModel.Email).FirstOrDefault();
                if (result != null)
                {
                    if (PasswordEncryption.GetMD5Hash(changePasswordViewModel.OldPassword) == result.Password)
                    {
                        int resultResponse = context.spUpdateNewPassword(changePasswordViewModel.Email, PasswordEncryption.GetMD5Hash(changePasswordViewModel.NewPassword));
                        if (resultResponse != 0)
                        {
                            isTrue = true;
                            return isTrue;
                        }
                        else
                        {
                            isTrue = false;
                            return isTrue;
                        }
                    }
                }
                return isTrue;
            }
        }

        public bool GetUserByUsernameAndEmail(UserRegistrationViewModel userRegistrationViewModel)
        {
            try
            {
                bool isTrue = false;
                using (var context = new DNPDBEntities())
                {
                    var result = context.spGetUserByUsernameAndEmail(userRegistrationViewModel.Email,userRegistrationViewModel.Username).FirstOrDefault();
                    if (result != null)
                    {                        
                            return isTrue;                      
                    }
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<SelectListItem> GetCountry()
        {
            List<SelectListItem> lstCountry = new List<SelectListItem>();
            try
            {
                using (var context = new DNPDBEntities())
                {
                    var data = context.spGetCountry();
                    foreach(var item in data.ToList())
                    {
                        lstCountry.Add(new SelectListItem { Text = item.CountryName, Value = item.CountryId.ToString() });
                    }
                    return lstCountry;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<SelectListItem> GetState(int countryId)
        {
            List<SelectListItem> lstState = new List<SelectListItem>();
            try
            {
                using (var context = new DNPDBEntities())
                {
                    var data = context.spGetStateByCountryId(countryId);
                    foreach (var item in data.ToList())
                    {
                        lstState.Add(new SelectListItem { Text = item.StateName, Value = item.StateId.ToString() });
                    }
                    return lstState;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<SelectListItem> GetCity(int stateId)
        {
            List<SelectListItem> lstCity = new List<SelectListItem>();
            try
            {
                using (var context = new DNPDBEntities())
                {
                    var data = context.spGetCityByStateId(stateId);
                    foreach (var item in data.ToList())
                    {
                        lstCity.Add(new SelectListItem { Text = item.CityName, Value = item.CityId.ToString() });
                    }
                    return lstCity;
                }
            }
            catch
            {
                throw;
            }
        }

       
    }
}
