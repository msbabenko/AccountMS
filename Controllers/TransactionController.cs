using AccountMS.Models;
using AccountMS.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;

        public TransactionController(TransactionService transactionService)
        {
            _service = transactionService;

        }
        // GET: api/<TransactionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<TransactionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<TransactionController>
        [Route("Deposit")]
        [HttpPost]
        public async Task<ActionResult<TransactionStatusDTOcs>> Deposit([FromBody] DepositDTO value)
        {
            return _service.DepositMoney(value);
        }
        [Route("Withdraw")]
        [HttpPost]
        public async Task<ActionResult<TransactionStatusDTOcs>> Withdraw([FromBody] DepositDTO value)
        {
            return _service.WithdrawMoney(value);
        }

        [Route("Transfer")]
        [HttpPost]
        public async Task<ActionResult<TransactionStatusDTOcs>> Transfer([FromBody] TransferDTO value)
        {
            return _service.Transfer(value);
        }

        // PUT api/<TransactionController>/5
        //[HttpPut("{id}")]
        //public string Put(DepositDTO id, [FromBody] Account value)
        //{
        //    return _service.DepositMoney(id);
        //}

        // DELETE api/<TransactionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
