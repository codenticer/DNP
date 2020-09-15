using DNP.Models.Paykun;
using DNP.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DNP.Repository.AccountRepositry
{
   public interface IAccountService
    {
        UsersViewModel Login(LoginViewModel loginViewModel);
        bool CreateUser(UserRegistrationViewModel userRegistrationViewModel);
        bool SendEmailAsync(UsersViewModel usersViewModel, string Subject = "Forgot Password");
        string CreateRandomNumber(int length);
        bool UpdateOTP(UsersViewModel usersViewModel);
        bool IsValidOTP(UsersViewModel usersViewModel);
        bool UpdatePassword(UsersViewModel usersViewModel);
        bool ChangePassword(ChangePasswordViewModel changePasswordViewModel);
        bool GetUserByUsernameAndEmail(UserRegistrationViewModel userRegistrationViewModel);
        List<SelectListItem> GetCountry();
        List<SelectListItem> GetState(int countryId);
        List<SelectListItem> GetCity(int stateId);
        

    }
}
