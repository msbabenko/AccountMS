using AccountMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMS.Services
{
    public interface ITransactionService
    {
        public TransactionStatusDTOcs DepositMoney(DepositDTO depositDTO);
        public TransactionStatusDTOcs WithdrawMoney(DepositDTO depositDTO);
        public TransactionStatusDTOcs Transfer(TransferDTO transferDTO);
    }
}
