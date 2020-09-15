using DNP.Models.Paykun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Repository.PaykumRepositry
{
   public interface IPaykumService
    {
        PaykumUserViewModel GetUserDetailsByEmail(string email);
        bool CreateTransactionDetails(TransactionDetailsViewModel transactionDetailsViewModel);
        bool CreateUserOrderDetails(TransactionDetailsViewModel transactionDetailsViewModel);
    }
}
