using AccountMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMS.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AccountsApiDbContext _Context;
        private readonly AccountService _AccountService;

        public TransactionService(AccountsApiDbContext accountApiDbContext,AccountService accountService)
        {
            _Context = accountApiDbContext;
            _AccountService = accountService;
        }
        private void updateStatement(int accountId, DateTime transactionDate,string description,double amount,string transactionType,string status,double? closingBalance)
        {
            AccountStatement accountStatement = new();
            accountStatement.AccountId = accountId;
            accountStatement.TransactionDate = transactionDate;
            accountStatement.Descriptions = description;
            accountStatement.ValueDate = DateTime.Now;
            accountStatement.Amount = amount;
            accountStatement.TransactionType = transactionType;
            accountStatement.TransactionStatus = status;
            accountStatement.ClosingBalance = closingBalance;

            _Context.AccountStatements.Add(accountStatement);
            _Context.SaveChanges();
        }
        public TransactionStatusDTOcs DepositMoney(DepositDTO depositDTO)
        {
            string transacStatus = "";
            TransactionStatusDTOcs transactionStatusDTOcs = new();
            AccountDTO account = new();
            account = _AccountService.GetAccount(depositDTO.AccountId);
            var updateBalance = _Context.Accounts.FirstOrDefault(i => i.AccountId == depositDTO.AccountId);
            if (updateBalance != null)
            {
                updateBalance.Balance += depositDTO.Amount;
                try
                {
                    _Context.Accounts.Update(updateBalance);
                    _Context.SaveChanges();
                    updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "CREDITED", "SUCESS", updateBalance.Balance);
                    transactionStatusDTOcs.status = "SUCESS";
                    //return transactionStatusDTOcs;
                }
                catch (Exception)
                {
                    updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "CREDITED", "FAIL", 0);
                    transactionStatusDTOcs.status = "FAIL";
                    //return "FAIL";

                }
               

            }
            else
            {
                updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "CREDITED", "Account NOT FOUND", 0);
                transactionStatusDTOcs.status = "ACCOUNT NOT FOUND";
                //return "ACCOUNT NOT FOUND";
            }

            return transactionStatusDTOcs;
        }

        public TransactionStatusDTOcs WithdrawMoney(DepositDTO depositDTO)
        {
            //string transacStatus = "";
            TransactionStatusDTOcs transactionStatusDTOcs = new();
            AccountDTO account = new();
            account = _AccountService.GetAccount(depositDTO.AccountId);
            var updateBalance = _Context.Accounts.FirstOrDefault(i => i.AccountId == depositDTO.AccountId);
            if (updateBalance != null)
            {
                double? balanceAmount = updateBalance.Balance - depositDTO.Amount;
                if (balanceAmount<0)
                {
                    updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "DEBITED", "INSUFFICIENT FUNDS", updateBalance.Balance);
                    transactionStatusDTOcs.status = "INSUFFICIENT FUNDS";
                    return transactionStatusDTOcs;
                }
                updateBalance.Balance -= depositDTO.Amount;
                try
                {
                    _Context.Accounts.Update(updateBalance);
                    _Context.SaveChanges();
                    updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "DEBITED", "SUCESS", updateBalance.Balance);
                    transactionStatusDTOcs.status = "SUCESS";
                    //return "SUCESS";
                }
                catch (Exception)
                {
                    updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "DEBITED", "FAIL", 0);
                    transactionStatusDTOcs.status = "FAIL";
                    //return "FAIL";

                }


            }
            else
            {
                updateStatement(depositDTO.AccountId, depositDTO.TransactionDate, depositDTO.Descriptions, depositDTO.Amount, "DEBITED", "Account NOT FOUND", 0);
                transactionStatusDTOcs.status = "ACCOUNT NOT FOUND";
                //return "ACCOUNT NOT FOUND";
            }

            return transactionStatusDTOcs;

        }

        public TransactionStatusDTOcs Transfer(TransferDTO transferDTO)
        {
            TransactionStatusDTOcs transactionStatusDTO = new(); 
            DepositDTO FromDTO = new();
            DepositDTO ToDTO = new();
            AccountDTO FromAccount = _AccountService.GetAccount(transferDTO.FromAccountId);
            if (FromAccount == null)
            {
                transactionStatusDTO.status = "From ID NOT FOUND";
                return transactionStatusDTO;
            }
            else
            {
                //DepositDTO FromDTO = new();
                FromDTO.AccountId = FromAccount.AccountId;
                FromDTO.Amount = transferDTO.Amount;
                FromDTO.Descriptions = transferDTO.Descriptions;

            }
            AccountDTO ToAccount = _AccountService.GetAccount(transferDTO.ToAccountId);
            if (ToAccount == null)
            {
                transactionStatusDTO.status = "TO ID NOT FOUND";
                return transactionStatusDTO;
            }
            else
            {
                //DepositDTO ToDTO = new();
                ToDTO.AccountId = ToAccount.AccountId;
                ToDTO.Amount = transferDTO.Amount;
                ToDTO.Descriptions = transferDTO.Descriptions;
            }
            TransactionStatusDTOcs debitStatus=WithdrawMoney(FromDTO);
            if (debitStatus.status == "SUCESS")
            {
                TransactionStatusDTOcs creditStatus = DepositMoney(ToDTO);
                if (creditStatus.status== "SUCESS")
                {
                    transactionStatusDTO.ToAccountstatus = "SUCESS";
                    transactionStatusDTO.status = "SUCESS";
                    return transactionStatusDTO;
                }
                else
                {
                    transactionStatusDTO.ToAccountstatus = creditStatus.status;
                    transactionStatusDTO.status = "SUCESS";
                    return transactionStatusDTO;
                }

            }
            else
            {
                transactionStatusDTO.status = debitStatus.status;
                return transactionStatusDTO;
            }
            return transactionStatusDTO;
        }
    }
}
