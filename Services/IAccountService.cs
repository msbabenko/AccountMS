using AccountMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMS.Services
{
    public interface IAccountService
    {
        public string CreateAccount(AccountDTO customerId);
        public IList<Account> GetCustomerAccounts(int customerId);
        public AccountDTO GetAccount(int AccountId);
        public IList<AccountStatement> GetAccountStatement(StatementDTO statementDTO);


    }
}
