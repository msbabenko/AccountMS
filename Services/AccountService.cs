using AccountMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMS.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountsApiDbContext _Context;

        public AccountService(AccountsApiDbContext accountApiDbContext)
        {
            _Context = accountApiDbContext;
        }
        public string CreateAccount(AccountDTO customerId)
        {
            string status = " ";
            for (int i = 0; i < 2; i++)
            {
                Account account = new();
                account.CustomerId = customerId.CustomerId;
                if (i == 0)
                    account.AccountType = "SAVINGS";
                else
                    account.AccountType = "CURRENT";
                account.Balance = 0;
                try
                {
                    _Context.Accounts.Add(account);
                    _Context.SaveChanges();
                    AccountStatusUpdate(account.AccountId, "SUCESS");
                    status = "SUCESS";
                }
                catch (Exception)
                {
                    AccountStatusUpdate(account.AccountId, "FAIL");
                    status = "FAIL";

                }

            }
            return status;
        }

        private void AccountStatusUpdate(int accountId,string status)
        {
            AccountStatus accountStatus = new();
            accountStatus.AccountId = accountId;
            accountStatus.AccountCreationStatus = status;
            try
            {
                _Context.AccountStatuses.Add(accountStatus);
                _Context.SaveChanges();
            }
            catch (Exception)
            {

                return;
            }
            
        }

        public AccountDTO GetAccount(int accountId)
        {
            Account account = _Context.Accounts.FirstOrDefault(i => i.AccountId == accountId);
            AccountDTO accountDTO = new();
            if (account != null)
            {
                //AccountDTO accountDTO = new();
                accountDTO.AccountId = account.AccountId;
                accountDTO.Balance =(double) account.Balance;
                return accountDTO;
            }
            return accountDTO;
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            List<Account> accounts = _Context.Accounts.Where(i => i.CustomerId == customerId).ToList();
            
            return accounts;
        }

        public List<AccountStatement> GetAccountStatement(StatementDTO statementDTO)
        {
            List<AccountStatement> accountStatements = null;
            accountStatements= _Context.AccountStatements.Where(i => i.AccountId == statementDTO.AccountId && i.TransactionDate >= statementDTO.FromDate && i.TransactionDate <= statementDTO.ToDate).ToList();
            //from c in accountStatements
            //orderby c.TransactionId
            //select c
            return accountStatements;
        }
    }
}
