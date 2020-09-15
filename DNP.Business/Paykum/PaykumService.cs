using DNP.Data;
using DNP.Models.Paykun;
using DNP.Repository.PaykumRepositry;
using DNP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Business.Paykum
{
   public class PaykumService: IPaykumService
    {
        public bool CreateTransactionDetails(TransactionDetailsViewModel transactionDetailsViewModel)
        {            
            try
            {
                using (var context = new DNPDBEntities())
                {
                    int resultResponse = context.spCreateTransactionDetails(transactionDetailsViewModel.TransactionId, (long)transactionDetailsViewModel.Amount,transactionDetailsViewModel.OrderId,
                        transactionDetailsViewModel.CustomerName, transactionDetailsViewModel.CustomerEmail, transactionDetailsViewModel.Mobile, transactionDetailsViewModel.ProductName,
                        transactionDetailsViewModel.UserID, transactionDetailsViewModel.PaymentStatus);
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
            catch (Exception ex)
            {
                Logger.Info(ex.ToString());
                throw;
            }
        }

        public bool CreateUserOrderDetails(TransactionDetailsViewModel transactionDetailsViewModel)
        {
            try
            {
                using (var context = new DNPDBEntities())
                {
                    int resultResponse = context.spCreateUserOrderDetails(transactionDetailsViewModel.OrderId, transactionDetailsViewModel.UserID, transactionDetailsViewModel.ProductName,
                       Convert.ToInt32(transactionDetailsViewModel.Amount), transactionDetailsViewModel.Photo);
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
            catch (Exception ex)
            {
                Logger.Info(ex.ToString());
                throw;
            }
        }

        public PaykumUserViewModel GetUserDetailsByEmail(string email)
        {
            PaykumUserViewModel paykumUserViewModel = new PaykumUserViewModel();
            try
            {
                using (var context = new DNPDBEntities())
                {
                    var result = context.spGetUserDetailsByEmailorUsername(email).FirstOrDefault();
                    if (result != null)
                    {
                        paykumUserViewModel.UserId = result.UserID;                        
                        paykumUserViewModel.Username = result.Username;
                        paykumUserViewModel.Email = result.Email;
                        paykumUserViewModel.ZipCode = result.ZipCode;
                        paykumUserViewModel.Address = result.Address;
                        paykumUserViewModel.ContactFirstName = result.ContactFirstName;
                        paykumUserViewModel.ContactLastName = result.ContactLastName;
                        paykumUserViewModel.ContactTelephone = result.ContactTelephone;
                        paykumUserViewModel.ContactMobile = result.ContactMobile;
                        paykumUserViewModel.ContactTelephone = result.ContactTelephone;
                        paykumUserViewModel.Address = result.Address;
                        paykumUserViewModel.ZipCode = result.ZipCode;
                        paykumUserViewModel.CountryName = result.CountryName;
                        paykumUserViewModel.StateName = result.StateName;
                        paykumUserViewModel.CityName = result.CityName;
                        paykumUserViewModel.FullName = result.ContactFirstName + " " + result.ContactLastName;
                        paykumUserViewModel.CompanyName = result.CompanyName;
                    }
                    return paykumUserViewModel;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(ex.ToString());
                throw;
            }
        }
    }
}
